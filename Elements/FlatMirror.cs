using Base;

namespace Raytracer.Elements
{
	public class FlatMirror : BaseElement
	{
		public FlatMirror()
		{
			Size = new Vector2(10f, 200f);
		}

		// public override Vector2 GetTransformation(float initial, float final) => new Vector2(0f, -1f);

		public override float GetAngle(float incoming, float initial, float final) => -incoming;
	}
}