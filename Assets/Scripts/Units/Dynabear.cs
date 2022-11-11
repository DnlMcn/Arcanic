using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dynabear : BasicUnit
{

    [SerializeField] FloatVariable explosionRadius;
    [SerializeField] FloatVariable ressurectionTime;
    [SerializeField] FloatVariable postRessurectionCooldownSO;
    private float postRessurectionCooldown;
    [SerializeField] FloatVariable attackRange;

    bool ressurecting = false;
    bool isInPostRessurectionCooldown = false;

    public bool drawExplosionRadius;

    void Start()
    {
        float postRessurectionCooldown = postRessurectionCooldownSO.Value;
    }

    void Update()
    {
        FindClosestEnemy(hasTargetedEnemy);
        if (target != null && !ressurecting) 
            MoveTowardsClosestEnemy(target);

        if (target != null && Vector3.Distance(transform.position, target.position) <= attackRange.Value && !ressurecting && !isInPostRessurectionCooldown) 
            Explode();
    }

    void Explode()
    {
        if (!ressurecting)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius.Value);

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
        StartCoroutine(PostRessurectionCooldown());
        ressurecting = false;

    }

    IEnumerator PostRessurectionCooldown()
    {
        isInPostRessurectionCooldown = true;
        yield return new WaitForSeconds(postRessurectionCooldown);
        isInPostRessurectionCooldown = false;
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
 