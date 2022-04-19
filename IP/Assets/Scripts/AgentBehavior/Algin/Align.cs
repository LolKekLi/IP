//Выровнять

using System;
using Project;
using UnityEngine;

public class Align : AgentBehaviour
{
    [SerializeField]
    private float targetRadius;
    [SerializeField]
    private float slowRadius;
    
    private float timeToTarget = 0.1f;

    public override StateType Type => StateType.Algin;

    public override Steering GetSteering()
    {
        Steering steering = new Steering();
        var targetOrientation = target.GetComponent<Agent>().orientation;
        var rotation = targetOrientation - _agent.orientation;
        rotation = MapToRange(rotation);

        var rotationSize = Mathf.Abs(rotation);

        var targetRotation = 0f;

        if (rotationSize < targetRadius)
        {
            return steering;
        }

//TODO:Стрынные мувы
        if (rotationSize > slowRadius)
        {
            targetRotation = _agent.maxRotation;
        }
        else
        {
            targetRotation = _agent.maxRotation * rotationSize / slowRadius;
        }

        targetRotation *= rotation / rotationSize;
        steering.angular = targetRotation - _agent.rotation;
        steering.angular /= timeToTarget;
        var angularAccel = Mathf.Abs(steering.angular);

        if (angularAccel > _agent.maxAngularAccel)
        {
            steering.angular /= angularAccel;
            steering.angular *= _agent.maxAngularAccel;
        }

        return steering;
    }

    protected virtual void OnDrawGizmos()
    {
        var offsetY = 0.5f;
        
        GizmosHelper.DrawRadius(transform, targetRadius, Color.green, offsetY);

        GizmosHelper.DrawRadius(transform, slowRadius, Color.red, offsetY);
    }
}