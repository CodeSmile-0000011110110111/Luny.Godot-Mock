using System;

namespace Godot
{
	public class Node3D : Node
	{
		public Vector3 Position { get; set; }
		public Vector3 Rotation { get; set; }
		public Vector3 Scale { get; set; } = Vector3.One;
		public Boolean Visible { get; set; } = true;

		public Boolean IsVisibleInTree() => Visible && (GetParent() is not Node3D p || p.IsVisibleInTree());
	}
}
