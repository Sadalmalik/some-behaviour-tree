using System;
using UnityEngine;

namespace Sadalmalik.BehaviourTree
{
	public class MovableEntity : Entity
	{
		public Vector3 Position
		{
			get => transform.position;
			set => transform.position = value;
		}

		public float velocity;

		public event Action OnTargetReached;

		private bool    _move;
		private float   _stoppingDistance;
		private Vector3 _targetPosition;
		private bool    _reached;

		public bool IsMooving => _move;
		public bool IsReached => _reached;

		protected virtual void Update()
		{
			MoveUpdate();
		}

		public void SetMoveTarget(Vector3 position, float stoppingDistance)
		{
			_targetPosition   = position;
			_stoppingDistance = stoppingDistance;

			_reached = false;
			_move    = true;
		}

		public void StopMove()
		{
			_reached = false;
			_move    = false;
		}

		protected void MoveUpdate()
		{
			if (!_move)
				return;

			var dist = Vector3.Distance(transform.position, _targetPosition);
			if (dist <= _stoppingDistance)
			{
				_reached = true;
				_move    = false;
				OnTargetReached?.Invoke();
				return;
			}

			transform.position = Vector3.MoveTowards(
				transform.position,
				_targetPosition,
				velocity * Time.deltaTime);
			transform.LookAt(_targetPosition);
		}
	}
}