namespace Raytracer
{
	public static class Constants
	{
		// https://en.wikipedia.org/wiki/List_of_refractive_indices
		// https://en.wikipedia.org/wiki/Sellmeier_equation

		public static class RefractiveIndexes
		{
			public const float Vacuum = 1;
			public const float Air = 1.000277f;
			public const float Glass = 1.458f;
			public const float Water = 1.333f;
			public const float VegetableOil = 1.47f;
		}
	}
}