using Base;

namespace Raytracer.Elements
{
	public class DirectionalLight : Laser
	{
		private double _wavelength;
		private Ray[] rays;

		public double Wavelength
		{
			get => _wavelength;
			set
			{
				_wavelength = value;
				Color = PhysicsUtility.GetColor(_wavelength);
			}
		}

		public DirectionalLight()
		{
			Wavelength = 600f;
		}

		public override void Update()
		{
			Vector2 direction = Vector2.Transform(Vector2.UnitX, quaternion);
			Vector2 start = Position + direction * Size.X * 0.51f;
			rays = new Ray[15];

			for (int i = 0; i < rays.Length; i++)
			{
				rays[i]=new Ray(start+new Vector2(0f, -_size.Y*0.5f + _size.Y*((float)i/rays.Length)), direction, Wavelength);
			}

			for (int i = 0; i < 20; i++)
			{
				foreach (Ray ray in rays) ray.Advance();
			}
		}

		public override void DrawRay()
		{
			foreach (Ray ray in rays)
			{
				for (int i = 0; i < ray.collisionPoints.Count - 1; i++)
				{
					Renderer2D.DrawLine(ray.collisionPoints[i + 1], ray.collisionPoints[i], ray.color, 2.5f);
				}
			}
		}
	}
}