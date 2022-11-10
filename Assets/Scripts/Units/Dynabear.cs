using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dynabear : BasicUnit
{

    [SerializeField] FloatVariable explosionRadius;
    [SerializeField] FloatVariable ressurectionTime;
    [SerializeField] FloatVariable attackRange;

    bool ressurecting = false;

    void Update()
    {
        FindClosestEnemy(hasTargetedEnemy);
        if (target != null) MoveTowardsClosestEnemy(target);

        if (target != null && Vector3.Distance(transform.position, target.position) <= attackRange.Value) Explode();
    }

    void Explode()
    {
        if (!ressurecting)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius.Value);

            int i = 0;
            foreach (Collider collider in colliders)
            {
                i++;
            }

            Debug.Log(i);

            // StartCoroutine(Ressurect());
        }
    }
        
    IEnumerator Ressurect()
    {
        ressurecting = true;
        float movementSpeedBackup = movementSpeed;
        movementSpeed = 0;

        yield return new WaitForSeconds(ressurectionTime.Value);

        movementSpeed = movementSpeedBackup;
        ressurecting = false;
    }
}
 