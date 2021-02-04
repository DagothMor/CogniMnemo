using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
				Console.WriteLine("Welcome to the CorgiMneemo!" +
					"The best application for training your skills!");
				Console.WriteLine("Main menu:" +
					"1-Start new game!" +
					"2-Card menu." +
					"3-Options" +
					"Exit-Exit");
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
					NewCard.Start();
				}
				else if (int.Parse(input) == 3)//Going to the card menu.
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
