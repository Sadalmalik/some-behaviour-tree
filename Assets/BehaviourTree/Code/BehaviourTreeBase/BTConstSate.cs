namespace Sadalmalik.BehaviourTree
{
	public class BTConstSate : BTNode
	{
		private static BTNode _success;
		private static BTNode _failure;
		private static BTNode _running;

		public static BTNode Success => _success ??= new BTConstSate(BTState.Success);
		public static BTNode Failure => _failure ??= new BTConstSate(BTState.Failure);
		public static BTNode Running => _running ??= new BTConstSate(BTState.Running);

		private BTState _state;
		public BTConstSate(BTState                 state) { _state = state; }
		public override BTState Tick(BTContext context) => _state;
	}
}