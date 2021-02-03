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
		public static void AddAuthomatedCard(string question, string answer)
		{
			// here need a countofcard integer
			string folder = $"{_rootFolder}" + "1.txt";//todo:add a nterpolation with checking serial number of pages
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
			//sortingnotesindatabase

			//create list of int
			var rawlist = Directory.GetFiles(_rootFolder);
			//add in list a node with  int.parsed name

			//other node name(which cant parsed) are ignored
			//get list.count
			return 0;
		}
		/// <summary>
		/// Needed to sort them in the start of program.
		/// </summary>
		public static void SortingCardsInDataBase()
		{

			DirectoryInfo d = new DirectoryInfo(_rootFolder);//Assuming Test is your Folder
			FileInfo[] Files = d.GetFiles("*.txt"); //Getting Text files
			string str = "";
			var filenames = Path.GetFileName(_rootFolder);
			//get array of named notes
			//var rawlist = Directory.GetDirectories($"{AppContext.BaseDirectory}" + @"CorgiMnemoDataBase\");
			//string[] filePaths = Directory.GetFiles($"{AppContext.BaseDirectory}" + @"CorgiMnemoDataBase\", "*.txt",SearchOption.TopDirectoryOnly);
			//create list of int
			var list = new List<int>();
			//add in list a node with  int.parsed name
			//other node name(which cant parsed) are ignored
			//sort(by date of create?)
			//for(list.count)
			//streamwriter.changenotename(list[i],i)

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
