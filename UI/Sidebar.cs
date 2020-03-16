using OpenTK.Graphics;
using Raytracer.Elements;
using System;
using System.Linq;
using System.Reflection;

namespace Raytracer.UI
{
	public class Sidebar : UIPanel
	{
		public Sidebar()
		{
			Width = new StyleDimension { Percent = 0.2f };
			Height = new StyleDimension { Percent = 1f };
			X = new StyleDimension { Percent = 1f };
			MinWidth = 200f;
			Padding = new Padding(8f);

			UIPanel panel = new UIPanel
			{
				Width = { Percent = 1f },
				Height = { Percent = 1f },
				BackgroundColor = new Color4(20, 20, 20, 255),
				Padding = new Padding(8f)
			};
			Append(panel);

			UIGrid grid = new UIGrid
			{
				Width = { Percent = 1f },
				Height = { Percent = 1f }
			};
			panel.Append(grid);

			foreach (Type type in Assembly.GetEntryAssembly().GetTypes().Where(x => !x.IsAbstract && x.IsSubclassOf(typeof(BaseElement))))
			{
				UIOpticalElement b = new UIOpticalElement(type)
				{
					Width = { Percent = 0.5f, Pixels = -4f }
				};
				grid.Append(b);
			}
		}

		protected override void PostDraw()
		{
			if (UIOpticalElement.MouseElement != null)
			{
				UIOpticalElement.MouseElement.Position = GameLayer.MousePosition;

				UIOpticalElement.MouseElement.Draw();
			}
		}
	}
}