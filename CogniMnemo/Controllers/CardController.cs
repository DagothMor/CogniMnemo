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
			
			var dataBaseFolder = $"{AppContext.BaseDirectory}" + @"CorgiMnemoDataBase\";
			string pathToWrite = dataBaseFolder + $"{GetNumberOfCardsInDataBaseFolder()}.txt";
			var textFromMnemoCard = new StringBuilder();
			// 6.1 Было text, стало textFromMnemoCard
			textFromMnemoCard.Append("[date of creation]" + DateTime.Now.ToString() + Environment.NewLine);
			textFromMnemoCard.Append("[date of last recall]" + DateTime.Now.ToString() + Environment.NewLine);
			textFromMnemoCard.Append("[level-]0" + Environment.NewLine);
			textFromMnemoCard.Append("[date of next recall]" + EbbinghausCurve.GetTimeRecallByForgettingCurve(DateTime.Now, 0,'+').ToString() + Environment.NewLine);
			textFromMnemoCard.Append("[zerolinks]" + Environment.NewLine);
			textFromMnemoCard.Append("[links]" + Environment.NewLine);
			textFromMnemoCard.Append("[tags]" + Environment.NewLine);
			textFromMnemoCard.Append("[question]" + question + Environment.NewLine);
			textFromMnemoCard.Append("[answer]" + answer + Environment.NewLine);
			try
			{
				using StreamWriter sw = new StreamWriter(pathToWrite);
				sw.Write(textFromMnemoCard.ToString());
				//Console.WriteLine("recording completed.");
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
			var textFromMnemoCard = new StringBuilder();
			textFromMnemoCard.Append("[date of creation]" + DateTime.Now.ToString() + Environment.NewLine);
			textFromMnemoCard.Append("[date of last recall]" + DateTime.Now.ToString() + Environment.NewLine);
			textFromMnemoCard.Append("[level-]0" + Environment.NewLine);
			textFromMnemoCard.Append("[date of next recall]" + EbbinghausCurve.GetTimeRecallByForgettingCurve(DateTime.Now, 0,'+').ToString() + Environment.NewLine);
			textFromMnemoCard.Append("[zerolinks]" + Environment.NewLine);
			textFromMnemoCard.Append("[links]" + Environment.NewLine);
			textFromMnemoCard.Append("[tags]" + Environment.NewLine);
			textFromMnemoCard.Append("[question]" + question + Environment.NewLine);
			textFromMnemoCard.Append("[answer]" + answer + Environment.NewLine);
			File.WriteAllText(path, string.Empty);
			try
			{
				using (StreamWriter sw = new StreamWriter(path))
				{
					sw.Write(textFromMnemoCard.ToString());
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
			// 6.1 Было count, стало countOfValidatedCards
			int countOfValidatedCards = 0;
			var rawlist = Directory.GetFiles($"{ AppContext.BaseDirectory }" + @"CorgiMnemoDataBase\");
			foreach (string path in rawlist)
			{
				if (CardNameValidation(path)) { countOfValidatedCards++; }
			}
			return countOfValidatedCards;
		}
		/// <summary>
		/// Checking for valid card name.
		/// </summary>
		/// <param name="pathfile">path file</param>
		/// <returns></returns>
		public static bool CardNameValidation(string pathfile)
		{
			// 7.2 Добавлена bool PathFileIsFound
			var PathFileIsFound = int.TryParse(Path.GetFileNameWithoutExtension(pathfile), out _);
			if (PathFileIsFound)
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
				// 6.1 Было text, стало textFromMnemoCard
				string textFromMnemoCard = File.ReadAllText($"{ AppContext.BaseDirectory }" + @"CorgiMnemoDataBase\" + $"{number}" + ".txt");
				Console.Write(textFromMnemoCard);
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
			var textFromMnemoCard = File.ReadAllText(path);
			Card card = new Card
			{
				Id = int.Parse(Path.GetFileNameWithoutExtension(path)),
				DateOfCreation = DateTime.Parse(TextController.ParsingTextFromManualOrAuthomatedInsertCard(textFromMnemoCard, "[date of creation]")),
				DateOfLastRecall = DateTime.Parse(TextController.ParsingTextFromManualOrAuthomatedInsertCard(textFromMnemoCard, "[date of last recall]")),
				Level = byte.Parse(TextController.ParsingTextFromManualOrAuthomatedInsertCard(textFromMnemoCard, "[level-]")),
				DateOfNextRecall = DateTime.Parse(TextController.ParsingTextFromManualOrAuthomatedInsertCard(textFromMnemoCard, "[date of next recall]")),
				Question = TextController.ParsingTextFromManualOrAuthomatedInsertCard(textFromMnemoCard, "[question]"),
				Answer = TextController.ParsingTextFromManualOrAuthomatedInsertCard(textFromMnemoCard, "[answer]")
			};

			return card;
		}
		// 6.1 Было list стало listOfMnemoCards.
		public static Card GetOldestCard(List<Card> listOfMnemoCards)
		{
			var buffercard = new Card();
			TimeSpan interval = new TimeSpan();
			buffercard.DateOfNextRecall = DateTime.Now;
			var cardWithBiggerInterval = new Card();

			foreach (var card in listOfMnemoCards)
			{
				if (interval<buffercard.DateOfNextRecall-card.DateOfNextRecall)
				{
					interval = buffercard.DateOfNextRecall - card.DateOfNextRecall;
					cardWithBiggerInterval = card;
				}
			}
			return cardWithBiggerInterval;
		}
		
	}
}
