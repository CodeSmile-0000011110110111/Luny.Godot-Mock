using System;

namespace Godot
{
	public struct Quaternion : IEquatable<Quaternion>
	{
		private System.Numerics.Quaternion _value;

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

		public Single W
		{
			get => _value.W;
			set => _value.W = value;
		}

		public Single this[Int32 index]
		{
			get => index switch
			{
				0 => X,
				1 => Y,
				2 => Z,
				3 => W,
				var _ => throw new ArgumentOutOfRangeException(nameof(index)),
			};
			set
			{
				switch (index)
				{
					case 0: X = value; break;
					case 1: Y = value; break;
					case 2: Z = value; break;
					case 3: W = value; break;
					default: throw new ArgumentOutOfRangeException(nameof(index));
				}
			}
		}

		public Quaternion(Single x, Single y, Single z, Single w) => _value = new System.Numerics.Quaternion(x, y, z, w);

		public static readonly Quaternion Identity = new(0, 0, 0, 1);

		public Single Length() => MathF.Sqrt(X * X + Y * Y + Z * Z + W * W);
		public Single LengthSquared() => X * X + Y * Y + Z * Z + W * W;

		public Quaternion Normalized() => FromNumerics(System.Numerics.Quaternion.Normalize(_value));
		public void Normalize() => _value = System.Numerics.Quaternion.Normalize(_value);

		public Single Dot(Quaternion with) => System.Numerics.Quaternion.Dot(_value, with._value);
		public Quaternion Inverse() => FromNumerics(System.Numerics.Quaternion.Inverse(_value));

		public Quaternion Slerp(Quaternion to, Single weight) =>
			FromNumerics(System.Numerics.Quaternion.Slerp(_value, to._value, weight));

		public static Quaternion operator *(Quaternion lhs, Quaternion rhs) =>
			FromNumerics(System.Numerics.Quaternion.Multiply(lhs._value, rhs._value));

		public static Vector3 operator *(Quaternion q, Vector3 v)
		{
			var qn = q._value;
			var u = new System.Numerics.Vector3(qn.X, qn.Y, qn.Z);
			var s = qn.W;
			var p = new System.Numerics.Vector3(v.X, v.Y, v.Z);
			var result = 2f * System.Numerics.Vector3.Dot(u, p) * u
				+ (s * s - System.Numerics.Vector3.Dot(u, u)) * p
				+ 2f * s * System.Numerics.Vector3.Cross(u, p);
			return new Vector3(result.X, result.Y, result.Z);
		}

		public static Boolean operator ==(Quaternion lhs, Quaternion rhs) => lhs._value.Equals(rhs._value);
		public static Boolean operator !=(Quaternion lhs, Quaternion rhs) => !lhs._value.Equals(rhs._value);

		public Boolean Equals(Quaternion other) => _value.Equals(other._value);
		public override Boolean Equals(Object obj) => obj is Quaternion other && Equals(other);
		public override Int32 GetHashCode() => _value.GetHashCode();
		public override String ToString() => $"({X}, {Y}, {Z}, {W})";

		private static Quaternion FromNumerics(System.Numerics.Quaternion v)
		{
			var result = new Quaternion();
			result._value = v;
			return result;
		}
	}
}
