using Base;
using OpenTK.Input;
using Raytracer.Elements;
using System;
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
	}
}