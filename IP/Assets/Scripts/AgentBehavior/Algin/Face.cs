//Лицо

using System;
using UnityEngine;

public class Face : Align
{
    protected GameObject targetAux = null;

    public override StateType Type => StateType.Face;
    
    public override void Prepare(GameObject agent)
    {
        base.Prepare(agent);
        target = new GameObject();
        targetAux = target;
        target.AddComponent<Agent>();
    }

    private void OnDestroy()
    {
        Destroy(target);
    }

    public override Steering GetSteering()
    {
        var direction = targetAux.transform.position - transform.position;

        if (direction.magnitude > 0f)
        {
            var targetOrientation = Mathf.Atan2(direction.x, direction.z);
            targetOrientation *= Mathf.Rad2Deg;
            target.GetComponent<Agent>().orientation = targetOrientation;
        }

        return base.GetSteering();
    }
}