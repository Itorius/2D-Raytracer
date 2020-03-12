using Base;
using OpenTK;
using OpenTK.Graphics;
using System;
using System.Collections.Generic;

namespace Raytracer
{
	public class Laser : BaseElement
	{
		public override Base.Vector2 GetTransformation(float initial, float final) => new Base.Vector2(0f,1f);

		private List<Base.Vector2> collisions = new List<Base.Vector2>();

		private List<(Base.Vector2, Base.Vector2)> normals = new List<(Base.Vector2, Base.Vector2)>();

		public override void Update()
		{
			collisions.Clear();
			normals.Clear();

			Base.Vector2 direction = new Base.Vector2(MathF.Cos(rotation), MathF.Sin(rotation));
			Base.Vector2 start = position + direction * size.X * 0.51f;

			collisions.Add(start);

			int index = 0;
			while (Raycast.CastRay(start + direction * 0.001f, direction, out RaycastInfo info))
			{
				start = info.point;

				collisions.Add(start);

				float angle = Base.Vector2.SignedAngle(info.normal, -direction);

				normals.Add((info.point, info.normal));

				(float x, float y) = index == 0 ? info.element.GetTransformation(1f, 1.4f) : info.element.GetTransformation(1.4f, 1f);
				float o = angle * x + angle * y;

				// var vec = Vector4.Transform(new Vector4(0f, angle, 0f, 0f), index == 0 ? info.element.GetTransformation(1f, 1.4f) : info.element.GetTransformation(1.4f, 1f)).Xy;

				// direction = Base.Vector2.Transform(-info.normal, Quaternion.FromAxisAngle(Vector3.UnitZ, vec.Y)); /* * (element is FlatMirror ? 1 : -1)*/
				direction = Base.Vector2.Transform(-info.normal, Quaternion.FromAxisAngle(Vector3.UnitZ, o)); /* * (element is FlatMirror ? 1 : -1)*/

				index++;
				if (index > 1) break;
			}

			collisions.Add(start + direction * 1000f);
		}

		public override void Draw()
		{
			var rot = Quaternion.FromAxisAngle(Vector3.UnitZ, rotation);

			for (int i = 0; i < collisions.Count - 1; i++) Renderer2D.DrawLine(collisions[i + 1], collisions[i], color);
			Renderer2D.DrawQuad(position, size, Color4.White, rot);
			Renderer2D.DrawQuad(position, size - new Base.Vector2(4f), color, rot);

			foreach (Base.Vector2 collision in collisions)
			{
				Renderer2D.DrawQuad(collision, new Base.Vector2(5f), Color4.Yellow);
			}

			foreach ((Base.Vector2 p, Base.Vector2 n) in normals)
			{
				Renderer2D.DrawLine(p, p + n * 50f, Color4.Yellow);
			}
		}
	}
}