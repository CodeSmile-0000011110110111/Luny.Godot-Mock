using System;

namespace Godot
{
	public struct Vector2 : IEquatable<Vector2>
	{
		private System.Numerics.Vector2 _value;

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

		public Single this[Int32 index]
		{
			get => index switch
			{
				0 => X,
				1 => Y,
				var _ => throw new ArgumentOutOfRangeException(nameof(index)),
			};
			set
			{
				switch (index)
				{
					case 0: X = value; break;
					case 1: Y = value; break;
					default: throw new ArgumentOutOfRangeException(nameof(index));
				}
			}
		}

		public Vector2(Single x, Single y) => _value = new System.Numerics.Vector2(x, y);

		public static readonly Vector2 Zero = new(0, 0);
		public static readonly Vector2 One = new(1, 1);
		public static readonly Vector2 Up = new(0, -1);
		public static readonly Vector2 Down = new(0, 1);
		public static readonly Vector2 Left = new(-1, 0);
		public static readonly Vector2 Right = new(1, 0);
		public static readonly Vector2 Inf = new(Single.PositiveInfinity, Single.PositiveInfinity);

		public Single Length() => _value.Length();
		public Single LengthSquared() => _value.LengthSquared();

		public Vector2 Normalized()
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
				_value = System.Numerics.Vector2.Zero;
		}

		public Single Dot(Vector2 with) => System.Numerics.Vector2.Dot(_value, with._value);
		public Single DistanceTo(Vector2 to) => System.Numerics.Vector2.Distance(_value, to._value);
		public Single DistanceSquaredTo(Vector2 to) => System.Numerics.Vector2.DistanceSquared(_value, to._value);

		public Vector2 Lerp(Vector2 to, Single weight) =>
			FromNumerics(System.Numerics.Vector2.Lerp(_value, to._value, weight));

		public Vector2 Clamp(Vector2 min, Vector2 max) =>
			FromNumerics(System.Numerics.Vector2.Clamp(_value, min._value, max._value));

		public Vector2 Reflect(Vector2 normal) => FromNumerics(System.Numerics.Vector2.Reflect(_value, normal._value));
		public Vector2 Abs() => FromNumerics(System.Numerics.Vector2.Abs(_value));

		public static Vector2 operator +(Vector2 a, Vector2 b) => FromNumerics(a._value + b._value);
		public static Vector2 operator -(Vector2 a, Vector2 b) => FromNumerics(a._value - b._value);
		public static Vector2 operator -(Vector2 a) => FromNumerics(-a._value);
		public static Vector2 operator *(Vector2 a, Single d) => FromNumerics(a._value * d);
		public static Vector2 operator *(Single d, Vector2 a) => FromNumerics(a._value * d);
		public static Vector2 operator *(Vector2 a, Vector2 b) => FromNumerics(a._value * b._value);
		public static Vector2 operator /(Vector2 a, Single d) => FromNumerics(a._value / d);
		public static Vector2 operator /(Vector2 a, Vector2 b) => FromNumerics(a._value / b._value);

		public static Boolean operator ==(Vector2 lhs, Vector2 rhs) => lhs._value.Equals(rhs._value);
		public static Boolean operator !=(Vector2 lhs, Vector2 rhs) => !lhs._value.Equals(rhs._value);

		public Boolean Equals(Vector2 other) => _value.Equals(other._value);
		public override Boolean Equals(Object obj) => obj is Vector2 other && Equals(other);
		public override Int32 GetHashCode() => _value.GetHashCode();
		public override String ToString() => $"({X}, {Y})";

		private static Vector2 FromNumerics(System.Numerics.Vector2 v)
		{
			var result = new Vector2();
			result._value = v;
			return result;
		}
	}
}
