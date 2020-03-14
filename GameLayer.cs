using Base;
using ImGuiNET;
using OpenTK;
using OpenTK.Graphics.OpenGL4;
using Raytracer.Elements;
using System.Collections.Generic;
using System.Linq;

namespace Raytracer
{
	public partial class GameLayer : Layer
	{
		private const float RotationRingSize = 60f;
		private const float RotationRingMax = RotationRingSize + 2.5f;
		private const float RotationRingMin = RotationRingSize - 7.5f;

		internal static GameLayer Instance;

		public static Base.Vector2 MouseWorld;
		public static Base.Vector2 MousePosition;

		public readonly List<BaseElement> Elements = new List<BaseElement>();

		private Shader screenShader;
		private Shader elementShader;

		private MultisampleFramebuffer framebuffer;

		public GameLayer() => Instance = this;

		public override void OnAttach()
		{
			camera = new Camera();

			screenShader = ResourceManager.GetShader("Assets/Shaders/Screen.vert", "Assets/Shaders/Screen.frag");
			elementShader = ResourceManager.GetShader("Assets/Shaders/Flat.vert", "Assets/Shaders/Flat.frag");

			// todo: replace with loading and saving
			Elements.Add(new Laser { position = new Base.Vector2(-300f, -50f), size = new Base.Vector2(100f, 20f) });
			Elements.Add(new FlatRefractor { size = new Base.Vector2(100f, 1000f) });

			framebuffer = new MultisampleFramebuffer(1280, 720, 8);
		}

		public override void OnUpdate()
		{
			(float x, float y) = Base.Vector2.Transform(MousePosition, camera.View.Inverted());
			MouseWorld = new Base.Vector2(x - Game.Viewport.X * 0.5f, Game.Viewport.Y * 0.5f - y);

			if (dragElement != null) dragElement.position = MouseWorld - offset;

			inRotationCircle = false;
			if (selectedElement != null)
			{
				float dist = Base.Vector2.DistanceSquared(selectedElement.position, MouseWorld);
				if (dist < RotationRingMax * RotationRingMax && dist > RotationRingMin * RotationRingMin) inRotationCircle = true;
			}

			if (rotating && selectedElement != null) selectedElement.Rotation = originalRotation + (Base.Vector2.Atan(MouseWorld - selectedElement.position) - rotationOffset);

			foreach (BaseElement element in Elements) element.Update();
		}

		public override void OnRender()
		{
			GL.Enable(EnableCap.Blend);
			GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);

			Renderer2D.BeginScene(camera, elementShader, framebuffer);
			framebuffer.Clear();

			foreach (Laser laser in Elements.OfType<Laser>()) laser.DrawRay();
			foreach (BaseElement element in Elements) element.Draw();

			Renderer2D.EndScene();

			if (selectedElement != null)
			{
				Shader s = ResourceManager.GetShader("Assets/Shaders/Circle.vert", "Assets/Shaders/Circle.frag");
				Renderer2D.BeginScene(camera, s, framebuffer);
				s.UploadUniformFloat("u_Radius", 1f);
				s.UploadUniformFloat("u_Thickness", 0.95f);

				Renderer2D.DrawQuad(selectedElement.position, new Base.Vector2(RotationRingSize * 2f), inRotationCircle ? Color.White : new Color(100, 100, 100, 255));

				Renderer2D.EndScene();
			}

			screenShader.Bind();
			screenShader.UploadUniformMat4("u_ViewProjection", Matrix4.Identity);
			screenShader.UploadUniformFloat2("u_Viewport", Game.Viewport);

			framebuffer.Draw();

			screenShader.Unbind();
		}

		public override void OnGUI()
		{
			ImGui.Begin("Editor");

			ImGui.Text($"FPS: {1 / Time.DeltaDrawTime:F2}");
			ImGui.Text($"Frametime: {Time.DeltaDrawTime * 1000f:F2}ms");

			ImGui.End();
		}
	}
}