using Base;
using Raytracer.UI;
using System;

namespace Raytracer
{
	internal class Game : BaseWindow
	{
		internal static Game Instance;
		internal static Vector2 Viewport;
		internal static bool DebugDraw = false;

		public Game()
		{
			Instance = this;

			Layers.PushLayer(new GameLayer());
			Layers.PushOverlay(new UILayer());
			Layers.PushOverlay(new ImGuiLayer());
		}

		protected override void OnResize(EventArgs e)
		{
			Viewport = new Vector2(Width, Height);

			base.OnResize(e);
		}
	}
}