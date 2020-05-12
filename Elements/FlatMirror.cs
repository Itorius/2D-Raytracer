using Base;

namespace Raytracer.Elements
{
	public abstract class Mirror : BaseElement
	{
		public override float GetAngle(float incoming, float initial, float final) => -incoming;
	}

	public class FlatMirror : Mirror
	{
		public FlatMirror()
		{
			Size = new Vector2(10f, 200f);
		}
	}
}