using System;

namespace Godot
{
	public struct Vector3 : IEquatable<Vector3>
	{
		private System.Numerics.Vector3 _value;

		public Single X
		{
			get => _value.X;
			set => _value.X = value;
		}

		public Single Y
		{
			get => _value.Y;
			set => _value.Y = value;
		}

		public Single Z
		{
			get => _value.Z;
			set => _value.Z = value;
		}

		public Single this[Int32 index]
		{
			get => index switch
			{
				0 => X,
				1 => Y,
				2 => Z,
				var _ => throw new ArgumentOutOfRangeException(nameof(index)),
			};
			set
			{
				switch (index)
				{
					case 0: X = value; break;
					case 1: Y = value; break;
					case 2: Z = value; break;
					default: throw new ArgumentOutOfRangeException(nameof(index));
				}
			}
		}

		public Vector3(Single x, Single y, Single z) => _value = new System.Numerics.Vector3(x, y, z);

		public static readonly Vector3 Zero = new(0, 0, 0);
		public static readonly Vector3 One = new(1, 1, 1);
		public static readonly Vector3 Up = new(0, 1, 0);
		public static readonly Vector3 Down = new(0, -1, 0);
		public static readonly Vector3 Left = new(-1, 0, 0);
		public static readonly Vector3 Right = new(1, 0, 0);
		public static readonly Vector3 Forward = new(0, 0, -1);
		public static readonly Vector3 Back = new(0, 0, 1);
		public static readonly Vector3 Inf = new(Single.PositiveInfinity, Single.PositiveInfinity, Single.PositiveInfinity);

		public Single Length() => _value.Length();
		public Single LengthSquared() => _value.LengthSquared();

		public Vector3 Normalized()
		{
			var len = _value.Length();
			return len > 1e-05f ? FromNumerics(_value / len) : Zero;
		}

		public void Normalize()
		{
			var len = _value.Length();
			if (len > 1e-05f)
				_value /= len;
			else
				_value = System.Numerics.Vector3.Zero;
		}

		public Single Dot(Vector3 with) => System.Numerics.Vector3.Dot(_value, with._value);
		public Vector3 Cross(Vector3 with) => FromNumerics(System.Numerics.Vector3.Cross(_value, with._value));
		public Single DistanceTo(Vector3 to) => System.Numerics.Vector3.Distance(_value, to._value);
		public Single DistanceSquaredTo(Vector3 to) => System.Numerics.Vector3.DistanceSquared(_value, to._value);

		public Vector3 Lerp(Vector3 to, Single weight) =>
			FromNumerics(System.Numerics.Vector3.Lerp(_value, to._value, weight));

		public Vector3 Clamp(Vector3 min, Vector3 max) =>
			FromNumerics(System.Numerics.Vector3.Clamp(_value, min._value, max._value));

		public Vector3 Reflect(Vector3 normal) => FromNumerics(System.Numerics.Vector3.Reflect(_value, normal._value));
		public Vector3 Abs() => FromNumerics(System.Numerics.Vector3.Abs(_value));

		public static Vector3 operator +(Vector3 a, Vector3 b) => FromNumerics(a._value + b._value);
		public static Vector3 operator -(Vector3 a, Vector3 b) => FromNumerics(a._value - b._value);
		public static Vector3 operator -(Vector3 a) => FromNumerics(-a._value);
		public static Vector3 operator *(Vector3 a, Single d) => FromNumerics(a._value * d);
		public static Vector3 operator *(Single d, Vector3 a) => FromNumerics(a._value * d);
		public static Vector3 operator *(Vector3 a, Vector3 b) => FromNumerics(a._value * b._value);
		public static Vector3 operator /(Vector3 a, Single d) => FromNumerics(a._value / d);
		public static Vector3 operator /(Vector3 a, Vector3 b) => FromNumerics(a._value / b._value);

		public static Boolean operator ==(Vector3 lhs, Vector3 rhs) => lhs._value.Equals(rhs._value);
		public static Boolean operator !=(Vector3 lhs, Vector3 rhs) => !lhs._value.Equals(rhs._value);

		public Boolean Equals(Vector3 other) => _value.Equals(other._value);
		public override Boolean Equals(Object obj) => obj is Vector3 other && Equals(other);
		public override Int32 GetHashCode() => _value.GetHashCode();
		public override String ToString() => $"({X}, {Y}, {Z})";

		private static Vector3 FromNumerics(System.Numerics.Vector3 v)
		{
			var result = new Vector3();
			result._value = v;
			return result;
		}
	}
}
