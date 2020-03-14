using Base;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Input;
using System.Collections.Generic;
using System.Linq;

namespace Raytracer.UI
{
	public class UIElement
	{
		protected static Color4 PanelColor = new Color4(40, 40, 40, 255);

		public UIElement Parent { get; private set; }
		public readonly List<UIElement> Children = new List<UIElement>();

		public StyleDimension Width;
		public StyleDimension Height;
		public StyleDimension X;
		public StyleDimension Y;

		public float? MinWidth;
		public float? MaxWidth;

		public Padding Padding = Padding.Zero;

		protected Rectangle InnerDimensions;
		public Rectangle Dimensions;

		public bool OverflowHidden = false;

		public bool IsMouseHovering { get; private set; }

		#region Events
		protected virtual bool MouseScroll(MouseWheelEventArgs args)
		{
			return false;
		}

		public bool InternalMouseWheel(MouseWheelEventArgs args)
		{
			foreach (UIElement child in Children)
			{
				if (child.ContainsPoint(args.X, args.Y) && child.InternalMouseWheel(args)) return true;
			}

			return ContainsPoint(args.X, args.Y) && MouseScroll(args);
		}

		protected virtual bool MouseDown(MouseButtonEventArgs args) => false;

		public bool InternalMouseDown(MouseButtonEventArgs args, out UIElement element)
		{
			foreach (UIElement child in Children)
			{
				if (child.ContainsPoint(args.X, args.Y) && child.InternalMouseDown(args, out element)) return true;
			}

			bool cond = ContainsPoint(args.X, args.Y) && MouseDown(args);
			element = cond ? this : null;
			return cond;
		}

		protected virtual bool MouseUp(MouseButtonEventArgs args) => false;

		public bool InternalMouseUp(MouseButtonEventArgs args)
		{
			foreach (UIElement child in Children)
			{
				if (child.InternalMouseUp(args)) return true;
			}

			return MouseUp(args);
		}

		public virtual void MouseEnter()
		{
			IsMouseHovering = true;
		}

		public virtual void MouseLeave()
		{
			IsMouseHovering = false;
		}
		#endregion

		#region UIElement
		protected virtual void Recalculate()
		{
			float minWidth = MinWidth ?? 0f;
			float maxWidth = MaxWidth ?? float.PositiveInfinity;

			Rectangle parent = Parent?.InnerDimensions ?? new Rectangle(0f, 0f, Game.Viewport.X, Game.Viewport.Y);

			Dimensions.Width = Utility.Clamp(parent.Width * Width.Percent + Width.Pixels, minWidth, maxWidth);
			Dimensions.Height = parent.Height * Height.Percent + Height.Pixels;

			Dimensions.X = parent.X + (parent.Width - Dimensions.Width) * X.Percent + X.Pixels;
			Dimensions.Y = parent.Y + (parent.Height - Dimensions.Height) * Y.Percent + Y.Pixels;

			InnerDimensions.Width = Dimensions.Width - Padding.Left - Padding.Right;
			InnerDimensions.Height = Dimensions.Height - Padding.Top - Padding.Left;
			InnerDimensions.X = Dimensions.X + Padding.Left;
			InnerDimensions.Y = Dimensions.Y + Padding.Top;
		}

		public void InternalRecalculate()
		{
			Recalculate();
			foreach (UIElement child in Children) child.InternalRecalculate();
		}

		public void Append(UIElement child)
		{
			child.Parent = this;
			Children.Add(child);
			// child.InternalRecalculate();
		}

		public void Remove(UIElement child)
		{
			if (Children.Remove(child)) child.Parent = null;
		}

		public UIElement GetElementAt(float x, float y)
		{
			UIElement element = Children.FirstOrDefault(current => current.ContainsPoint(x, y));

			if (element != null) return element.GetElementAt(x, y);

			return ContainsPoint(x, y) ? this : null;
		}

		public UIElement GetElementAt(Vector2 point) => GetElementAt(point.X, point.Y);

		protected bool ContainsPoint(float x, float y) => Dimensions.Contains(x, y);
		#endregion

		#region Tick
		protected virtual void PostDraw()
		{
		}

		protected virtual void PreDraw()
		{
		}

		protected virtual void Draw()
		{
		}

		public void InternalDraw()
		{
			PreDraw();

			Draw();

			Renderer2D.Flush();

			if (OverflowHidden)
			{
				GL.Enable(EnableCap.ScissorTest);
				Rectangle sc = new Rectangle(InnerDimensions.X, Game.Viewport.Y - (InnerDimensions.Y + InnerDimensions.Height), InnerDimensions.Width, InnerDimensions.Height);
				GL.Scissor((int)sc.X, (int)sc.Y, (int)sc.Width, (int)sc.Height);
			}

			foreach (UIElement child in Children) child.InternalDraw();

			if (OverflowHidden) GL.Disable(EnableCap.ScissorTest);

			PostDraw();
		}

		protected virtual void Update()
		{
		}

		public void InternalUpdate()
		{
			Update();
			foreach (UIElement child in Children) child.InternalUpdate();
		}
		#endregion
	}
}