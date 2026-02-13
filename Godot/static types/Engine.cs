using System;

namespace Godot
{
	public static class Engine
	{
		public static Double GetFramesPerSecond() => 60.0;
		public static Int64 GetFramesDrawn() => (Int64)GetProcessFrames();
		public static UInt64 GetProcessFrames() => Time.SimulatedFrameCount;
		public static Boolean IsEditorHint() => false;
		public static MainLoop GetMainLoop() => SceneTree.Instance;
	}
}
