using Base;
using System;
using System.Collections.Generic;

namespace Raytracer.Elements
{
	public class Lens : BaseElement
	{
		public float Radius1 = 5f;
		public float Radius2 = 1000f;
		public float Thickness = 5f;

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

			float offset = 0f;
			if (Radius1 < 0) offset = radius-Thickness*0.5f;
			else offset = MathF.Sqrt(radius * radius * 4f - _size.Y * _size.Y) * 0.5f - Thickness * 0.5f;

			for (int i = 0; i <= 100; i++)
			{
				float y = -_size.Y * 0.5f + _size.Y * i * 0.01f;
				float x = -MathF.Sqrt(radius * radius - y * y) * MathF.Sign(Radius1);

				vertices.Add(new Vector2(x + offset, y));
			}

			radius = Radius2 * _size.Y * 0.5f;
			
			if (Radius2 > 0) offset = radius+Thickness*0.5f;
			else offset = -MathF.Sqrt(radius * radius * 4f - _size.Y * _size.Y) * 0.5f + Thickness * 0.5f;
			
			for (int i = 100; i >= 0; i--)
			{
				float y = -_size.Y * 0.5f + _size.Y * i * 0.01f;
				float x = -MathF.Sqrt(radius * radius - y * y)* MathF.Sign(Radius2);
			
				vertices.Add(new Vector2(x + offset, y));
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