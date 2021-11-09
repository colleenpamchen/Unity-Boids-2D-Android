using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Flock/Behavior/Separation")]
public class SeparationBehavior : FlockBehavior 
{
    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        // if not neighbors, return no adjustment
        if (context.Count == 0)
        {
            return Vector2.zero;
        }
        //add all points together and average 
        Vector2 separationMove = Vector2.zero;
        int numberSeparate = 0; 
        foreach (Transform item in context)
        {
            if(Vector2.SqrMagnitude(item.position - agent.transform.position) < flock.SquareAvoidanceRadius)
            {
                numberSeparate++; 
                separationMove += (Vector2)(agent.transform.position - item.position); 
            }
        }
        if (numberSeparate > 0)
        {
            separationMove /= numberSeparate; 
        }
        return separationMove; 

    }

}
