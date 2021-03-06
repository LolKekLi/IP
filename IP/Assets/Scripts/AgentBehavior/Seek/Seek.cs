//Стремится
public class Seek : AgentBehaviour
{
    public override StateType Type => StateType.Seek;
    
    public override Steering GetSteering()
    {
        var steering = new Steering();
        steering.linear = target.transform.position - transform.position;
        steering.linear.Normalize();
        steering.linear *= _agent.maxAccel;

        return steering;
    }
}