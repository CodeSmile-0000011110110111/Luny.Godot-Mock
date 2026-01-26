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
		public static double GetFramesPerSecond() => 60.0;
		public static long GetFramesDrawn() => (long)GetProcessFrames();
		public static ulong GetProcessFrames() => Time.SimulatedFrameCount;
		public static bool IsEditorHint() => false;
		public static SceneTree GetMainLoop() => throw new NotImplementedException("Godot.Engine.GetMainLoop");
	}

	public static class Time
	{
		public static ulong SimulatedFrameCount { get; internal set; }
		public static ulong SimulatedTimeMsec { get; internal set; }

		public static ulong GetTicksMsec() => SimulatedTimeMsec;
		public static ulong GetTicksUsec() => SimulatedTimeMsec * 1000;
	}

	public class Mesh : GodotObject { }
	public class BoxMesh : Mesh { }
	public class SphereMesh : Mesh { }
	public class CapsuleMesh : Mesh { }
	public class CylinderMesh : Mesh { }
	public class PlaneMesh : Mesh { }
	public class QuadMesh : Mesh { }
}
