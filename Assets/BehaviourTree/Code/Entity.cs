using System.Collections.Generic;
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
		
		public List<string> tags = new List<string>();
		public bool         HasTag(string tag) => tags.Contains(tag);
		public void         AddTag(string tag) => tags.Add(tag);
		public bool         RenTag(string tag) => tags.Remove(tag);
		
	}
}