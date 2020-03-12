using Base;

namespace Raytracer
{
	public struct RaycastInfo
	{
		public Vector2 point;
		public Vector2 normal;
		public BaseElement element;
	}

	public static class Raycast
	{
		public static bool CastRay(Vector2 start, Vector2 direction, float maxDistance = 1000f)
		{
			Line line = new Line(start, start + direction * maxDistance);

			bool hit = false;

			foreach (BaseElement element in GameLayer.Instance.Elements)
			{
				if (Collision.LinePolygon(line, element.collider)) hit = true;
			}

			return hit;
		}

		public static bool CastRay(Vector2 start, Vector2 direction, out RaycastInfo info, float maxDistance = 1000f)
		{
			Line line = new Line(start, start + direction * maxDistance);

			foreach (BaseElement element in GameLayer.Instance.Elements)
			{
				if (Collision.LinePolygon(line, element.collider, out (Line line, Vector2 intersection) point))
				{
					var dir = Vector2.Normalize(point.line.end - point.line.start);
					info = new RaycastInfo
					{
						point = point.intersection,
						normal = dir.PerpendicularRight.X < 0 ? dir.PerpendicularRight : dir.PerpendicularLeft,
						element = element
					};

					return true;
				}
			}

			info = new RaycastInfo();
			return false;
		}
	}
}