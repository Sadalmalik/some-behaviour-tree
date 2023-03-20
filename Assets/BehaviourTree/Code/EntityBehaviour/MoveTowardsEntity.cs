using UnityEngine;

namespace Sadalmalik.BehaviourTree
{
	public class MoveTowardsEntity : BTNode
	{
		public float  speed;
		public float  stoppingDistance;
		public string targetVariable;

		public MoveTowardsEntity(float speed, float stoppingDistance, string targetVariable)
		{
			this.speed            = speed;
			this.stoppingDistance = stoppingDistance;
			this.targetVariable   = targetVariable;
		}

		public override BTState Tick(BTContext context)
		{
			var self   = context.GetData<Entity>("self");
			var target = context.GetData<Entity>(targetVariable);
			
			if (self == null) return BTState.Failure;
			if (target == null) return BTState.Failure;

			var distance = Vector3.Distance(self.Position, target.Position);
			if (distance < stoppingDistance)
				return BTState.Success;

			self.Position = Vector3.MoveTowards(
				self.Position,
				target.Position,
				speed * Time.deltaTime);
			self.transform.LookAt(target.Position);
			return BTState.Running;
		}
	}
}