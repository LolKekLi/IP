using System;
using System.Collections.Generic;
using UnityEngine;

namespace Project
{

    public class Path : MonoBehaviour
    {
        public List<GameObject> nodes;
        private List<PathSegment> _segments;

        private void Start()
        {
            _segments = GetSegments();
        }

        private List<PathSegment> GetSegments()
        {
            List<PathSegment> segments = new List<PathSegment>();
            int i;
            for (i = 0; i < nodes.Count - 1; i++)
            {
                var src = nodes[i].transform.position;
                var dst = nodes[i + 1].transform.position;
                PathSegment segment = new PathSegment(src, dst);
                segments.Add(segment);
            }

            return segments;
        }

        public float GetParam(Vector3 position, float lastParam)
        {
            var param = 0f;
            PathSegment currentSegment = null;
            var tempParam = 0f;

            foreach (var ps in _segments)
            {
                tempParam += Vector3.Distance(ps.a, ps.b);

                if (lastParam <= tempParam)
                {
                    currentSegment = ps;
                    break;
                }
            }

            if (currentSegment == null)
            {
                return 0f;
            }

            var currPos = position - currentSegment.a;
            var segmentDirection = currentSegment.b - currentSegment.a;
            segmentDirection.Normalize();

            var pointInSegment = Vector3.Project(currPos, segmentDirection);

            param = tempParam - Vector3.Distance(currPos, segmentDirection);

            param += pointInSegment.magnitude;

            return param;
        }

        public Vector3 GetPosition(float param)
        {
            Vector3 position = Vector3.zero;
            PathSegment currentSegment = null;
            float tempParam = 0f;

            foreach (var ps in _segments)
            {
                tempParam += Vector3.Distance(ps.a, ps.b);

                if (param <= tempParam)
                {
                    currentSegment = ps;
                    break;
                }
            }

            if (currentSegment == null)
            {
                return Vector3.zero;
            }

            var segmentDirection = currentSegment.b - currentSegment.a;
            segmentDirection.Normalize();
            tempParam -= Vector3.Distance(currentSegment.a, currentSegment.b);
            tempParam = param - tempParam;
            position = currentSegment.a + segmentDirection * tempParam;
            return position;
        }

        private void OnDrawGizmos()
        {
            if (nodes.Count <= 2)
            {
                return;
            }

            Vector3 direction;
            Color tmp = Gizmos.color;
            Gizmos.color = Color.magenta;

            for (int i = 0; i < nodes.Count - 1; i++)
            {
                Vector3 src = nodes[i].transform.position;
                Vector3 dst = nodes[i + 1].transform.position;

                direction = dst - src;
                Gizmos.DrawRay(src, direction);
            }

            Gizmos.color = tmp;
        }
    }
}
