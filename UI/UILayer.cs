using Base;
using OpenTK;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Input;

namespace Raytracer.UI
{
	public class UILayer : Layer
	{
		public static Base.Vector2 MousePosition;

		private Matrix4 ViewProjection;
		private Shader shader;

		private UIElement Base;

		public override void OnAttach()
		{
			shader = ResourceManager.GetShader("Assets/Shaders/Flat.vert", "Assets/Shaders/Flat.frag");

			Base = new UIElement
			{
				Width = { Percent = 1f },
				Height = { Percent = 1f }
			};

			Sidebar sidebar = new Sidebar();
			Base.Append(sidebar);
		}

		public override void OnWindowResize(int width, int height)
		{
			ViewProjection = Matrix4.CreateOrthographicOffCenter(0, width, height, 0, -1f, 1f);

			GL.Viewport(0, 0, width, height);

			Base.InternalRecalculate();
		}

		public override bool OnMouseUp(MouseButtonEventArgs args)
		{
			if (mousedowned != null)
			{
				mousedowned.InternalMouseUp(args);
				mousedowned = null;
				return true;
			}

			return false;
		}

		private UIElement mousedowned;

		public override bool OnMouseDown(MouseButtonEventArgs args)
		{
			return Base.InternalMouseDown(args, out mousedowned);
		}

		private UIElement prev;

		public override bool OnMouseMove(MouseMoveEventArgs args)
		{
			MousePosition = new Base.Vector2(args.X, args.Y);

			return false;
		}

		public override bool OnMouseScroll(MouseWheelEventArgs args)
		{
			return Base.InternalMouseWheel(args);
		}

		public override void OnUpdate()
		{
			Base.InternalUpdate();

			UIElement mouse = Base.GetElementAt(MousePosition.X, MousePosition.Y);

			if (mouse != prev)
			{
				prev?.MouseLeave();
				mouse?.MouseEnter();

				prev = mouse;
			}
		}

		public override void OnRender()
		{
			Renderer2D.BeginScene(ViewProjection, shader);

			Base.InternalDraw();

			Renderer2D.EndScene();
		}
	}
}