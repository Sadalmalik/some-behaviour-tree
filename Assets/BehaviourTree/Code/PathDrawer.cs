using UnityEngine;

namespace Sadalmalik.BehaviourTree
{
	public class PathDrawer : MonoBehaviour
	{
		public void OnDrawGizmos()
		{
			var count = transform.childCount;

			for (var i = 0; i < count; i++)
			{
				var a = transform.GetChild(i);
				var b = transform.GetChild((i+1)%count);
				Debug.DrawLine(a.position, b.position, Color.black);
			}
		}
	}
}