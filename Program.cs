using OpenTK;

namespace Raytracer
{
	internal static class Program
	{
		private static void Main(string[] args)
		{
			using Game game = new Game
			{
				VSync = VSyncMode.On
			};
			game.Run(60);
		}
	}
}