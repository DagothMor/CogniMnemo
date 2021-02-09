using CogniMnemo.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CogniMnemo.Menus
{
	public static class TrainingMenu
	{
		public static void Start()
		{
			while (true)
			{
				Console.WriteLine("Initialize a database...");
				DataBaseInitialization.Start();
				Console.WriteLine("Getting all cards...");
				var listOfAllFilePaths = FolderController.GetAllFileNamesInDataBase();
				Console.WriteLine("Filtering...");
				var listOfAllWorkableFilePaths = new List<string>();
				foreach (var item in listOfAllFilePaths)
				{
					if (CardController.CardNameValidation(item))
					{
						listOfAllWorkableFilePaths.Add(item);
					}
				}
				Console.WriteLine("Creating a list of card objects");
				var cards = new List<Card>();
				var buffercard = new Card();
				foreach (var item in listOfAllWorkableFilePaths)
				{
					buffercard = CardController.GetCardFromPathFile(item);
					if (buffercard.DateOfNextRecall<DateTime.Now)
					{
						cards.Add(buffercard);
					}
				}
				







				Console.WriteLine(
					"1 - start training!" + Environment.NewLine +
					"2 - Get all pages" + Environment.NewLine +
					"back - Back to main menu");
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
				}
				else if (int.Parse(input) == 2)
				{
				}
				Console.Clear();
			}
		}
	}
}
