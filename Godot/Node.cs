using System;
using System.Collections.Generic;

namespace Godot
{
	public partial class Node : GodotObject
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

		public StringName Name { get; set; }
		public String SceneFilePath { get; set; }
		public ProcessModeEnum ProcessMode { get; set; }

		public virtual void _Ready() {}
		public virtual void _Process(Double delta) {}
		public virtual void _PhysicsProcess(Double delta) {}
		public override void _Notification(Int32 what) {}
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

			// If we are already inside tree, the new child must also enter tree
			if (_isInsideTree)
				node.SetInsideTree(true);
		}

		public Node GetParent() => _parent;

		public Array<Node> GetChildren()
		{
			var arr = new Array<Node>();
			arr.AddRange(_children);
			return arr;
		}

		public void SetInsideTree(Boolean value)
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

				SceneTree.Instance.OnNodeAdded(this);
			}
			else
			{
				_ExitTree();
				SceneTree.Instance.OnNodeRemoved(this);
			}

			// Propagate to children
			// Use a copy because SetInsideTree might trigger child removals/additions in complex scenarios
			// We MUST use GetChildren() or new List<Node>(_children)
			var childrenCopy = GetChildren();
			foreach (var child in childrenCopy)
				child.SetInsideTree(value);
		}
	}

	// stub to ensure 'partial' isn't removed by "Code Cleanup"
	public partial class Node {}

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

	public class Array<T> : List<T> {}
}
