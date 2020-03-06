using UnityEngine;

namespace Tracer
{
	public abstract class BaseElement : Moveable
	{
		public abstract Matrix4x4 Transform { get; }

		public float RefractiveIndex = 1;

		[HideInInspector]
		public float Initial;

		[HideInInspector]
		public float Final;
	}
}