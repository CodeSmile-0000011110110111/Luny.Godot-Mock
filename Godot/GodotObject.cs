using System;
using System.Collections.Generic;
using System.Linq;

namespace Godot
{
	public class GodotObject : IDisposable
	{
		private static UInt64 _nextId = 1;
		internal static readonly HashSet<GodotObject> _allObjects = new();

		private readonly UInt64 _instanceId = _nextId++;

		public static Boolean IsInstanceValid(GodotObject obj) => obj != null && _allObjects.Contains(obj);

		public static IEnumerable<T> GetNodes<T>() where T : Node
		{
			var root = SceneTree.Instance.Root;
			return GetNodesInternal<T>(root);
		}

		private static IEnumerable<T> GetNodesInternal<T>(Node parent) where T : Node
		{
			if (parent is T t)
				yield return t;

			foreach (var child in parent.GetChildren())
			{
				foreach (var node in GetNodesInternal<T>(child))
					yield return node;
			}
		}

		public static T GetFirstNode<T>() where T : Node => GetNodes<T>().FirstOrDefault();

		internal static void Reset_UnitTestsOnly()
		{
			_nextId = 1;
			_allObjects.Clear();
		}

		public GodotObject() => _allObjects.Add(this);

		public virtual void Dispose()
		{
			_allObjects.Remove(this);
			GC.SuppressFinalize(this);
		}

		public UInt64 GetInstanceId() => _instanceId;

		public override Boolean Equals(Object obj) =>
			ReferenceEquals(this, obj) || obj is GodotObject other && _instanceId == other._instanceId;

		public override Int32 GetHashCode() => _instanceId.GetHashCode();
	}
}
