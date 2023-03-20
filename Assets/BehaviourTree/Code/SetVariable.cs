namespace Sadalmalik.BehaviourTree
{
	public class SetVariable : BTNode
	{
		public string variableName;
		public int    addition;

		public SetVariable(string variableName, int addition)
		{
			this.variableName = variableName;
			this.addition     = addition;
		}

		public override BTState Tick(BTContext context)
		{
			var variable = context.GetData<Container<int>>(variableName);

			variable.value += addition;

			return BTState.Success;
		}
	}
}