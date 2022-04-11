using UnityEngine;

public class Wander : Face
{
    public float offset;
    public float radius;
    public float rate;

    public override void Awake()
    {
        target = new GameObject();
        target.transform.position = transform.position;
        base.Awake();
    }

    public override Steering GetSteering()
    {
        Steering steering = new Steering();
        var wanderOrientation = Random.Range(-1f, 1f) * rate;
        var targetOrientation = wanderOrientation + _agent.orientation;
        var orientationVec = GetOriAsVec(_agent.orientation);
        var position = transform.position;
        var targetPostion = (offset * orientationVec) + position;

        targetPostion += (GetOriAsVec(targetOrientation) * radius);
        targetAux.transform.position = targetPostion;

        steering = base.GetSteering();
        steering.linear = targetAux.transform.position - position;
        steering.linear.Normalize();
        steering.linear *= _agent.maxAccel;
        return steering;

        return base.GetSteering();
    }
}