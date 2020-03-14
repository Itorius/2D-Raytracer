namespace Raytracer.UI
{
	public struct Padding
	{
		public static Padding Zero = new Padding(0f, 0f, 0f, 0f);

		public float Left, Right, Top, Bottom;

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