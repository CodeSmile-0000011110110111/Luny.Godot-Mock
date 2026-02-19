using System;

namespace Godot
{
	public partial class Node3D : Node
	{
		public Vector3 Position { get; set; }
		public Vector3 Rotation { get; set; }
		public Quaternion Quaternion { get; set; } = Quaternion.Identity;
		public Vector3 Scale { get; set; } = Vector3.One;
		public Boolean Visible { get; set; } = true;

		public Vector3 GlobalPosition
		{
			get
			{
				var parent = GetParent() as Node3D;
				return parent == null ? Position : parent.GlobalPosition + Position;
			}
			set
			{
				var parent = GetParent() as Node3D;
				Position = parent == null ? value : value - parent.GlobalPosition;
			}
		}

		public Vector3 GlobalRotation
		{
			get
			{
				var parent = GetParent() as Node3D;
				return parent == null ? Rotation : parent.GlobalRotation + Rotation;
			}
			set
			{
				var parent = GetParent() as Node3D;
				Rotation = parent == null ? value : value - parent.GlobalRotation;
			}
		}

		public Vector3 GlobalScale
		{
			get
			{
				var parent = GetParent() as Node3D;
				return parent == null ? Scale : new Vector3(parent.GlobalScale.X * Scale.X, parent.GlobalScale.Y * Scale.Y, parent.GlobalScale.Z * Scale.Z);
			}
		}

		public Boolean IsVisibleInTree() => Visible && (GetParent() is not Node3D p || p.IsVisibleInTree());
	}

	// stub to preserve 'partial' keyword
	public partial class Node3D {}
}
