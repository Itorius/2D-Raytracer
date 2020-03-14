using Base;
using OpenTK.Input;
using Raytracer.Elements;
using System.Linq;

namespace Raytracer
{
	public partial class GameLayer
	{
		#region Camera
		public Camera camera;
		private Vector2 CameraPosition;

		public override void OnWindowResize(int width, int height)
		{
			camera.SetViewport(width, height);

			if (width != 0 && height != 0) framebuffer.SetSize(width, height);
		}

		public override bool OnKeyDown(KeyboardKeyEventArgs args)
		{
			bool handled = false;

			if (args.Key == Key.W)
			{
				CameraPosition.Y -= 10f;
				handled = true;
			}
			else if (args.Key == Key.S)
			{
				CameraPosition.Y += 10f;
				handled = true;
			}
			else if (args.Key == Key.A)
			{
				CameraPosition.X += 10f;
				handled = true;
			}
			else if (args.Key == Key.D)
			{
				CameraPosition.X -= 10f;
				handled = true;
			}

			camera.SetPosition(CameraPosition);

			return handled;
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
				rotationOffset = Vector2.Atan(MouseWorld - selectedElement.position);
				originalRotation = selectedElement.Rotation;
				return true;
			}

			if (selectedElement != null) selectedElement.selected = false;

			selectedElement = Elements.LastOrDefault(element => element.ContainsPoint(MouseWorld));

			if (selectedElement != null)
			{
				selectedElement.selected = true;

				dragElement = selectedElement;
				offset = MouseWorld - dragElement.position;
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