using System;
using System.Collections.Generic;

namespace Sadalmalik.BehaviourTree
{
	public abstract class BTAsyncNode : BTNode
	{
		private string _excludeGroup;
		private string _variableName;

		public string ExcludeGroup => _excludeGroup;
		
		public BTAsyncNode(string excludeGroup)
		{
			_excludeGroup = excludeGroup;
			_variableName = $".BTAsyncNode:{excludeGroup}";
		}
		
		public override BTState Tick(BTContext context)
		{
			var node = context.GetData<BTAsyncNode>(_variableName);

			if (node == null)
			{
				context.SetData(_variableName, this);
				OnStart(context);
				return BTState.Running;
			}

			if (node != this)
			{
				node.OnHalt(context);
				context.SetData(_variableName, this);
				OnStart(context);
				return BTState.Running;
			}
			
			var result = OnTick(context);

			if (result != BTState.Running)
			{
				context.ClearData(_variableName);
			}
			
			return result;
		}

		public abstract void    OnStart(BTContext context);
		public abstract BTState OnTick(BTContext  context);
		public abstract void    OnHalt(BTContext  context);
	}
}