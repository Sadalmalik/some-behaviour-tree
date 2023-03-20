using UnityEngine;

namespace Sadalmalik.BehaviourTree
{
	public class KillEntity : BTNode
	{
		public string targetVariable;

		public KillEntity(string targetVariable)
		{
			this.targetVariable = targetVariable;
		}

		public override BTState Tick(BTContext context)
		{
			var target = context.GetData<Entity>(targetVariable);
			if (target == null)
				return BTState.Failure;

			GameObject.Destroy(target.gameObject);

			return BTState.Success;
		}
	}
}