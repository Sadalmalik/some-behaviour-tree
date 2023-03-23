using System;

namespace Sadalmalik.BehaviourTree
{
	public class TaskVariableCheck : BTNode
	{
		public string variableName;
		public int    targetValue;

		public TaskVariableCheck(string variableName, int targetValue)
		{
			this.variableName = variableName;
			this.targetValue  = targetValue;
		}

		public override BTState Tick(BTContext context)
		{
			var variable = context.GetData<Container<int>>(variableName);

			return variable.value == targetValue ? BTState.Success : BTState.Failure;
		}
	}
}