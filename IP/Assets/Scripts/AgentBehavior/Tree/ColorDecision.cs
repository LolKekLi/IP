using System;
using Tree;
using UnityEngine;

public class ColorDecision : Decision
{
    [SerializeField]
    private bool _true = default;
    
    private Transform _activeNode = null;
    
    public override DecisionTreeNode GetBranch()
    {
        if (_true)
        {
            _activeNode = nodeTrue.transform;
            return nodeTrue;
        }

        _activeNode = nodeFalse.transform;
        return nodeFalse;
    }

    private void OnDrawGizmos()
    {
        if (_activeNode != null)
        {
            Gizmos.DrawLine(transform.position, _activeNode.position);
        }
    }
}
