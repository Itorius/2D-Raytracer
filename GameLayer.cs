using Base;
using ImGuiNET;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Input;
using Raytracer.UI;
using System.Collections.Generic;
using System.Linq;

namespace Raytracer
{
	public class GameLayer : Layer
	{
		internal static GameLayer Instance;
		public static Base.Vector2 MouseWorld;

		private Shader shader;

		public Matrix4 View;
		private Matrix4 ViewProjection;

		private MultisampleFramebuffer framebuffer;
		private Shader ScreenShader;

		public readonly List<BaseElement> Elements = new List<BaseElement>();

		public GameLayer() => Instance = this;

		public override void OnAttach()
		{
			shader = ResourceManager.GetShader("Assets/Shaders/Flat.vert", "Assets/Shaders/Flat.frag");
			ScreenShader = ResourceManager.GetShader("Assets/Shaders/Screen.vert", "Assets/Shaders/Screen.frag");

			Elements.Add(new Laser { position = new Base.Vector2(-300f, -50f), size = new Base.Vector2(100f, 20f), Rotation = 30f * Utility.DegToRad });
			Elements.Add(new FlatRefractor { size = new Base.Vector2(100f, 1000f) });

			framebuffer = new MultisampleFramebuffer(1280, 720, 8);
		}

		public override void OnWindowResize(int width, int height)
		{
			View = Matrix4.CreateTranslation(0f, 0f, 0f);
			ViewProjection = View * Matrix4.CreateOrthographic(width, height, -1f, 1f);

			if (width != 0 && height != 0) framebuffer.SetSize(width, height);
		}

		public BaseElement SelectedElement;

		private Base.Vector2 offset;
		private BaseElement DraggableElement;

		public override bool OnMouseDown(MouseButtonEventArgs args)
		{
			SelectedElement = Elements.FirstOrDefault(element => element.ContainsPoint(MouseWorld));
			DraggableElement = SelectedElement;

			offset = MouseWorld - DraggableElement?.position ?? Base.Vector2.Zero;

			return true;
		}

		public override bool OnMouseUp(MouseButtonEventArgs args)
		{
			DraggableElement = null;

			return true;
		}

		public override void OnUpdate()
		{
			Vector3 transformed = Vector3.TransformPosition(new Vector3(UILayer.MousePosition), View.Inverted());
			MouseWorld = new Base.Vector2(transformed.X - BaseWindow.Instance.Width * 0.5f, BaseWindow.Instance.Height * 0.5f - transformed.Y);

			if (DraggableElement != null) DraggableElement.position = MouseWorld - offset;

			foreach (BaseElement element in Elements) element.Update();
		}

		private float radius = 1f;
		private float thickness = 1f;

		public override void OnRender()
		{
			GL.Enable(EnableCap.Blend);
			GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);

			Renderer2D.BeginScene(ViewProjection, shader, framebuffer);
			GL.Clear(ClearBufferMask.ColorBufferBit);

			Elements[0].Rotation = angle * Utility.DegToRad;

			foreach (BaseElement element in Elements.OrderByDescending(element => element is Laser)) element.Draw();

			Renderer2D.EndScene();

			if (SelectedElement != null)
			{
				Shader s = ResourceManager.GetShader("Assets/Shaders/Circle.vert", "Assets/Shaders/Circle.frag");
				Renderer2D.BeginScene(ViewProjection, s, framebuffer);
				s.UploadUniformFloat("u_Radius", 1f);
				s.UploadUniformFloat("u_Thickness", 0.95f);

				Renderer2D.DrawQuad(SelectedElement.position, new Base.Vector2(120f), Color4.DeepPink);

				Renderer2D.EndScene();
			}

			Base.Vector2 screen = new Base.Vector2(BaseWindow.Instance.Width, BaseWindow.Instance.Height);

			ScreenShader.Bind();
			ScreenShader.UploadUniformMat4("u_ViewProjection", Matrix4.Identity);
			ScreenShader.UploadUniformFloat2("u_Viewport", screen);

			framebuffer.Draw();

			ScreenShader.Unbind();
		}

		private float angle;

		public override void OnGUI()
		{
			ImGui.Begin("Editor");

			ImGui.Text($"FPS: {1 / Time.DeltaDrawTime:F2}");
			ImGui.Text($"Frametime: {Time.DeltaDrawTime * 1000f:F2}ms");

			ImGui.DragFloat("rot", ref angle);
			ImGui.SliderFloat("Radius", ref radius, 0f, 1f);
			ImGui.SliderFloat("Thickness", ref thickness, 0f, 1f);

			ImGui.End();
		}
	}
}