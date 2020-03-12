using Base;
using ImGuiNET;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Raytracer
{
	public class GameLayer : Layer
	{
		internal static GameLayer Instance;

		private Shader shader;
		private Matrix4 ViewProjection;

		private MultisampleFramebuffer framebuffer;
		private Shader ScreenShader;

		public readonly List<BaseElement> Elements = new List<BaseElement>();

		public GameLayer() => Instance = this;

		public override void OnAttach()
		{
			shader = new Shader("Assets/Shaders/Flat.vert", "Assets/Shaders/Flat.frag");
			ScreenShader = new Shader("Assets/Shaders/Screen.vert", "Assets/Shaders/Screen.frag");

			Elements.Add(new Laser { position = new Base.Vector2(-300f, -50f), size = new Base.Vector2(100f, 20f), color = Color4.FromHsv(new Vector4(new Random().NextFloat(), 1f, 0.9f, 1f)), rotation = 30f * Utility.DegToRad });
			Elements.Add(new FlatRefractor { size = new Base.Vector2(100f, 1000f), color = new Color4(100, 149, 237, 100) });

			framebuffer = new MultisampleFramebuffer(1280, 720, 8);
		}

		public override void OnWindowResize(int width, int height)
		{
			ViewProjection = Matrix4.CreateOrthographic(width, height, -1f, 1f);
			framebuffer.SetSize(width, height);
		}

		public override void OnUpdate()
		{
			foreach (BaseElement element in Elements) element.Update();
		}

		public override void OnRender()
		{
			GL.Enable(EnableCap.Blend);
			GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);

			Renderer2D.BeginScene(ViewProjection, shader, framebuffer);
			GL.Clear(ClearBufferMask.ColorBufferBit);

			Elements[0].rotation = angle * Utility.DegToRad;

			foreach (BaseElement element in Elements.OrderByDescending(element => element is Laser)) element.Draw();

			Renderer2D.EndScene();

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

			ImGui.End();
		}
	}
}