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
		public static void Add(string question, string answer)
		{
			// here need a countofcard integer
			string folder = $"{_rootFolder}" + "1.txt";//todo:add a nterpolation with checking serial number of pages
			var notetext = new StringBuilder();
			notetext.Append("[Date of creation]" + DateTime.Now.ToString() + Environment.NewLine);
			notetext.Append("[Date of last recall]" + DateTime.Now.ToString() + Environment.NewLine);
			notetext.Append("[Level-]1" + Environment.NewLine);
			notetext.Append("[Question]" + question + Environment.NewLine);
			notetext.Append("[Answer]" + answer + Environment.NewLine);

			try
			{
				using (StreamWriter sw = new StreamWriter(folder))
				{
					sw.Write(notetext.ToString());
				}

				//using (StreamWriter sw = new StreamWriter(folder, true, System.Text.Encoding.Default))
				//{
				//	sw.Write(notetext.ToString());
				//}
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
		/// Check a text of file which inserted manual.
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
		/// 
		/// </summary>
		/// <param name="pathfile">path file</param>
		/// <returns></returns>
		public static bool AutomatedInsertCardTextValidation(string pathfile)
		{
			var card = File.ReadAllText(pathfile);
			if (card.Contains("[Date of creation]"))
			{
				if (card.Contains("[Date of last recall]"))
				{
					if (card.Contains("[Level-]"))
					{
						if (card.Contains("[Question]"))
						{
							if (card.Contains("[Answer]"))
							{
								return true;
							}
						}
					}
				}
			}
			return false;
		}
		public static string RenameFile(string oldpath, string newname)
		{
			List<char> oldpathlist = oldpath.ToCharArray().ToList();
			for (int i = oldpathlist.Count-1; i >= 0; i--)
			{
				if (oldpathlist[i] == '.')
				{
					i--;
					while (oldpathlist[i] != '\\')
					{
						oldpathlist.RemoveAt(i);
						i--;
					}
					i++;
					for (int j = 0; j < newname.Length; j++)
					{
						
						oldpathlist.Insert(i, newname[j]);
						i++;
					}
					break;
				}
			}
			var newfilename = new StringBuilder();
			for (int i = 0; i < oldpathlist.Count; i++)
			{
				newfilename.Append(oldpathlist[i]);
			}

			return newfilename.ToString();
		}
	}
}
