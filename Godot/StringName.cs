using System;

namespace Godot
{
	public partial struct StringName : IEquatable<StringName>
	{
		private readonly String _value;

		public StringName(String name) => _value = name ?? String.Empty;

		public static implicit operator StringName(String s) => new(s);
		public static implicit operator String(StringName s) => s._value ?? String.Empty;

		public override String ToString() => _value ?? String.Empty;

		public Boolean Equals(StringName other) => _value == other._value;
		public override Boolean Equals(Object obj) => obj is StringName other && Equals(other);
		public override Int32 GetHashCode() => _value?.GetHashCode() ?? 0;

		public static Boolean operator ==(StringName left, StringName right) => left.Equals(right);
		public static Boolean operator !=(StringName left, StringName right) => !left.Equals(right);
	}
}
