using System;

namespace Godot
{
	public static class GD
	{
		public static void Print(params object[] args) => Console.WriteLine(string.Join(" ", args));
		public static void PrintRaw(params object[] args) => Console.Write(string.Join(" ", args));
		public static void PrintRich(params object[] args) => Console.WriteLine(string.Join(" ", args));
		public static void PrintS(params object[] args) => Console.WriteLine(string.Join(" ", args));
		public static void PrintT(params object[] args) => Console.WriteLine(string.Join("\t", args));
		public static void PrintErr(params object[] args) => Console.Error.WriteLine(string.Join(" ", args));
		public static void PushError(params object[] args) => Console.Error.WriteLine($"[ERROR] {string.Join(" ", args)}");
		public static void PushWarning(params object[] args) => Console.WriteLine($"[WARNING] {string.Join(" ", args)}");
	}

	public static class Engine
	{
		public static double GetFramesPerSecond() => throw new NotImplementedException("Godot.Engine.GetFramesPerSecond");
		public static long GetFramesDrawn() => throw new NotImplementedException("Godot.Engine.GetFramesDrawn");
		public static ulong GetProcessFrames() => throw new NotImplementedException("Godot.Engine.GetProcessFrames");
		public static bool IsEditorHint() => throw new NotImplementedException("Godot.Engine.IsEditorHint");
		public static SceneTree GetMainLoop() => throw new NotImplementedException("Godot.Engine.GetMainLoop");
	}

	public static class Time
	{
		public static ulong GetTicksMsec() => throw new NotImplementedException("Godot.Time.GetTicksMsec");
		public static ulong GetTicksUsec() => throw new NotImplementedException("Godot.Time.GetTicksUsec");
	}

	public class MeshInstance3D : Node3D
	{
		public Mesh Mesh { get; set; }
	}

	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
	public class ExportAttribute : Attribute {}

	public class Mesh : GodotObject {}
	public class BoxMesh : Mesh {}
	public class SphereMesh : Mesh {}
	public class CapsuleMesh : Mesh {}
	public class CylinderMesh : Mesh {}
	public class PlaneMesh : Mesh {}
	public class QuadMesh : Mesh {}
}
