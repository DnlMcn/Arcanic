using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoxxiProjectile : MonoBehaviour
{

    [SerializeField] [Range(20f, 500f)] private float speed = 70f;
    [SerializeField] private float projectileLifespan = 5f;
    private Vector3 velocity;

    void Start()
    {
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
            Debug.Log("Inimigo atingido.");
            Destroy(transform.gameObject);
            EnemyComponent.TakeDamage(1);
        }
    }

    IEnumerator ProjectileLifespan()
    {
        yield return new WaitForSeconds(projectileLifespan);
        Destroy(transform.gameObject);
    }
}

