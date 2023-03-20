using UnityEngine;

namespace Sadalmalik.BehaviourTree
{
	public class BTSequence : BTGroupNode
	{
		public BTSequence(params BTNode[] children) : base(children)
		{
		}

		public override BTState Tick(BTContext context)
		{
			BTState state;
			foreach (var child in Children)
				if (BTState.Success != (state = child.Tick(context)))
					return state;
			return BTState.Success;
		}
	}
}