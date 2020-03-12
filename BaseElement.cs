using Base;
using OpenTK;
using OpenTK.Graphics;

namespace Raytracer
{
	public abstract class BaseElement
	{
		public Base.Vector2 size;
		public Base.Vector2 position;
		public float rotation;
		public Color4 color;

		public Polygon collider => new Polygon(position - size * 0.5f, size);

		public float RefractiveIndex = 1;

		public abstract Base.Vector2 GetTransformation(float initial, float final);
		
		// public abstract Matrix4 GetTransformation(float initial, float final);

		public virtual void Update()
		{
		}

		public virtual void Draw()
		{
			Renderer2D.DrawQuad(position, size, color, Quaternion.FromAxisAngle(Vector3.UnitZ, rotation));
		}
	}
}