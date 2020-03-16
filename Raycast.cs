using Base;
using Raytracer.Elements;
using System.Collections.Generic;
using System.Linq;

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
		public static bool CastRay(Vector2 start, Vector2 direction, float maxDistance = 1000000f)
		{
			Line line = new Line(start, start + direction * maxDistance);

			bool hit = false;

			foreach (BaseElement element in GameLayer.Instance.Elements)
			{
				if (Collision.LinePolygon(line, element.rotatedCollider)) hit = true;
			}

			return hit;
		}

		public static bool CastRay(Vector2 start, Vector2 direction, out RaycastInfo info, float maxDistance = 1000000f)
		{
			Line line = new Line(start, start + direction * maxDistance);

			List<(Line line, Vector2 point, BaseElement element)> hits = new List<(Line, Vector2, BaseElement)>();

			for (int i = 0; i < GameLayer.Instance.Elements.Count; i++)
			{
				BaseElement element = GameLayer.Instance.Elements[i];
				var collider = element.rotatedCollider.lines;
				foreach (Line line1 in collider)
				{
					if (Collision.LineLine(line, line1, out Vector2 intersection)) hits.Add((line1, intersection, element));
				}
			}

			if (hits.Count > 0)
			{
				(Line line1, Vector2 point, BaseElement element) = hits.OrderBy(x => Vector2.DistanceSquared(x.Item2, start)).First();

				var dir = Vector2.Normalize(line1.end - line1.start);
				float dot = Vector2.Dot(direction, dir.PerpendicularLeft);

				info = new RaycastInfo
				{
					point = point,
					normal = dot < 0 ? dir.PerpendicularLeft : dir.PerpendicularRight,
					element = element
				};

				return true;
			}

			info = new RaycastInfo();
			return false;
		}
	}
}