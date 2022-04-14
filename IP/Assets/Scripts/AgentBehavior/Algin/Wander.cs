using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Wander : Face
{
    [SerializeField]
    private float _offset = 0f;
    [SerializeField]
    private float _radius = 0f;
    [SerializeField]
    private float _rate = 0f;
    
    public override StateType Type => StateType.Wander;

    public override void Awake()
    {
        base.Awake();
        
        target = new GameObject();
        target.transform.position = transform.position;
    }
    
    private Vector3 tPos = Vector3.zero;
    
    public override Steering GetSteering()
    {
        Steering steering = new Steering();
        var wanderOrientation = Random.Range(-1f, 1f) * _rate;
        var targetOrientation = wanderOrientation + _agent.orientation;
        var orientationVec = GetOriAsVec(_agent.orientation);
        var position = transform.position;
        var targetPostion = (_offset * orientationVec) + position;

        targetPostion += (GetOriAsVec(targetOrientation) * _radius);
        tPos = targetPostion;
        _targetAux.transform.position = targetPostion;

        steering = base.GetSteering();
        steering.linear = _targetAux.transform.position - position;
        steering.linear.Normalize();
        steering.linear *= _agent.maxAccel;
        return steering;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        
        Gizmos.DrawWireSphere(transform.position, _radius);
        
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, tPos);
    }
}