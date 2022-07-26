using CogniMnemo.Controllers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CogniMnemo
{
	/// <summary>
	/// Data base initialization.
	/// </summary>
	public static class DataBaseInitialization
	{
        private const string DATA_BASE_FOLDER = @"CorgiMnemoDataBase\";

        public static void Start()
		{
			var readylist = new List<string>();
			var filtratedqueue = new Queue<string>();
			// 7.2 Добавил Success.
			var success = false;

			CheckForExistFolder();

			var bufferlist = FolderController.GetAllFileNamesInDataBase();

			for (int i = 0; i < bufferlist.Count; i++)
			{
				success = TextController.ManualCardHasAllAttributes(bufferlist[i]);
				if (success)
				{
					filtratedqueue.Enqueue(bufferlist[i]);
					continue;
				}
				success = TextController.AutomatedCardHasAllAttributes(bufferlist[i]);
				if (success)
				{
					readylist.Add(bufferlist[i]);
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
							bufferlist[bufferlist.IndexOf(bufferstart)] = bufferend;
							bufferlist[i] = FolderController.CreateNewNameForFilePath(bufferlist[i], "incorrect");
						}
						else
						{
							FolderController.SafeFileRename(bufferlist[i], FolderController.CreateNewNameForFilePath(bufferlist[i], "incorrect"));
							bufferlist[i] = FolderController.CreateNewNameForFilePath(bufferlist[i], "incorrect");
						}
					}
				}
			}

			_ = readylist.OrderBy(q => q);

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
			if (!Directory.Exists($"{AppContext.BaseDirectory}" + DATA_BASE_FOLDER))
			{
				Directory.CreateDirectory($"{AppContext.BaseDirectory}" + DATA_BASE_FOLDER);
			}
		}

        // TODO: для безопасности данных, переносить все удаляемые файлы во внутреннюю папку bin
        public static void DeleteAllCardsInDataBase()
		{
			var paths = Directory.GetFiles($"{AppContext.BaseDirectory}" + DATA_BASE_FOLDER);
			foreach (var path in paths)
			{
				File.Delete(path);
			}
		}

	}
}
