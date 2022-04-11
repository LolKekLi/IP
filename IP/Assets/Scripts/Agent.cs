using UnityEngine;

public class Agent : MonoBehaviour
{
    public float maxSpeed;
    public float maxAccel;
    public float orientation;
    public float rotation;
    public float maxRotation;
    public float maxAngularAccel;

    public Vector3 velocity;

    protected Steering _steering = null;
    
    private void Start()
    {
        velocity = Vector3.zero;
        _steering = new Steering();
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
        var deltaTime = Time.deltaTime;

        velocity += _steering.linear * deltaTime;
        rotation += _steering.angular * deltaTime;

        if (velocity.magnitude > maxSpeed)
        {
            velocity.Normalize();
            velocity *= maxSpeed;
        }

        if (_steering.angular == 0)
        {
            rotation = 0;
        }

        if (_steering.linear.sqrMagnitude == 0)
        {
            velocity = Vector3.zero;
        }

        _steering = new Steering();
    }


    public void SetStreering(Steering steering)
    {
        _steering = steering;
    }
}