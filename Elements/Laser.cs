using Base;
using OpenTK.Graphics;
using System.Collections.Generic;

namespace Raytracer.Elements
{
	public abstract class Laser : BaseElement
	{
		public override bool BlocksRays => true;

		protected List<(Vector2, Vector2)> normals = new List<(Vector2, Vector2)>();
		public const float MaxDistance = 1000000f;

		public Laser()
		{
			Size = new Vector2(100f, 20f);
		}

		public override BaseElement Clone()
		{
			Laser element = (Laser)MemberwiseClone();
			element.normals = new List<(Vector2, Vector2)>();
			return element;
		}

		public virtual void DrawRay()
		{
		}

		public override void Draw()
		{
			var color = Color.Lerp(Color, Color * 0.7f, selected ? Utility.UnsignedSin(Time.TotalDrawTime * 3f) : 0f);
			Renderer2D.DrawQuad(Position, Size, color, quaternion);

			if (Game.DebugDraw)
			{
				foreach ((Vector2 point, Vector2 normal) in normals) Renderer2D.DrawLine(point, point + normal * 50f, Color4.Yellow);
			}
		}

		/// <param name="wavelength">Wavelength in nanometers</param>
		public static void HandleRefractiveIndices(BaseElement lastCollided, BaseElement element, double wavelength, out float initial, out float final)
		{
			wavelength *= 0.001;
			float rIndex = (float)PhysicsUtility.GetRefractiveIndex(element.material, wavelength);
			
			if (lastCollided == element)
			{
				initial = rIndex;
				final = 1.000293f;
			}
			else
			{
				initial = 1.000293f;
				final = rIndex;
			}
		}
	}
}