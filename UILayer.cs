using Base;
using OpenTK;
using OpenTK.Graphics;
using System.Collections.Generic;

namespace Raytracer
{
	public class UILayer : Layer
	{
		private Matrix4 ViewProjection;
		private Shader shader;

		public List<UIElement> Elements = new List<UIElement>();

		public override void OnAttach()
		{
			shader = new Shader("Assets/Shaders/Flat.vert", "Assets/Shaders/Flat.frag");

			Elements.Add(new UIPanel
			{
				Width = 0.2f,
				Height = 1f,
				X = 1f
			});
		}

		public override void OnWindowResize(int width, int height)
		{
			ViewProjection = Matrix4.CreateOrthographic(width, height, -1f, 1f);

			foreach (UIElement element in Elements) element.InternalRecalculate();
		}

		public override void OnRender()
		{
			Renderer2D.BeginScene(ViewProjection, shader);

			foreach (UIElement element in Elements) element.InternalDraw();

			Renderer2D.EndScene();
		}
	}

	public abstract class UIElement
	{
		protected static Color4 PanelColor = new Color4(40, 40, 40, 255);

		public UIElement Parent;
		public List<UIElement> Children = new List<UIElement>();

		public float Width;
		public float Height;
		public float X;
		public float Y;

		protected Base.Vector2 size;
		protected Base.Vector2 position;

		protected virtual void Recalculate()
		{
			var size = Parent?.size ?? new Base.Vector2(BaseWindow.Instance.Width, BaseWindow.Instance.Height);
			this.size = new Base.Vector2(size.X * Width, size.Y * Height);

			position = new Base.Vector2(size.X * X - this.size.X * X, size.Y * Y - this.size.Y * Y) * 0.5f;
		}

		public void InternalRecalculate()
		{
			Recalculate();
			foreach (UIElement child in Children) child.InternalRecalculate();
		}

		protected virtual void Draw()
		{
		}

		public void InternalDraw()
		{
			Draw();
			foreach (UIElement child in Children) child.InternalDraw();
		}
	}

	public class UIPanel : UIElement
	{
		public Color4 BorderColor = Color4.White;
		public Color4 BackgroundColor = PanelColor;

		protected override void Draw()
		{
			Renderer2D.DrawQuad(position, size, BorderColor);
			Renderer2D.DrawQuad(position, size - new Base.Vector2(4f), BackgroundColor);
		}
	}
}