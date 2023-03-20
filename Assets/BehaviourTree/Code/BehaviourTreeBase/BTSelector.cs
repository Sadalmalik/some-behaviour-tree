using UnityEngine;

namespace Sadalmalik.BehaviourTree
{
	public class BTSelector : BTGroupNode
	{
		public BTSelector(params BTNode[] children) : base(children)
		{
		}

		public override BTState Tick(BTContext context)
		{
			BTState state;
			foreach (var child in Children)
				if (BTState.Failure != (state = child.Tick(context)))
					return state;
			return BTState.Failure;
		}
	}
}