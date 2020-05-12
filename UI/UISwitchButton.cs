using Base;
using System;

namespace Raytracer.UI
{
	public class UISwitchButton : UIButton
	{
		public string[] choices;
		public int index;
		private Vector2 size;

		public UISwitchButton(params string[] textures)
		{
			if (textures == null || textures.Length <= 0) throw new ArgumentException("Array is null or empty", nameof(textures));
			choices = textures;
		}

		public void Next()
		{
			if (++index >= choices.Length) index = 0;
		}

		protected override void Draw()
		{
			string choice = choices[index];

			Renderer2D.DrawQuadTL(Dimensions.Position, Dimensions.Size, BorderColor);
			Renderer2D.DrawQuadTL(Dimensions.Position + new Vector2(2f), Dimensions.Size - new Vector2(4f), IsMouseHovering ? HoveredColor : BackgroundColor);

			size = Renderer2D.DrawString(Localization.GetTranslation(choice), Dimensions.X + Dimensions.Width * 0.5f - size.X * 0.5f, Dimensions.Y + Dimensions.Height * 0.5f - size.Y * 0.5f, scale: 0.75f);
		}
	}
}