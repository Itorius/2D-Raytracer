using Base;
using OpenTK.Graphics;
using System;
using System.Collections.Generic;

namespace Raytracer.Elements
{
	public class Laser : BaseElement
	{
		public override bool BlocksRays => true;

		// public override Vector2 GetTransformation(float initial, float final) => new Vector2(0f, 1f);

		private List<Vector2> collisions = new List<Vector2>();

		private List<(Vector2, Vector2)> normals = new List<(Vector2, Vector2)>();

		public Laser()
		{
			Color = Color.FromHsv(new Random().NextFloat(), 1f, 0.9f, 1f);

			Size = new Vector2(100f, 20f);
		}

		public override void Update()
		{
			collisions.Clear();
			normals.Clear();

			Vector2 direction = Vector2.Transform(Vector2.UnitX, quaternion);
			Vector2 start = Position + direction * Size.X * 0.51f;

			collisions.Add(start);

			BaseElement lastCollided = null;

			int index = 0;
			while (Raycast.CastRay(start + direction * 0.001f, direction, out RaycastInfo info))
			{
				BaseElement element = info.element;

				HandleRefractiveIndices(lastCollided, element, out float initial, out float final);

				lastCollided = element;

				start = info.point;

				collisions.Add(start);

				if (element.BlocksRays) return;


				normals.Add((info.point, info.normal));

				float angle = Vector2.SignedAngle(info.normal, -direction);
				float outAngle = element.GetAngle(angle, initial, final);
				direction = Vector2.Transform(-info.normal, Quaternion.FromAxisAngle(Vector3.UnitZ, outAngle)) * (info.element is FlatMirror ? -1 : 1);

				// if (!float.IsNaN(aaaa))
				// {
				// }
				// else
				// {
				// 	(float x, float y) = info.element.GetTransformation(initial, final);
				// 	float o = angle * x + angle * y;
				//
				// 	direction = Vector2.Transform(-info.normal, Quaternion.FromAxisAngle(Vector3.UnitZ, o)) * (info.element is FlatMirror ? -1 : 1);
				// }

				index++;
				if (index > 50) break;
			}

			collisions.Add(start + direction * 1000000f);
		}

		public override void Draw()
		{
			var color = Color.Lerp(Color, Color * 0.7f, selected ? Utility.UnsignedSin(Time.TotalDrawTime * 3f) : 0f);
			Renderer2D.DrawQuad(Position, Size, color, quaternion);

			if (Game.DebugDraw)
			{
				foreach (Vector2 collision in collisions) Renderer2D.DrawQuad(collision, new Vector2(5f), Color4.Yellow);
				foreach ((Vector2 point, Vector2 normal) in normals) Renderer2D.DrawLine(point, point + normal * 50f, Color4.Yellow);
			}
		}

		public override BaseElement Clone()
		{
			Laser element = (Laser)MemberwiseClone();
			element.collisions = new List<Vector2>();
			element.normals = new List<(Vector2, Vector2)>();
			return element;
		}

		public void DrawRay()
		{
			for (int i = 0; i < collisions.Count - 1; i++) Renderer2D.DrawLine(collisions[i + 1], collisions[i], Color, 2.5f);
		}

		private static void HandleRefractiveIndices(BaseElement lastCollided, BaseElement element, out float initial, out float final)
		{
			if (lastCollided == element)
			{
				initial = element.RefractiveIndex;
				final = Constants.RefractiveIndexes.Air;
			}
			else
			{
				initial = Constants.RefractiveIndexes.Air;
				final = element.RefractiveIndex;
			}
		}
	}
}