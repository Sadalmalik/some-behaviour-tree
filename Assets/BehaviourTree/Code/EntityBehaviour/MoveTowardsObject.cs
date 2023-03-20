using UnityEngine;

namespace Sadalmalik.BehaviourTree
{
	public class MoveTowardsObject : BTNode
	{
		public float  speed;
		public float  stoppingDistance;
		public string targetVariable;

		public MoveTowardsObject(float speed, float stoppingDistance, string targetVariable)
		{
			this.speed            = speed;
			this.stoppingDistance = stoppingDistance;
			this.targetVariable   = targetVariable;
		}

		public override BTState Tick(BTContext context)
		{
			var self   = context.GetData<Entity>("self");
			var target = context.GetData<Transform>(targetVariable);
			
			if (self == null) return BTState.Failure;
			if (target == null) return BTState.Failure;

			var distance = Vector3.Distance(self.Position, target.position);
			if (distance < stoppingDistance)
				return BTState.Success;

			self.Position = Vector3.MoveTowards(
				self.Position,
				target.position,
				speed * Time.deltaTime);
			self.transform.LookAt(target.position);
			return BTState.Running;
		}
	}
}