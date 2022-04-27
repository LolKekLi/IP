using Tree;
using UnityEngine;

public class ColorDecision : Decision
{
    [SerializeField]
    private KeyCode _code = default;
    
    public override Action GetBranch()
    {
        if (Input.GetKeyDown(_code))
        {
            return nodeTrue;
        }

        return nodeFalse;
    }
}
