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
}
