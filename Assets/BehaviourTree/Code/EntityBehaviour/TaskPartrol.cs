using UnityEngine;

namespace Sadalmalik.BehaviourTree
{
	public class TaskPartrol : BTNode
	{
		private Transform   _transform;
		private Transform[] _waypoints;

		private int _currentWaypointIndex = 0;

		private float  _waitTime    = 1f; // in seconds
		private float  _waitCounter = 0f;
		private bool   _waiting     = false;
		private string _lastWaypoint;

		private float _speed;
		
		public TaskPartrol(Transform transform, Transform[] waypoints, float speed, string lastWaypointVariable)
		{
			_transform = transform;
			_waypoints = waypoints;

			_lastWaypoint = lastWaypointVariable;

			_speed = speed;
		}

		public override BTState Tick(BTContext context)
		{
			if (_waiting)
			{
				_waitCounter += Time.deltaTime;
				if (_waitCounter >= _waitTime)
				{
					_waiting = false;
				}
			}
			else
			{
				Transform wp = _waypoints[_currentWaypointIndex];
				context.SetData(_lastWaypoint, wp);

				if (Vector3.Distance(_transform.position, wp.position) < 0.01f)
				{
					_transform.position = wp.position;
					_waitCounter        = 0f;
					_waiting            = true;

					_currentWaypointIndex = (_currentWaypointIndex + 1) % _waypoints.Length;
				}
				else
				{
					_transform.position = Vector3.MoveTowards(
						_transform.position,
						wp.position,
						_speed * Time.deltaTime);
					_transform.LookAt(wp.position);
				}
			}

			return BTState.Running;
		}
	}
}