using Base;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using OpenTK.Input;
using Raytracer.Elements;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Raytracer
{
	public partial class GameLayer
	{
		#region Camera
		public Camera camera;
		private Vector2 CameraPosition;
		private bool[] pressed = new bool[Enum.GetNames(typeof(Key)).Length];
		private float cameraZoom = 1f;

		public override void OnWindowResize(int width, int height)
		{
			camera.SetViewport(width, height);

			if (width != 0 && height != 0) framebuffer.SetSize(width, height);
		}

		public override bool OnKeyDown(KeyboardKeyEventArgs args)
		{
			pressed[(int)args.Key] = true;

			return true;
		}

		public override bool OnKeyUp(KeyboardKeyEventArgs args)
		{
			pressed[(int)args.Key] = false;

			return true;
		}

		public override bool OnMouseScroll(MouseWheelEventArgs args)
		{
			cameraZoom += args.DeltaPrecise * 0.1f;
			cameraZoom = Utility.Clamp(cameraZoom, 0.1f, 10f);

			camera.SetZoom(cameraZoom);

			return true;
		}
		#endregion

		#region Dragging & selecting
		private BaseElement selectedElement;
		private BaseElement dragElement;
		private Vector2 offset;
		private float rotationOffset;
		private float originalRotation;
		private bool inRotationCircle;
		private bool rotating;

		public override bool OnMouseMove(MouseMoveEventArgs args)
		{
			MousePosition = new Vector2(args.X, args.Y);
			return true;
		}

		public override bool OnMouseDown(MouseButtonEventArgs args)
		{
			if (inRotationCircle)
			{
				rotating = true;
				rotationOffset = Vector2.Atan(MouseWorld - selectedElement.Position);
				originalRotation = selectedElement.Rotation;
				return true;
			}

			if (selectedElement != null) selectedElement.selected = false;

			selectedElement = Elements.LastOrDefault(element => element.ContainsPoint(MouseWorld));

			if (selectedElement != null)
			{
				selectedElement.selected = true;

				dragElement = selectedElement;
				offset = MouseWorld - dragElement.Position;
			}

			return true;
		}

		public override bool OnMouseUp(MouseButtonEventArgs args)
		{
			dragElement = null;
			rotating = false;

			return true;
		}
		#endregion

		public void Save()
		{
			List<SaveData> list = Elements.Select(element => new SaveData(element.GetType().AssemblyQualifiedName, element.Position, element.Size, element.Rotation, element.Color)).ToList();

			string json = JsonConvert.SerializeObject(list, new JsonSerializerSettings
			{
				ContractResolver = new WritablePropertiesOnlyResolver()
			});
			File.WriteAllText("Data.json", json);
		}
	}

	internal class WritablePropertiesOnlyResolver : DefaultContractResolver
	{
		protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
		{
			IList<JsonProperty> props = base.CreateProperties(type, memberSerialization);
			return props.Where(property => property.Writable).ToList();
		}
	}

	[Serializable]
	internal struct SaveData
	{
		public string type;
		public Vector2 position;
		public Vector2 size;
		public float rotation;
		public Color color;

		public SaveData(string type, Vector2 position, Vector2 size, float rotation, Color color)
		{
			this.type = type;
			this.position = position;
			this.size = size;
			this.rotation = rotation;
			this.color = color;
		}
	}
}