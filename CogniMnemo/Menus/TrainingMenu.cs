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
				Console.Clear();
				Console.WriteLine("Initialize a database...");
				DataBaseInitialization.Start();
				Console.WriteLine("Getting all card paths...");
				var listOfAllFilePaths = FolderController.GetAllFileNamesInDataBase();
				Console.WriteLine("Filtering...");
				var validatedCardPaths = new List<string>();
				foreach (var item in listOfAllFilePaths)
				{
					if (CardController.CardNameIsValid(item))
					{
						// 6.4 было listOfAllWorkableFilePaths стало validatedCardPaths
						validatedCardPaths.Add(item);
					}
				}
				Console.WriteLine("Creating a list of card objects");
				var cards = new List<Card>();
				var buffercard = new Card();
				foreach (var item in validatedCardPaths)
				{
					buffercard = CardController.GetCardFromPathFile(item);
					if (buffercard.DateOfNextRecall < DateTime.Now)
					{
						cards.Add(buffercard);
					}
				}
				if (cards.Count == 0)
				{
					Console.WriteLine($"There is no card for recall");
					Console.WriteLine("press enter for back to main menu.");
					Console.ReadLine();
					Console.Clear();
					break;
				}
				Console.WriteLine($"Cards which need for recall: {cards.Count}");
				Console.WriteLine("press enter to start recall.");
				Console.ReadLine();
				Console.Clear();
				//get the oldest card
				var oldercard = CardController.GetOldestCard(cards);
				//display a card
				Console.WriteLine($"{oldercard.Question}" + Environment.NewLine);
				Console.WriteLine("press enter if you read the question.");
				Console.ReadLine();
				Console.Clear();
				Console.WriteLine("Enter the answer.");
				var userAnswer = Console.ReadLine();
				Console.Clear();
				Console.WriteLine($"{oldercard.Answer}");
				Console.WriteLine($"your answer:{userAnswer}");
				while (true)
				{
					Console.WriteLine("is your answer correct? Y/N");
					var userinput = Console.ReadLine();
					if (userinput.ToLower() == "y")
					{
						TextController.RewriteTextAfterAnInputAttribute(oldercard.Id, "date of last recall", DateTime.Now.ToString() + Environment.NewLine);
						oldercard.DateOfNextRecall = EbbinghausCurve.GetTimeRecallByForgettingCurve(DateTime.Now, oldercard.Level, '+');
						TextController.RewriteTextAfterAnInputAttribute(oldercard.Id, "date of next recall", oldercard.DateOfNextRecall.ToString() + Environment.NewLine);
						oldercard.Level++;
						TextController.RewriteTextAfterAnInputAttribute(oldercard.Id, "level-", oldercard.Level.ToString() + Environment.NewLine);
						break;
					}
					if (userinput.ToLower() == "n")
					{
						TextController.RewriteTextAfterAnInputAttribute(oldercard.Id, "date of last recall", DateTime.Now.ToString() + Environment.NewLine);
						oldercard.DateOfNextRecall = EbbinghausCurve.GetTimeRecallByForgettingCurve(DateTime.Now, oldercard.Level, '-');
						TextController.RewriteTextAfterAnInputAttribute(oldercard.Id, "date of next recall", oldercard.DateOfNextRecall.ToString() + Environment.NewLine);
						if (oldercard.Level != 0) { oldercard.Level--; TextController.RewriteTextAfterAnInputAttribute(oldercard.Id, "level-", oldercard.Level.ToString() + Environment.NewLine); }
						break;
					}
				}
				string input;
				while (true)
				{
					Console.WriteLine("next card? Y/N");
					input = Console.ReadLine();
					if (string.IsNullOrEmpty(input) || string.IsNullOrWhiteSpace(input))
					{
						Console.Clear();
						continue;
					}
					else
					{
						break;
					}
				}
				if (input.ToLower() == "n")
				{
					break;
				}
				else if (input.ToLower() == "y")
				{
					continue;
				}
				Console.Clear();
			}
		}
	}
}
