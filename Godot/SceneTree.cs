using System;
using System.Linq;

namespace Godot
{
	public class SceneTree : GodotObject
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
			
			// Initialize with a default scene for unit tests
			CurrentScene = new Node { Name = "DefaultScene", SceneFilePath = "res://default.tscn" };
			Root.AddChild(CurrentScene);
		}

		public void CallDeferred(String method, params Object[] args)
		{
			var type = GetType();
			var mi = type.GetMethod(method);
			mi?.Invoke(this, args);
		}

		public static void ForceReset_UnitTestsOnly() => Instance = new();

		public void Reset_UnitTestsOnly()
		{
			Root.GetChildren().ToList().ForEach(c => c.QueueFree());
			CurrentScene = null;
		}

		public Error ChangeSceneToFile(String path)
		{
			if (CurrentScene != null)
				CurrentScene.QueueFree();
			CurrentScene = new Node { Name = "NewScene", SceneFilePath = path };
			Root.AddChild(CurrentScene);
			return Error.Ok;
		}

		public Error ReloadCurrentScene()
		{
			if (CurrentScene == null)
				return Error.Failed;

			return ChangeSceneToFile(CurrentScene.SceneFilePath);
		}
	}

	public enum Error
	{
		Ok = 0,
		Failed = 1,
	}
}
