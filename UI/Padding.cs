using Base;

namespace Raytracer.UI
{
	public struct Padding
	{
		public static Padding Zero = new Padding(0f, 0f, 0f, 0f);

		public float Left, Right, Top, Bottom;

		public Vector2 TopLeft => new Vector2(Left, Top);
		public Vector2 Total => new Vector2(Left + Right, Top + Bottom);

		public Padding(float left, float right, float top, float bottom)
		{
			Left = left;
			Right = right;
			Top = top;
			Bottom = bottom;
		}

		public Padding(float padding)
		{
			Left = padding;
			Right = padding;
			Top = padding;
			Bottom = padding;
		}
	}
}