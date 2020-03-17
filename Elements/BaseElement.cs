using Base;

namespace Raytracer.Elements
{
	public abstract class BaseElement
	{
		public Quaternion quaternion = Quaternion.Identity;

		public virtual bool BlocksRays => false;

		#region Backing fields
		private Vector2 _position;
		private Vector2 _size;
		private float _rotation;
		#endregion

		public Vector2 Position
		{
			get => _position;
			set
			{
				_position = value;
				collider = new Polygon(-_size * 0.5f, _size);
				rotatedCollider = Polygon.Transform(collider, Matrix4.CreateRotationZ(_rotation) * Matrix4.CreateTranslation(Position.X, Position.Y, 0f));
			}
		}

		public Vector2 Size
		{
			get => _size;
			set
			{
				_size = value;
				collider = new Polygon(-value * 0.5f, value);
				rotatedCollider = Polygon.Transform(collider, Matrix4.CreateRotationZ(_rotation) * Matrix4.CreateTranslation(Position.X, Position.Y, 0f));
			}
		}

		public float Rotation
		{
			get => _rotation;
			set
			{
				_rotation = value;
				quaternion = Quaternion.FromAxisAngle(Vector3.UnitZ, value);
				collider = new Polygon(-_size * 0.5f, _size);
				rotatedCollider = Polygon.Transform(collider, Matrix4.CreateRotationZ(_rotation) * Matrix4.CreateTranslation(Position.X, Position.Y, 0f));
			}
		}

		public Color Color = new Color(100, 149, 237, 100);
		protected Color ColorSelected = new Color(100, 149, 237, 150);

		public Polygon collider;
		public Polygon rotatedCollider;

		public Material material = Material.BK7;
		public bool selected;

		// public abstract Vector2 GetTransformation(float initial, float final);

		public virtual float GetAngle(float incoming, float initial, float final) => incoming;

		public virtual void Update()
		{
		}

		public virtual void Draw()
		{
			var color = Color.Lerp(Color, ColorSelected, selected ? Utility.UnsignedSin(Time.TotalDrawTime * 3f) : 0f);
			Renderer2D.DrawQuad(Position, Size, color, quaternion);
		}

		public bool ContainsPoint(Vector2 point) => Collision.PointPolygon(rotatedCollider, point);

		public virtual BaseElement Clone() => (BaseElement)MemberwiseClone();
	}
}