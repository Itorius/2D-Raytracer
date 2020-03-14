using Base;
using Raytracer.UI;

namespace Raytracer
{
	internal class Game : BaseWindow
	{
		public Game()
		{
			Layers.PushLayer(new GameLayer());
			Layers.PushOverlay(new UILayer());
			Layers.PushOverlay(new ImGuiLayer());
		}
	}
}