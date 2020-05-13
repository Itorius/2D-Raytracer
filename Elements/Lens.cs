using Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Raytracer.Elements
{
	public class Lens : BaseElement
	{
		public float Radius1 = 1.1f;
		public float Radius2 = -1.1f;
		public int segments = 30;
		public float Thickness = 0f;

		private IEnumerable<Vector2> GenerateVerticesFront()
		{
			for (int i = -segments; i <= segments; i++)
			{
			}

			yield break;

			// float localScale = _size.Y;
			// float clip = localScale * 2f;
			// float radius = Radius1 * MathF.Abs(localScale);
			//
			// if (Math.Abs(Radius1) < 1)
			// {
			// 	float s = -localScale / segments;
			// 	for (int i = -segments / 2 - 1; i <= segments / 2; i++) yield return new Vector2(-Thickness * 0.5f, s * i + s * 0.5f);
			// }
			// else
			// {
			// 	float offset = MathF.Sqrt(4f * radius * radius - clip * clip) * 0.5f;
			//
			// 	float maxAngle = MathF.Atan(localScale / offset);
			//
			// 	Vector2 center = radius < 0 ? new Vector2(radius - Thickness * 0.5f, 0f) : new Vector2(offset - Thickness * 0.5f, 0);
			//
			// 	float segmentSize = 2f * maxAngle / segments;
			//
			// 	for (int i = -segments / 2 - 1; i <= segments / 2; i++)
			// 	{
			// 		float angle = i * segmentSize + segmentSize * 0.5f;
			//
			// 		float x = -MathF.Cos(angle);
			// 		float y = MathF.Sin(angle) / localScale;
			//
			// 		Vector2 vertex =  center + new Vector2(x, y) * radius;
			//
			// 		yield return vertex;
			// 	}
			// }
		}

		private IEnumerable<Vector2> GenerateVerticesBack()
		{
			float localScale = _size.Y;
			float clip = localScale * 2f;
			float radius = Radius2 * MathF.Abs(localScale);

			if (Math.Abs(Radius2) < 1)
			{
				float s = -localScale / segments;
				for (int i = -segments / 2 - 1; i <= segments / 2; i++) yield return new Vector2(Thickness * 0.5f, s * i + s * 0.5f);
			}
			else
			{
				float offset = MathF.Sqrt(4f * radius * radius - clip * clip) * 0.5f;
				float maxAngle = MathF.Atan(localScale / offset);

				Vector2 center = radius > 0 ? new Vector2(radius + Thickness * 0.5f, 0f) : new Vector2(-offset + Thickness * 0.5f, 0);

				float segmentSize = 2f * maxAngle / segments;

				for (int i = -segments / 2 - 1; i <= segments / 2; i++)
				{
					float angle = i * segmentSize + segmentSize * 0.5f;

					float x = -MathF.Cos(angle);
					float y = MathF.Sin(angle) / localScale;

					Vector2 vertex = center + new Vector2(x, y) * radius;

					yield return vertex;
				}
			}
		}

		public override Vector2 Position
		{
			get => _position;
			set
			{
				_position = value;
				collider = GenerateCollider();
				rotatedCollider = Polygon.Transform(collider, Matrix4.CreateRotationZ(_rotation) * Matrix4.CreateTranslation(Position.X, Position.Y, 0f));
			}
		}

		public override Vector2 Size
		{
			get => _size;
			set
			{
				_size = value;
				collider = GenerateCollider();
				rotatedCollider = Polygon.Transform(collider, Matrix4.CreateRotationZ(_rotation) * Matrix4.CreateTranslation(Position.X, Position.Y, 0f));
			}
		}

		public override float Rotation
		{
			get => _rotation;
			set
			{
				_rotation = value;
				quaternion = Quaternion.FromAxisAngle(Vector3.UnitZ, value);
				collider = GenerateCollider();
				rotatedCollider = Polygon.Transform(collider, Matrix4.CreateRotationZ(_rotation) * Matrix4.CreateTranslation(Position.X, Position.Y, 0f));
			}
		}

		public Polygon GenerateCollider()
		{
			List<Vector2> vertices = new List<Vector2>();

			float radius = Radius1 * _size.Y * 0.5f;
			float offset = MathF.Sqrt(radius * radius * 4f - _size.Y * _size.Y) * 0.5f;

			for (int i = 0; i <= 100; i++)
			{
				float y = -_size.Y * 0.5f + _size.Y * i * 0.01f;
				float x = -MathF.Sqrt(radius * radius - y * y);

				vertices.Add(new Vector2(x + offset, y));
			}

			radius = Radius2 * _size.Y * 0.5f;
			offset = MathF.Sqrt(radius * radius * 4f - _size.Y * _size.Y) * 0.5f;

			for (int i = 100; i >= 0; i--)
			{
				float y = -_size.Y * 0.5f + _size.Y * i * 0.01f;
				float x = MathF.Sqrt(radius * radius - y * y);

				vertices.Add(new Vector2(x - offset, y));
			}

			return new Polygon(vertices.ToArray());
		}

		public Lens()
		{
			Size = new Vector2(20f, 200f);
		}

		public void UIDraw()
		{
			var color = Color.Lerp(Color, ColorSelected, selected ? Utility.UnsignedSin(Time.TotalDrawTime * 3f) : 0f);
			foreach (Line line in GenerateCollider().lines)
			{
				Renderer2D.DrawLine(line.start + _position, line.end + _position, color, 5f);
			}
		}

		public override void Draw()
		{
			var color = Color.Lerp(Color, ColorSelected, selected ? Utility.UnsignedSin(Time.TotalDrawTime * 3f) : 0f);

			// Vector2 vec = Vector2.Normalize(rotatedCollider.vertices[rotatedCollider.vertices.Length / 4] - _position);

			// Renderer2D.DrawQuad(_position + vec * _size.X * 0.25f, new Vector2(2f), Color.Goldenrod);
			// Renderer2D.DrawString("F", _position.X + vec.X * _size.X * 0.25f - 6f, _position.Y + vec.Y * _size.X * 0.25f - 3f, Color.Goldenrod, 0.75f, true);

			foreach (Line line in rotatedCollider.lines)
			{
				Renderer2D.DrawLine(line.start, line.end, color);
			}
		}

		public override float GetAngle(float incoming, float initial, float final)
		{
			float angle = MathF.Asin(initial * MathF.Sin(incoming) / final);
			return float.IsNaN(angle) ? MathF.PI - incoming : angle;
		}
	}
}