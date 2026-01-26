using System;

namespace Godot
{
	public static class GD
	{
		public static void Print(params object[] args) => throw new NotImplementedException("Godot.GD.Print");
		public static void PrintRaw(params object[] args) => throw new NotImplementedException("Godot.GD.PrintRaw");
		public static void PrintRich(params object[] args) => throw new NotImplementedException("Godot.GD.PrintRich");
		public static void PrintS(params object[] args) => throw new NotImplementedException("Godot.GD.PrintS");
		public static void PrintT(params object[] args) => throw new NotImplementedException("Godot.GD.PrintT");
		public static void PrintErr(params object[] args) => throw new NotImplementedException("Godot.GD.PrintErr");
		public static void PushError(params object[] args) => throw new NotImplementedException("Godot.GD.PushError");
		public static void PushWarning(params object[] args) => throw new NotImplementedException("Godot.GD.PushWarning");
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
