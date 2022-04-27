namespace Tree
{
    public class DecisionTree : DecisionTreeNode
    {
        public DecisionTreeNode root;
        private Action _actionNew;
        private Action _actionOld;

        public override DecisionTreeNode MakeDecision()
        {
            return root.MakeDecision();
        }

        private void Update()
        {
            if (_actionNew != null)
            {
                _actionNew.activated = false;
                _actionOld = _actionNew;
            }
            
            _actionNew = root.MakeDecision() as Action;
            
            if (_actionNew == null)
            {
                _actionNew = _actionOld;
            }

            _actionNew.activated = true;
        }
    }
}