using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CogniMnemo.Controllers
{
	/// <summary>
	/// Card controller.
	/// </summary>
	public static class CardController
	{
		/// <summary>
		/// Added a working card.
		/// </summary>
		/// <param name="question">question</param>
		/// <param name="answer">answer</param>
		public static void AddAuthomatedCard(string question, string answer)
		{
			string folder = $"{ AppContext.BaseDirectory }" + @"CorgiMnemoDataBase\" + $"{GetNumberOfCardsInDataBaseFolder()}.txt";
			var notetext = new StringBuilder();
			notetext.Append("[date of creation]" + DateTime.Now.ToString() + Environment.NewLine);
			notetext.Append("[date of last recall]" + DateTime.Now.ToString() + Environment.NewLine);
			notetext.Append("[level-]0" + Environment.NewLine);
			notetext.Append("[date of next recall]" + EbbinghausCurve.GetTimeRecallByForgettingCurve(DateTime.Now, 0,'+').ToString() + Environment.NewLine);
			notetext.Append("[zerolinks]" + Environment.NewLine);
			notetext.Append("[links]" + Environment.NewLine);
			notetext.Append("[tags]" + Environment.NewLine);
			notetext.Append("[question]" + question + Environment.NewLine);
			notetext.Append("[answer]" + answer + Environment.NewLine);
			try
			{
				using (StreamWriter sw = new StreamWriter(folder))
				{
					sw.Write(notetext.ToString());
				}
				Console.WriteLine("recording completed.");
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
		}

		/// <summary>
		/// Rewriting manual inserted card to working.
		/// </summary>
		public static void CreateWorkCardFromManualInsertedCard(string path)
		{
			var text = File.ReadAllText(path);
			string question = TextController.ParsingTextFromManualOrAuthomatedInsertCard(text, "[?]");
			string answer = TextController.ParsingTextFromManualOrAuthomatedInsertCard(text, "[!]");
			var notetext = new StringBuilder();
			notetext.Append("[date of creation]" + DateTime.Now.ToString() + Environment.NewLine);
			notetext.Append("[date of last recall]" + DateTime.Now.ToString() + Environment.NewLine);
			notetext.Append("[level-]0" + Environment.NewLine);
			notetext.Append("[date of next recall]" + EbbinghausCurve.GetTimeRecallByForgettingCurve(DateTime.Now, 0,'+').ToString() + Environment.NewLine);
			notetext.Append("[zerolinks]" + Environment.NewLine);
			notetext.Append("[links]" + Environment.NewLine);
			notetext.Append("[tags]" + Environment.NewLine);
			notetext.Append("[question]" + question + Environment.NewLine);
			notetext.Append("[answer]" + answer + Environment.NewLine);
			File.WriteAllText(path, string.Empty);
			try
			{
				using (StreamWriter sw = new StreamWriter(path))
				{
					sw.Write(notetext.ToString());
				}
				Console.WriteLine("recording completed.");
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
		}
		/// <summary>
		/// Get count of cards in database.
		/// </summary>
		/// <returns></returns>
		public static int GetNumberOfCardsInDataBaseFolder()
		{
			int count = 0;
			var rawlist = Directory.GetFiles($"{ AppContext.BaseDirectory }" + @"CorgiMnemoDataBase\");
			foreach (string path in rawlist)
			{
				if (CardNameValidation(path)) { count++; }
			}
			return count;
		}
		/// <summary>
		/// Checking for valid card name.
		/// </summary>
		/// <param name="pathfile">path file</param>
		/// <returns></returns>
		public static bool CardNameValidation(string pathfile)
		{
			if (int.TryParse(Path.GetFileNameWithoutExtension(pathfile), out _))
			{
				if (TextController.AutomatedInsertCardTextValidation(pathfile))
				{
					return true;
				}
			}
			return false;
		}
		/// <summary>
		/// needed for pagination
		/// </summary>
		public static void DisplayAllCards()
		{
			var listOfPaths = Directory.GetFiles($"{ AppContext.BaseDirectory }" + @"CorgiMnemoDataBase\");
			var listOfCards = new List<string>();
			foreach (var item in listOfPaths)
			{
				listOfCards.Add("Card id:" + Path.GetFileNameWithoutExtension(item) + Environment.NewLine + File.ReadAllText(item));
			}
			foreach (var item in listOfCards)
			{
				Console.Write(item);
				Console.WriteLine("_____________");
			}
			Console.WriteLine("Press enter for back to Card menu.");
			Console.ReadLine();
		}
		public static void DisplayCardById(int number)
		{
			try
			{
				string text = File.ReadAllText($"{ AppContext.BaseDirectory }" + @"CorgiMnemoDataBase\" + $"{number}" + ".txt");
				Console.Write(text);
				Console.WriteLine("_____________");
				Console.WriteLine("Press enter for back to Card menu.");
				Console.ReadLine();
			}
			catch (Exception)
			{

				Console.WriteLine("Bad request, press enter for back to Card Menu");
				Console.ReadLine();
			}
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="path"></param>
		/// <returns></returns>
		public static Card GetCardFromPathFile(string path)
		{
			Card card = new Card
			{
				DateOfCreation = DateTime.Parse(TextController.ParsingTextFromManualOrAuthomatedInsertCard(path, "[date of creation]")),
				DateOfLastRecall = DateTime.Parse(TextController.ParsingTextFromManualOrAuthomatedInsertCard(path, "[date of last recall]")),
				Level = byte.Parse(TextController.ParsingTextFromManualOrAuthomatedInsertCard(path, "[level-]")),
				DateOfNextRecall = DateTime.Parse(TextController.ParsingTextFromManualOrAuthomatedInsertCard(path, "[date of next recall]")),
				Question = TextController.ParsingTextFromManualOrAuthomatedInsertCard(path, "[question]"),
				Answer = TextController.ParsingTextFromManualOrAuthomatedInsertCard(path, "[answer]")
			};

			return card;
		}
		public static Card GetOlderCard(List<Card> list)
		{
			var buffercard = new Card();
			TimeSpan interval = new TimeSpan();
			buffercard.DateOfNextRecall = DateTime.Now;
			var cardWithBiggerInterval = new Card();
			foreach (var item in list)
			{
				if (interval<buffercard.DateOfNextRecall-item.DateOfNextRecall)
				{
					interval = buffercard.DateOfNextRecall - item.DateOfNextRecall;
					cardWithBiggerInterval = item;
				}
			}
			return cardWithBiggerInterval;
		}
	}
}
