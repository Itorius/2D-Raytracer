using Base;
using OpenTK;

namespace Raytracer
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

		protected Color Color = new Color(100, 149, 237, 100);
		protected Color ColorSelected = new Color(100, 149, 237, 150);

		public Polygon collider => new Polygon(position - size * 0.5f, size);

		public float RefractiveIndex = 1;

		public abstract Base.Vector2 GetTransformation(float initial, float final);

		public virtual void Update()
		{
		}

		public virtual void Draw()
		{
			var color = Color.Lerp(Color, ColorSelected, GameLayer.Instance.SelectedElement == this ? Utility.UnsignedSin(Time.TotalDrawTime * 3f) : 0f);
			Renderer2D.DrawQuad(position, size, color, quaternion);
		}

		public bool ContainsPoint(Base.Vector2 point)
		{
			return Collision.PointPolygon(collider, point);
		}

		public virtual BaseElement Clone()
		{
			BaseElement element = (BaseElement)MemberwiseClone();
			return element;
		}
	}
}