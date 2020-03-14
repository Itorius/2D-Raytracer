using Base;

namespace Raytracer.UI
{
	public struct Rectangle
	{
		public float X, Y, Width, Height;

		public Vector2 Position => new Vector2(X, Y);
		public Vector2 Size => new Vector2(Width, Height);

		public Vector2 Center => new Vector2(X + Width * 0.5f, Y + Height * 0.5f);

		public Rectangle(float x, float y, float width, float height)
		{
			X = x;
			Y = y;
			Width = width;
			Height = height;
		}

		public bool Contains(in float x, in float y) => x >= X && x <= X + Width && y >= Y && y <= Y + Height;
	}
}