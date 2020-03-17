using Base;

namespace Raytracer.Elements
{
	public class LaserMonochrome : Laser
	{
		private double _wavelength;
		private Ray ray;

		public double Wavelength
		{
			get => _wavelength;
			set
			{
				_wavelength = value;
				Color = RgbCalculator.Calc(_wavelength);
			}
		}

		public LaserMonochrome()
		{
			Wavelength = 600f;
		}
		
		public override void Update()
		{
			Vector2 direction = Vector2.Transform(Vector2.UnitX, quaternion);
			Vector2 start = Position + direction * Size.X * 0.51f;
			ray = new Ray(start, direction, Wavelength);

			for (int i = 0; i < 10; i++) ray.Advance();
		}

		public override void DrawRay()
		{
			if (ray != null)
			{
				for (int i = 0; i < ray.collisionPoints.Count - 1; i++)
				{
					Renderer2D.DrawLine(ray.collisionPoints[i + 1], ray.collisionPoints[i], ray.color, 2.5f);
				}
			}
		}
	}
}