using Base;
using OpenTK;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Input;
using Raytracer.Elements;
using System;
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
			Elements.Add(new Laser { Position = new Base.Vector2(-300f, -50f), Size = new Base.Vector2(100f, 20f) });
			Elements.Add(new FlatRefractor { Size = new Base.Vector2(100f, 1000f) });

			framebuffer = new MultisampleFramebuffer(1280, 720, 8);
		}

		public override void OnUpdate()
		{
			Base.Vector2 velocity = Base.Vector2.Zero;
			if (pressed[(int)Key.W]) velocity.Y = -1f;
			if (pressed[(int)Key.S]) velocity.Y = 1f;
			if (pressed[(int)Key.A]) velocity.X = 1f;
			if (pressed[(int)Key.D]) velocity.X = -1f;

			CameraPosition += velocity * (pressed[(int)Key.ShiftLeft] || pressed[(int)Key.ShiftRight] ? 20f : 10f);
			camera.SetPosition(CameraPosition);

			(float x, float y) = Base.Vector2.Transform(MousePosition, camera.View.Inverted());
			MouseWorld = new Base.Vector2(x - Game.Viewport.X * 0.5f, Game.Viewport.Y * 0.5f - y);

			if (dragElement != null) dragElement.Position = MouseWorld - offset;

			inRotationCircle = false;
			if (selectedElement != null)
			{
				float dist = Base.Vector2.DistanceSquared(selectedElement.Position, MouseWorld);
				if (dist < RotationRingMax * RotationRingMax && dist > RotationRingMin * RotationRingMin) inRotationCircle = true;
			}

			if (rotating && selectedElement != null)
			{
				float rot = originalRotation + Base.Vector2.Atan(MouseWorld - selectedElement.Position) - rotationOffset;
				selectedElement.Rotation = pressed[(int)Key.LShift] ? MathF.Round(rot / 0.08726645F) * 0.08726645F : rot;
			}

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

				Renderer2D.DrawQuad(selectedElement.Position, new Base.Vector2(RotationRingSize * 2f), inRotationCircle ? Color.White : new Color(100, 100, 100, 255));

				if (rotating) Renderer2D.DrawStringFlipped($"{MathF.Asin(MathF.Sin(selectedElement.Rotation * 0.5f)) * -2f * Utility.RadToDeg:F2}°", selectedElement.Position.X + RotationRingSize + 10f, selectedElement.Position.Y + 5f, scale: 0.5f);

				Renderer2D.EndScene();
			}

			screenShader.Bind();
			screenShader.UploadUniformMat4("u_ViewProjection", Matrix4.Identity);
			screenShader.UploadUniformFloat2("u_Viewport", Game.Viewport);

			framebuffer.Draw();

			screenShader.Unbind();
		}
	}
}