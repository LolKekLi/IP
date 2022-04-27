namespace Tree
{
    public class DecisionTree : DecisionTreeNode
    {
        public DecisionTreeNode root;
        private DecisionTreeNode oldroot;
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

            var decisionTreeNode = root.MakeDecision();

            var treeNode = decisionTreeNode as Action;
            
            if (treeNode != null)
            {
                _actionNew = treeNode;
                
                if (_actionNew == null)
                {
                    _actionNew = _actionOld;
                }

                _actionNew.activated = true;
            }
            else
            {
                root = decisionTreeNode;
            }
        }
    }
}