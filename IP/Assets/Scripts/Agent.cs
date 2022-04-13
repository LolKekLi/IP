using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{
    public float maxSpeed;
    public float maxAccel;
    public float orientation;
    public float rotation;
    public float maxRotation;
    public float maxAngularAccel;
    public float priorityThreshold = 0.2f;
    
    public Vector3 velocity;

    private Steering _steering = null;

    private Dictionary<int, List<Steering>> _groups;

    private void Start()
    {
        velocity = Vector3.zero;
        _steering = new Steering();

        _groups = new Dictionary<int, List<Steering>>();
    }

    public virtual void Update()
    {
        var deltaTime = Time.deltaTime;
        
        var displacement = velocity * deltaTime;
        
        orientation += rotation * deltaTime;

        if (orientation < 0)
        {
            orientation += 360;
        }
        else if (orientation > 360)
        {
            orientation -= 360;
        }

        var transform1 = transform;

        transform1.Translate(displacement, Space.World);
        transform1.rotation = new Quaternion();
        transform1.Rotate(Vector3.up, orientation);
    }

    public virtual void LateUpdate()
    {
        if (_steering == null)
        {
            return;
        }
        
        var deltaTime = Time.deltaTime;

        velocity += _steering.linear * deltaTime;
        rotation += _steering.angular * deltaTime;
        
        if (velocity.magnitude > maxSpeed)
        {
            velocity.Normalize();
            velocity *= maxSpeed;
        }

        if (_steering.angular == 0f)
        {
            rotation = 0f;
        }

        if (_steering.linear.sqrMagnitude == 0f)
        {
            velocity = Vector3.zero;
        }
        
        _steering = GetPrioritySteering();
        _groups.Clear();
        
        
        //_steering = new Steering();
    }

    private Steering GetPrioritySteering( )
    {
        Steering steering = new Steering();
        var sqrThreshold = priorityThreshold * priorityThreshold;

        var transformName = gameObject.transform.name;

        foreach (var group in _groups.Values)
        {
            steering = new Steering();

            foreach (var singleSteering in group)
            {
                steering.linear += singleSteering.linear;
                steering.angular += singleSteering.angular;
            }

            if (steering.linear.sqrMagnitude > sqrThreshold || Mathf.Abs(steering.angular) > priorityThreshold)
            {
                return steering;
            }
        }
        
        return null;
    }
    
    public void SetSteering(Steering steering, int priority)
    {
        if (!_groups.ContainsKey(priority))
        {
            _groups.Add(priority, new List<Steering>());
        }
        
        _groups[priority].Add(steering);
    }
}