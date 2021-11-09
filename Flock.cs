using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
    public FlockAgent agentPrefab;
    List<FlockAgent> agents = new List<FlockAgent>();
    public FlockBehavior behavior;

    public int flockSize = 50;
    const float AgentDensity = 0.08f;

    // driveFactor - to make agents move faster 
    [Range(1f, 100f)]
    public float driveFactor = 10f;

    // maxSpeed - to ensure the speed has a ceiling 
    [Range(1f, 50f)]
    public float maxSpeed = 1f;

    // neighborRadius - deinfes the neighborhood of any particular agent 
    [Range(1f, 10f)]
    public float neighborRadius = 1.0f;

    //avoidanceRadiusMultiplier - this is to ensure that each agent has a neighborhood within a neighborRadius 
    [Range(0f, 1f)]
    public float avoidanceRadiusMultiplier = 0.5f;

    // utility variables to avoid excessive math. These are squared parameters so that when you later compare the magnitudes 
    // of these values, you don't have to perform the square root operations. 
    float squareMaxSpeed;
    float squareNeighborRadius;
    float squareAvoidanceRadius; 
    public float SquareAvoidanceRadius { get { return squareAvoidanceRadius; } }


    void Start()
    {
        squareMaxSpeed = maxSpeed * maxSpeed;
        squareNeighborRadius = neighborRadius * neighborRadius;
        squareAvoidanceRadius = squareNeighborRadius * avoidanceRadiusMultiplier * avoidanceRadiusMultiplier; 

        for (int i=0; i<flockSize; i++)
        {
            FlockAgent newAgent = Instantiate(
                agentPrefab,
                Random.insideUnitCircle * flockSize * AgentDensity,
                Quaternion.Euler(Vector3.down * Random.Range(360f, 360f)),
                transform);
            newAgent.name = "AGent " + i;
            agents.Add(newAgent); 
        }       
    }


    void Update()
    {
        foreach(FlockAgent agent in agents)
        {
            List<Transform> context = GetNearbyOjbects(agent);

            // This is to test that the flock behaviors the way it should 
            //agent.GetComponentInChildren<SpriteRenderer>().color = Color.Lerp(Color.white, Color.red, context.Count / 2f); 

            Vector2 move = behavior.CalculateMove(agent, context, this);
            move *= driveFactor;
            if (move.sqrMagnitude > squareMaxSpeed)
            {
                move = move.normalized * maxSpeed;
            }
            agent.Move(move);
        }     
    }

    List<Transform> GetNearbyOjbects(FlockAgent agent)
    {
        List<Transform> context = new List<Transform>();
        Collider2D[] contextColliders = Physics2D.OverlapCircleAll(agent.transform.position, neighborRadius);
        foreach(Collider2D c in contextColliders)
        {
            if (c != agent.AgentCollider)
            {
                context.Add(c.transform);
            }
        }
        return context; 

    }



}
