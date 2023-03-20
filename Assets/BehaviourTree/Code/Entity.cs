using UnityEngine;

namespace Sadalmalik.BehaviourTree
{
	[SelectionBase]
	public class Entity : MonoBehaviour
	{
		public Vector3 Position
		{
			get => transform.position;
			set => transform.position = value;
		}
	}
}