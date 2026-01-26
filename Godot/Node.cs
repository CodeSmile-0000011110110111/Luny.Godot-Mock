using System;

namespace Godot
{
	public class Node : GodotObject
	{
		public String Name { get; set; }
		public String SceneFilePath { get; set; }
		public ProcessModeEnum ProcessMode { get; set; }

		public enum ProcessModeEnum
		{
			Inherit,
			Pausable,
			WhenPaused,
			Always,
			Disabled,
		}

		public virtual void _Ready() => throw new NotImplementedException("Godot.Node._Ready");
		public virtual void _Process(double delta) => throw new NotImplementedException("Godot.Node._Process");
		public virtual void _PhysicsProcess(double delta) => throw new NotImplementedException("Godot.Node._PhysicsProcess");
		public virtual void _Notification(int what) => throw new NotImplementedException("Godot.Node._Notification");
		public virtual void _ExitTree() => throw new NotImplementedException("Godot.Node._ExitTree");

		public void QueueFree() => throw new NotImplementedException("Godot.Node.QueueFree");
		public bool IsInsideTree() => throw new NotImplementedException("Godot.Node.IsInsideTree");
		public bool CanProcess() => throw new NotImplementedException("Godot.Node.CanProcess");
		public void PropagateNotification(int what) => throw new NotImplementedException("Godot.Node.PropagateNotification");
		public void AddChild(Node node, bool forceReadableName = false, InternalMode internal_ = InternalMode.Disabled) => throw new NotImplementedException("Godot.Node.AddChild");
		public Godot.Collections.Array<Node> GetChildren() => throw new NotImplementedException("Godot.Node.GetChildren");
		public void CallDeferred(string method, params object[] args) => throw new NotImplementedException("Godot.Node.CallDeferred");

		public enum InternalMode
		{
			Disabled,
			Front,
			Back,
		}

		public const int NotificationCrash = 1012;
		public const int NotificationWMCloseRequest = 1006;
	}
}

namespace Godot.Collections
{
	public class Array<T> : System.Collections.Generic.List<T> {}
}
