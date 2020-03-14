namespace Raytracer.UI
{
	public struct StyleDimension
	{
		public float Pixels;
		public float Percent;

		public StyleDimension(float pixels, float percent)
		{
			Pixels = pixels;
			Percent = percent;
		}

		public void Deconstruct(out float pixels, out float percent)
		{
			pixels = Pixels;
			percent = Percent;
		}
	}
}