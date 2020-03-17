using Base;

namespace Raytracer.Elements
{
	public abstract class Laser : BaseElement
	{
		public const float MaxDistance = 1000000f;

		public override bool BlocksRays => true;

		public Laser() => Size = new Vector2(100f, 20f);

		public virtual void DrawRay()
		{
		}

		public override void Draw()
		{
			Color color = Color.Lerp(Color, Color * 0.7f, selected ? Utility.UnsignedSin(Time.TotalDrawTime * 3f) : 0f);
			Renderer2D.DrawQuad(Position, Size, color, quaternion);
		}

		public static void HandleRefractiveIndices(BaseElement lastCollided, BaseElement element, double wavelength, out float initial, out float final)
		{
			wavelength *= 0.001;
			float rIndex = (float)PhysicsUtility.GetRefractiveIndex(element.material, wavelength);

			if (lastCollided == element)
			{
				initial = rIndex;
				final = 1.000293f;
			}
			else
			{
				initial = 1.000293f;
				final = rIndex;
			}
		}
	}
}