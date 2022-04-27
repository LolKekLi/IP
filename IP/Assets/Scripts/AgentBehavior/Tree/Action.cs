namespace Tree
{
    public abstract class Action : DecisionTreeNode
    {
        public bool activated = false;

        public override DecisionTreeNode MakeDecision()
        {
            return this;
        }

        public void LateUpdate()
        {
            if (!activated)
            {
                return;
            }

            DoAction();
        }

        protected abstract void DoAction();
    }
}