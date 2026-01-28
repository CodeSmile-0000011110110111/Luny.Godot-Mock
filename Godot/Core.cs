using System;

namespace Godot
{
	public static class GD
	{
		public static void Print(params Object[] args) => Console.WriteLine(String.Join(" ", args));
		public static void PrintRaw(params Object[] args) => Console.Write(String.Join(" ", args));
		public static void PrintRich(params Object[] args) => Console.WriteLine(String.Join(" ", args));
		public static void PrintS(params Object[] args) => Console.WriteLine(String.Join(" ", args));
		public static void PrintT(params Object[] args) => Console.WriteLine(String.Join("\t", args));
		public static void PrintErr(params Object[] args) => Console.Error.WriteLine(String.Join(" ", args));
		public static void PushError(params Object[] args) => Console.Error.WriteLine($"[ERROR] {String.Join(" ", args)}");
		public static void PushWarning(params Object[] args) => Console.WriteLine($"[WARNING] {String.Join(" ", args)}");
	}

	public static class Engine
	{
		public static Double GetFramesPerSecond() => 60.0;
		public static Int64 GetFramesDrawn() => (Int64)GetProcessFrames();
		public static UInt64 GetProcessFrames() => Time.SimulatedFrameCount;
		public static Boolean IsEditorHint() => false;
		public static MainLoop GetMainLoop() => SceneTree.Instance;
	}

	public static class Time
	{
		public static UInt64 SimulatedFrameCount { get; internal set; }
		public static UInt64 SimulatedTimeMsec { get; internal set; }

		public static UInt64 GetTicksMsec() => SimulatedTimeMsec;
		public static UInt64 GetTicksUsec() => SimulatedTimeMsec * 1000;
	}

	public class Mesh : GodotObject {}
	public class BoxMesh : Mesh {}
	public class SphereMesh : Mesh {}
	public class CapsuleMesh : Mesh {}
	public class CylinderMesh : Mesh {}
	public class PlaneMesh : Mesh {}
	public class QuadMesh : Mesh {}
}
