using Base;
using OpenTK;

namespace Raytracer.Elements
{
	public abstract class BaseElement
	{
		public Base.Vector2 size;
		public Base.Vector2 position;

		protected Quaternion quaternion = Quaternion.Identity;

		private float _rotation;

		public float Rotation
		{
			get => _rotation;
			set
			{
				_rotation = value;
				quaternion = Quaternion.FromAxisAngle(Vector3.UnitZ, value);
			}
		}

		private Matrix4 localtoworld => Matrix4.CreateRotationZ(_rotation) * Matrix4.CreateTranslation(position.X, position.Y, 0f);

		protected Color Color = new Color(100, 149, 237, 100);
		protected Color ColorSelected = new Color(100, 149, 237, 150);

		public Polygon collider => new Polygon(-size * 0.5f, size);

		public Polygon rotatedCollider => Polygon.Transform(collider, localtoworld);

		public float RefractiveIndex = 1;
		public bool selected;

		public abstract Base.Vector2 GetTransformation(float initial, float final);

		public virtual void Update()
		{
		}

		public virtual void Draw()
		{
			var color = Color.Lerp(Color, ColorSelected, selected ? Utility.UnsignedSin(Time.TotalDrawTime * 3f) : 0f);
			Renderer2D.DrawQuad(position, size, color, quaternion);
		}

		public bool ContainsPoint(Base.Vector2 point)
		{
			return Collision.PointPolygon(Polygon.Transform(collider, localtoworld), point);
		}

		public virtual BaseElement Clone()
		{
			BaseElement element = (BaseElement)MemberwiseClone();
			return element;
		}
	}
}