using OpenTK;
using OpenTK.Graphics;
using System;

namespace Raytracer
{
	[Serializable]
	public struct Color : IEquatable<Color>
	{
		public float R;
		public float G;
		public float B;
		public float A;

		public Color(float r, float g, float b, float a)
		{
			R = r;
			G = g;
			B = b;
			A = a;
		}

		public Color(byte r, byte g, byte b, byte a)
		{
			R = r / (float)byte.MaxValue;
			G = g / (float)byte.MaxValue;
			B = b / (float)byte.MaxValue;
			A = a / (float)byte.MaxValue;
		}

		public int ToArgb()
		{
			return (int)((uint)(((int)(uint)(A * byte.MaxValue) << 24) | ((int)(uint)(R * (double)byte.MaxValue) << 16) | ((int)(uint)(G * (double)byte.MaxValue) << 8)) | (uint)(B * (double)byte.MaxValue));
		}

		public static bool operator ==(Color left, Color right) => left.Equals(right);

		public static bool operator !=(Color left, Color right) => !left.Equals(right);

		public static implicit operator Color4(Color color) => new Color4(color.R, color.G, color.B, color.A);

		public static implicit operator Color(Color4 color) => new Color(color.R, color.G, color.B, color.A);

		public override bool Equals(object obj) => obj is Color other && Equals(other);

		public override int GetHashCode() => ToArgb();

		public override string ToString() => $"{{(R, G, B, A) = ({R}, {G}, {B}, {A})}}";

		/*
		public static Color Transparent => new Color(byte.MaxValue, byte.MaxValue, byte.MaxValue, (byte) 0);

		public static Color AliceBlue => new Color((byte) 240, (byte) 248, byte.MaxValue, byte.MaxValue);

		public static Color AntiqueWhite => new Color((byte) 250, (byte) 235, (byte) 215, byte.MaxValue);

		/// <summary>
		/// Gets the system color with (R, G, B, A) = (0, 255, 255, 255).
		/// </summary>
		public static Color Aqua
		{
			get
			{
				return new Color((byte) 0, byte.MaxValue, byte.MaxValue, byte.MaxValue);
			}
		}

		/// <summary>
		/// Gets the system color with (R, G, B, A) = (127, 255, 212, 255).
		/// </summary>
		public static Color Aquamarine
		{
			get
			{
				return new Color((byte) 127, byte.MaxValue, (byte) 212, byte.MaxValue);
			}
		}

		/// <summary>
		/// Gets the system color with (R, G, B, A) = (240, 255, 255, 255).
		/// </summary>
		public static Color Azure
		{
			get
			{
				return new Color((byte) 240, byte.MaxValue, byte.MaxValue, byte.MaxValue);
			}
		}

		/// <summary>
		/// Gets the system color with (R, G, B, A) = (245, 245, 220, 255).
		/// </summary>
		public static Color Beige
		{
			get
			{
				return new Color((byte) 245, (byte) 245, (byte) 220, byte.MaxValue);
			}
		}

		/// <summary>
		/// Gets the system color with (R, G, B, A) = (255, 228, 196, 255).
		/// </summary>
		public static Color Bisque
		{
			get
			{
				return new Color(byte.MaxValue, (byte) 228, (byte) 196, byte.MaxValue);
			}
		}

		/// <summary>
		/// Gets the system color with (R, G, B, A) = (0, 0, 0, 255).
		/// </summary>
		public static Color Black
		{
			get
			{
				return new Color((byte) 0, (byte) 0, (byte) 0, byte.MaxValue);
			}
		}

		/// <summary>
		/// Gets the system color with (R, G, B, A) = (255, 235, 205, 255).
		/// </summary>
		public static Color BlanchedAlmond
		{
			get
			{
				return new Color(byte.MaxValue, (byte) 235, (byte) 205, byte.MaxValue);
			}
		}

		/// <summary>
		/// Gets the system color with (R, G, B, A) = (0, 0, 255, 255).
		/// </summary>
		public static Color Blue
		{
			get
			{
				return new Color((byte) 0, (byte) 0, byte.MaxValue, byte.MaxValue);
			}
		}

		/// <summary>
		/// Gets the system color with (R, G, B, A) = (138, 43, 226, 255).
		/// </summary>
		public static Color BlueViolet
		{
			get
			{
				return new Color((byte) 138, (byte) 43, (byte) 226, byte.MaxValue);
			}
		}

		/// <summary>
		/// Gets the system color with (R, G, B, A) = (165, 42, 42, 255).
		/// </summary>
		public static Color Brown
		{
			get
			{
				return new Color((byte) 165, (byte) 42, (byte) 42, byte.MaxValue);
			}
		}

		/// <summary>
		/// Gets the system color with (R, G, B, A) = (222, 184, 135, 255).
		/// </summary>
		public static Color BurlyWood
		{
			get
			{
				return new Color((byte) 222, (byte) 184, (byte) 135, byte.MaxValue);
			}
		}

		/// <summary>
		/// Gets the system color with (R, G, B, A) = (95, 158, 160, 255).
		/// </summary>
		public static Color CadetBlue
		{
			get
			{
				return new Color((byte) 95, (byte) 158, (byte) 160, byte.MaxValue);
			}
		}

		/// <summary>
		/// Gets the system color with (R, G, B, A) = (127, 255, 0, 255).
		/// </summary>
		public static Color Chartreuse
		{
			get
			{
				return new Color((byte) 127, byte.MaxValue, (byte) 0, byte.MaxValue);
			}
		}

		/// <summary>
		/// Gets the system color with (R, G, B, A) = (210, 105, 30, 255).
		/// </summary>
		public static Color Chocolate
		{
			get
			{
				return new Color((byte) 210, (byte) 105, (byte) 30, byte.MaxValue);
			}
		}

		/// <summary>
		/// Gets the system color with (R, G, B, A) = (255, 127, 80, 255).
		/// </summary>
		public static Color Coral
		{
			get
			{
				return new Color(byte.MaxValue, (byte) 127, (byte) 80, byte.MaxValue);
			}
		}

		/// <summary>
		/// Gets the system color with (R, G, B, A) = (100, 149, 237, 255).
		/// </summary>
		public static Color CornflowerBlue
		{
			get
			{
				return new Color((byte) 100, (byte) 149, (byte) 237, byte.MaxValue);
			}
		}

		/// <summary>
		/// Gets the system color with (R, G, B, A) = (255, 248, 220, 255).
		/// </summary>
		public static Color Cornsilk
		{
			get
			{
				return new Color(byte.MaxValue, (byte) 248, (byte) 220, byte.MaxValue);
			}
		}

		/// <summary>
		/// Gets the system color with (R, G, B, A) = (220, 20, 60, 255).
		/// </summary>
		public static Color Crimson
		{
			get
			{
				return new Color((byte) 220, (byte) 20, (byte) 60, byte.MaxValue);
			}
		}

		/// <summary>
		/// Gets the system color with (R, G, B, A) = (0, 255, 255, 255).
		/// </summary>
		public static Color Cyan
		{
			get
			{
				return new Color((byte) 0, byte.MaxValue, byte.MaxValue, byte.MaxValue);
			}
		}

		/// <summary>
		/// Gets the system color with (R, G, B, A) = (0, 0, 139, 255).
		/// </summary>
		public static Color DarkBlue
		{
			get
			{
				return new Color((byte) 0, (byte) 0, (byte) 139, byte.MaxValue);
			}
		}

		/// <summary>
		/// Gets the system color with (R, G, B, A) = (0, 139, 139, 255).
		/// </summary>
		public static Color DarkCyan
		{
			get
			{
				return new Color((byte) 0, (byte) 139, (byte) 139, byte.MaxValue);
			}
		}

		/// <summary>
		/// Gets the system color with (R, G, B, A) = (184, 134, 11, 255).
		/// </summary>
		public static Color DarkGoldenrod
		{
			get
			{
				return new Color((byte) 184, (byte) 134, (byte) 11, byte.MaxValue);
			}
		}

		/// <summary>
		/// Gets the system color with (R, G, B, A) = (169, 169, 169, 255).
		/// </summary>
		public static Color DarkGray
		{
			get
			{
				return new Color((byte) 169, (byte) 169, (byte) 169, byte.MaxValue);
			}
		}

		/// <summary>
		/// Gets the system color with (R, G, B, A) = (0, 100, 0, 255).
		/// </summary>
		public static Color DarkGreen
		{
			get
			{
				return new Color((byte) 0, (byte) 100, (byte) 0, byte.MaxValue);
			}
		}

		/// <summary>
		/// Gets the system color with (R, G, B, A) = (189, 183, 107, 255).
		/// </summary>
		public static Color DarkKhaki
		{
			get
			{
				return new Color((byte) 189, (byte) 183, (byte) 107, byte.MaxValue);
			}
		}

		/// <summary>
		/// Gets the system color with (R, G, B, A) = (139, 0, 139, 255).
		/// </summary>
		public static Color DarkMagenta
		{
			get
			{
				return new Color((byte) 139, (byte) 0, (byte) 139, byte.MaxValue);
			}
		}

		/// <summary>
		/// Gets the system color with (R, G, B, A) = (85, 107, 47, 255).
		/// </summary>
		public static Color DarkOliveGreen
		{
			get
			{
				return new Color((byte) 85, (byte) 107, (byte) 47, byte.MaxValue);
			}
		}

		/// <summary>
		/// Gets the system color with (R, G, B, A) = (255, 140, 0, 255).
		/// </summary>
		public static Color DarkOrange
		{
			get
			{
				return new Color(byte.MaxValue, (byte) 140, (byte) 0, byte.MaxValue);
			}
		}

		/// <summary>
		/// Gets the system color with (R, G, B, A) = (153, 50, 204, 255).
		/// </summary>
		public static Color DarkOrchid
		{
			get
			{
				return new Color((byte) 153, (byte) 50, (byte) 204, byte.MaxValue);
			}
		}

		/// <summary>
		/// Gets the system color with (R, G, B, A) = (139, 0, 0, 255).
		/// </summary>
		public static Color DarkRed
		{
			get
			{
				return new Color((byte) 139, (byte) 0, (byte) 0, byte.MaxValue);
			}
		}

		/// <summary>
		/// Gets the system color with (R, G, B, A) = (233, 150, 122, 255).
		/// </summary>
		public static Color DarkSalmon
		{
			get
			{
				return new Color((byte) 233, (byte) 150, (byte) 122, byte.MaxValue);
			}
		}

		/// <summary>
		/// Gets the system color with (R, G, B, A) = (143, 188, 139, 255).
		/// </summary>
		public static Color DarkSeaGreen
		{
			get
			{
				return new Color((byte) 143, (byte) 188, (byte) 139, byte.MaxValue);
			}
		}

		/// <summary>
		/// Gets the system color with (R, G, B, A) = (72, 61, 139, 255).
		/// </summary>
		public static Color DarkSlateBlue
		{
			get
			{
				return new Color((byte) 72, (byte) 61, (byte) 139, byte.MaxValue);
			}
		}

		/// <summary>
		/// Gets the system color with (R, G, B, A) = (47, 79, 79, 255).
		/// </summary>
		public static Color DarkSlateGray
		{
			get
			{
				return new Color((byte) 47, (byte) 79, (byte) 79, byte.MaxValue);
			}
		}

		/// <summary>
		/// Gets the system color with (R, G, B, A) = (0, 206, 209, 255).
		/// </summary>
		public static Color DarkTurquoise
		{
			get
			{
				return new Color((byte) 0, (byte) 206, (byte) 209, byte.MaxValue);
			}
		}

		/// <summary>
		/// Gets the system color with (R, G, B, A) = (148, 0, 211, 255).
		/// </summary>
		public static Color DarkViolet
		{
			get
			{
				return new Color((byte) 148, (byte) 0, (byte) 211, byte.MaxValue);
			}
		}

		/// <summary>
		/// Gets the system color with (R, G, B, A) = (255, 20, 147, 255).
		/// </summary>
		public static Color DeepPink
		{
			get
			{
				return new Color(byte.MaxValue, (byte) 20, (byte) 147, byte.MaxValue);
			}
		}

		/// <summary>
		/// Gets the system color with (R, G, B, A) = (0, 191, 255, 255).
		/// </summary>
		public static Color DeepSkyBlue
		{
			get
			{
				return new Color((byte) 0, (byte) 191, byte.MaxValue, byte.MaxValue);
			}
		}

		/// <summary>
		/// Gets the system color with (R, G, B, A) = (105, 105, 105, 255).
		/// </summary>
		public static Color DimGray
		{
			get
			{
				return new Color((byte) 105, (byte) 105, (byte) 105, byte.MaxValue);
			}
		}

		/// <summary>
		/// Gets the system color with (R, G, B, A) = (30, 144, 255, 255).
		/// </summary>
		public static Color DodgerBlue
		{
			get
			{
				return new Color((byte) 30, (byte) 144, byte.MaxValue, byte.MaxValue);
			}
		}

		/// <summary>
		/// Gets the system color with (R, G, B, A) = (178, 34, 34, 255).
		/// </summary>
		public static Color Firebrick
		{
			get
			{
				return new Color((byte) 178, (byte) 34, (byte) 34, byte.MaxValue);
			}
		}

		/// <summary>
		/// Gets the system color with (R, G, B, A) = (255, 250, 240, 255).
		/// </summary>
		public static Color FloralWhite
		{
			get
			{
				return new Color(byte.MaxValue, (byte) 250, (byte) 240, byte.MaxValue);
			}
		}

		/// <summary>
		/// Gets the system color with (R, G, B, A) = (34, 139, 34, 255).
		/// </summary>
		public static Color ForestGreen
		{
			get
			{
				return new Color((byte) 34, (byte) 139, (byte) 34, byte.MaxValue);
			}
		}

		/// <summary>
		/// Gets the system color with (R, G, B, A) = (255, 0, 255, 255).
		/// </summary>
		public static Color Fuchsia
		{
			get
			{
				return new Color(byte.MaxValue, (byte) 0, byte.MaxValue, byte.MaxValue);
			}
		}

		/// <summary>
		/// Gets the system color with (R, G, B, A) = (220, 220, 220, 255).
		/// </summary>
		public static Color Gainsboro
		{
			get
			{
				return new Color((byte) 220, (byte) 220, (byte) 220, byte.MaxValue);
			}
		}

		/// <summary>
		/// Gets the system color with (R, G, B, A) = (248, 248, 255, 255).
		/// </summary>
		public static Color GhostWhite
		{
			get
			{
				return new Color((byte) 248, (byte) 248, byte.MaxValue, byte.MaxValue);
			}
		}

		/// <summary>
		/// Gets the system color with (R, G, B, A) = (255, 215, 0, 255).
		/// </summary>
		public static Color Gold
		{
			get
			{
				return new Color(byte.MaxValue, (byte) 215, (byte) 0, byte.MaxValue);
			}
		}

		/// <summary>
		/// Gets the system color with (R, G, B, A) = (218, 165, 32, 255).
		/// </summary>
		public static Color Goldenrod
		{
			get
			{
				return new Color((byte) 218, (byte) 165, (byte) 32, byte.MaxValue);
			}
		}

		/// <summary>
		/// Gets the system color with (R, G, B, A) = (128, 128, 128, 255).
		/// </summary>
		public static Color Gray
		{
			get
			{
				return new Color((byte) 128, (byte) 128, (byte) 128, byte.MaxValue);
			}
		}

		/// <summary>
		/// Gets the system color with (R, G, B, A) = (0, 128, 0, 255).
		/// </summary>
		public static Color Green
		{
			get
			{
				return new Color((byte) 0, (byte) 128, (byte) 0, byte.MaxValue);
			}
		}

		/// <summary>
		/// Gets the system color with (R, G, B, A) = (173, 255, 47, 255).
		/// </summary>
		public static Color GreenYellow
		{
			get
			{
				return new Color((byte) 173, byte.MaxValue, (byte) 47, byte.MaxValue);
			}
		}

		/// <summary>
		/// Gets the system color with (R, G, B, A) = (240, 255, 240, 255).
		/// </summary>
		public static Color Honeydew
		{
			get
			{
				return new Color((byte) 240, byte.MaxValue, (byte) 240, byte.MaxValue);
			}
		}

		/// <summary>
		/// Gets the system color with (R, G, B, A) = (255, 105, 180, 255).
		/// </summary>
		public static Color HotPink
		{
			get
			{
				return new Color(byte.MaxValue, (byte) 105, (byte) 180, byte.MaxValue);
			}
		}

		/// <summary>
		/// Gets the system color with (R, G, B, A) = (205, 92, 92, 255).
		/// </summary>
		public static Color IndianRed
		{
			get
			{
				return new Color((byte) 205, (byte) 92, (byte) 92, byte.MaxValue);
			}
		}

		/// <summary>
		/// Gets the system color with (R, G, B, A) = (75, 0, 130, 255).
		/// </summary>
		public static Color Indigo
		{
			get
			{
				return new Color((byte) 75, (byte) 0, (byte) 130, byte.MaxValue);
			}
		}

		/// <summary>
		/// Gets the system color with (R, G, B, A) = (255, 255, 240, 255).
		/// </summary>
		public static Color Ivory
		{
			get
			{
				return new Color(byte.MaxValue, byte.MaxValue, (byte) 240, byte.MaxValue);
			}
		}

		/// <summary>
		/// Gets the system color with (R, G, B, A) = (240, 230, 140, 255).
		/// </summary>
		public static Color Khaki
		{
			get
			{
				return new Color((byte) 240, (byte) 230, (byte) 140, byte.MaxValue);
			}
		}

		/// <summary>
		/// Gets the system color with (R, G, B, A) = (230, 230, 250, 255).
		/// </summary>
		public static Color Lavender
		{
			get
			{
				return new Color((byte) 230, (byte) 230, (byte) 250, byte.MaxValue);
			}
		}

		/// <summary>
		/// Gets the system color with (R, G, B, A) = (255, 240, 245, 255).
		/// </summary>
		public static Color LavenderBlush
		{
			get
			{
				return new Color(byte.MaxValue, (byte) 240, (byte) 245, byte.MaxValue);
			}
		}

		/// <summary>
		/// Gets the system color with (R, G, B, A) = (124, 252, 0, 255).
		/// </summary>
		public static Color LawnGreen
		{
			get
			{
				return new Color((byte) 124, (byte) 252, (byte) 0, byte.MaxValue);
			}
		}

		/// <summary>
		/// Gets the system color with (R, G, B, A) = (255, 250, 205, 255).
		/// </summary>
		public static Color LemonChiffon
		{
			get
			{
				return new Color(byte.MaxValue, (byte) 250, (byte) 205, byte.MaxValue);
			}
		}

		/// <summary>
		/// Gets the system color with (R, G, B, A) = (173, 216, 230, 255).
		/// </summary>
		public static Color LightBlue
		{
			get
			{
				return new Color((byte) 173, (byte) 216, (byte) 230, byte.MaxValue);
			}
		}

		/// <summary>
		/// Gets the system color with (R, G, B, A) = (240, 128, 128, 255).
		/// </summary>
		public static Color LightCoral
		{
			get
			{
				return new Color((byte) 240, (byte) 128, (byte) 128, byte.MaxValue);
			}
		}

		/// <summary>
		/// Gets the system color with (R, G, B, A) = (224, 255, 255, 255).
		/// </summary>
		public static Color LightCyan
		{
			get
			{
				return new Color((byte) 224, byte.MaxValue, byte.MaxValue, byte.MaxValue);
			}
		}

		/// <summary>
		/// Gets the system color with (R, G, B, A) = (250, 250, 210, 255).
		/// </summary>
		public static Color LightGoldenrodYellow
		{
			get
			{
				return new Color((byte) 250, (byte) 250, (byte) 210, byte.MaxValue);
			}
		}

		/// <summary>
		/// Gets the system color with (R, G, B, A) = (144, 238, 144, 255).
		/// </summary>
		public static Color LightGreen
		{
			get
			{
				return new Color((byte) 144, (byte) 238, (byte) 144, byte.MaxValue);
			}
		}

		/// <summary>
		/// Gets the system color with (R, G, B, A) = (211, 211, 211, 255).
		/// </summary>
		public static Color LightGray
		{
			get
			{
				return new Color((byte) 211, (byte) 211, (byte) 211, byte.MaxValue);
			}
		}

		/// <summary>
		/// Gets the system color with (R, G, B, A) = (255, 182, 193, 255).
		/// </summary>
		public static Color LightPink
		{
			get
			{
				return new Color(byte.MaxValue, (byte) 182, (byte) 193, byte.MaxValue);
			}
		}

		/// <summary>
		/// Gets the system color with (R, G, B, A) = (255, 160, 122, 255).
		/// </summary>
		public static Color LightSalmon
		{
			get
			{
				return new Color(byte.MaxValue, (byte) 160, (byte) 122, byte.MaxValue);
			}
		}

		/// <summary>
		/// Gets the system color with (R, G, B, A) = (32, 178, 170, 255).
		/// </summary>
		public static Color LightSeaGreen
		{
			get
			{
				return new Color((byte) 32, (byte) 178, (byte) 170, byte.MaxValue);
			}
		}

		/// <summary>
		/// Gets the system color with (R, G, B, A) = (135, 206, 250, 255).
		/// </summary>
		public static Color LightSkyBlue
		{
			get
			{
				return new Color((byte) 135, (byte) 206, (byte) 250, byte.MaxValue);
			}
		}

		/// <summary>
		/// Gets the system color with (R, G, B, A) = (119, 136, 153, 255).
		/// </summary>
		public static Color LightSlateGray
		{
			get
			{
				return new Color((byte) 119, (byte) 136, (byte) 153, byte.MaxValue);
			}
		}

		/// <summary>
		/// Gets the system color with (R, G, B, A) = (176, 196, 222, 255).
		/// </summary>
		public static Color LightSteelBlue
		{
			get
			{
				return new Color((byte) 176, (byte) 196, (byte) 222, byte.MaxValue);
			}
		}

		/// <summary>
		/// Gets the system color with (R, G, B, A) = (255, 255, 224, 255).
		/// </summary>
		public static Color LightYellow
		{
			get
			{
				return new Color(byte.MaxValue, byte.MaxValue, (byte) 224, byte.MaxValue);
			}
		}

		/// <summary>
		/// Gets the system color with (R, G, B, A) = (0, 255, 0, 255).
		/// </summary>
		public static Color Lime
		{
			get
			{
				return new Color((byte) 0, byte.MaxValue, (byte) 0, byte.MaxValue);
			}
		}

		/// <summary>
		/// Gets the system color with (R, G, B, A) = (50, 205, 50, 255).
		/// </summary>
		public static Color LimeGreen
		{
			get
			{
				return new Color((byte) 50, (byte) 205, (byte) 50, byte.MaxValue);
			}
		}

		/// <summary>
		/// Gets the system color with (R, G, B, A) = (250, 240, 230, 255).
		/// </summary>
		public static Color Linen
		{
			get
			{
				return new Color((byte) 250, (byte) 240, (byte) 230, byte.MaxValue);
			}
		}

		/// <summary>
		/// Gets the system color with (R, G, B, A) = (255, 0, 255, 255).
		/// </summary>
		public static Color Magenta
		{
			get
			{
				return new Color(byte.MaxValue, (byte) 0, byte.MaxValue, byte.MaxValue);
			}
		}

		/// <summary>
		/// Gets the system color with (R, G, B, A) = (128, 0, 0, 255).
		/// </summary>
		public static Color Maroon
		{
			get
			{
				return new Color((byte) 128, (byte) 0, (byte) 0, byte.MaxValue);
			}
		}

		/// <summary>
		/// Gets the system color with (R, G, B, A) = (102, 205, 170, 255).
		/// </summary>
		public static Color MediumAquamarine
		{
			get
			{
				return new Color((byte) 102, (byte) 205, (byte) 170, byte.MaxValue);
			}
		}

		/// <summary>
		/// Gets the system color with (R, G, B, A) = (0, 0, 205, 255).
		/// </summary>
		public static Color MediumBlue
		{
			get
			{
				return new Color((byte) 0, (byte) 0, (byte) 205, byte.MaxValue);
			}
		}

		/// <summary>
		/// Gets the system color with (R, G, B, A) = (186, 85, 211, 255).
		/// </summary>
		public static Color MediumOrchid
		{
			get
			{
				return new Color((byte) 186, (byte) 85, (byte) 211, byte.MaxValue);
			}
		}

		/// <summary>
		/// Gets the system color with (R, G, B, A) = (147, 112, 219, 255).
		/// </summary>
		public static Color MediumPurple
		{
			get
			{
				return new Color((byte) 147, (byte) 112, (byte) 219, byte.MaxValue);
			}
		}

		/// <summary>
		/// Gets the system color with (R, G, B, A) = (60, 179, 113, 255).
		/// </summary>
		public static Color MediumSeaGreen
		{
			get
			{
				return new Color((byte) 60, (byte) 179, (byte) 113, byte.MaxValue);
			}
		}

		/// <summary>
		/// Gets the system color with (R, G, B, A) = (123, 104, 238, 255).
		/// </summary>
		public static Color MediumSlateBlue
		{
			get
			{
				return new Color((byte) 123, (byte) 104, (byte) 238, byte.MaxValue);
			}
		}

		/// <summary>
		/// Gets the system color with (R, G, B, A) = (0, 250, 154, 255).
		/// </summary>
		public static Color MediumSpringGreen
		{
			get
			{
				return new Color((byte) 0, (byte) 250, (byte) 154, byte.MaxValue);
			}
		}

		/// <summary>
		/// Gets the system color with (R, G, B, A) = (72, 209, 204, 255).
		/// </summary>
		public static Color MediumTurquoise
		{
			get
			{
				return new Color((byte) 72, (byte) 209, (byte) 204, byte.MaxValue);
			}
		}

		/// <summary>
		/// Gets the system color with (R, G, B, A) = (199, 21, 133, 255).
		/// </summary>
		public static Color MediumVioletRed
		{
			get
			{
				return new Color((byte) 199, (byte) 21, (byte) 133, byte.MaxValue);
			}
		}

		/// <summary>
		/// Gets the system color with (R, G, B, A) = (25, 25, 112, 255).
		/// </summary>
		public static Color MidnightBlue
		{
			get
			{
				return new Color((byte) 25, (byte) 25, (byte) 112, byte.MaxValue);
			}
		}

		/// <summary>
		/// Gets the system color with (R, G, B, A) = (245, 255, 250, 255).
		/// </summary>
		public static Color MintCream
		{
			get
			{
				return new Color((byte) 245, byte.MaxValue, (byte) 250, byte.MaxValue);
			}
		}

		/// <summary>
		/// Gets the system color with (R, G, B, A) = (255, 228, 225, 255).
		/// </summary>
		public static Color MistyRose
		{
			get
			{
				return new Color(byte.MaxValue, (byte) 228, (byte) 225, byte.MaxValue);
			}
		}

		/// <summary>
		/// Gets the system color with (R, G, B, A) = (255, 228, 181, 255).
		/// </summary>
		public static Color Moccasin
		{
			get
			{
				return new Color(byte.MaxValue, (byte) 228, (byte) 181, byte.MaxValue);
			}
		}

		/// <summary>
		/// Gets the system color with (R, G, B, A) = (255, 222, 173, 255).
		/// </summary>
		public static Color NavajoWhite
		{
			get
			{
				return new Color(byte.MaxValue, (byte) 222, (byte) 173, byte.MaxValue);
			}
		}

		/// <summary>
		/// Gets the system color with (R, G, B, A) = (0, 0, 128, 255).
		/// </summary>
		public static Color Navy
		{
			get
			{
				return new Color((byte) 0, (byte) 0, (byte) 128, byte.MaxValue);
			}
		}

		/// <summary>
		/// Gets the system color with (R, G, B, A) = (253, 245, 230, 255).
		/// </summary>
		public static Color OldLace
		{
			get
			{
				return new Color((byte) 253, (byte) 245, (byte) 230, byte.MaxValue);
			}
		}

		/// <summary>
		/// Gets the system color with (R, G, B, A) = (128, 128, 0, 255).
		/// </summary>
		public static Color Olive
		{
			get
			{
				return new Color((byte) 128, (byte) 128, (byte) 0, byte.MaxValue);
			}
		}

		/// <summary>
		/// Gets the system color with (R, G, B, A) = (107, 142, 35, 255).
		/// </summary>
		public static Color OliveDrab
		{
			get
			{
				return new Color((byte) 107, (byte) 142, (byte) 35, byte.MaxValue);
			}
		}

		/// <summary>
		/// Gets the system color with (R, G, B, A) = (255, 165, 0, 255).
		/// </summary>
		public static Color Orange
		{
			get
			{
				return new Color(byte.MaxValue, (byte) 165, (byte) 0, byte.MaxValue);
			}
		}

		/// <summary>
		/// Gets the system color with (R, G, B, A) = (255, 69, 0, 255).
		/// </summary>
		public static Color OrangeRed
		{
			get
			{
				return new Color(byte.MaxValue, (byte) 69, (byte) 0, byte.MaxValue);
			}
		}

		/// <summary>
		/// Gets the system color with (R, G, B, A) = (218, 112, 214, 255).
		/// </summary>
		public static Color Orchid
		{
			get
			{
				return new Color((byte) 218, (byte) 112, (byte) 214, byte.MaxValue);
			}
		}

		/// <summary>
		/// Gets the system color with (R, G, B, A) = (238, 232, 170, 255).
		/// </summary>
		public static Color PaleGoldenrod
		{
			get
			{
				return new Color((byte) 238, (byte) 232, (byte) 170, byte.MaxValue);
			}
		}

		/// <summary>
		/// Gets the system color with (R, G, B, A) = (152, 251, 152, 255).
		/// </summary>
		public static Color PaleGreen
		{
			get
			{
				return new Color((byte) 152, (byte) 251, (byte) 152, byte.MaxValue);
			}
		}

		/// <summary>
		/// Gets the system color with (R, G, B, A) = (175, 238, 238, 255).
		/// </summary>
		public static Color PaleTurquoise
		{
			get
			{
				return new Color((byte) 175, (byte) 238, (byte) 238, byte.MaxValue);
			}
		}

		/// <summary>
		/// Gets the system color with (R, G, B, A) = (219, 112, 147, 255).
		/// </summary>
		public static Color PaleVioletRed
		{
			get
			{
				return new Color((byte) 219, (byte) 112, (byte) 147, byte.MaxValue);
			}
		}

		/// <summary>
		/// Gets the system color with (R, G, B, A) = (255, 239, 213, 255).
		/// </summary>
		public static Color PapayaWhip
		{
			get
			{
				return new Color(byte.MaxValue, (byte) 239, (byte) 213, byte.MaxValue);
			}
		}

		/// <summary>
		/// Gets the system color with (R, G, B, A) = (255, 218, 185, 255).
		/// </summary>
		public static Color PeachPuff
		{
			get
			{
				return new Color(byte.MaxValue, (byte) 218, (byte) 185, byte.MaxValue);
			}
		}

		/// <summary>
		/// Gets the system color with (R, G, B, A) = (205, 133, 63, 255).
		/// </summary>
		public static Color Peru
		{
			get
			{
				return new Color((byte) 205, (byte) 133, (byte) 63, byte.MaxValue);
			}
		}

		/// <summary>
		/// Gets the system color with (R, G, B, A) = (255, 192, 203, 255).
		/// </summary>
		public static Color Pink
		{
			get
			{
				return new Color(byte.MaxValue, (byte) 192, (byte) 203, byte.MaxValue);
			}
		}

		/// <summary>
		/// Gets the system color with (R, G, B, A) = (221, 160, 221, 255).
		/// </summary>
		public static Color Plum
		{
			get
			{
				return new Color((byte) 221, (byte) 160, (byte) 221, byte.MaxValue);
			}
		}

		/// <summary>
		/// Gets the system color with (R, G, B, A) = (176, 224, 230, 255).
		/// </summary>
		public static Color PowderBlue
		{
			get
			{
				return new Color((byte) 176, (byte) 224, (byte) 230, byte.MaxValue);
			}
		}

		/// <summary>
		/// Gets the system color with (R, G, B, A) = (128, 0, 128, 255).
		/// </summary>
		public static Color Purple
		{
			get
			{
				return new Color((byte) 128, (byte) 0, (byte) 128, byte.MaxValue);
			}
		}

		/// <summary>
		/// Gets the system color with (R, G, B, A) = (255, 0, 0, 255).
		/// </summary>
		public static Color Red
		{
			get
			{
				return new Color(byte.MaxValue, (byte) 0, (byte) 0, byte.MaxValue);
			}
		}

		/// <summary>
		/// Gets the system color with (R, G, B, A) = (188, 143, 143, 255).
		/// </summary>
		public static Color RosyBrown
		{
			get
			{
				return new Color((byte) 188, (byte) 143, (byte) 143, byte.MaxValue);
			}
		}

		/// <summary>
		/// Gets the system color with (R, G, B, A) = (65, 105, 225, 255).
		/// </summary>
		public static Color RoyalBlue
		{
			get
			{
				return new Color((byte) 65, (byte) 105, (byte) 225, byte.MaxValue);
			}
		}

		/// <summary>
		/// Gets the system color with (R, G, B, A) = (139, 69, 19, 255).
		/// </summary>
		public static Color SaddleBrown
		{
			get
			{
				return new Color((byte) 139, (byte) 69, (byte) 19, byte.MaxValue);
			}
		}

		/// <summary>
		/// Gets the system color with (R, G, B, A) = (250, 128, 114, 255).
		/// </summary>
		public static Color Salmon
		{
			get
			{
				return new Color((byte) 250, (byte) 128, (byte) 114, byte.MaxValue);
			}
		}

		/// <summary>
		/// Gets the system color with (R, G, B, A) = (244, 164, 96, 255).
		/// </summary>
		public static Color SandyBrown
		{
			get
			{
				return new Color((byte) 244, (byte) 164, (byte) 96, byte.MaxValue);
			}
		}

		/// <summary>
		/// Gets the system color with (R, G, B, A) = (46, 139, 87, 255).
		/// </summary>
		public static Color SeaGreen
		{
			get
			{
				return new Color((byte) 46, (byte) 139, (byte) 87, byte.MaxValue);
			}
		}

		/// <summary>
		/// Gets the system color with (R, G, B, A) = (255, 245, 238, 255).
		/// </summary>
		public static Color SeaShell
		{
			get
			{
				return new Color(byte.MaxValue, (byte) 245, (byte) 238, byte.MaxValue);
			}
		}

		/// <summary>
		/// Gets the system color with (R, G, B, A) = (160, 82, 45, 255).
		/// </summary>
		public static Color Sienna
		{
			get
			{
				return new Color((byte) 160, (byte) 82, (byte) 45, byte.MaxValue);
			}
		}

		/// <summary>
		/// Gets the system color with (R, G, B, A) = (192, 192, 192, 255).
		/// </summary>
		public static Color Silver
		{
			get
			{
				return new Color((byte) 192, (byte) 192, (byte) 192, byte.MaxValue);
			}
		}

		/// <summary>
		/// Gets the system color with (R, G, B, A) = (135, 206, 235, 255).
		/// </summary>
		public static Color SkyBlue
		{
			get
			{
				return new Color((byte) 135, (byte) 206, (byte) 235, byte.MaxValue);
			}
		}

		/// <summary>
		/// Gets the system color with (R, G, B, A) = (106, 90, 205, 255).
		/// </summary>
		public static Color SlateBlue
		{
			get
			{
				return new Color((byte) 106, (byte) 90, (byte) 205, byte.MaxValue);
			}
		}

		/// <summary>
		/// Gets the system color with (R, G, B, A) = (112, 128, 144, 255).
		/// </summary>
		public static Color SlateGray
		{
			get
			{
				return new Color((byte) 112, (byte) 128, (byte) 144, byte.MaxValue);
			}
		}

		/// <summary>
		/// Gets the system color with (R, G, B, A) = (255, 250, 250, 255).
		/// </summary>
		public static Color Snow
		{
			get
			{
				return new Color(byte.MaxValue, (byte) 250, (byte) 250, byte.MaxValue);
			}
		}

		/// <summary>
		/// Gets the system color with (R, G, B, A) = (0, 255, 127, 255).
		/// </summary>
		public static Color SpringGreen
		{
			get
			{
				return new Color((byte) 0, byte.MaxValue, (byte) 127, byte.MaxValue);
			}
		}

		/// <summary>
		/// Gets the system color with (R, G, B, A) = (70, 130, 180, 255).
		/// </summary>
		public static Color SteelBlue
		{
			get
			{
				return new Color((byte) 70, (byte) 130, (byte) 180, byte.MaxValue);
			}
		}

		/// <summary>
		/// Gets the system color with (R, G, B, A) = (210, 180, 140, 255).
		/// </summary>
		public static Color Tan
		{
			get
			{
				return new Color((byte) 210, (byte) 180, (byte) 140, byte.MaxValue);
			}
		}

		/// <summary>
		/// Gets the system color with (R, G, B, A) = (0, 128, 128, 255).
		/// </summary>
		public static Color Teal
		{
			get
			{
				return new Color((byte) 0, (byte) 128, (byte) 128, byte.MaxValue);
			}
		}

		/// <summary>
		/// Gets the system color with (R, G, B, A) = (216, 191, 216, 255).
		/// </summary>
		public static Color Thistle
		{
			get
			{
				return new Color((byte) 216, (byte) 191, (byte) 216, byte.MaxValue);
			}
		}

		/// <summary>
		/// Gets the system color with (R, G, B, A) = (255, 99, 71, 255).
		/// </summary>
		public static Color Tomato
		{
			get
			{
				return new Color(byte.MaxValue, (byte) 99, (byte) 71, byte.MaxValue);
			}
		}

		/// <summary>
		/// Gets the system color with (R, G, B, A) = (64, 224, 208, 255).
		/// </summary>
		public static Color Turquoise
		{
			get
			{
				return new Color((byte) 64, (byte) 224, (byte) 208, byte.MaxValue);
			}
		}

		/// <summary>
		/// Gets the system color with (R, G, B, A) = (238, 130, 238, 255).
		/// </summary>
		public static Color Violet
		{
			get
			{
				return new Color((byte) 238, (byte) 130, (byte) 238, byte.MaxValue);
			}
		}

		/// <summary>
		/// Gets the system color with (R, G, B, A) = (245, 222, 179, 255).
		/// </summary>
		public static Color Wheat
		{
			get
			{
				return new Color((byte) 245, (byte) 222, (byte) 179, byte.MaxValue);
			}
		}

		/// <summary>
		/// Gets the system color with (R, G, B, A) = (255, 255, 255, 255).
		/// </summary>
		public static Color White
		{
			get
			{
				return new Color(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue);
			}
		}

		/// <summary>
		/// Gets the system color with (R, G, B, A) = (245, 245, 245, 255).
		/// </summary>
		public static Color WhiteSmoke
		{
			get
			{
				return new Color((byte) 245, (byte) 245, (byte) 245, byte.MaxValue);
			}
		}

		/// <summary>
		/// Gets the system color with (R, G, B, A) = (255, 255, 0, 255).
		/// </summary>
		public static Color Yellow
		{
			get
			{
				return new Color(byte.MaxValue, byte.MaxValue, (byte) 0, byte.MaxValue);
			}
		}

		/// <summary>
		/// Gets the system color with (R, G, B, A) = (154, 205, 50, 255).
		/// </summary>
		public static Color YellowGreen
		{
			get
			{
				return new Color((byte) 154, (byte) 205, (byte) 50, byte.MaxValue);
			}
		}*/

		public static Color FromSrgb(Color srgb)
		{
			return new Color((double)srgb.R > 0.0404499992728233 ? (float)Math.Pow((srgb.R + 0.0549999997019768) / 1.05499994754791, 2.40000009536743) : srgb.R / 12.92f, (double)srgb.G > 0.0404499992728233 ? (float)Math.Pow((srgb.G + 0.0549999997019768) / 1.05499994754791, 2.40000009536743) : srgb.G / 12.92f, (double)srgb.B > 0.0404499992728233 ? (float)Math.Pow((srgb.B + 0.0549999997019768) / 1.05499994754791, 2.40000009536743) : srgb.B / 12.92f, srgb.A);
		}

		public static Color ToSrgb(Color rgb)
		{
			return new Color((double)rgb.R > 0.0031308 ? (float)(1.05499994754791 * Math.Pow(rgb.R, 0.416666656732559) - 0.0549999997019768) : 12.92f * rgb.R, (double)rgb.G > 0.0031308 ? (float)(1.05499994754791 * Math.Pow(rgb.G, 0.416666656732559) - 0.0549999997019768) : 12.92f * rgb.G, (double)rgb.B > 0.0031308 ? (float)(1.05499994754791 * Math.Pow(rgb.B, 0.416666656732559) - 0.0549999997019768) : 12.92f * rgb.B, rgb.A);
		}

		public static Color FromHsl(Vector4 hsl)
		{
			float num1 = hsl.X * 360f;
			float y = hsl.Y;
			float z = hsl.Z;
			float num2 = (1f - Math.Abs((float)(2.0 * z - 1.0))) * y;
			float num3 = num1 / 60f;
			float num4 = num2 * (1f - Math.Abs((float)(num3 % 2.0 - 1.0)));
			float num5;
			float num6;
			float num7;
			if (0.0 <= num3 && num3 < 1.0)
			{
				num5 = num2;
				num6 = num4;
				num7 = 0.0f;
			}
			else if (1.0 <= num3 && num3 < 2.0)
			{
				num5 = num4;
				num6 = num2;
				num7 = 0.0f;
			}
			else if (2.0 <= num3 && num3 < 3.0)
			{
				num5 = 0.0f;
				num6 = num2;
				num7 = num4;
			}
			else if (3.0 <= num3 && num3 < 4.0)
			{
				num5 = 0.0f;
				num6 = num4;
				num7 = num2;
			}
			else if (4.0 <= num3 && num3 < 5.0)
			{
				num5 = num4;
				num6 = 0.0f;
				num7 = num2;
			}
			else if (5.0 <= num3 && num3 < 6.0)
			{
				num5 = num2;
				num6 = 0.0f;
				num7 = num4;
			}
			else
			{
				num5 = 0.0f;
				num6 = 0.0f;
				num7 = 0.0f;
			}

			float num8 = z - num2 / 2f;
			return new Color(num5 + num8, num6 + num8, num7 + num8, hsl.W);
		}

		public static Vector4 ToHsl(Color rgb)
		{
			float num1 = Math.Max(rgb.R, Math.Max(rgb.G, rgb.B));
			float num2 = Math.Min(rgb.R, Math.Min(rgb.G, rgb.B));
			float num3 = num1 - num2;
			float num4 = 0.0f;
			if (num1 == (double)rgb.R) num4 = (rgb.G - rgb.B) / num3;
			else if (num1 == (double)rgb.G) num4 = (float)((rgb.B - (double)rgb.R) / num3 + 2.0);
			else if (num1 == (double)rgb.B) num4 = (float)((rgb.R - (double)rgb.G) / num3 + 4.0);
			float x = num4 / 6f;
			if (x < 0.0) ++x;
			float z = (float)((num1 + (double)num2) / 2.0);
			float y = 0.0f;
			if (0.0 != z && z != 1.0) y = num3 / (1f - Math.Abs((float)(2.0 * z - 1.0)));
			return new Vector4(x, y, z, rgb.A);
		}

		public static Color FromHsv(Vector4 hsv) => FromHsv(hsv.X, hsv.Y, hsv.Z, hsv.W);

		public static Color FromHsv(float hue, float saturation, float value, float alpha)
		{
			float num1 = hue * 360f;
			float y = saturation;
			float z = value;
			float num2 = z * y;
			float num3 = num1 / 60f;
			float num4 = num2 * (1f - Math.Abs((float)(num3 % 2.0 - 1.0)));
			float num5;
			float num6;
			float num7;
			if (0.0 <= num3 && num3 < 1.0)
			{
				num5 = num2;
				num6 = num4;
				num7 = 0.0f;
			}
			else if (1.0 <= num3 && num3 < 2.0)
			{
				num5 = num4;
				num6 = num2;
				num7 = 0.0f;
			}
			else if (2.0 <= num3 && num3 < 3.0)
			{
				num5 = 0.0f;
				num6 = num2;
				num7 = num4;
			}
			else if (3.0 <= num3 && num3 < 4.0)
			{
				num5 = 0.0f;
				num6 = num4;
				num7 = num2;
			}
			else if (4.0 <= num3 && num3 < 5.0)
			{
				num5 = num4;
				num6 = 0.0f;
				num7 = num2;
			}
			else if (5.0 <= num3 && num3 < 6.0)
			{
				num5 = num2;
				num6 = 0.0f;
				num7 = num4;
			}
			else
			{
				num5 = 0.0f;
				num6 = 0.0f;
				num7 = 0.0f;
			}

			float num8 = z - num2;
			return new Color(num5 + num8, num6 + num8, num7 + num8, alpha);
		}
		
		public static Vector4 ToHsv(Color rgb)
		{
			float z = Math.Max(rgb.R, Math.Max(rgb.G, rgb.B));
			float num1 = Math.Min(rgb.R, Math.Min(rgb.G, rgb.B));
			float num2 = z - num1;
			float num3 = 0.0f;
			if (z == (double)rgb.R) num3 = (float)((rgb.G - (double)rgb.B) / num2 % 6.0);
			else if (z == (double)rgb.G) num3 = (float)((rgb.B - (double)rgb.R) / num2 + 2.0);
			else if (z == (double)rgb.B) num3 = (float)((rgb.R - (double)rgb.G) / num2 + 4.0);
			float x = (float)(num3 * 60.0 / 360.0);
			float y = 0.0f;
			if (0.0 != z) y = num2 / z;
			return new Vector4(x, y, z, rgb.A);
		}

		public static Color FromXyz(Vector4 xyz)
		{
			return new Color((float)(0.418469995260239 * xyz.X + -0.158659994602203 * xyz.Y + -0.0828349962830544 * xyz.Z), (float)(-0.091168999671936 * xyz.X + 0.252429991960526 * xyz.Y + 0.0157079994678497 * xyz.Z), (float)(0.000920900027267635 * xyz.X + -0.00254980009049177 * xyz.Y + 0.178599998354912 * xyz.Z), xyz.W);
		}

		/// <summary>Converts RGB color values to XYZ color values.</summary>
		/// <returns>
		///     Returns the converted color value with the trisimulus values of X, Y, and Z in the corresponding element, and the W
		///     element with Alpha (a copy of the input's Alpha value).
		///     Each has a range of 0.0 to 1.0.
		/// </returns>
		/// <param name="rgb">Color value to convert.</param>
		/// <remarks>Uses the CIE XYZ colorspace.</remarks>
		public static Vector4 ToXyz(Color rgb)
		{
			return new Vector4((float)((0.490000009536743 * rgb.R + 0.310000002384186 * rgb.G + 0.200000002980232 * rgb.B) / 0.1769700050354), (float)((0.1769700050354 * rgb.R + 0.812399983406067 * rgb.G + 0.0106300003826618 * rgb.B) / 0.1769700050354), (float)((0.0 * rgb.R + 0.00999999977648258 * rgb.G + 0.990000009536743 * rgb.B) / 0.1769700050354), rgb.A);
		}

		/// <summary>Converts YCbCr color values to RGB color values.</summary>
		/// <returns>Returns the converted color value.</returns>
		/// <param name="ycbcr">
		///     Color value to convert in Luma-Chrominance (YCbCr) aka YUV.
		///     The X element contains Luma (Y, 0.0 to 1.0), the Y element contains Blue-difference chroma (U, -0.5 to 0.5), the Z
		///     element contains the Red-difference chroma (V, -0.5 to 0.5), and the W element contains the Alpha (which is copied
		///     to the output's Alpha value).
		/// </param>
		/// <remarks>Converts using ITU-R BT.601/CCIR 601 W(r) = 0.299 W(b) = 0.114 U(max) = 0.436 V(max) = 0.615.</remarks>
		public static Color FromYcbcr(Vector4 ycbcr)
		{
			return new Color((float)(1.0 * ycbcr.X + 0.0 * ycbcr.Y + 1.40199995040894 * ycbcr.Z), (float)(1.0 * ycbcr.X + -0.344135999679565 * ycbcr.Y + -0.714136004447937 * ycbcr.Z), (float)(1.0 * ycbcr.X + 1.77199995517731 * ycbcr.Y + 0.0 * ycbcr.Z), ycbcr.W);
		}

		/// <summary>Converts RGB color values to YUV color values.</summary>
		/// <returns>
		///     Returns the converted color value in Luma-Chrominance (YCbCr) aka YUV.
		///     The X element contains Luma (Y, 0.0 to 1.0), the Y element contains Blue-difference chroma (U, -0.5 to 0.5), the Z
		///     element contains the Red-difference chroma (V, -0.5 to 0.5), and the W element contains the Alpha (a copy of the
		///     input's Alpha value).
		///     Each has a range of 0.0 to 1.0.
		/// </returns>
		/// <param name="rgb">Color value to convert.</param>
		/// <remarks>Converts using ITU-R BT.601/CCIR 601 W(r) = 0.299 W(b) = 0.114 U(max) = 0.436 V(max) = 0.615.</remarks>
		public static Vector4 ToYcbcr(Color rgb)
		{
			return new Vector4((float)(0.29899999499321 * rgb.R + 0.587000012397766 * rgb.G + 57.0 / 500.0 * rgb.B), (float)(-0.16873599588871 * rgb.R + -0.331263989210129 * rgb.G + 0.5 * rgb.B), (float)(0.5 * rgb.R + -0.418687999248505 * rgb.G + -0.0813120007514954 * rgb.B), rgb.A);
		}

		/// <summary>Converts HCY color values to RGB color values.</summary>
		/// <returns>Returns the converted color value.</returns>
		/// <param name="hcy">
		///     Color value to convert in hue, chroma, luminance (HCY).
		///     The X element is Hue (H), the Y element is Chroma (C), the Z element is luminance (Y), and the W element is Alpha
		///     (which is copied to the output's Alpha value).
		///     Each has a range of 0.0 to 1.0.
		/// </param>
		public static Color FromHcy(Vector4 hcy)
		{
			float num1 = hcy.X * 360f;
			float y = hcy.Y;
			float z = hcy.Z;
			float num2 = num1 / 60f;
			float num3 = y * (1f - Math.Abs((float)(num2 % 2.0 - 1.0)));
			float num4;
			float num5;
			float num6;
			if (0.0 <= num2 && num2 < 1.0)
			{
				num4 = y;
				num5 = num3;
				num6 = 0.0f;
			}
			else if (1.0 <= num2 && num2 < 2.0)
			{
				num4 = num3;
				num5 = y;
				num6 = 0.0f;
			}
			else if (2.0 <= num2 && num2 < 3.0)
			{
				num4 = 0.0f;
				num5 = y;
				num6 = num3;
			}
			else if (3.0 <= num2 && num2 < 4.0)
			{
				num4 = 0.0f;
				num5 = num3;
				num6 = y;
			}
			else if (4.0 <= num2 && num2 < 5.0)
			{
				num4 = num3;
				num5 = 0.0f;
				num6 = y;
			}
			else if (5.0 <= num2 && num2 < 6.0)
			{
				num4 = y;
				num5 = 0.0f;
				num6 = num3;
			}
			else
			{
				num4 = 0.0f;
				num5 = 0.0f;
				num6 = 0.0f;
			}

			float num7 = z - (float)(0.300000011920929 * num4 + 0.589999973773956 * num5 + 0.109999999403954 * num6);
			return new Color(num4 + num7, num5 + num7, num6 + num7, hcy.W);
		}

		/// <summary>Converts RGB color values to HCY color values.</summary>
		/// <returns>
		///     Returns the converted color value.
		///     The X element is Hue (H), the Y element is Chroma (C), the Z element is luminance (Y), and the W element is Alpha
		///     (a copy of the input's Alpha value).
		///     Each has a range of 0.0 to 1.0.
		/// </returns>
		/// <param name="rgb">Color value to convert.</param>
		public static Vector4 ToHcy(Color rgb)
		{
			float num1 = Math.Max(rgb.R, Math.Max(rgb.G, rgb.B));
			float num2 = Math.Min(rgb.R, Math.Min(rgb.G, rgb.B));
			float y = num1 - num2;
			float num3 = 0.0f;
			if (num1 == (double)rgb.R) num3 = (float)((rgb.G - (double)rgb.B) / y % 6.0);
			else if (num1 == (double)rgb.G) num3 = (float)((rgb.B - (double)rgb.R) / y + 2.0);
			else if (num1 == (double)rgb.B) num3 = (float)((rgb.R - (double)rgb.G) / y + 4.0);
			float x = (float)(num3 * 60.0 / 360.0);
			float z = (float)(0.300000011920929 * rgb.R + 0.589999973773956 * rgb.G + 0.109999999403954 * rgb.B);
			return new Vector4(x, y, z, rgb.A);
		}

		/// <summary>
		///     Compares whether this Color structure is equal to the specified Color.
		/// </summary>
		/// <param name="other">The Color structure to compare to.</param>
		/// <returns>True if both Color structures contain the same components; false otherwise.</returns>
		public bool Equals(Color other)
		{
			return R == (double)other.R && G == (double)other.G && B == (double)other.B && A == (double)other.A;
		}

		public static Color operator *(Color color, float scalar) => new Color(color.R * scalar, color.G * scalar, color.B * scalar, color.A);
		
		public static Color Lerp(Color a, Color b, float blend)
		{
			blend = MathHelper.Clamp(blend, 0f, 1f);
			
			a.R = blend * (b.R - a.R) + a.R;
			a.G = blend * (b.G - a.G) + a.G;
			a.B = blend * (b.B - a.B) + a.B;
			a.A = blend * (b.A - a.A) + a.A;
			return a;
		}
	}
}