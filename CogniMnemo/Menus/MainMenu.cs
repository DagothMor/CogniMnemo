using System;

namespace CogniMnemo.Menus
{
	/// <summary>
	/// Main menu.
	/// </summary>
	public static class MainMenu
	{
		public static void Start()
		{
			while (true)
			{
				Console.WriteLine("Welcome to the CorgiMneemo!" + Environment.NewLine +
					"The best application for training your skills!");
				Console.WriteLine("Main menu:" + Environment.NewLine +
					"1-Start new game!" + Environment.NewLine +
					"2-Card menu." + Environment.NewLine +
					"3-Options" + Environment.NewLine +
					"exit-exit from application");
				var input = Console.ReadLine();
				if (input.ToLower() == "exit")
				{
					Environment.Exit(0);
				}
				else if (int.Parse(input) == 1)//Start practicing.
				{
				}
				else if (int.Parse(input) == 2)//Going to the card menu.
				{
					Console.Clear();
					CardMenu.Start();
				}
				else if (int.Parse(input) == 3)//Options.
				{
					Console.Clear();
					//options.start;
				}
				else if (int.Parse(input) == 5)//Developer menu.
				{
					Console.Clear();
					DeveloperMenu.Start();
				}
				Console.Clear();
			}
		}
	}
}
