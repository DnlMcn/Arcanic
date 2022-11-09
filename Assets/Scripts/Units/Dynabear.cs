using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dynabear : BasicUnit
{
    DynabearLevelSO dynabearLevel;
    float explosionRadius;

    void Start()
    {
        dynabearLevel = level.dynabearLevel;

        explosionRadius = dynabearLevel.explosionRadius;
    }

    void Update()
    {
    }

    void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);

        int i = 0;
        foreach (Collider collider in colliders)
        {
            i++;
        }

        Debug.Log(i);
    }
}
 