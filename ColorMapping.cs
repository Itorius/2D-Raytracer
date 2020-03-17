using Base;
using System;

namespace Raytracer
{
	public static class RgbCalculator
	{
		private const int LEN_MIN = 380, LEN_MAX = 780, LEN_STEP = 5;

		private static readonly double[] X =
		{
			0.000160, 0.000662, 0.002362, 0.007242, 0.019110, 0.043400, 0.084736, 0.140638, 0.204492, 0.264737,
			0.314679, 0.357719, 0.383734, 0.386726, 0.370702, 0.342957, 0.302273, 0.254085, 0.195618, 0.132349,
			0.080507, 0.041072, 0.016172, 0.005132, 0.003816, 0.015444, 0.037465, 0.071358, 0.117749, 0.172953,
			0.236491, 0.304213, 0.376772, 0.451584, 0.529826, 0.616053, 0.705224, 0.793832, 0.878655, 0.951162,
			1.014160, 1.074300, 1.118520, 1.134300, 1.123990, 1.089100, 1.030480, 0.950740, 0.856297, 0.754930,
			0.647467, 0.535110, 0.431567, 0.343690, 0.268329, 0.204300, 0.152568, 0.112210, 0.081261, 0.057930,
			0.040851, 0.028623, 0.019941, 0.013842, 0.009577, 0.006605, 0.004553, 0.003145, 0.002175, 0.001506,
			0.001045, 0.000727, 0.000508, 0.000356, 0.000251, 0.000178, 0.000126, 0.000090, 0.000065, 0.000046,
			0.000033
		};

		private static readonly double[] Y =
		{
			0.000017, 0.000072, 0.000253, 0.000769, 0.002004, 0.004509, 0.008756, 0.014456, 0.021391, 0.029497,
			0.038676, 0.049602, 0.062077, 0.074704, 0.089456, 0.106256, 0.128201, 0.152761, 0.185190, 0.219940,
			0.253589, 0.297665, 0.339133, 0.395379, 0.460777, 0.531360, 0.606741, 0.685660, 0.761757, 0.823330,
			0.875211, 0.923810, 0.961988, 0.982200, 0.991761, 0.999110, 0.997340, 0.982380, 0.955552, 0.915175,
			0.868934, 0.825623, 0.777405, 0.720353, 0.658341, 0.593878, 0.527963, 0.461834, 0.398057, 0.339554,
			0.283493, 0.228254, 0.179828, 0.140211, 0.107633, 0.081187, 0.060281, 0.044096, 0.031800, 0.022602,
			0.015905, 0.011130, 0.007749, 0.005375, 0.003718, 0.002565, 0.001768, 0.001222, 0.000846, 0.000586,
			0.000407, 0.000284, 0.000199, 0.000140, 0.000098, 0.000070, 0.000050, 0.000036, 0.000025, 0.000018,
			0.000013
		};

		private static readonly double[] Z =
		{
			0.000705, 0.002928, 0.010482, 0.032344, 0.086011, 0.197120, 0.389366, 0.656760, 0.972542, 1.282500,
			1.553480, 1.798500, 1.967280, 2.027300, 1.994800, 1.900700, 1.745370, 1.554900, 1.317560, 1.030200,
			0.772125, 0.570060, 0.415254, 0.302356, 0.218502, 0.159249, 0.112044, 0.082248, 0.060709, 0.043050,
			0.030451, 0.020584, 0.013676, 0.007918, 0.003988, 0.001091, 0.000000, 0.000000, 0.000000, 0.000000,
			0.000000, 0.000000, 0.000000, 0.000000, 0.000000, 0.000000, 0.000000, 0.000000, 0.000000, 0.000000,
			0.000000, 0.000000, 0.000000, 0.000000, 0.000000, 0.000000, 0.000000, 0.000000, 0.000000, 0.000000,
			0.000000, 0.000000, 0.000000, 0.000000, 0.000000, 0.000000, 0.000000, 0.000000, 0.000000, 0.000000,
			0.000000, 0.000000, 0.000000, 0.000000, 0.000000, 0.000000, 0.000000, 0.000000, 0.000000, 0.000000,
			0.000000
		};

		private static readonly double[] MATRIX_SRGB_D65 =
		{
			3.2404542, -1.5371385, -0.4985314,
			-0.9692660, 1.8760108, 0.0415560,
			0.0556434, -0.2040259, 1.0572252
		};

		public static Color Calc(double len)
		{
			// if (len < LEN_MIN || len > LEN_MAX) return Vector3.Zero;

			len -= LEN_MIN;
			int index = (int)Math.Floor(len / LEN_STEP);
			double offset = len - LEN_STEP * index;

			double x = Interpolate(X, index, offset);
			double y = Interpolate(Y, index, offset);
			double z = Interpolate(Z, index, offset);

			double[] m = MATRIX_SRGB_D65;

			double r = m[0] * x + m[1] * y + m[2] * z;
			double g = m[3] * x + m[4] * y + m[5] * z;
			double b = m[6] * x + m[7] * y + m[8] * z;

			r = Clip(GammaCorrect_sRGB(r));
			g = Clip(GammaCorrect_sRGB(g));
			b = Clip(GammaCorrect_sRGB(b));

			return new Color((float)r, (float)g, (float)b, 1f);
		}

		private static double Interpolate(double[] values, int index, double offset)
		{
			if (offset == 0) return values[index];

			int x0 = index * LEN_STEP;
			int x1 = x0 + LEN_STEP;
			double y0 = values[index];
			double y1 = values[1 + index];

			return y0 + offset * (y1 - y0) / (x1 - x0);
		}

		private static double GammaCorrect_sRGB(double c)
		{
			if (c <= 0.0031308) return 12.92 * c;

			double a = 0.055;
			return (1 + a) * Math.Pow(c, 1 / 2.4) - a;
		}

		private static double Clip(double c)
		{
			if (c < 0) return 0;
			if (c > 1) return 1;
			return c;
		}
	}

	public static class ColorMapping
	{
		public static void Test()
		{
			double x, y, z, r, g, b;
			ColorSystem cs = ColorSystem.CIEsystem;

			for (int t = 1000; t <= 10000; t += 500)
			{
				bbTemp = t;
				spectrum_to_xyz(bb_spectrum, out x, out y, out z);
				xyz_to_rgb(cs, x, y, z, out r, out g, out b);
				Console.WriteLine("  {0} K      {1} {2} {3}   ", t, x, y, z);
				if (constrain_rgb(ref r, ref g, ref b))
				{
					norm_rgb(ref r, ref g, ref b);
					Console.WriteLine("{0} {1} {2} (approx)", r, g, b);
				}
				else
				{
					norm_rgb(ref r, ref g, ref b);
					Console.WriteLine("{0} {1} {2}", r, g, b);
				}
			}
		}

		private struct ColorSystem
		{
			public static readonly ColorSystem NTSCsystem = new ColorSystem("NTSC", 0.67, 0.33, 0.21, 0.71, 0.14, 0.08, IlluminantC, GAMMA_REC709);
			public static readonly ColorSystem EBUsystem = new ColorSystem("EBU (PAL/SECAM)", 0.64, 0.33, 0.29, 0.60, 0.15, 0.06, IlluminantD65, GAMMA_REC709);
			public static readonly ColorSystem SMPTEsystem = new ColorSystem("SMPTE", 0.630, 0.340, 0.310, 0.595, 0.155, 0.070, IlluminantD65, GAMMA_REC709);
			public static readonly ColorSystem HDTVsystem = new ColorSystem("HDTV", 0.670, 0.330, 0.210, 0.710, 0.150, 0.060, IlluminantD65, GAMMA_REC709);
			public static readonly ColorSystem CIEsystem = new ColorSystem("CIE", 0.7355, 0.2645, 0.2658, 0.7243, 0.1669, 0.0085, IlluminantE, GAMMA_REC709);
			public static readonly ColorSystem Rec709system = new ColorSystem("CIE REC 709", 0.64, 0.33, 0.30, 0.60, 0.15, 0.06, IlluminantD65, GAMMA_REC709);

			private string name;

			public double xRed;
			public double yRed, xGreen, yGreen, xBlue, yBlue, xWhite, yWhite, gamma;

			public ColorSystem(string name, double xRed, double yRed, double xGreen, double yGreen, double xBlue, double yBlue, Vector2 white, double gamma)
			{
				this.name = name;
				this.xRed = xRed;
				this.yRed = yRed;
				this.xGreen = xGreen;
				this.yGreen = yGreen;
				this.xBlue = xBlue;
				this.yBlue = yBlue;
				xWhite = white.X;
				yWhite = white.Y;
				this.gamma = gamma;
			}
		}

		private static readonly Vector2 IlluminantC = new Vector2(0.3101f, 0.3162f);
		private static readonly Vector2 IlluminantD65 = new Vector2(0.3127f, 0.3291f);
		private static readonly Vector2 IlluminantE = new Vector2(0.33333333f, 0.33333333f);

		private const float GAMMA_REC709 = 0f;

		private static void upvp_to_xy(double up, double vp, out double xc, out double yc)
		{
			xc = 9 * up / (6 * up - 16 * vp + 12);
			yc = 4 * vp / (6 * up - 16 * vp + 12);
		}

		private static void xy_to_upvp(double xc, double yc, out double up, out double vp)
		{
			up = 4 * xc / (-2 * xc + 12 * yc + 3);
			vp = 9 * yc / (-2 * xc + 12 * yc + 3);
		}

		private static void xyz_to_rgb(ColorSystem cs, double xc, double yc, double zc, out double r, out double g, out double b)
		{
			double xr, yr, zr, xg, yg, zg, xb, yb, zb;
			double xw, yw, zw;
			double rx, ry, rz, gx, gy, gz, bx, by, bz;
			double rw, gw, bw;

			xr = cs.xRed;
			yr = cs.yRed;
			zr = 1 - (xr + yr);
			xg = cs.xGreen;
			yg = cs.yGreen;
			zg = 1 - (xg + yg);
			xb = cs.xBlue;
			yb = cs.yBlue;
			zb = 1 - (xb + yb);

			xw = cs.xWhite;
			yw = cs.yWhite;
			zw = 1 - (xw + yw);

			/* xyz . rgb matrix, before scaling to white. */

			rx = yg * zb - yb * zg;
			ry = xb * zg - xg * zb;
			rz = xg * yb - xb * yg;
			gx = yb * zr - yr * zb;
			gy = xr * zb - xb * zr;
			gz = xb * yr - xr * yb;
			bx = yr * zg - yg * zr;
			by = xg * zr - xr * zg;
			bz = xr * yg - xg * yr;

			/* White scaling factors.
			   Dividing by yw scales the white luminance to unity, as conventional. */

			rw = (rx * xw + ry * yw + rz * zw) / yw;
			gw = (gx * xw + gy * yw + gz * zw) / yw;
			bw = (bx * xw + by * yw + bz * zw) / yw;

			/* xyz . rgb matrix, correctly scaled to white. */

			rx = rx / rw;
			ry = ry / rw;
			rz = rz / rw;
			gx = gx / gw;
			gy = gy / gw;
			gz = gz / gw;
			bx = bx / bw;
			by = by / bw;
			bz = bz / bw;

			/* rgb of the desired point */

			r = rx * xc + ry * yc + rz * zc;
			g = gx * xc + gy * yc + gz * zc;
			b = bx * xc + by * yc + bz * zc;
		}

		private static bool inside_gamut(double r, double g, double b)
		{
			return r >= 0 && g >= 0 && b >= 0;
		}

		private static bool constrain_rgb(ref double r, ref double g, ref double b)
		{
			double w;

			/* Amount of white needed is w = - min(0, *r, *g, *b) */

			w = 0 < r ? 0 : r;
			w = w < g ? w : g;
			w = w < b ? w : b;
			w = -w;

			/* Add just enough white to make r, g, b all positive. */

			if (w > 0)
			{
				r += w;
				g += w;
				b += w;
				return true; /* Colour modified to fit RGB gamut */
			}

			return false; /* Colour within RGB gamut */
		}

		private static void gamma_correct(ColorSystem cs, ref double c)
		{
			double gamma;

			gamma = cs.gamma;

			if (gamma == GAMMA_REC709)
			{
				/* Rec. 709 gamma correction. */
				double cc = 0.018;

				if (c < cc)
				{
					c *= (1.099 * Math.Pow(cc, 0.45) - 0.099) / cc;
				}
				else
				{
					c = 1.099 * Math.Pow(c, 0.45) - 0.099;
				}
			}
			else
			{
				/* Nonlinear colour = (Linear colour)^(1/gamma) */
				c = Math.Pow(c, 1.0 / gamma);
			}
		}

		private static void gamma_correct_rgb(ColorSystem cs, ref double r, ref double g, ref double b)
		{
			gamma_correct(cs, ref r);
			gamma_correct(cs, ref g);
			gamma_correct(cs, ref b);
		}

		private static void norm_rgb(ref double r, ref double g, ref double b)
		{
			double greatest = Math.Max(r, Math.Max(g, b));

			if (greatest > 0)
			{
				r /= greatest;
				g /= greatest;
				b /= greatest;
			}
		}

		private static double[][] cie_colour_match =
		{
			new[]
			{
				0.0014, 0.0000, 0.0065
			},
			new[]
			{
				0.0022, 0.0001, 0.0105
			},
			new[]
			{
				0.0042, 0.0001, 0.0201
			},
			new[]
			{
				0.0076, 0.0002, 0.0362
			},
			new[]
			{
				0.0143, 0.0004, 0.0679
			},
			new[]
			{
				0.0232, 0.0006, 0.1102
			},
			new[]
			{
				0.0435, 0.0012, 0.2074
			},
			new[]
			{
				0.0776, 0.0022, 0.3713
			},
			new[]
			{
				0.1344, 0.0040, 0.6456
			},
			new[]
			{
				0.2148, 0.0073, 1.0391
			},
			new[]
			{
				0.2839, 0.0116, 1.3856
			},
			new[]
			{
				0.3285, 0.0168, 1.6230
			},
			new[]
			{
				0.3483, 0.0230, 1.7471
			},
			new[]
			{
				0.3481, 0.0298, 1.7826
			},
			new[]
			{
				0.3362, 0.0380, 1.7721
			},
			new[]
			{
				0.3187, 0.0480, 1.7441
			},
			new[]
			{
				0.2908, 0.0600, 1.6692
			},
			new[]
			{
				0.2511, 0.0739, 1.5281
			},
			new[]
			{
				0.1954, 0.0910, 1.2876
			},
			new[]
			{
				0.1421, 0.1126, 1.0419
			},
			new[]
			{
				0.0956, 0.1390, 0.8130
			},
			new[]
			{
				0.0580, 0.1693, 0.6162
			},
			new[]
			{
				0.0320, 0.2080, 0.4652
			},
			new[]
			{
				0.0147, 0.2586, 0.3533
			},
			new[]
			{
				0.0049, 0.3230, 0.2720
			},
			new[]
			{
				0.0024, 0.4073, 0.2123
			},
			new[]
			{
				0.0093, 0.5030, 0.1582
			},
			new[]
			{
				0.0291, 0.6082, 0.1117
			},
			new[]
			{
				0.0633, 0.7100, 0.0782
			},
			new[]
			{
				0.1096, 0.7932, 0.0573
			},
			new[]
			{
				0.1655, 0.8620, 0.0422
			},
			new[]
			{
				0.2257, 0.9149, 0.0298
			},
			new[]
			{
				0.2904, 0.9540, 0.0203
			},
			new[]
			{
				0.3597, 0.9803, 0.0134
			},
			new[]
			{
				0.4334, 0.9950, 0.0087
			},
			new[]
			{
				0.5121, 1.0000, 0.0057
			},
			new[]
			{
				0.5945, 0.9950, 0.0039
			},
			new[]
			{
				0.6784, 0.9786, 0.0027
			},
			new[]
			{
				0.7621, 0.9520, 0.0021
			},
			new[]
			{
				0.8425, 0.9154, 0.0018
			},
			new[]
			{
				0.9163, 0.8700, 0.0017
			},
			new[]
			{
				0.9786, 0.8163, 0.0014
			},
			new[]
			{
				1.0263, 0.7570, 0.0011
			},
			new[]
			{
				1.0567, 0.6949, 0.0010
			},
			new[]
			{
				1.0622, 0.6310, 0.0008
			},
			new[]
			{
				1.0456, 0.5668, 0.0006
			},
			new[]
			{
				1.0026, 0.5030, 0.0003
			},
			new[]
			{
				0.9384, 0.4412, 0.0002
			},
			new[]
			{
				0.8544, 0.3810, 0.0002
			},
			new[]
			{
				0.7514, 0.3210, 0.0001
			},
			new[]
			{
				0.6424, 0.2650, 0.0000
			},
			new[]
			{
				0.5419, 0.2170, 0.0000
			},
			new[]
			{
				0.4479, 0.1750, 0.0000
			},
			new[]
			{
				0.3608, 0.1382, 0.0000
			},
			new[]
			{
				0.2835, 0.1070, 0.0000
			},
			new[]
			{
				0.2187, 0.0816, 0.0000
			},
			new[]
			{
				0.1649, 0.0610, 0.0000
			},
			new[]
			{
				0.1212, 0.0446, 0.0000
			},
			new[]
			{
				0.0874, 0.0320, 0.0000
			},
			new[]
			{
				0.0636, 0.0232, 0.0000
			},
			new[]
			{
				0.0468, 0.0170, 0.0000
			},
			new[]
			{
				0.0329, 0.0119, 0.0000
			},
			new[]
			{
				0.0227, 0.0082, 0.0000
			},
			new[]
			{
				0.0158, 0.0057, 0.0000
			},
			new[]
			{
				0.0114, 0.0041, 0.0000
			},
			new[]
			{
				0.0081, 0.0029, 0.0000
			},
			new[]
			{
				0.0058, 0.0021, 0.0000
			},
			new[]
			{
				0.0041, 0.0015, 0.0000
			},
			new[]
			{
				0.0029, 0.0010, 0.0000
			},
			new[]
			{
				0.0020, 0.0007, 0.0000
			},
			new[]
			{
				0.0014, 0.0005, 0.0000
			},
			new[]
			{
				0.0010, 0.0004, 0.0000
			},
			new[]
			{
				0.0007, 0.0002, 0.0000
			},
			new[]
			{
				0.0005, 0.0002, 0.0000
			},
			new[]
			{
				0.0003, 0.0001, 0.0000
			},
			new[]
			{
				0.0002, 0.0001, 0.0000
			},
			new[]
			{
				0.0002, 0.0001, 0.0000
			},
			new[]
			{
				0.0001, 0.0000, 0.0000
			},
			new[]
			{
				0.0001, 0.0000, 0.0000
			},
			new[]
			{
				0.0001, 0.0000, 0.0000
			},
			new[]
			{
				0.0000, 0.0000, 0.0000
			}
		};

		private static void spectrum_to_xyz(Func<double, double> wavelength, out double x, out double y, out double z)
		{
			int i;
			double lambda, X = 0, Y = 0, Z = 0, XYZ;

			for (i = 0, lambda = 380; lambda < 780.1; i++, lambda += 5)
			{
				double Me;

				Me = /*wavelength.Invoke(lambda)*/129.043000 * 1E-6;
				X += Me * cie_colour_match[i][0];
				Y += Me * cie_colour_match[i][1];
				Z += Me * cie_colour_match[i][2];
			}

			XYZ = X + Y + Z;
			x = X / XYZ;
			y = Y / XYZ;
			z = Z / XYZ;
		}

		private static double bbTemp = 5000;

		private static double bb_spectrum(double wavelength)
		{
			double wlm = wavelength * 1e-9;

			return 3.74183e-16 * Math.Pow(wlm, -5.0) / (Math.Exp(1.4388e-2 / (wlm * bbTemp)) - 1.0);
		}
	}
}