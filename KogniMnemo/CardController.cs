using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CorgiMnemo
{
	public static class CardController
	{
		private static string _rootFolder = $"{ AppContext.BaseDirectory }" + @"CorgiMnemoDataBase\";
		/// <summary>
		/// Added a working card
		/// </summary>
		/// <param name="question">question</param>
		/// <param name="answer">answer</param>
		public static void AddAuthomatedCard(string question, string answer)
		{
			string folder = $"{_rootFolder}" + $"{GetNumberOfCardsInDataBaseFolder()}.txt";
			var notetext = new StringBuilder();
			notetext.Append("[date of creation]" + DateTime.Now.ToString() + Environment.NewLine);
			notetext.Append("[date of last recall]" + DateTime.Now.ToString() + Environment.NewLine);
			notetext.Append("[level-]1" + Environment.NewLine);
			notetext.Append("[question]" + question + Environment.NewLine);
			notetext.Append("[answer]" + answer + Environment.NewLine);
			try
			{
				using (StreamWriter sw = new StreamWriter(folder))
				{
					sw.Write(notetext.ToString());
				}
				Console.WriteLine("Запись выполнена");
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
		}
		/// <summary>
		/// Rewriting manual inserted card to workable card.
		/// </summary>
		public static void CreateWorkCardFromManualInsertedCard(string path)
		{
			var text = File.ReadAllText(path);
			string question = TextController.ParsingTextFromManualOrAuthomatedInsertCard(text,"[?]");
			string answer = TextController.ParsingTextFromManualOrAuthomatedInsertCard(text,"[!]");
			var notetext = new StringBuilder();
			notetext.Append("[date of creation]" + DateTime.Now.ToString() + Environment.NewLine);
			notetext.Append("[date of last recall]" + DateTime.Now.ToString() + Environment.NewLine);
			notetext.Append("[level-]1" + Environment.NewLine);
			notetext.Append("[question]" + question + Environment.NewLine);
			notetext.Append("[answer]" + answer + Environment.NewLine);
			File.WriteAllText(path, string.Empty);
			try
			{
				using (StreamWriter sw = new StreamWriter(path))
				{
					sw.Write(notetext.ToString());
				}
				Console.WriteLine("Запись выполнена");
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
		}
		public static void Read()
		{

		}
		/// <summary>
		/// Get count of cards in database.
		/// </summary>
		/// <returns></returns>
		public static int GetNumberOfCardsInDataBaseFolder()//todo: need to wrap in garbage collector
		{
			int count = 0;
			var rawlist = Directory.GetFiles(_rootFolder);
			foreach (string path in rawlist)
			{
				if (CardNameValidation(path)) { count++; }
			}
			return count;
		}
		/// <summary>
		/// GetAll
		/// </summary>
		/// <returns></returns>
		public static List<string> GetAllFileNamesInDataBase()
		{
			var PathsOfCardsInDataBase = new List<string>();
			var paths = Directory.GetFiles(_rootFolder);
			for (int i = 0; i < paths.Length; i++)
			{
				PathsOfCardsInDataBase.Add(paths[i]);
			}
			return PathsOfCardsInDataBase;
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
				return true;
			}
			return false;

		}
		/// <summary>
		/// Checks the text validity of the card which inserted manual.
		/// </summary>
		/// <param name="pathfile">path file</param>
		/// <returns></returns>
		public static bool ManualInsertCardTextValidation(string pathfile)
		{
			var card = File.ReadAllText(pathfile);
			if (card.Contains("[?]"))
			{
				if (card.Contains("[!]"))
				{
					return true;
				}
			}
			return false;
		}
		/// <summary>
		/// Checks the text validity of the card
		/// </summary>
		/// <param name="pathfile">path file</param>
		/// <returns></returns>
		public static bool AutomatedInsertCardTextValidation(string pathfile)
		{
			var card = File.ReadAllText(pathfile);
			if (card.Contains("[date of creation]"))
			{
				if (card.Contains("[date of last recall]"))
				{
					if (card.Contains("[level-]"))
					{
						if (card.Contains("[question]"))
						{
							if (card.Contains("[answer]"))
							{
								return true;
							}
						}
					}
				}
			}
			return false;
		}
		
	}
}
