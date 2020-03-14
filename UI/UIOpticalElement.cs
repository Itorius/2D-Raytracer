using Base;
using OpenTK.Graphics;
using OpenTK.Input;
using Raytracer.Elements;
using System;

namespace Raytracer.UI
{
	public class UIGrid : UIElement
	{
		private float yOffset;
		private float innerListHeight;

		public UIGrid()
		{
			OverflowHidden = true;
		}

		protected override void Recalculate()
		{
			base.Recalculate();

			float left = 0;
			float top = yOffset;

			for (int i = 0; i < Children.Count; i++)
			{
				UIElement item = Children[i];

				item.X.Pixels = left;
				item.Y.Pixels = top /* + item.Margin.Top*/;
				item.InternalRecalculate();
				Rectangle dimensions = item.Dimensions;

				if (i % 2 == 2 - 1 || i == Children.Count - 1)
				{
					top += dimensions.Height + 8f;
					left = 0;
				}
				else left += dimensions.Width + 8f;
			}

			innerListHeight = top - yOffset;
		}

		protected override bool MouseScroll(MouseWheelEventArgs args)
		{
			yOffset -= args.DeltaPrecise * 5f;
			Recalculate();

			return true;
		}

		protected override void Draw()
		{
		}
	}

	public class UIOpticalElement : UIElement
	{
		public Color4 BorderColor = Color4.White;
		public Color4 BackgroundColor = PanelColor;
		public Color4 HoveredColor = new Color4(60, 60, 60, 255);

		private BaseElement element;
		private bool IsMouseHovering;

		public static BaseElement MouseElement;

		public UIOpticalElement(Type type)
		{
			element = (BaseElement)Activator.CreateInstance(type);
		}

		protected override void Recalculate()
		{
			base.Recalculate();

			Dimensions.Height = Dimensions.Width;
			InnerDimensions.Height = InnerDimensions.Width;

			element.position = InnerDimensions.Center;
			element.size = new Vector2(MathF.Min(InnerDimensions.Width, InnerDimensions.Height) * 0.6f);
		}

		protected override bool MouseDown(MouseButtonEventArgs args)
		{
			MouseElement = element.Clone();

			return true;
		}

		protected override bool MouseUp(MouseButtonEventArgs args)
		{
			(float x, float y) = Vector2.Transform(GameLayer.MousePosition, GameLayer.Instance.camera.View);
			MouseElement.position = new Vector2(x - Game.Viewport.X * 0.5f, Game.Viewport.Y * 0.5f - y);

			GameLayer.Instance.Elements.Add(MouseElement);
			MouseElement = null;

			return true;
		}

		public override void MouseEnter()
		{
			IsMouseHovering = true;

			destScale = 0.7f;
		}

		public override void MouseLeave()
		{
			IsMouseHovering = false;

			destScale = 0.6f;
		}

		private float destScale = 0.6f;
		private float scale = 0.6f;

		private Vector2 size;

		protected override void Draw()
		{
			if (scale < destScale) scale += 1f * Time.DeltaDrawTime;
			if (scale > destScale) scale -= 1f * Time.DeltaDrawTime;

			element.size = new Vector2(MathF.Min(InnerDimensions.Width, InnerDimensions.Height) * scale);

			Renderer2D.DrawQuadTL(Dimensions.Position, Dimensions.Size, BorderColor);
			Renderer2D.DrawQuadTL(Dimensions.Position + new Vector2(2f), Dimensions.Size - new Vector2(4f), IsMouseHovering ? HoveredColor : BackgroundColor);

			element.Draw();

			Renderer2D.DrawQuadTL(Dimensions.Position + new Vector2(2f, Dimensions.Height - 22f), new Vector2(Dimensions.Width - 4f, 20f), IsMouseHovering ? HoveredColor : BackgroundColor);
			size = Renderer2D.DrawString(element.GetType().Name, InnerDimensions.X + InnerDimensions.Width * 0.5f - size.X * 0.5f, InnerDimensions.Y + InnerDimensions.Height - size.Y - 2f, scale: 0.4f);
		}
	}
}