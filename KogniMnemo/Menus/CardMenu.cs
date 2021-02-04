using System;
namespace CorgiMnemo.Menus
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
				Console.WriteLine("Card menu:" +
				"1-Create new card!" +
				"2-Read a card" +
				"3-Update a card" +
				"4-Delete a card" +
				"Exit-Back to main menu");
				var input = Console.ReadLine();
				if (input.ToLower() == "exit")
				{
					break;
				}
				else if (int.Parse(input) == 1)//Create new card.
				{
					Console.Clear();
					NewCard.Start();
				}
				else if (int.Parse(input) == 2)//Read a card.
				{
					Console.Clear();
					//NewPage.Start();
					//CardControler.FindACard();
				}
				else if (int.Parse(input) == 3)//  a cart.
				{
					Console.Clear();
					//options.start;
				}
				else if (int.Parse(input) == 4)//Delete a cart.
				{
					Console.Clear();
					//options.start;
				}
				Console.Clear();
			}
		}
	}
}
