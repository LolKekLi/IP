using System;
using UnityEngine;

[RequireComponent(typeof(Agent))]
public class AgentBehaviour : MonoBehaviour
{
    public int priority = 1;
    
    protected GameObject target = null;
    protected Agent _agent;

    public virtual StateType Type
    {
        get;
        private set;
    }

    public virtual void Awake()
    {
        _agent = gameObject.GetComponent<Agent>();
    }

    private void Update()
    {
        _agent.SetSteering(GetSteering(), priority);
    }
    
    public virtual void Prepare(GameObject agent)
    {
        target = agent;
    }

    public Vector3 GetOriAsVec(float orientation)
    {
        Vector3 vector = Vector3.zero;
        vector.x = Mathf.Sin(orientation * Mathf.Deg2Rad) * 1f;
        vector.z = Mathf.Cos(orientation * Mathf.Deg2Rad) * 1f;

        return vector.normalized;
    }

    public float MapToRange(float rotation)
    {
        rotation %= 360f;

        if (Mathf.Abs(rotation) > 180f)
        {
            if (rotation < 0f)
            {
                rotation += 360f;
            }
            else
            {
                rotation -= 360f;
            }
        }

        return rotation;
    }

    public virtual Steering GetSteering()
    {
        return new Steering();
    }
}