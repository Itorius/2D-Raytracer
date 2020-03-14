using Base;
using OpenTK.Graphics;

namespace Raytracer.UI
{
	public class UIPanel : UIElement
	{
		public Color4 BorderColor = Color4.White;
		public Color4 BackgroundColor = PanelColor;

		protected override void Draw()
		{
			Renderer2D.DrawQuadTL(Dimensions.Position, Dimensions.Size, BorderColor);
			Renderer2D.DrawQuadTL(Dimensions.Position + new Vector2(2f), Dimensions.Size - new Vector2(4f), BackgroundColor);
		}
	}
}