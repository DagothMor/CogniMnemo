using CogniMnemo.Controllers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CogniMnemo.Menus
{
	/// <summary>
	/// Developer menu.
	/// </summary>
	/// пока не пойму че тут можно добавить
	public static class DeveloperMenu
	{
		public static void Start()
		{
			while (true)
			{
				Console.WriteLine("You are in developer menu!");
				Console.WriteLine(
					"1 - Add page in folder" + Environment.NewLine +
					"2 - Get all pages" + Environment.NewLine +
					"back - Back to main menu");
				var input = Console.ReadLine();
				if (input.ToLower() == "back")
				{
					break;
				}
				else if (int.Parse(input) == 1)
				{
					Console.Clear();
				}
				else if (int.Parse(input) == 2)
				{
					CardController.DisplayAllCards();
					
				}
				Console.Clear();
			}
		}
	}
}
