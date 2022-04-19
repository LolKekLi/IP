using Project;
using UnityEngine;

public class PathFollower : Seek
{
    [SerializeField]
    private Path _path;
    [SerializeField]
    private float _pathOffset = 0f;
    
    private float _currentParam;
    
    private Vector3 _currentPathPoint = Vector3.zero;
    
    public override StateType Type => StateType.PathFollower;
    
    public override void Prepare(GameObject agent)
    {
        base.Prepare(agent);
        
        target = new GameObject();
        target.transform.position = agent.transform.position;
        
        target.transform.name = "Path";
        _currentParam = 0f;
    }

    public override Steering GetSteering()
    {
        _currentParam = _path.GetParam(transform.position, _currentParam);
        float targetParam = _currentParam + _pathOffset;
        _currentPathPoint = _path.GetPosition(targetParam);
        target.transform.position = _path.GetPosition(targetParam);

        return base.GetSteering();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        Gizmos.DrawLine(transform.position, _currentPathPoint);
    }
}