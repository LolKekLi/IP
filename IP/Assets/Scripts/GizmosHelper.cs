using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project
{
    public static class GizmosHelper
    {
        public static void DrawRadius(Transform transform, float radius, Color color, float offsetY = 0)
        {
            var position = transform.position;
            var transformPosition =
                new Vector3(position.x, position.y + offsetY, position.z);
            Gizmos.color = color;
            float theta = 0;
            var x = radius * Mathf.Cos(theta);
            var y = radius * Mathf.Sin(theta);
            var pos = transformPosition + new Vector3(x, -0.88f, y);
            var newPos = pos;
            var lastPos = pos;

            for (theta = 0.1f; theta < Mathf.PI * 2; theta += 0.1f)
            {
                x = radius * Mathf.Cos(theta);
                y = radius * Mathf.Sin(theta);
                newPos = transformPosition + new Vector3(x, -0.88f, y);
                Gizmos.DrawLine(pos, newPos);
                pos = newPos;
            }

            Gizmos.DrawLine(pos, lastPos);
        }

        public static void DrawCube(Transform transform, Vector3 size, Color color)
        {
            Gizmos.color = color;

            Gizmos.DrawCube(transform.position, size);
        }

        public static void DrawAngle(Transform transform, float angel, float visionDistance, Color color)
        {
            angel /= 2f;
            Gizmos.color = color;

            Vector3 leftLine = transform.forward * visionDistance;
            Vector3 rightLine = leftLine;

            Quaternion leftAngle = Quaternion.Euler(0, angel, 0);
            Quaternion rightAngle = Quaternion.Euler(0, -angel, 0);

            var position = transform.position;
            leftLine = leftAngle * leftLine;
            rightLine = rightAngle * rightLine;

            leftLine = leftLine + transform.position;
            rightLine = rightLine + transform.position;

            Gizmos.DrawLine(position, leftLine);
            Gizmos.DrawLine(position, rightLine);
        }

        // public static void DrawPath<T>(List<T> pathPoints,int pathPointCount,Color color)
        // {
        //     Gizmos.color = color;
        //     
        //     for (int i = 0; i < pathPointCount-1; i++)
        //     {
        //         Gizmos.DrawLine(pathPoints[i].Transform.position, pathPoints[i+1].Transform.position);
        //     }
        // }

        // public static void DrawBorder(float borderCount,Vector3 center, FloatRange border, Color Color)
        // {
        //     Gizmos.color = Color;
        //     
        //     for (int i = 0; i < borderCount; i++)
        //     {
        //         var startMin = center.ChangeX(center.x + border.Min).ChangeZ(center.z + i);
        //         var startMax = center.ChangeX(center.x + border.Max).ChangeZ(center.z + i);
        //         Gizmos.DrawLine(startMin, startMin.ChangeY(startMin.y + 10));
        //         Gizmos.DrawLine(startMax, startMax.ChangeY(startMax.y + 10));
        //     }
        // }
    }
}