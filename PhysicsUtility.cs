using System;

namespace Raytracer
{
	public enum Material
	{
		BK7,
		BAF10,
		BAK1,
		FK51A,
		LASF9,
		SF5,
		SF10,
		SF11,
		Quartz,
		SodaLime,
		Polycarbonate,
		Al2O3,
		NaCl,
		Sapphire
	}

	public static partial class PhysicsUtility
	{
		public static double GetRefractiveIndex(Material material, double wavelength)
		{
			return material switch
			{
				Material.BK7 => Math.Sqrt(1 + 1.03961212 / (1 - 0.00600069867 / Math.Pow(wavelength, 2)) + 0.231792344 / (1 - 0.0200179144 / Math.Pow(wavelength, 2)) + 1.01046945 / (1 - 103.560653 / Math.Pow(wavelength, 2))),
				Material.BAF10 => Math.Sqrt(1 + 1.5851495 / (1 - 0.00926681282 / Math.Pow(wavelength, 2)) + 0.143559385 / (1 - 0.0424489805 / Math.Pow(wavelength, 2)) + 1.08521269 / (1 - 105.613573 / Math.Pow(wavelength, 2))),
				Material.BAK1 => Math.Sqrt(1 + 1.12365662 / (1 - 0.00644742752 / Math.Pow(wavelength, 2)) + 0.309276848 / (1 - 0.0222284402 / Math.Pow(wavelength, 2)) + 0.881511957 / (1 - 107.297751 / Math.Pow(wavelength, 2))),
				Material.FK51A => Math.Sqrt(1 + 0.971247817 / (1 - 0.00472301995 / Math.Pow(wavelength, 2)) + 0.216901417 / (1 - 0.0153575612 / Math.Pow(wavelength, 2)) + 0.904651666 / (1 - 168.68133 / Math.Pow(wavelength, 2))),
				Material.LASF9 => Math.Sqrt(1 + 2.00029547 / (1 - 0.0121426017 / Math.Pow(wavelength, 2)) + 0.298926886 / (1 - 0.0538736236 / Math.Pow(wavelength, 2)) + 1.80691843 / (1 - 156.530829 / Math.Pow(wavelength, 2))),
				Material.SF5 => Math.Sqrt(1 + 1.52481889 / (1 - 0.011254756 / Math.Pow(wavelength, 2)) + 0.187085527 / (1 - 0.0588995392 / Math.Pow(wavelength, 2)) + 1.42729015 / (1 - 129.141675 / Math.Pow(wavelength, 2))),
				Material.SF10 => Math.Sqrt(1 + 1.62153902 / (1 - 0.0122241457 / Math.Pow(wavelength, 2)) + 0.256287842 / (1 - 0.0595736775 / Math.Pow(wavelength, 2)) + 1.64447552 / (1 - 147.468793 / Math.Pow(wavelength, 2))),
				Material.SF11 => Math.Sqrt(1 + 1.73759695 / (1 - 0.013188707 / Math.Pow(wavelength, 2)) + 0.313747346 / (1 - 0.0623068142 / Math.Pow(wavelength, 2)) + 1.89878101 / (1 - 155.23629 / Math.Pow(wavelength, 2))),
				Material.Quartz => Math.Sqrt(1 + 0.6961663 / (1 - Math.Pow(0.0684043 / wavelength, 2)) + 0.4079426 / (1 - Math.Pow(0.1162414 / wavelength, 2)) + 0.8974794 / (1 - Math.Pow(9.896161 / wavelength, 2))),
				Material.SodaLime => 1.5130 - 0.003169 * Math.Pow(wavelength, 2) + 0.003962 * Math.Pow(wavelength, -2),
				Material.Polycarbonate => Math.Sqrt(1 + 1.4182 / (1 - 0.021304 / Math.Pow(wavelength, 2))),
				Material.Al2O3 => Math.Sqrt(1 + 1.4313493 / (1 - Math.Pow(0.0726631 / wavelength, 2)) + 0.65054713 / (1 - Math.Pow(0.1193242 / wavelength, 2)) + 5.3414021 / (1 - Math.Pow(18.028251 / wavelength, 2))),
				Material.NaCl => Math.Sqrt(1 + 0.00055 + 0.19800 / (1 - Math.Pow(0.050 / wavelength, 2)) + 0.48398 / (1 - Math.Pow(0.100 / wavelength, 2)) + 0.38696 / (1 - Math.Pow(0.128 / wavelength, 2)) + 0.25998 / (1 - Math.Pow(0.158 / wavelength, 2)) + 0.08796 / (1 - Math.Pow(40.50 / wavelength, 2)) + 3.17064 / (1 - Math.Pow(60.98 / wavelength, 2)) + 0.30038 / (1 - Math.Pow(120.34 / wavelength, 2))),
				Material.Sapphire => Math.Sqrt(1 + 1.4313493 / (1 - Math.Pow(0.0726631 / wavelength, 2)) + 0.65054713 / (1 - Math.Pow(0.1193242 / wavelength, 2)) + 5.3414021 / (1 - Math.Pow(18.028251 / wavelength, 2))),
				_ => throw new Exception("Invalid material")
			};
		}
	}
}