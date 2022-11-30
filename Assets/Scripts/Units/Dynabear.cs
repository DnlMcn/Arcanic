using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dynabear : BasicUnit
{

    [SerializeField] FloatVariable explosionRadius;
    [SerializeField] FloatVariable ressurectionTime;
    [SerializeField] FloatVariable attackRange;

    bool ressurecting = false;

    public bool drawExplosionRadius;

    void Start()
    {
        GetUnitLevelData();
        ResourceManager.Matter.Add(50);
    }

    void Update()
    {
        FindClosestEnemy(hasTargetedEnemy);
        if (target != null && !ressurecting) 
            MoveTowardsClosestEnemy(target);

        if (target != null && Vector3.Distance(transform.position, target.position) <= attackRange.Value && !ressurecting) 
            Explode();
    }

    void Explode()
    {
        if (!ressurecting)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius.Value);
            AudioManager.Play("Dynabear0 Explosion");

            foreach (Collider collider in colliders)
            {
                if (collider.TryGetComponent<BasicEnemy>(out BasicEnemy enemyComponent))
                {
                    enemyComponent.TakeDamage(baseDamage);
                }
            }

            StartCoroutine(Ressurect());
        }
    }
        
    IEnumerator Ressurect()
    {
        ressurecting = true;
        yield return new WaitForSeconds(ressurectionTime.Value);
        ressurecting = false;
    }

    void OnDrawGizmos()
    {
        if (drawExplosionRadius)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(transform.position, explosionRadius.Value);
        }
    }
}
 