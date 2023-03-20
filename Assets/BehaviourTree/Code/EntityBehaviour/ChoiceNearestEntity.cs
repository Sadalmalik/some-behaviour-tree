using System.Linq;
using UnityEngine;

namespace Sadalmalik.BehaviourTree
{
	public class ChoiceNearestEntity : BTNode
	{
		public string listVariable;
		public string entityVariable;

		public ChoiceNearestEntity(
			string listVariable,
			string entityVariable)
		{
			this.listVariable   = listVariable;
			this.entityVariable = entityVariable;
		}

		public override BTState Tick(BTContext context)
		{
			var self = context.GetData<Entity>("self");
			var list = context.GetData<Entity[]>(listVariable);

			if (list == null)
			{
				context.ClearData(entityVariable);
				return BTState.Failure;
			}

			var nearest = list[0];
			var nearDist = Vector3.Distance(
				self.Position,
				nearest.transform.position);
			for (int i = 1, s = list.Length; i < s; i++)
			{
				var entity = list[i];
				var dist = Vector3.Distance(
					self.Position,
					entity.transform.position);
				if (dist > nearDist) continue;
				nearest  = entity;
				nearDist = dist;
			}

			context.SetData(entityVariable, nearest);
			return BTState.Success;
		}
	}
}