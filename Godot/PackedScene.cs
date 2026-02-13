using System;

namespace Godot
{
	public class PackedScene : Resource
	{
		private Node _bundled;

		public Error Pack(Node path)
		{
			_bundled = path?.Duplicate();
			return Error.Ok;
		}

		public Boolean CanInstantiate() => _bundled != null;

		public Node Instantiate(Node.InternalMode internalMode = Node.InternalMode.Disabled)
		{
			if (_bundled == null)
				return null;

			return _bundled.Duplicate();
		}
	}
}
