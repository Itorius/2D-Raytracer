using Base;
using System.Collections.Generic;

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
			color = PhysicsUtility.GetColor(wavelength);

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

					start = info.point;
					collisionPoints.Add(start);

					if (element.BlocksRays)
					{
						canAdvance = false;
						return;
					}

					float angle = Vector2.SignedAngle(info.normal, -direction);

					float scale = 1f;
					if (element is Refractor) scale = 1f + (1f - (float)((wavelength - 400f) / (750f - 400f))) * 0.1f;

					float outAngle = element.GetAngle(angle, initial, final) * scale;

					direction = Vector2.Transform(info.normal, Quaternion.FromAxisAngle(Vector3.UnitZ, outAngle)) * (info.element is Mirror ? 1 : -1);
					start += direction * 0.01f;
				}
				else
				{
					canAdvance = false;
					collisionPoints.Add(start + direction * Laser.MaxDistance);
				}
			}
		}
	}
}