using UnityEngine;

public class Steering
{
    public float angular = 0f;
    public Vector3 linear = Vector3.zero;

    public Steering()
    {
        angular = 0f;
        linear = new Vector3();
    }
}
