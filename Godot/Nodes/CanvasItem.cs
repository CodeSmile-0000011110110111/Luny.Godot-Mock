using System;

namespace Godot
{
	public class CanvasItem : Node
	{
		public Boolean Visible { get; set; } = true;
		public Boolean IsVisibleInTree() => Visible && (GetParent() is not CanvasItem p || p.IsVisibleInTree());
	}
}
