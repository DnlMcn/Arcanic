using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] ProjectileSO projectileType;

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
        if (collider.gameObject.TryGetComponent<BasicEnemy>(out BasicEnemy enemyComponent))
        {
            Destroy(transform.gameObject);
            enemyComponent.TakeDamage(damage);
        }
    }

    IEnumerator ProjectileLifespan()
    {
        yield return new WaitForSeconds(lifespan);
        Destroy(transform.gameObject);
    }
}

