using System;
using Project;
using UnityEngine;

public class PathFollower : Seek
{
    public Path path;
    public float pathOffset = 0f;
    private float currentParam;
    
    private Vector3 _currentPathPoint = Vector3.zero;

    public override void Awake()
    {
        base.Awake();

        target = new GameObject();
        currentParam = 0f;
    }

    public override Steering GetSteering()
    {
        currentParam = path.GetParam(transform.position, currentParam);
        float targetParam = currentParam + pathOffset;
        _currentPathPoint = path.GetPosition(targetParam);
        target.transform.position = path.GetPosition(targetParam);

        return base.GetSteering();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        Gizmos.DrawLine(transform.position, _currentPathPoint);
    }
}