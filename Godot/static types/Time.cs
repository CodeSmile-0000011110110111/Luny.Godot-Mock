using System;

namespace Godot
{
	public static class Time
	{
		public static UInt64 SimulatedFrameCount { get; internal set; }
		public static UInt64 SimulatedTimeMsec { get; internal set; }

		public static UInt64 GetTicksMsec() => SimulatedTimeMsec;
		public static UInt64 GetTicksUsec() => SimulatedTimeMsec * 1000;
	}
}
