using System;
using System.Collections.Generic;
using Project;
using UnityEngine;

public class RoadPart : MonoBehaviour
{
    [SerializeField]
    private float _friendPartSeeRadius = 0f;

    [SerializeField]
    private bool _offGizmo = true;

    private List<RoadPart> _friends = new List<RoadPart>();

    public float F = 0;

    //Энергия, затрачиваемая на передвижение из стартовой клетки A в текущую рассматриваемую клетку, следуя найденному пути к этой клетк
    public float G = 0;

    //Примерное количество энергии, затрачдиваемое на перевижение от текущей клетки до целевой клетки B.
    //Изначально эта величина равна предположительному значению, такому, что если бы мы шли напрямую, игнорируя препятствия (но исключив диагональные перемещения).
    //В процессе поиска она корректируется в зависимости от встречающихся на пути преград
    public float H = 0;
    
    private RoadPart _parentPart = null;

    [field: SerializeField]
    public bool IsCanMove
    {
        get;
        private set;
    }

    private void Start()
    {
        _friends = FindFriends();
    }

    public List<RoadPart> FindFriends()
    {
        List<RoadPart> list = new List<RoadPart>(PathCalculator.Instacne.RoadParts);
        list.Remove(this);
        
        List<RoadPart> resultList = new List<RoadPart>();
        
        var friendPartSeeRadius = _friendPartSeeRadius * _friendPartSeeRadius;

        for (int i = 0; i < list.Count; i++)
        {
            if ((list[i].transform.position - transform.position).sqrMagnitude < friendPartSeeRadius)
            {
                resultList.Add(list[i]);
            }
        }
        
        return resultList;
    }
    
    private void OnDrawGizmos()
    {
        if (_offGizmo)
        {
            return;
        }
        
        GizmosHelper.DrawRadius(transform, _friendPartSeeRadius, Color.blue, 1);

        if (_friends != null)
        {
            if (_friends.Count > 0)
            {
                for (int i = 0; i < _friends.Count; i++)
                {
                    Gizmos.DrawLine(transform.position, _friends[i].transform.position);
                }
            }
        }
    }
}