//Прибыть
public class Arrive : AgentBehaviour
{
    public float targetRadius;
    public float slowradius;
    public float timeToTarget = 0.1f;

    public override Steering GetSteering()
    {
        Steering steering = new Steering();
        var direction = target.transform.position - transform.position;
        var distance = direction.magnitude;
        var targetSpeed = 0f;

        if (distance < targetRadius)
        {
            return steering;
        }

        if (distance > slowradius)
        {
            targetSpeed = _agent.maxSpeed;
        }
        else
        {
            targetSpeed = _agent.maxSpeed * distance / slowradius;
        }

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