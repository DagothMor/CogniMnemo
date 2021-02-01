using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorgiMnemo
{
	public static class DataBaseInitialization
	{
		public static void Start()
		{
			var readylist = new List<string>();
			var filtratedqueue = new Queue<string>();

			CheckForExistFolder();

			var bufferlist = CardController.GetAllFileNamesInDataBase();

			for (int i = 0; i < bufferlist.Count; i++)
			{
				if (CardController.ManualInsertCardTextValidation(bufferlist[i]))
				{
					filtratedqueue.Enqueue(bufferlist[i]);
					continue;
				}
				if (CardController.AutomatedInsertCardTextValidation(bufferlist[i]))
				{
					if (CardController.CardNameValidation(bufferlist[i]))
					{
						readylist.Add(bufferlist[i]);
						continue;
					}
					filtratedqueue.Enqueue(bufferlist[i]);
					continue;
				}

				else
				{
					Console.WriteLine($"Bad card on path {bufferlist[i]},Please edit card by rules");
					if (!bufferlist[i].Contains("incorrect"))
					{
						var buffer = CardController.RenameFile(bufferlist[i], Path.GetFileNameWithoutExtension(bufferlist[i]) + "-incorrect");

						File.Move(bufferlist[i], buffer);
						bufferlist[i] = buffer;
					}
				}
			}

			readylist.OrderBy(q => q).ToList();

			while (filtratedqueue.Count != 0)
			{
				readylist.Add(filtratedqueue.Dequeue());
			}

			for (int i = 0; i < readylist.Count; i++)
			{
				var buffer = CardController.RenameFile(readylist[i], "copy");
				if (readylist.Contains(i.ToString()))//todo тут хуйня надо переделаывать наверно отдельным методом
				{
					File.Move(readylist[readylist.IndexOf(i.ToString())], buffer);
					readylist[readylist.IndexOf(i.ToString())] = buffer;
				}
				buffer = CardController.RenameFile(readylist[i], i.ToString());
				File.Move(readylist[i], CardController.RenameFile(readylist[i], i.ToString()));
				readylist[i] = buffer;
			}
		}
		/// <summary>
		/// Checking for exist folder
		/// </summary>
		public static void CheckForExistFolder()
		{
			if (!Directory.Exists($"{AppContext.BaseDirectory}" + @"CorgiMnemoDataBase\"))
			{
				Directory.CreateDirectory($"{AppContext.BaseDirectory}" + @"CorgiMnemoDataBase\");
			}
		}

	}
}
