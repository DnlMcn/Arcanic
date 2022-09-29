using System.Collections;
using UnityEngine;

public class MoxxiProjectile : MonoBehaviour
{
    [SerializeField] Projectile projectileType;

    private float speed;
    private float lifespan;
    private float damage;
    private Vector3 velocity;

    void Start()
    {
        speed = projectileType.travelSpeed;
        lifespan = projectileType.lifespan;
        damage = projectileType.damage;
        StartCoroutine(ProjectileLifespan());
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        velocity = Vector3.forward * speed * Time.deltaTime;
        transform.Translate(velocity);
    }
    
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.TryGetComponent<Enemy>(out Enemy EnemyComponent))
        {
            Destroy(transform.gameObject);
            EnemyComponent.TakeDamage(damage);
        }
    }

    IEnumerator ProjectileLifespan()
    {
        yield return new WaitForSeconds(lifespan);
        Destroy(transform.gameObject);
    }
}

