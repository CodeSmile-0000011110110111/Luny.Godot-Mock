using System;
using System.Linq;

namespace Godot
{
	public class SceneTree : GodotObject
	{
		public static SceneTree Instance { get; } = new SceneTree();

		public Node Root { get; } = new Node { Name = "Root" };
		public Node CurrentScene { get; set; }

		public event Action<Node> NodeAdded;
		public event Action<Node> NodeRemoved;

		public SceneTree()
		{
			Root.SetInsideTree(true);
		}

		public void CallDeferred(string method, params object[] args)
		{
			var type = GetType();
			var mi = type.GetMethod(method);
			mi?.Invoke(this, args);
		}

		public Error ChangeSceneToFile(string path)
		{
			if (CurrentScene != null)
			{
				CurrentScene.QueueFree();
			}
			CurrentScene = new Node { Name = "NewScene", SceneFilePath = path };
			Root.AddChild(CurrentScene);
			return Error.Ok;
		}

		public Error ReloadCurrentScene()
		{
			if (CurrentScene == null) return Error.Failed;
			return ChangeSceneToFile(CurrentScene.SceneFilePath);
		}
	}

	public enum Error
	{
		Ok = 0,
		Failed = 1,
	}
}
