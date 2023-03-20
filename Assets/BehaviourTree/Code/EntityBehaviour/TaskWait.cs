using UnityEngine;

namespace Sadalmalik.BehaviourTree
{
	public class TaskWait : BTAsyncNode
	{
		private float _duration;

		private float _endTime;
		private bool  _halted;
		
		public TaskWait(
			float  duration,
			string excludeGroup ="Wait")
			: base(excludeGroup)
		{
			_duration = duration;
			_endTime  = 0;
		}

		public override void OnStart(BTContext context)
		{
			_halted  = false;
			_endTime = Time.time + _duration;
		}

		public override BTState OnTick(BTContext context)
		{
			if (_halted)
				return BTState.Failure;
			if (_endTime <= Time.time)
				return BTState.Success;
			return BTState.Running;
		}

		public override void OnHalt(BTContext context)
		{
			_halted = true;
		}
	}
}