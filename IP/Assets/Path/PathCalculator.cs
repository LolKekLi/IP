using System;
using System.Collections.Generic;
using UnityEngine;

public class PathCalculator : MonoBehaviour
{
    [field: SerializeField]
    public List<RoadPart> RoadParts
    {
        get;
        private set;
    }

    [SerializeField]
    private RoadPart _startPoint = null;

    [SerializeField]
    private RoadPart _endPoint = null;

    private List<RoadPart> _openList = new List<RoadPart>();
    private List<RoadPart> _сloseList = new List<RoadPart>();

    private static PathCalculator _instacne = null;

    public static PathCalculator Instacne
    {
        get;
        private set;
    }

    private void Awake()
    {
        Instacne = this;
    }

    private void Start()
    {
        CalculatePrioritets(_startPoint, _startPoint.FindFriends());
    }

    private void CalculatePrioritets(RoadPart currentPoint, List<RoadPart> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            var roadPart = list[i];
            var transformPosition = roadPart.transform.localPosition;
            var deltaPosition = transformPosition - transform.localPosition;
            
            if (deltaPosition.x == 0 || deltaPosition.z == 0)
            {
                roadPart.G = 10;
            }
            else
            {
                roadPart.G = 14;
            }
        }
    }
}
