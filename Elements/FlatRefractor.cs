namespace Raytracer.Elements
{
	public class FlatRefractor : BaseElement
	{
		public FlatRefractor()
		{
			RefractiveIndex = Constants.RefractiveIndexes.Glass;
		}

		public override Base.Vector2 GetTransformation(float initial, float final) => new Base.Vector2(0, initial / final);
	}
}