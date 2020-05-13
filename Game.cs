using Base;
using Raytracer.UI;
using System;

namespace Raytracer
{
	// todo: material selection
	// todo: fix font rendering
	// todo: triangular prism
	// todo: multicast monochrome laser
	// todo: modular adjust nob system
	// todo: mesh rendering
	// todo: adjust focus point of lenses/curved mirrors
	// todo: load scene from file

	internal class Game : BaseWindow
	{
		internal static Game Instance;
		internal static Vector2 Viewport;
		internal const bool DebugDraw = false;

		public Game()
		{
			// ColorMapping.Test();

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