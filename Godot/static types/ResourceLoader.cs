using System;
using System.Collections.Generic;

namespace Godot
{
	public static class ResourceLoader
	{
		public enum CacheMode
		{
			None,
			Reuse,
			Replace,
			Ignore,
		}

		private static readonly Dictionary<String, Resource> _loadedResources = new();

		public static Resource Load(String path, String typeHint = "", CacheMode cacheMode = CacheMode.Reuse)
		{
			if (_loadedResources.TryGetValue(path, out var res))
				return res;

			// Mock: if path contains "Prefab", return a PackedScene
			if (path.Contains("Prefab") || path.EndsWith(".tscn") || path.EndsWith(".scn"))
			{
				var scene = new PackedScene();
				scene.Pack(new Node3D { Name = "DefaultMockPrefabRoot" });
				_loadedResources[path] = scene;
				return scene;
			}

			return null;
		}

		public static T Load<T>(String path, String typeHint = "", CacheMode cacheMode = CacheMode.Reuse) where T : class =>
			Load(path, typeHint, cacheMode) as T;

		internal static void Reset_UnitTestsOnly() => _loadedResources.Clear();
	}
}
