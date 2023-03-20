namespace Sadalmalik.BehaviourTree
{
	public class TaskSelectElement<T> : BTNode
	{
		private string _variableList;
		private string _variableIndex;
		private string _variableResult;
		
		public TaskSelectElement(
			string variableList,
			string variableIndex,
			string variableResult)
		{
			_variableList   = variableList;
			_variableIndex  = variableIndex;
			_variableResult = variableResult;
		}

		public override BTState Tick(BTContext context)
		{
			var list = context.GetData<T[]>(_variableList);
			var index = context.GetData<Container<int>>(_variableIndex);
			
			if (list == null)
				return BTState.Failure;
			if (index == null)
				return BTState.Failure;

			var idx = index.value;
			if (idx < 0 || list.Length <= idx)
				return BTState.Failure;
			
			context.SetData(_variableResult, list[idx]);
			
			return BTState.Success;
		}
	}
}