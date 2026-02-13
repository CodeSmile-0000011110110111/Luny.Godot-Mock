using System;

namespace Godot
{
	public partial class MainLoop : GodotObject
	{
		public virtual void _Initialize() {}
		public virtual void _Iteration(Double delta) {}
		public virtual void _Idle(Double delta) {}
		public virtual void _Finalize() {}
	}

	// ensures Rider's "Code Cleanup" won't remove the required 'partial' keyword
	public partial class MainLoop {}
}
