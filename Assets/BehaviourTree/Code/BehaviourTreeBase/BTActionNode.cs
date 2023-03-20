using System;

namespace Sadalmalik.BehaviourTree
{
	public class BTActionNode : BTNode
	{
		private Action<BTContext> _action;

		public BTActionNode(Action<BTContext> action)
		{
			_action = action;
		}

		public override BTState Tick(BTContext context)
		{
			if (_action == null)
			{
				return BTState.Failure;
			}

			try
			{
				_action?.Invoke(context);
			}
			catch
			{
				return BTState.Failure;
			}

			return BTState.Success;
		}
	}
}