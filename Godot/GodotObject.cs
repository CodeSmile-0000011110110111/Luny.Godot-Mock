using System;

namespace Godot
{
	public class GodotObject : IDisposable
	{
		public virtual void Dispose() => throw new NotImplementedException("Godot.GodotObject.Dispose");
		public ulong GetInstanceId() => throw new NotImplementedException("Godot.GodotObject.GetInstanceId");
		public static bool IsInstanceValid(GodotObject obj) => throw new NotImplementedException("Godot.GodotObject.IsInstanceValid");
	}
}
