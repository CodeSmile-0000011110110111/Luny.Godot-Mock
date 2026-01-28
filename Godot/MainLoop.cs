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

	// stub to ensure 'partial' isn't removed by "Code Cleanup"
	public partial class MainLoop {}
}
