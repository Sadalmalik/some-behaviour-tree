using UnityEngine;

namespace Sadalmalik.BehaviourTree
{
	public class MoveWaypointToCurrentPosition : BTNode
	{
		private string _lastWaypoint;
		
		public MoveWaypointToCurrentPosition(string waypointVariable)
		{
			_lastWaypoint = waypointVariable;
		}
		
		public override BTState Tick(BTContext context)
		{
			var self  = context.GetData<Entity>("self");
			var point = context.GetData<Transform>(_lastWaypoint);

			if (self == null) return BTState.Failure;
			if (point == null) return BTState.Success;

			point.position = self.Position;
			context.ClearData(_lastWaypoint);
			
			return BTState.Success;
		}
	}
}