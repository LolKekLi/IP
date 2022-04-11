public class Leave : AgentBehaviour
{
    public float escapeRadius;
    public float dangerRadius;
    public float timeToTarget;

    public override Steering GetSteering()
    {
        Steering steering = new Steering();
        var direction = transform.position -  target.transform.position;
        var distance = direction.magnitude;
        var reduce = 0f;
        
        if (distance > dangerRadius)
        {
            return steering;
        }

        if (distance < escapeRadius)
        {
            reduce = 0f;
        }
        else
        {
            reduce = distance / dangerRadius * _agent.maxSpeed;
        }

        var targetSpeed = _agent.maxSpeed - reduce;
        var desiredVelocity = direction;
        desiredVelocity.Normalize();
        desiredVelocity *= targetSpeed;

        steering.linear = desiredVelocity - _agent.velocity;
        steering.linear /= timeToTarget;

        if (steering.linear.magnitude > _agent.maxAccel)
        {
            steering.linear.Normalize();
            steering.linear *= _agent.maxAccel;
        }

        return steering;
    }
}