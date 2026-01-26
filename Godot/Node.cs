using Godot.Collections;
using System;
using System.Collections.Generic;

namespace Godot
{
	public class Node : GodotObject
	{
		public enum ProcessModeEnum
		{
			Inherit,
			Pausable,
			WhenPaused,
			Always,
			Disabled,
		}

		public enum InternalMode
		{
			Disabled,
			Front,
			Back,
		}

		public const Int32 NotificationCrash = 1012;
		public const Int32 NotificationWMCloseRequest = 1006;
		private readonly List<Node> _children = new();
		private Node _parent;
		private Boolean _isInsideTree;

		internal Boolean _readyCalled;

		public String Name { get; set; }
		public String SceneFilePath { get; set; }
		public ProcessModeEnum ProcessMode { get; set; }

		public virtual void _Ready() {}
		public virtual void _Process(Double delta) {}
		public virtual void _PhysicsProcess(Double delta) {}
		public virtual void _Notification(Int32 what) {}
		public virtual void _ExitTree() {}

		public void QueueFree()
		{
			_parent?._children.Remove(this);
			Dispose();
		}

		public Boolean IsInsideTree() => _isInsideTree;

		public Boolean CanProcess()
		{
			if (ProcessMode == ProcessModeEnum.Disabled)
				return false;
			if (ProcessMode == ProcessModeEnum.Always)
				return true;

			// Simplification: assume always pausable/inherit
			return _parent == null || _parent.CanProcess();
		}

		public void PropagateNotification(Int32 what)
		{
			_Notification(what);
			foreach (var child in _children)
				child.PropagateNotification(what);
		}

		public void AddChild(Node node, Boolean forceReadableName = false, InternalMode internal_ = InternalMode.Disabled)
		{
			if (node._parent != null)
				node._parent._children.Remove(node);
			node._parent = this;
			_children.Add(node);
			node.SetInsideTree(_isInsideTree);
		}

		public Node GetParent() => _parent;

		public Array<Node> GetChildren()
		{
			var arr = new Array<Node>();
			arr.AddRange(_children);
			return arr;
		}

		public void CallDeferred(String method, params Object[] args)
		{
			// Immediate call in mock for simplicity, or we could queue it
			var type = GetType();
			var mi = type.GetMethod(method);
			mi?.Invoke(this, args);
		}

		internal void SetInsideTree(Boolean value)
		{
			if (_isInsideTree == value)
				return;

			_isInsideTree = value;
			if (_isInsideTree)
			{
				if (!_readyCalled)
				{
					_readyCalled = true;
					_Ready();
				}
			}
			else
			{
				_ExitTree();
				// If we leave the tree, we should be ready again if we re-enter?
				// Godot behavior: _Ready is only called ONCE unless requested otherwise, 
				// but let's keep it simple.
			}

			// Use a copy to avoid modification during iteration
			var children = new List<Node>(_children);
			foreach (var child in children)
				child.SetInsideTree(value);
		}
	}

	public class Node3D : Node
	{
		public Vector3 Position { get; set; }
		public Vector3 Rotation { get; set; }
		public Vector3 Scale { get; set; } = Vector3.One;
		public Boolean Visible { get; set; } = true;

		public Boolean IsVisibleInTree() => Visible && (GetParent() is not Node3D p || p.IsVisibleInTree());
	}

	public class CanvasItem : Node
	{
		public Boolean Visible { get; set; } = true;
		public Boolean IsVisibleInTree() => Visible && (GetParent() is not CanvasItem p || p.IsVisibleInTree());
	}

	public class MeshInstance3D : Node3D
	{
		public Mesh Mesh { get; set; }
	}

	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
	public class ExportAttribute : Attribute {}

	public class CanvasLayer : Node
	{
		public Boolean Visible { get; set; } = true;
	}

	public struct Vector3
	{
		public Single X, Y, Z;

		public Vector3(Single x, Single y, Single z)
		{
			X = x;
			Y = y;
			Z = z;
		}

		public static Vector3 Zero => new(0, 0, 0);
		public static Vector3 One => new(1, 1, 1);
	}
}

namespace Godot.Collections
{
	public class Array<T> : List<T> {}
}
