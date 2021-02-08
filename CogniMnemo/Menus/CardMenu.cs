using CogniMnemo.Controllers;
using System;
namespace CogniMnemo.Menus
{
	/// <summary>
	/// Menu have all CRUD operations for card.
	/// </summary>
	public static class CardMenu
	{
		public static void Start()
		{
			while (true)
			{
				Console.WriteLine("Card menu:" + Environment.NewLine +
				"1-Create new card!" + Environment.NewLine +
				"2-Get all cards" + Environment.NewLine +
				"3-Get a card by id " + Environment.NewLine +
				"4-Update a card" + Environment.NewLine +
				"5-Delete a card" + Environment.NewLine +
				"back-Back to main menu");
				var input = Console.ReadLine();
				if (string.IsNullOrEmpty(input) || string.IsNullOrWhiteSpace(input))
				{
					Console.Clear();
					continue;
				}
				if (input.ToLower() == "back")
				{
					break;
				}
				else if (int.Parse(input) == 1)
				{
					Console.Clear();
					CreateNewCardMenu.Start();
				}
				else if (int.Parse(input) == 2)
				{
					Console.Clear();
					CardController.DisplayAllCards();
				}
				else if (int.Parse(input) == 3)
				{
					Console.Clear();
					Console.WriteLine("Enter a card id");
					CardController.DisplayCardById(int.Parse(Console.ReadLine()));
				}
				else if (int.Parse(input) == 4)// Update a card.
				{
					Console.Clear();
				}
				else if (int.Parse(input) == 4)// Delete a card.
				{
					Console.Clear();
				}
				Console.Clear();
			}
		}
	}
}
