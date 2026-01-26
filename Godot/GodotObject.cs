using System;
using System.Collections.Generic;

namespace Godot
{
	public class GodotObject : IDisposable
	{
		private static ulong _nextId = 1;
		internal static readonly HashSet<GodotObject> _allObjects = new();

		internal static void Reset_UnitTestsOnly()
		{
			_nextId = 1;
			_allObjects.Clear();
		}

		private readonly ulong _instanceId = _nextId++;

		public GodotObject() => _allObjects.Add(this);

		public virtual void Dispose()
		{
			_allObjects.Remove(this);
			GC.SuppressFinalize(this);
		}

		public ulong GetInstanceId() => _instanceId;

		public static bool IsInstanceValid(GodotObject obj) => obj != null && _allObjects.Contains(obj);

		public override bool Equals(object obj) => ReferenceEquals(this, obj) || (obj is GodotObject other && _instanceId == other._instanceId);
		public override int GetHashCode() => _instanceId.GetHashCode();
	}
}
