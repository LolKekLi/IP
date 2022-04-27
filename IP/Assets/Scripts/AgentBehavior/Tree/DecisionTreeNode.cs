using UnityEngine;

namespace Tree
{
    public class DecisionTreeNode : MonoBehaviour
    {
        public virtual DecisionTreeNode MakeDecision()
        {
            return null;
        }
    }
}