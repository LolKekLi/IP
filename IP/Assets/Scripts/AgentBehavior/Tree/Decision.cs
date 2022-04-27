namespace Tree
{
    public class Decision : DecisionTreeNode
    {
        public Action nodeTrue;
        public Action nodeFalse;

        public virtual Action GetBranch()
        {
            return null;
        }
    }
}