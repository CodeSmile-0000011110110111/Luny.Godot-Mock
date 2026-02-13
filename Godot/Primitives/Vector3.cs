using System;

namespace Godot
{
	public struct Vector3
	{
		public Single X, Y, Z;

		public Vector3(Single x, Single y, Single z)
		{
			X = x;
			Y = y;
			Z = z;
		}

		public static Vector3 Zero => new(0, 0, 0);
		public static Vector3 One => new(1, 1, 1);
	}
}
