using Base;
using System.Linq;

namespace Raytracer.Elements
{
	public class LaserWhite : Laser
	{
		private Mesh mesh = new Mesh();

		private Ray[] rays;

		public LaserWhite()
		{
			Color = Color.White;
		}

		public static Vector2 range;

		public override void Update()
		{
			Vector2 direction = Vector2.Transform(Vector2.UnitX, quaternion);
			Vector2 start = Position + direction * Size.X * 0.51f;

			rays = new Ray[50];
			for (int i = 0; i < rays.Length; i++) rays[i] = new Ray(start, direction, 400f + 250f / rays.Length * i);
			range = new Vector2((float)rays.Min(ray => ray.wavelength), (float)rays.Max(ray => ray.wavelength));

			for (int i = 0; i < 25; i++)
			{
				foreach (Ray ray in rays) ray.Advance();
			}
		}

		public override void DrawRay()
		{
			if (rays != null)
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
}