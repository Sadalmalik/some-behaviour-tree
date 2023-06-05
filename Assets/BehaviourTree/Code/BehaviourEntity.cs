using System;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Sadalmalik.BehaviourTree
{
	public class BehaviourEntity : MovableEntity
	{
		public Text uiText;
		
		public float     radiusSearch = 6.0f;
		public float     radiusAttack = 1.5f;
		public float     radiusCharge = 1.5f;
		public float     radiusMove   = 0.01f;
		public int       charges      = 3;
		public Transform recharger;
		
		public BTNode    root;
		public BTContext context;

		public Transform[] waypoints;

		public void Awake()
		{
			BuildTree();

			context = new BTContext();
			context.SetData("self", (MovableEntity) this);
			context.SetData("charges", new Container<int>{value = charges});
			context.SetData("recharger", recharger);
			context.SetData("patrol-path", waypoints);
			context.SetData("patrol-index", new Container<int>{value = 0});
		}

		protected override void Update()
		{
			MoveUpdate();
			
			root.Tick(context);

			DumpContext();
		}
		
		private void DumpContext()
		{
			if (uiText==null)
				return;

			var sb = new StringBuilder();

			sb.AppendLine("Context:");
			foreach (var pair in context.Data)
				sb.AppendLine($"  {pair.Key} : {pair.Value}");

			uiText.text = sb.ToString();
		}

		private void BuildTree()
		{
			/*
			root = new BTSequence(
				new BTMessage("AAA"),
				new TaskWait(1),
				new BTMessage("BBB")
				);
			/*/
			root = new BTSelector(
				new BTSequence( // Перезарядка
					new TaskVariableCheck("charges", 0),
					new TaskMoveWaypointToCurrentPosition("patrol-waypoint"),
					new TaskMoveEntity(this, radiusCharge, "recharger"),
					new TaskWait(0.5f),
					new TaskVariableSet("charges", 3)
					),
				new BTSequence( // Патрулирование
					new TaskFindEntities(radiusSearch, "entities"),
					new TaskChoiceNearestEntity("entities", "target"),
					new TaskMoveEntity(this, radiusAttack, "target"),
					new TaskKillEntity("target"),
					new TaskVariableSet("charges", -1)
					),
				new BTSequence( // Уничтожение врагов
					new TaskSelectElement<Transform>(
						"patrol-path",
						"patrol-index",
						"patrol-waypoint"),
					new TaskMoveEntity(this, radiusMove, "patrol-waypoint"),
					new TaskWait(0.5f),
					new TaskVariableSet("patrol-index", 1, mod: waypoints.Length)
					)
				//new TaskPartrol(transform, waypoints, velocity, "patrol-waypoint")
			);
			//*/
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