using Base;

namespace Raytracer.Elements
{
	public class Blocker : BaseElement
	{
		public override bool BlocksRays => true;

		public Blocker()
		{
			Color = new Color(240, 240, 240, 255);
			ColorSelected = new Color(170, 170, 170, 255);

			Size = new Vector2(100f, 100f);
		}

		// public override Vector2 GetTransformation(float initial, float final) => Vector2.Zero;
	}
}