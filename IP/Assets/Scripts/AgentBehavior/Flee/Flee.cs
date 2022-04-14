//Бежать
public class Flee : AgentBehaviour
{
    public override StateType Type => StateType.Flee;

    public override Steering GetSteering()
    {
        var steering = new Steering();
        steering.linear = transform.position - target.transform.position ;
        steering.linear.Normalize();
        steering.linear *= _agent.maxAccel;

        return steering;
    }
}