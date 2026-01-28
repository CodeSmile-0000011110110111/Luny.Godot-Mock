using System;

namespace Godot
{
	public partial class SceneTree : MainLoop
	{
		public event Action<Node> NodeAdded;
		public event Action<Node> NodeRemoved;

		public static SceneTree Instance { get; private set; } = new();

		public Node Root { get; } = new() { Name = "Root" };
		public Node CurrentScene { get; set; }

		public SceneTree()
		{
			Instance = this;
			Root.SetInsideTree(true);

			// Initialize with a default scene
			ChangeSceneToFile("res://MainScene.tscn");
		}

		public Error ChangeSceneToFile(String path)
		{
			// In Godot, ChangeSceneToFile happens at the end of the frame
			if (CurrentScene != null)
			{
				CurrentScene.QueueFree();
				CurrentScene = null;
			}

			CurrentScene = new Node { Name = path.Replace("res://", ""), SceneFilePath = path };

			// We MUST add it to Root so that OnNativeSceneLoaded works correctly.
			// When AddChild is called, SetInsideTree(true) will be called on CurrentScene,
			// which in turn will call SceneTree.Instance.OnNodeAdded(CurrentScene).
			Root.AddChild(CurrentScene);

			return Error.Ok;
		}

		public void ReloadCurrentScene()
		{
			if (CurrentScene == null)
				return; // No scene to reload

			ChangeSceneToFile(CurrentScene.SceneFilePath);
		}

		internal void OnNodeAdded(Node node) => NodeAdded?.Invoke(node);
		internal void OnNodeRemoved(Node node) => NodeRemoved?.Invoke(node);

		public void CallDeferred(String method, params Object[] args)
		{
			var type = GetType();
			var mi = type.GetMethod(method);
			mi?.Invoke(this, args);
		}
	}

	// stub to ensure 'partial' isn't removed by "Code Cleanup"
	public partial class SceneTree {}

	public enum Error
	{
		Ok = 0,
		Failed = 1,
	}
}
