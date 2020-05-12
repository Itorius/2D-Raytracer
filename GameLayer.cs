using Base;
using Newtonsoft.Json;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Input;
using Raytracer.Elements;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Raytracer
{
	public partial class GameLayer : Layer
	{
		private const float RotationRingSize = 60f;
		private const float RotationRingMax = RotationRingSize + 2.5f;
		private const float RotationRingMin = RotationRingSize - 7.5f;

		internal static GameLayer Instance;

		public static Vector2 MouseWorld;
		public static Vector2 MousePosition;

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

			if (File.Exists("Data.json"))
			{
				string json = File.ReadAllText("Data.json");
				List<SaveData> data = JsonConvert.DeserializeObject<List<SaveData>>(json);

				foreach (SaveData saveData in data)
				{
					BaseElement element = (BaseElement)Activator.CreateInstance(Type.GetType(saveData.type));
					element.Size = saveData.size;
					element.Position = saveData.position;
					element.Rotation = saveData.rotation;
					element.Color = saveData.color;
					element.material = saveData.refractionIndex;
					Elements.Add(element);
				}
			}

			framebuffer = new MultisampleFramebuffer(1280, 720, 8);
		}

		public override void OnUpdate()
		{
			Vector2 velocity = Vector2.Zero;
			if (pressed[(int)Key.W]) velocity.Y = -1f;
			if (pressed[(int)Key.S]) velocity.Y = 1f;
			if (pressed[(int)Key.A]) velocity.X = 1f;
			if (pressed[(int)Key.D]) velocity.X = -1f;

			CameraPosition += velocity * (pressed[(int)Key.ShiftLeft] || pressed[(int)Key.ShiftRight] ? 20f : 10f);
			camera.SetPosition(CameraPosition);

			MouseWorld = new Vector2(MousePosition.X - Game.Viewport.X * 0.5f, Game.Viewport.Y * 0.5f - MousePosition.Y) / cameraZoom - CameraPosition;

			if (dragElement != null)
			{
				dragElement.Position = MouseWorld - offset;
				RecalculateScaleNobs();
			}

			inRotationCircle = false;
			if (selectedElement != null)
			{
				float dist = Vector2.DistanceSquared(selectedElement.Position, MouseWorld);
				if (dist < RotationRingMax * RotationRingMax && dist > RotationRingMin * RotationRingMin) inRotationCircle = true;
			}

			if (rotating && selectedElement != null)
			{
				float rot = originalRotation + Vector2.Atan(MouseWorld - selectedElement.Position) - rotationOffset;
				selectedElement.Rotation = pressed[(int)Key.LShift] ? MathF.Round(rot / 0.08726645F) * 0.08726645F : rot;
				RecalculateScaleNobs();
			}

			if (scaling && selectedElement != null)
			{
				if (scalingX)
				{
					Vector2 size = selectedElement.Size;
					Vector2 diff = scaleOffset - MouseWorld;
					size.X += Vector2.Dot(dirX, diff) * 0.01f;
					selectedElement.Size = size;
				}
				else if (scalingY && !(selectedElement is CircularMirror))
				{
					Vector2 size = selectedElement.Size;
					Vector2 diff = scaleOffset - MouseWorld;
					size.Y -= Vector2.Dot(dirY, diff) * 0.01f;
					selectedElement.Size = size;
				}
			}

			foreach (BaseElement element in Elements) element.Update();
		}

		private Vector2 nobX, nobY;
		private Vector2 dirX, dirY;
		private Polygon colliderX, colliderY;

		private void RecalculateScaleNobs()
		{
			Matrix4 matrix = Matrix4.CreateRotationZ(selectedElement.Rotation);

			dirX = Vector2.Transform(Vector2.UnitX, matrix);
			nobX = selectedElement.Position - dirX * (RotationRingSize + 10f);
			dirY = Vector2.Transform(Vector2.UnitY, matrix);
			nobY = selectedElement.Position + dirY * (RotationRingSize + 10f);

			const float colliderSize = 7.5f;

			colliderX = new Polygon(new[]
			{
				new Vector2(nobX.X + colliderSize, nobX.Y + colliderSize),
				new Vector2(nobX.X + colliderSize, nobX.Y - colliderSize),
				new Vector2(nobX.X - colliderSize, nobX.Y - colliderSize),
				new Vector2(nobX.X - colliderSize, nobX.Y + colliderSize)
			});

			colliderY = new Polygon(new[]
			{
				new Vector2(nobY.X + colliderSize, nobY.Y + colliderSize),
				new Vector2(nobY.X + colliderSize, nobY.Y - colliderSize),
				new Vector2(nobY.X - colliderSize, nobY.Y - colliderSize),
				new Vector2(nobY.X - colliderSize, nobY.Y + colliderSize)
			});
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

				Renderer2D.DrawQuad(selectedElement.Position, new Vector2(RotationRingSize * 2f), (inRotationCircle || rotating) && !scaling ? Color.White : Color.White * 0.4f);

				Renderer2D.EndScene();

				Renderer2D.BeginScene(camera, elementShader, framebuffer);

				Color colorX = (Collision.PointPolygon(colliderX, MouseWorld) || scalingX) && !rotating ? Color.Red : Color.Red * 0.5f;
				Color colorY = (Collision.PointPolygon(colliderY, MouseWorld) || scalingY) && !rotating ? Color.Lime : Color.Lime * 0.5f;

				Renderer2D.DrawLine(selectedElement.Position, nobX, colorX);
				if (!(selectedElement is CircularMirror)) Renderer2D.DrawLine(selectedElement.Position, nobY, colorY);

				Renderer2D.DrawQuad(nobX, new Vector2(7.5f), colorX, selectedElement.quaternion);
				if (!(selectedElement is CircularMirror)) Renderer2D.DrawQuad(nobY, new Vector2(7.5f), colorY, selectedElement.quaternion);

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