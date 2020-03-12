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

		public List<BaseElement> Elements = new List<BaseElement>();

		public GameLayer() => Instance = this;

		public override void OnAttach()
		{
			shader = new Shader("Assets/Shaders/Flat.vert", "Assets/Shaders/Flat.frag");

			Elements.Add(new Laser { position = new Base.Vector2(-300f, -50f), size = new Base.Vector2(100f, 20f), color = Color4.FromHsv(new Vector4(new Random().NextFloat(), 1f, 0.9f, 1f)), rotation = 30f * Utility.DegToRad });
			Elements.Add(new FlatRefractor { size = new Base.Vector2(100f, 1000f), color = new Color4(100, 149, 237, 100) });
		}

		public override void OnWindowResize(int width, int height)
		{
			ViewProjection = Matrix4.CreateOrthographic(width, height, -1f, 1f);
		}

		public override void OnUpdate()
		{
			foreach (BaseElement element in Elements) element.Update();
		}

		public override void OnRender()
		{
			GL.Enable(EnableCap.Blend);
			GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);

			Renderer2D.BeginScene(ViewProjection, shader);

			Elements[0].rotation = angle * Utility.DegToRad;

			foreach (BaseElement element in Elements.OrderByDescending(element => element is Laser)) element.Draw();

			Renderer2D.EndScene();
		}

		private float angle;

		public override void OnGUI()
		{
			ImGui.Begin("Editor");

			ImGui.Text($"FPS: {1 / Time.DeltaDrawTime:F2}");

			ImGui.DragFloat("rot", ref angle);

			ImGui.End();
		}
	}
}