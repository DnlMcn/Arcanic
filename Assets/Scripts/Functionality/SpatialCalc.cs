using UnityEngine;

/*
    Essa classe tem como objetivo compilar vários calculos espaciais úteis.
*/

public static class SpatialCalc : object
{
    public static Transform GetClosestEnemy(RuntimeSet<BasicEnemy> potentialTargets, Transform fromThis)
    {
        Transform bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = fromThis.position;

        for(int i = 0; i < potentialTargets.Count; i++)
        {
            Transform potentialTarget = potentialTargets.Items[i].transform;
            Vector3 directionToTarget = potentialTarget.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if(dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget;
            }
        }  

        return bestTarget;
    }
}
