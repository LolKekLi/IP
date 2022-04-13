using UnityEngine;

public class JumpPoint
{
    public Vector3 jumpLocation;
    public Vector3 landingLocation;
    public Vector3 deltaPosition;

    public JumpPoint() : this(Vector3.zero, Vector3.zero)
    {
        
    }

    public JumpPoint(Vector3 a, Vector3 b)
    {
        jumpLocation = a;
        landingLocation = b;
        deltaPosition = landingLocation - this.jumpLocation;
    }
}