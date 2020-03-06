using UnityEngine;

namespace Tracer
{
	public class FlatRefractor : BaseElement
	{
		public override Matrix4x4 Transform => new Matrix4x4(new Vector4(1, 0, 0, 0), new Vector4(0, Initial / Final, 0, 0), Vector4.zero, Vector4.zero);

		public FlatRefractor()
		{
			RefractiveIndex = Constants.RefractiveIndexes.Glass;
		}
	}
}