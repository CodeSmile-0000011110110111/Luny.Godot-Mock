using System;

namespace Godot
{
	public class SceneTree : GodotObject
	{
		public Node Root => throw new NotImplementedException("Godot.SceneTree.Root");
		public Node CurrentScene { get; set; }

		public event Action<Node> NodeAdded;
		public event Action<Node> NodeRemoved;

		public void CallDeferred(string method, params object[] args) => throw new NotImplementedException("Godot.SceneTree.CallDeferred");
	}
}
