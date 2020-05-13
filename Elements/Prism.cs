using Base;
using System;
using System.Collections.Generic;

namespace Raytracer.Elements
{
	public class Prism : BaseElement
	{
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
			List<Vector2> vertices = new List<Vector2>
			{
				new Vector2(0f, _size.Y * 0.5f),
				new Vector2(_size.X * 0.5f, -_size.Y * 0.5f),
				new Vector2(-_size.X * 0.5f, -_size.Y * 0.5f)
			};

			return new Polygon(vertices.ToArray());
		}

		public Prism()
		{
			Size = new Vector2(100f, 86.6f);
		}

		public override void Draw()
		{
			var color = Color.Lerp(Color, ColorSelected, selected ? Utility.UnsignedSin(Time.TotalDrawTime * 3f) : 0f);

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