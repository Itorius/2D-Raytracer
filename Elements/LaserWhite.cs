using Base;
using System.Collections.Generic;
using System.Linq;

namespace Raytracer.Elements
{
	public class Ray
	{
		private Vector2 start;
		private Vector2 direction;
		public readonly double wavelength;
		public readonly List<Vector2> collisionPoints;
		public Color color;

		public Ray(Vector2 start, Vector2 direction, double wavelength)
		{
			this.start = start;
			this.direction = direction;
			this.wavelength = wavelength;
			color = RgbCalculator.Calc(wavelength);

			collisionPoints = new List<Vector2> { start };
		}

		private BaseElement lastCollided;
		public bool canAdvance = true;

		public void Advance()
		{
			if (canAdvance)
			{
				if (Raycast.CastRay(start, direction, out RaycastInfo info, Laser.MaxDistance))
				{
					BaseElement element = info.element;

					Laser.HandleRefractiveIndices(lastCollided, element, wavelength, out float initial, out float final);
					lastCollided = element;

					start = info.point + direction * 0.01f;
					collisionPoints.Add(start);

					if (element.BlocksRays)
					{
						canAdvance = false;
						return;
					}

					float angle = Vector2.SignedAngle(info.normal, -direction);

					float scale = 1f + (1f - (float)((wavelength - LaserWhite.range.X) / (LaserWhite.range.Y - LaserWhite.range.X))) * 0.1f;
					float outAngle = element.GetAngle(angle, initial, final) / scale;
					direction = Vector2.Transform(info.normal, Quaternion.FromAxisAngle(Vector3.UnitZ, outAngle)) * (info.element is FlatMirror ? 1 : -1);
				}
				else
				{
					canAdvance = false;
					collisionPoints.Add(start + direction * Laser.MaxDistance);
				}
			}
		}
	}

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

			for (int i = 0; i < 10; i++)
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