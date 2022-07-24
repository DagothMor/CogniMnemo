using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System;

namespace CogniMnemo.Controllers
{
	/// <summary>
	/// Folder Controller.
	/// </summary>
	public static class FolderController
	{
        private const string DATA_BASE_FOLDER = @"CorgiMnemoDataBase\";

        /// <summary>
        /// Safe file rename.
        /// </summary>
        /// <param name="oldPath">old path</param>
        /// <param name="newPath">new path</param>
        /// <returns>New renamed file conflicting in the process.</returns>
        public static string SafeFileRename(string oldPath, string newPath)
		{
			// было startConflictFileName стало existedConflictFileName
			string existedConflictFileName = newPath;
			// 7.4 начальное и конечное значение конфликтого имени файла.
			string startConflictFileName = newPath;
			string finishConflictFileName = newPath;
			bool fileIsExist = false;
			int iteration = 1;
			while (File.Exists(finishConflictFileName))
			{
				fileIsExist = true;
				if (!Path.GetFileNameWithoutExtension(finishConflictFileName).Contains("copy"))
				{
					finishConflictFileName = CreateNewNameForFilePath(finishConflictFileName, Path.GetFileNameWithoutExtension(finishConflictFileName) + $" copy({iteration})");
					iteration++;
				}
				else
				{
					finishConflictFileName = CreateNewCopyNameForFilePath(finishConflictFileName, iteration);
					iteration++;
				}
			}
			if (fileIsExist) { File.Move(existedConflictFileName, finishConflictFileName); }
			File.Move(oldPath, newPath);
			return finishConflictFileName;

		}
		/// <summary>
		/// Get all file names in data base.
		/// </summary>
		/// <returns>list of paths</returns>
		public static List<string> GetAllFileNamesInDataBase()
		{
			var PathsOfCardsInDataBase = new List<string>();
			var paths = Directory.GetFiles($"{ AppContext.BaseDirectory }" + DATA_BASE_FOLDER);
			for (int i = 0; i < paths.Length; i++)
			{
				PathsOfCardsInDataBase.Add(paths[i]);
			}
			return PathsOfCardsInDataBase;
		}
		/// <summary>
		/// Creating a new name for file.
		/// </summary>
		/// <param name="filepath">path to file</param>
		/// <param name="newname">new name for file</param>
		/// <returns>New path</returns>
		public static string CreateNewNameForFilePath(string filepath, string newname)
		{
			// было oldpathlist стало oldpathstring
			List<char> oldPathString = filepath.ToCharArray().ToList();
			// Выделил булеву для удобочитаемости.
			bool indexCharEqualDoubleBackSlash;
			for (int i = oldPathString.Count - 1; i >= 0; i--)
			{
				if (oldPathString[i] == '.')
				{
					i--;
					indexCharEqualDoubleBackSlash = oldPathString[i] == '\\';
					while (!indexCharEqualDoubleBackSlash)
					{
						oldPathString.RemoveAt(i);
						i--;
						indexCharEqualDoubleBackSlash = oldPathString[i] == '\\';

					}
					i++;
					for (int j = 0; j < newname.Length; j++)
					{
						oldPathString.Insert(i, newname[j]);
						i++;
					}
					break;
				}
			}
			var newfilename = new StringBuilder();
			for (int i = 0; i < oldPathString.Count; i++)
			{
				newfilename.Append(oldPathString[i]);
			}
			return newfilename.ToString();
		}
		/// <summary>
		/// Create a new copy name for file path.
		/// </summary>
		/// <param name="filepath">file path</param>
		/// <param name="iteration">iteration</param>
		/// <returns>new copy name for file path</returns>
		public static string CreateNewCopyNameForFilePath(string filepath, int iteration)
		{
			List<char> oldPathString = filepath.ToCharArray().ToList();
			int buffer = 0;
			for (int i = oldPathString.Count - 1; i >= 0; i--)
			{
				if (oldPathString[i] == '.')
				{
					i--;
					while (oldPathString[i] != '(')
					{
						oldPathString.RemoveAt(i);
						i--;
					}
					i++;
					for (int j = 0; j < iteration.ToString().Length; j++)
					{
						oldPathString.Insert(i, iteration.ToString()[j]);
						i++;
					}
					buffer = i;
					break;
				}
			}
			oldPathString.Insert(buffer, ')');
			buffer = -1;
			var newfilename = new StringBuilder();
			for (int i = 0; i < oldPathString.Count; i++)
			{
				newfilename.Append(oldPathString[i]);
			}

			return newfilename.ToString();
		}
	}
}
