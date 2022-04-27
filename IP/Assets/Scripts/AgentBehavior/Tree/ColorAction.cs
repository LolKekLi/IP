using UnityEngine;

namespace Tree
{
    public class ColorAction : Action
    {
        [SerializeField]
        private Material _material = null;
        [SerializeField]
        private Color _color = default;
        
        protected override void DoAction()
        {
            _material.color = Color.red;
        }
    }
}