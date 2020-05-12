using OpenTK;
using System;

namespace Raytracer.Elements
{
	public abstract class Refractor : BaseElement
	{
	}

	public class FlatRefractor : Refractor
	{
		public FlatRefractor() => Size = new Base.Vector2(80f, 200f);

		// public override Vector2 GetTransformation(float initial, float final) => new Vector2(0, initial / final);

		public override float GetAngle(float incoming, float initial, float final)
		{
			float angle = MathF.Asin(initial * MathF.Sin(incoming) / final);
			return float.IsNaN(angle) ? MathF.PI-incoming : angle;
		}
	}
}