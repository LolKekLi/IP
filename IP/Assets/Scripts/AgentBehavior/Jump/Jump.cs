using System.Collections.Generic;
using UnityEngine;

public class Jump : VelocityMatch
{
    public JumpPoint jumpPoint;

    //Признaк допустимости прыжка
    bool canAchieve = false;

    //Максимальная вертикальная скорость при прыжке
    public float maxYVelocity;

    public Vector3 gravity = new Vector3(0f, -9.8f, 0f);
    private Projectile projectile;

    private List<AgentBehaviour> behaviours;

    public override StateType Type => StateType.Jump;

    public override void Awake()
    {
        base.Awake();
        enabled = false;
        projectile = gameObject.AddComponent<Projectile>();
        behaviours = new List<AgentBehaviour>();
        AgentBehaviour[] abs;

        abs = gameObject.GetComponents<AgentBehaviour>();

        foreach (AgentBehaviour b in abs)
        {
            if (b == this)
            {
                continue;
            }

            behaviours.Add(b);
        }
    }

    public override Steering GetSteering()
    {
        Steering steering = new Steering();

        // Проверить наличие траектории и, еесли она отсутствует,
        // создать ее.

        if (jumpPoint != null && target == null)
        {
            CalculateTarget();
        }

        //Проверить равенство траектории нyJll).
        //Если нет, ускорение не требуется .
        if (!canAchieve)
        {
            return steering;
        }

        //Проверить попадание в точку nрыжка
        if (Mathf.Approximately((transform.position - target.transform.position).magnitude, 0f) &&
            Mathf.Approximately((_agent.velocity - target.GetComponent<Agent>().velocity).magnitude, 0f))
        {
            DoJump();
            return steering;
        }

        return base.GetSteering();
    }

    public void Isolate(bool state)
    {
        foreach (AgentBehaviour b in behaviours)
        {
            b.enabled = !state;
        }

        enabled = state;
    }

    public void DoJump()
    {
        projectile.enabled = true;
        Vector3 direction;

        direction = Projectile.GetFireDirection(jumpPoint.jumpLocation, jumpPoint.landingLocation, _agent.maxSpeed);
        projectile.Set(jumpPoint.jumpLocation, direction, _agent.maxSpeed); //Еще false
    }

    protected void CalculateTarget()
    {
        target = new GameObject();
        target.AddComponent<Agent>();

        //Вычислить время прыжка
        float sqrtTerm = Mathf.Sqrt(2f * gravity.y * jumpPoint.deltaPosition.y + maxYVelocity * _agent.maxSpeed);
        float time = (maxYVelocity - sqrtTerm) / gravity.y;

        //Проверить допустимость и при необходимости вычислить другое время
        if (!CheckJumpTime(time))
        {
            time = (maxYVelocity + sqrtTerm) / gravity.y;
        }
    }

    private bool CheckJumpTime(float time)
    {
        //Вычислить горизонтальную скорость
        float vx = jumpPoint.deltaPosition.x / time;
        float vz = jumpPoint.deltaPosition.z / time;
        float speedSq = vx * vx + vz * vz;

        //Проверить полученное решение на допустимость
        if (speedSq < _agent.maxSpeed * _agent.maxSpeed)
        {
            target.GetComponent<Agent>().velocity = new Vector3(vx, 0f, vz);
            canAchieve = true;
            return true;
        }

        return false;
    }
}