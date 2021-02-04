using CogniMnemo.Controllers;
using System;
using System.Collections.Generic;
using System.Text;

namespace CogniMnemo.Menus
{
	/// <summary>
	/// Developer menu.
	/// </summary>
	public static class DeveloperMenu
	{
		public static void Start()
		{
			while (true)
			{
				Console.WriteLine("You are in developer menu!");
				Console.WriteLine(
					"1 - Add page in folder\n" +
					"2 - Get all pages\n" +
					"exit - Back to main menu\n");
				var input = Console.ReadLine();
				if (input.ToLower() == "exit")
				{
					break;
				}
				else if (int.Parse(input) == 1)
				{
					Console.Clear();
					CardController.AddAuthomatedCard("question", "answer");
				}
				else if (int.Parse(input) == 2)
				{
				}
				Console.Clear();
			}
		}
	}
}
