using Base;
using OpenTK.Graphics;
using OpenTK.Input;

namespace Raytracer.UI
{
	public class Menu : UIElement
	{
		public Menu()
		{
			Width = new StyleDimension { Percent = 1f };
			Height = new StyleDimension { Percent = 1f };

			UIPanel panel = new UIPanel
			{
				Width = { Percent = 0.5f },
				Height = { Percent = 0.5f },
				X = { Percent = 0.5f },
				Y = { Percent = 0.5f },
				Padding = new Padding(8f)
			};
			Append(panel);

			UIButton button = new UIButton
			{
				Width = { Percent = 1f },
				Height = { Pixels = 40f },
				Y = { Percent = 1f },
				Text = new Translation("SaveAndQuit")
			};
			button.OnClick += () =>
			{
				GameLayer.Instance.Save();
				Game.Instance.Exit();
			};
			panel.Append(button);

			UISwitchButton buttonLanguage = new UISwitchButton("Czech", "English")
			{
				Width = { Percent = 1f },
				Height = { Pixels = 40f },
				Padding = new Padding(8f),
				Text = new Translation("ChangeLanguage")
			};
			buttonLanguage.OnClick += () =>
			{
				buttonLanguage.Next();
				Localization.SetLanguage(buttonLanguage.choices[buttonLanguage.index] == "Czech" ? Language.Czech : Language.English);
			};
			panel.Append(buttonLanguage);
		}

		protected override void PreDraw()
		{
			Renderer2D.DrawQuadTL(Vector2.Zero, Game.Viewport, new Color4(20, 20, 20, 240));
		}

		protected override bool MouseDown(MouseButtonEventArgs args) => true;

		protected override bool MouseUp(MouseButtonEventArgs args) => true;

		protected override bool MouseScroll(MouseWheelEventArgs args) => true;

		protected override bool KeyDown(KeyboardKeyEventArgs args) => true;

		protected override bool KeyUp(KeyboardKeyEventArgs args) => true;
	}
}