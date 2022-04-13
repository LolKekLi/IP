using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private bool _set = false;
    private Vector3 _firePos;
    private Vector3 _direction;
    private float _speed;
    private float _timeElapsed;

    private void Update()
    {
        if (!_set)
        {
            return;
        }

        _timeElapsed += Time.deltaTime;

        transform.position = _firePos + _direction * _speed * _timeElapsed;
        transform.position += Physics.gravity * (_timeElapsed * _timeElapsed) / 2f;

        if (transform.position.y < -1f)
        {
            Destroy(gameObject);
        }
    }

    public void Set(Vector3 firePos, Vector3 direction, float speed)
    {
        _firePos = firePos;
        _direction = direction.normalized;
        _speed = speed;
        transform.position = firePos;
        _set = true;
    }

    public static Vector3 GetFireDirection(Vector3 startPos, Vector3 endPos, float speed)
    {
        Vector3 direction = Vector3.zero;
        Vector3 delta = endPos - startPos;

        float a = Vector3.Dot(Physics.gravity, Physics.gravity);
        float b = -4 * (Vector3.Dot(Physics.gravity, delta) + speed * speed);
        float c = 4 * Vector3.Dot(delta, delta);

        if (4 * a * c > b * b)
        {
            return direction;
        }

        float time0 = Mathf.Sqrt((-b + Mathf.Sqrt(b * b - 4 * a * c)) / (2 * a));
        float time1 = Mathf.Sqrt((-b - Mathf.Sqrt(b * b - 4 * a * c)) / (2 * a));

        float time = 0;

        if (time0 < 0f)
        {
            if (time1 < 0)
            {
                return direction;
            }

            time = time1;
        }
        else
        {
            if (time1 < 0)
            {
                time = time0;
            }
            else
            {
                time = Mathf.Min(time0, time1);
            }
        }

        direction = 2 * delta - Physics.gravity * time * time;
        direction /= (2 * speed * time);

        return direction;
    }

    public float GetLandingTime(float height = 0f)
    {
        var position = transform.position;
        var time = 0f;
        var valueInt = (_direction.y * _direction.y) * (_speed * _speed);

        valueInt -= (Physics.gravity.y * 2 * (position.y - height));
        valueInt = Mathf.Sqrt(valueInt);

        float valueAdd = -_direction.y * _speed;
        float valueSub = -_direction.y * _speed;

        valueAdd = (valueAdd + valueInt) / Physics.gravity.y;
        valueSub = (valueSub - valueInt) / Physics.gravity.y;

        if (float.IsNaN(valueAdd) && !float.IsNaN(valueSub))
        {
            return valueSub;
        }

        if (!float.IsNaN(valueAdd) && float.IsNaN(valueSub))
        {
            return valueAdd;
        }

        if (float.IsNaN(valueAdd) && float.IsNaN(valueSub))
        {
            return -1f;
        }

        time = Mathf.Max(valueAdd, valueSub);

        return time;
    }

    public Vector3 GetLandingPos(float height = 0f)
    {
        Vector3 landingPos = Vector3.zero;

        float time = GetLandingTime();

        if (time < 0f)
        {
            return landingPos;
        }

        landingPos.y = height;
        landingPos.x = _firePos.x + _direction.x * _speed * time;
        landingPos.z = _firePos.z + _direction.z * _speed * time;

        return landingPos;
    }
}