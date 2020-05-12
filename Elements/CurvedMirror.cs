using Base;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;

namespace Raytracer.Elements
{
	public class CurvedMirror : Mirror
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
			List<Vector2> vertices = new List<Vector2>();

			Vector2 center = new Vector2(-_size.X * 0.5f, 0f);


			const float MaxAngle = MathF.PI / 6;
			const float AngleStep = MathF.PI / 90f;

			for (float i = -MaxAngle; i <= MaxAngle + AngleStep; i += AngleStep)
			{
				float x = MathF.Cos(i);
				float y = MathF.Sin(i);

				vertices.Add(center + new Vector2(x, y) * _size.X * 0.9f);
			}

			for (float i = MaxAngle; i >= -MaxAngle - AngleStep; i -= AngleStep)
			{
				float x = MathF.Cos(i);
				float y = MathF.Sin(i);

				vertices.Add(center + new Vector2(x, y) * _size.X);
			}

			return new Polygon(vertices.ToArray());
		}

		public CurvedMirror()
		{
			Size = new Vector2(100f, 200f);
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
			foreach (Line line in rotatedCollider.lines)
			{
				Renderer2D.DrawLine(line.start, line.end, color, 5f);
			}

			foreach (Line line in rotatedCollider.lines)
			{
				Renderer2D.DrawLine(line.start, line.end, Color.LimeGreen);
			}
		}
	}
}