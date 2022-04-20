using System;
using Project;
using UnityEngine;

public class AvoidWall : Seek
{
    [SerializeField]
    public float _avoidDistance;
    [SerializeField]
    public float _lookAhead;
    
    public override StateType Type => StateType.AvoidWall;
    
    public override void Prepare(GameObject agent)
    {
        base.Prepare(agent);
        target = new GameObject();
        target.transform.position = agent.transform.position;
        
        target.transform.name = "AvoidWall";
    }

    public override Steering GetSteering()
    {
        Steering steering = new Steering();
        Vector3 position = transform.position;
        Vector3 rayVector = _agent.velocity.normalized * _lookAhead;
        Vector3 direction = rayVector;
        RaycastHit hit;

        Debug.DrawRay(position, direction * 100, Color.cyan);
        
        if (Physics.Raycast(position, direction, out hit, _lookAhead))
        {
            position = hit.point + hit.normal * _avoidDistance;
            target.transform.position = position;
            steering = base.GetSteering();
        }
        
        return steering;
    }

    private void OnDrawGizmos()
    {
        GizmosHelper.DrawRadius(transform, _avoidDistance, Color.yellow, 0.5f);
    }
}
