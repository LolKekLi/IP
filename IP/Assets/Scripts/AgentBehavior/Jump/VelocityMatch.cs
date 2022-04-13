using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelocityMatch : AgentBehaviour
{
    public float timeToTarget = 0.1f;

    public override Steering GetSteering()
    {
        Steering steering = new Steering();
        
        steering.linear = target.GetComponent<Agent>().velocity - _agent.velocity;
        steering.linear /= timeToTarget;

        if (steering.linear.magnitude > _agent.maxAccel)
        {
            steering.linear = steering.linear.normalized * _agent.maxAccel;
        }

        steering.angular = 0f;
        return steering;
    }
}