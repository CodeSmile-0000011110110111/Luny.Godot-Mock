using System;
using System.Collections.Generic;
using System.Linq;

namespace Godot
{
	public class Node : GodotObject
	{
		private Node _parent;
		private readonly List<Node> _children = new();
		private bool _isInsideTree;

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

		public virtual void _Ready() { }
		public virtual void _Process(double delta) { }
		public virtual void _PhysicsProcess(double delta) { }
		public virtual void _Notification(int what) { }
		public virtual void _ExitTree() { }

		public void QueueFree() => Dispose();

		public bool IsInsideTree() => _isInsideTree;

		public bool CanProcess()
		{
			if (ProcessMode == ProcessModeEnum.Disabled) return false;
			if (ProcessMode == ProcessModeEnum.Always) return true;
			// Simplification: assume always pausable/inherit
			return _parent == null || _parent.CanProcess();
		}

		public void PropagateNotification(int what)
		{
			_Notification(what);
			foreach (var child in _children)
			{
				child.PropagateNotification(what);
			}
		}

		public void AddChild(Node node, bool forceReadableName = false, InternalMode internal_ = InternalMode.Disabled)
		{
			if (node._parent != null)
			{
				node._parent._children.Remove(node);
			}
			node._parent = this;
			_children.Add(node);
			node._isInsideTree = this._isInsideTree;
			if (node._isInsideTree)
			{
				node._Ready();
			}
		}

		public Node GetParent() => _parent;

		public Godot.Collections.Array<Node> GetChildren()
		{
			var arr = new Godot.Collections.Array<Node>();
			arr.AddRange(_children);
			return arr;
		}

		public void CallDeferred(string method, params object[] args)
		{
			// Immediate call in mock for simplicity, or we could queue it
			var type = GetType();
			var mi = type.GetMethod(method);
			mi?.Invoke(this, args);
		}

		internal void SetInsideTree(bool value)
		{
			_isInsideTree = value;
			if (_isInsideTree) _Ready();
			foreach (var child in _children) child.SetInsideTree(value);
		}

		public enum InternalMode
		{
			Disabled,
			Front,
			Back,
		}

		public const int NotificationCrash = 1012;
		public const int NotificationWMCloseRequest = 1006;
	}

	public class Node3D : Node
	{
		public Vector3 Position { get; set; }
		public Vector3 Rotation { get; set; }
		public Vector3 Scale { get; set; } = Vector3.One;
		public bool Visible { get; set; } = true;

		public bool IsVisibleInTree() => Visible && (GetParent() is not Node3D p || p.IsVisibleInTree());
	}

	public class CanvasItem : Node
	{
		public bool Visible { get; set; } = true;
		public bool IsVisibleInTree() => Visible && (GetParent() is not CanvasItem p || p.IsVisibleInTree());
	}

	public class MeshInstance3D : Node3D
	{
		public Mesh Mesh { get; set; }
	}

	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
	public class ExportAttribute : Attribute { }

	public class CanvasLayer : Node
	{
		public bool Visible { get; set; } = true;
	}

	public struct Vector3
	{
		public float X, Y, Z;
		public Vector3(float x, float y, float z) { X = x; Y = y; Z = z; }
		public static Vector3 Zero => new(0, 0, 0);
		public static Vector3 One => new(1, 1, 1);
	}
}

namespace Godot.Collections
{
	public class Array<T> : System.Collections.Generic.List<T> {}
}
