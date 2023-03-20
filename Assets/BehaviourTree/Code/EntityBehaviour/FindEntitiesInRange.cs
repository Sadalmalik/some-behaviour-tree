using System.Linq;
using UnityEngine;

namespace Sadalmalik.BehaviourTree
{
	public class FindEntitiesInRange : BTNode
	{
		public float     radius;
		public string    variable;

		public FindEntitiesInRange(
			float     radius,
			string    variable)
		{
			this.radius   = radius;
			this.variable = variable;
		}

		public override BTState Tick(BTContext context)
		{
			var self = context.GetData<Entity>("self");
			
			Entity[] objects = GameObject
				.FindObjectsOfType<Entity>()
				.Where(o =>
				{
					if (o == self)
						return false;
					var dist = Vector3.Distance(self.Position, o.transform.position);
					return dist < radius;
				} )
				.ToArray();

			Debug.Log($"Found {objects.Length} objects!");
			
			if (objects.Length > 0)
			{
				context.SetData(variable, objects);
				return BTState.Success;
			}

			context.ClearData(variable);
			return BTState.Failure;
		}
	}
}