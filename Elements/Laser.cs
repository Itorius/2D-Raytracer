using Base;
using OpenTK;
using OpenTK.Graphics;
using System;
using System.Collections.Generic;

namespace Raytracer.Elements
{
	public class Laser : BaseElement
	{
		public override Base.Vector2 GetTransformation(float initial, float final) => new Base.Vector2(0f, 1f);

		private List<Base.Vector2> collisions = new List<Base.Vector2>();

		private List<(Base.Vector2, Base.Vector2)> normals = new List<(Base.Vector2, Base.Vector2)>();

		public Laser()
		{
			Color = Color.FromHsv(new Random().NextFloat(), 1f, 0.9f, 1f);
		}

		public override void Update()
		{
			collisions.Clear();
			normals.Clear();

			Base.Vector2 direction = Base.Vector2.Transform(Base.Vector2.UnitX, quaternion);
			Base.Vector2 start = Position + direction * Size.X * 0.51f;

			collisions.Add(start);

			BaseElement lastCollided = null;
			float initial = 1f;
			float final = 1f;

			int index = 0;
			while (Raycast.CastRay(start + direction * 0.001f, direction, out RaycastInfo info))
			{
				BaseElement element = info.element;
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

				lastCollided = element;

				start = info.point;

				collisions.Add(start);

				if (element is Laser) return;

				float angle = Base.Vector2.SignedAngle(info.normal, -direction);

				normals.Add((info.point, info.normal));

				(float x, float y) = info.element.GetTransformation(initial, final);
				float o = angle * x + angle * y;

				direction = Base.Vector2.Transform(-info.normal, Quaternion.FromAxisAngle(Vector3.UnitZ, o)) * (info.element is FlatMirror ? -1 : 1);

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
				foreach (Base.Vector2 collision in collisions) Renderer2D.DrawQuad(collision, new Base.Vector2(5f), Color4.Yellow);
				foreach ((Base.Vector2 point, Base.Vector2 normal) in normals) Renderer2D.DrawLine(point, point + normal * 50f, Color4.Yellow);
			}
		}

		public override BaseElement Clone()
		{
			Laser element = (Laser)MemberwiseClone();
			element.collisions = new List<Base.Vector2>();
			element.normals = new List<(Base.Vector2, Base.Vector2)>();
			return element;
		}

		public void DrawRay()
		{
			for (int i = 0; i < collisions.Count - 1; i++) Renderer2D.DrawLine(collisions[i + 1], collisions[i], Color);
		}
	}
}