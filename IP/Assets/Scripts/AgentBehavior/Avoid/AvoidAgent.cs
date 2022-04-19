using UnityEngine;

public class AvoidAgent : AgentBehaviour
{
    public float collisionRadius = 0.4f;
    private GameObject[] targets;

    public override StateType Type => StateType.AvoidAgent;

    private void Start()
    {
        targets = GameObject.FindGameObjectsWithTag("Agent");
    }

    public override Steering GetSteering()
    {
        Steering steering = new Steering();
        float shortestTime = Mathf.Infinity;
        GameObject firstTarget = null;
        float firstMinSeparation = 0f;
        float firstDistance = 0f;
        Vector3 firstRelativePos = Vector3.zero;
        Vector3 firstRelativeVel = Vector3.zero;

        foreach (var target in targets)
        {
            Vector3 relativePos;
            Agent targetAgent = target.GetComponent<Agent>();
            relativePos = target.transform.position - transform.position;
            Vector3 relativeVel = targetAgent.velocity - _agent.velocity;
            float relativeSpeed = relativeVel.magnitude;
            float timeToCollision = Vector3.Dot(relativePos, relativeVel);
            timeToCollision /= relativeSpeed * relativeSpeed * -1;
            float distance = relativePos.magnitude;
            float minSeparation = distance - relativeSpeed * timeToCollision;

            if (minSeparation > 2 * collisionRadius)
            {
                continue;
            }
            
            Debug.Log($"{timeToCollision} {shortestTime}");

            if (timeToCollision > 0f && timeToCollision < shortestTime)
            {
                shortestTime = timeToCollision;
                firstTarget = target;
                firstMinSeparation = minSeparation;
                firstRelativePos = relativePos;
                firstRelativeVel = relativeVel;
            }
        }

        if (firstTarget == null)
        {
            return steering;
        }

        if (firstMinSeparation <= 0f || firstDistance < 2 * collisionRadius)
        {
            firstRelativePos = firstTarget.transform.position;
        }
        else
        {
            firstRelativePos = firstTarget.transform.position;
        }

        firstRelativePos.Normalize();
        steering.linear = -firstRelativePos * _agent.maxAccel;

        return steering;
    }
}