using Base;
using System;

namespace Raytracer.Elements
{
	public class FlatRefractor : BaseElement
	{
		public FlatRefractor()
		{
			RefractiveIndex = Constants.RefractiveIndexes.Glass;

			Size = new Vector2(80f, 200f);
		}

		// public override Vector2 GetTransformation(float initial, float final) => new Vector2(0, initial / final);

		public override float GetAngle(float incoming, float initial, float final) => MathF.Asin(initial * MathF.Sin(incoming) / final);
	}
}