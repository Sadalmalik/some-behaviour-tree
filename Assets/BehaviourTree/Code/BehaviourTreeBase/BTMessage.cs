using UnityEngine;

namespace Sadalmalik.BehaviourTree
{
	public class BTMessage : BTNode
	{
		public string  message;
		public BTState state;

		public BTMessage(string message, BTState state)
		{
			this.message = message;
			this.state   = state;
		}
		
		public override BTState Tick(BTContext context)
		{
			Debug.Log(message);
			
			return state;
		}
	}
}