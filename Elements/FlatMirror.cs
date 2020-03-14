namespace Raytracer
{
	public class FlatMirror : BaseElement
	{
		public override Base.Vector2 GetTransformation(float initial, float final) => new Base.Vector2(0f, -1f);
	}
}