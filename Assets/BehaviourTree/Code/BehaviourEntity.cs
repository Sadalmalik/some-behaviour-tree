using System;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Sadalmalik.BehaviourTree
{
	public class BehaviourEntity : MovableEntity
	{
		public float     radiusSearch = 6.0f;
		public float     radiusAttack = 1.5f;
		public float     radiusCharge = 1.5f;
		public int       charges      = 3;
		public Transform recharger;
		
		public BTNode    root;
		public BTContext context;

		public Transform[] waypoints;

		public void Awake()
		{
			BuildTree();

			context = new BTContext();
			context.SetData("self", this);
			context.SetData("charges", new Container<int>{value = charges});
			context.SetData("recharger", recharger);
		}

		private void BuildTree()
		{
			root = new BTSelector(
				new BTSequence(
					new CheckVariable("charges", 0),
					new MoveWaypointToCurrentPosition("lastWaypoint"),
					new MoveTowardsObject(velocity, radiusCharge, "recharger"),
					// new TaskMoveEntity(this, radiusCharge, "recharger"),
					new SetVariable("charges", 3)
					),
				new BTSequence(
					new FindEntitiesInRange(radiusSearch, "entities"),
					new ChoiceNearestEntity("entities", "target"),
					new MoveTowardsEntity(velocity, radiusAttack, "target"),
					// new TaskMoveEntity(this, radiusAttack, "target"),
					new KillEntity("target"),
					new SetVariable("charges", -1)
					),
				new TaskPartrol(transform, waypoints, velocity, "lastWaypoint")
			);
		}

		public void Update()
		{
			root.Tick(context);
		}

#if UNITY_EDITOR
		private void OnDrawGizmos()
		{
			var last = Handles.color;
			Handles.color = Color.black;
			Handles.DrawWireArc(
				transform.position,
				Vector3.up, 
				Vector3.forward, 
				360,
				radiusSearch,
				2.5f
				);
			Handles.color = last;
		}
#endif
	}
}