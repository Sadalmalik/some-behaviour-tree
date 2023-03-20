using UnityEngine;

namespace Sadalmalik.BehaviourTree
{
	public class TaskMoveEntity : BTAsyncNode
	{
		private MovableEntity _entity;
		private float         _stoppingDistance;
		private string        _targetVariable;
		
		public TaskMoveEntity(
			MovableEntity entity,
			float stoppingDistance,
			string targetVariable)
			: base("MoveEntity")
		{
			_entity           = entity;
			_stoppingDistance = stoppingDistance;
			_targetVariable   = targetVariable;
		}

		public override void OnStart(BTContext context)
		{
			Debug.Log("TaskMoveEntity.OnStart");
			var target    = (Transform) null;
			var component = context.GetData<Component>(_targetVariable);

			if (component == null)
			{
				_entity.StopMove();
				return;
			}

			target = component as Transform;
			if (target != null)
			{
				_entity.StopMove();
				_entity.SetMoveTarget(target.position, _stoppingDistance);
				return;
			}

			target = component.GetComponent<Transform>();
			if (target != null)
			{
				_entity.StopMove();
				_entity.SetMoveTarget(target.position, _stoppingDistance);
			}
		}

		public override BTState OnTick(BTContext context)
		{
			Debug.Log("TaskMoveEntity.OnTick");
			if (_entity.IsMooving)
				return BTState.Running;
			if (_entity.IsReached)
				return BTState.Success;
			return BTState.Failure;
		}

		public override void OnHalt(BTContext context)
		{
			Debug.Log("TaskMoveEntity.OnHalt");
			_entity.StopMove();
		}
	}
}