using CogniMnemo.Controllers;
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
			//Deleting all files
			//MockController.DeleteAllCardsInDataBase();
			//Fulling with new cards
			//MockController.CreateListOfTemplateCards();
			//todo:создать уведомление для ос о том что пора вспомнить карту(отталкиваясь от аттрибутов Last recall и level)
			while (true)
			{
				Console.WriteLine($"Welcome {Environment.UserName} to the CorgiMneemo!" + Environment.NewLine +
					"The best application for training your skills!");
				Console.WriteLine("Main menu:" + Environment.NewLine +
					"1-Training menu" + Environment.NewLine +
					"2-Card menu." + Environment.NewLine +
					"3-Options" + Environment.NewLine +
					"4-Help" + Environment.NewLine +
					"exit-exit from application");
				var input = Console.ReadLine();
				if (string.IsNullOrEmpty(input) || string.IsNullOrWhiteSpace(input))
				{
					Console.Clear();
					continue;
				}
				if (input.ToLower() == "exit")
				{
					Environment.Exit(0);
				}
				if (!int.TryParse(input,out _))
				{
					Console.Clear();
					continue;
				}
				else if (int.Parse(input) == 1)//Start practicing.
				{
					Console.Clear();
					TrainingMenu.Start();
				}
				else if (int.Parse(input) == 2)//Going to the card menu.
				{
					Console.Clear();
					CardMenu.Start();
				}
				else if (int.Parse(input) == 3)//Options.
				{
					Console.Clear();
					OptionsMenu.Start();
				}
				else if (int.Parse(input) == 4)//Help menu.
				{
					Console.Clear();
					//help menu
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
