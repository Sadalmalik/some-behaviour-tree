using System.Collections.Generic;

namespace Sadalmalik.BehaviourTree
{
	public abstract class BTNode
	{
		public BTNode parent;

		public BTNode()
		{
		}

		public abstract BTState Tick(BTContext context);
	}

	public abstract class BTGroupNode : BTNode
	{
		private List<BTNode> _children;

		public List<BTNode> Children => _children;

		public BTGroupNode(params BTNode[] children)
		{
			_children = new List<BTNode>();
			for (int i = 0; i < children.Length; i++)
				AttachNode(children[i]);
		}

		public void AttachNode(BTNode node)
		{
			node.parent = this;
			_children.Add(node);
		}
	}
}