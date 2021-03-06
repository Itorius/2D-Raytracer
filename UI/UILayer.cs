using Base;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Input;

namespace Raytracer.UI
{
	public class UILayer : Layer
	{
		private Camera camera;
		private Shader shader;
		private UIElement screen;
		private UIElement oldHovered;
		private UIElement oldPressed;

		private Menu menu;

		public override void OnAttach()
		{
			camera = new Camera();

			shader = ResourceManager.GetShader("Assets/Shaders/Flat.vert", "Assets/Shaders/Flat.frag");

			screen = new UIElement
			{
				Width = { Percent = 1f },
				Height = { Percent = 1f }
			};

			menu = new Menu();

			Sidebar sidebar = new Sidebar();
			screen.Append(sidebar);
			screen.InternalRecalculate();
		}

		public override void OnWindowResize(int width, int height)
		{
			GL.Viewport(0, 0, width, height);
			camera.SetViewportOffCenter(0, width, height, 0);

			screen.InternalRecalculate();
		}

		public override bool OnKeyDown(KeyboardKeyEventArgs args)
		{
			if (args.Key == Key.Escape)
			{
				if (screen.Contains(menu)) screen.Remove(menu);
				else
				{
					screen.Append(menu);
					screen.InternalRecalculate();
				}

				return true;
			}

			return screen.InternalKeyDown(args);
		}

		public override bool OnKeyUp(KeyboardKeyEventArgs args)
		{
			return screen.InternalKeyUp(args);
		}

		public override bool OnMouseUp(MouseButtonEventArgs args)
		{
			if (oldPressed != null)
			{
				oldPressed.InternalMouseUp(args);
				oldPressed = null;
				return true;
			}

			return false;
		}

		public override bool OnMouseDown(MouseButtonEventArgs args)
		{
			return screen.InternalMouseDown(args, out oldPressed);
		}

		public override bool OnMouseScroll(MouseWheelEventArgs args)
		{
			return screen.InternalMouseWheel(args);
		}

		public override void OnUpdate()
		{
			screen.InternalUpdate();

			UIElement mouse = screen.GetElementAt(GameLayer.MousePosition);

			if (mouse != oldHovered)
			{
				oldHovered?.MouseLeave();
				mouse?.MouseEnter();

				oldHovered = mouse;
			}
		}

		public override void OnRender()
		{
			Renderer2D.BeginScene(camera, shader);

			screen.InternalDraw();

			Renderer2D.DrawString($"FPS: {1 / Time.DeltaDrawTime:F2}", 10f, 10f, scale: 0.5f);

			Renderer2D.EndScene();
		}
	}
}