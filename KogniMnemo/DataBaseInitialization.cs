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
					readylist.Add(bufferlist[i]);
					//if (CardController.CardNameValidation(bufferlist[i]))
					//{
					//	readylist.Add(bufferlist[i]);
					//	continue;
					//}
					//filtratedqueue.Enqueue(bufferlist[i]);
					continue;
				}

				else
				{
					if (bufferlist[i] != FolderController.CreateNewNameForFilePath(bufferlist[i], "incorrect"))
					{
						string bufferstart = FolderController.CreateNewNameForFilePath(bufferlist[i], "incorrect");
						string bufferend;
						Console.WriteLine($"Bad card on path {bufferlist[i]},Please edit card by rules");
						if (File.Exists(bufferstart))
						{
							bufferend = FolderController.SafeFileRename(bufferlist[i], FolderController.CreateNewNameForFilePath(bufferlist[i], "incorrect"));
							bufferlist[i] = FolderController.CreateNewNameForFilePath(bufferlist[i], "incorrect");
							bufferlist[bufferlist.IndexOf(bufferstart)] = bufferend;
						}
						else
						{
							FolderController.SafeFileRename(bufferlist[i], FolderController.CreateNewNameForFilePath(bufferlist[i], "incorrect"));
							bufferlist[i] = FolderController.CreateNewNameForFilePath(bufferlist[i], "incorrect");
						}
					}
				}
			}

			readylist.OrderBy(q => q).ToList();
			foreach (string path in filtratedqueue)
			{
				CardController.CreateWorkCardFromManualInsertedCard(path);
			}

			while (filtratedqueue.Count != 0)
			{
				readylist.Add(filtratedqueue.Dequeue());
			}
			for (int i = 0; i < readylist.Count; i++)
			{
				if (readylist[i] != FolderController.CreateNewNameForFilePath(readylist[i], i.ToString()))
				{
					if (File.Exists(FolderController.CreateNewNameForFilePath(readylist[i], i.ToString())))
					{
						string bufferstart = FolderController.CreateNewNameForFilePath(readylist[i], i.ToString());
						string bufferend = FolderController.SafeFileRename(readylist[i], FolderController.CreateNewNameForFilePath(readylist[i], i.ToString()));
						readylist[readylist.IndexOf(bufferstart)] = bufferend;
					}
					else
					{
						FolderController.SafeFileRename(readylist[i], FolderController.CreateNewNameForFilePath(readylist[i], i.ToString()));
					}
				}
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
