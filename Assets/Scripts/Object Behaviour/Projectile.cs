using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] ProjectileSO projectileType;

    private float speed;
    private float lifespan;
    private float damage;
    private bool piercing;

    private Vector3 velocity;
    private Vector3 lastFramePosition;

    [SerializeField] LayerMask mask;

    void Start()
    {
        speed = projectileType.travelSpeed;
        lifespan = projectileType.lifespan;
        damage = projectileType.damage;
        piercing = projectileType.piercing;

        StartCoroutine(ProjectileLifespan());
    }

    private void FixedUpdate() 
    {
        lastFramePosition = transform.position;
        Move();
        DetectRayCollision();
    }

    private void DetectRayCollision()
    {
        /*
        Esse método projeta um raio entre a posição atual e a posição do projétil no último frame para detectar colisões.
        Isso previne que colisões não sejam detectadas quando o projétil se move rápido o suficiente para atravessar
        objetos entre frames.
        */
        
        Ray ray = new Ray(lastFramePosition, transform.forward);
        RaycastHit hitInfo;        

        float lastToCurrentFrameDistace = Vector3.Distance(lastFramePosition, transform.position);
        Debug.DrawLine(ray.origin, ray.origin + ray.direction * lastToCurrentFrameDistace, Color.red);

        if (Physics.Raycast(ray, out hitInfo, lastToCurrentFrameDistace, mask))
        {
            if (hitInfo.collider.gameObject.TryGetComponent<BasicEnemy>(out BasicEnemy enemyComponent))
            {
                if (!piercing) Destroy(transform.gameObject);
                enemyComponent.TakeDamage(damage);
            }
        }
    }

    void Move()
    {
        velocity = Vector3.forward * speed * Time.deltaTime;
        transform.Translate(velocity);
    }
    
    IEnumerator ProjectileLifespan()
    {
        yield return new WaitForSeconds(lifespan);
        Destroy(transform.gameObject);
    }
}

