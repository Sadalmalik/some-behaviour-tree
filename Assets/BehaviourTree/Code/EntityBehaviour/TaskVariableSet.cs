using System;

namespace Sadalmalik.BehaviourTree
{
	public class TaskVariableSet : BTNode
	{
		public string variableName;
		public int    addition;

		public int? min;
		public int? max;
		public int? mod;

		public TaskVariableSet(
			string variableName,
			int    addition,
			int?   min = null,
			int?   max = null,
			int?   mod = null)
		{
			this.variableName = variableName;
			this.addition     = addition;

			this.min = min;
			this.max = max;
			this.mod = mod;
		}

		public override BTState Tick(BTContext context)
		{
			var variable = context.GetData<Container<int>>(variableName);

			variable.value += addition;
			if (min.HasValue)
				if (variable.value < min.Value)
					variable.value = min.Value;
			if (max.HasValue)
				if (variable.value < max.Value)
					variable.value = max.Value;
			if (mod.HasValue)
				variable.value %= mod.Value;

			return BTState.Success;
		}
	}
}