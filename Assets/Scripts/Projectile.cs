using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] ProjectileSO projectileType;

    private float speed;
    private float lifespan;
    private float damage;

    private Vector3 velocity;
    private Vector3 lastFramePosition;

    [SerializeField] LayerMask mask;

    void Start()
    {
        speed = projectileType.travelSpeed;
        lifespan = projectileType.lifespan;
        damage = projectileType.damage;
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
        Ray ray = new Ray(lastFramePosition, transform.forward);
        RaycastHit hitInfo;

        float lastToCurrentFrameDistace = Vector3.Distance(lastFramePosition, transform.position);
        if (Physics.Raycast(ray, out hitInfo, lastToCurrentFrameDistace, mask))
        {
            if (hitInfo.collider.gameObject.TryGetComponent<BasicEnemy>(out BasicEnemy enemyComponent))
            {
                if (!projectileType.piercing) Destroy(transform.gameObject);
                enemyComponent.TakeDamage(damage);
            }
        }

        Debug.DrawLine(ray.origin, ray.origin + ray.direction * lastToCurrentFrameDistace, Color.red);
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

