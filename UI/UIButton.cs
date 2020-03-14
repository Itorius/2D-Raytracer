using Base;
using OpenTK.Graphics;
using OpenTK.Input;
using System;

namespace Raytracer.UI
{
	public class UIButton : UIElement
	{
		public Color4 BorderColor = Color4.White;
		public Color4 BackgroundColor = PanelColor;
		public Color4 HoveredColor = new Color4(60, 60, 60, 255);

		public string Text;

		public event Action OnClick;

		protected override bool MouseDown(MouseButtonEventArgs args) => true;

		protected override bool MouseUp(MouseButtonEventArgs args)
		{
			OnClick?.Invoke();
			return true;
		}

		private Vector2 size;

		protected override void Draw()
		{
			Renderer2D.DrawQuadTL(Dimensions.Position, Dimensions.Size, BorderColor);
			Renderer2D.DrawQuadTL(Dimensions.Position + new Vector2(2f), Dimensions.Size - new Vector2(4f), IsMouseHovering ? HoveredColor : BackgroundColor);

			if (!string.IsNullOrWhiteSpace(Text)) size = Renderer2D.DrawString(Text, Dimensions.X + Dimensions.Width * 0.5f - size.X * 0.5f, Dimensions.Y + Dimensions.Height * 0.5f - size.Y * 0.5f, scale: 0.75f);
		}
	}
}