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
		/// <summary>
		/// Safe file rename.
		/// </summary>
		/// <param name="oldPath">old path</param>
		/// <param name="newPath">new path</param>
		/// <returns>New renamed file conflicting in the process.</returns>
		public static string SafeFileRename(string oldPath, string newPath)
		{
			string startconflictfilename = newPath;
			string finishconflictfilename = newPath;
			bool FileIsExist = false;
			int iteration = 1;
			while (File.Exists(finishconflictfilename))
			{
				FileIsExist = true;
				if (!Path.GetFileNameWithoutExtension(finishconflictfilename).Contains("copy"))
				{
					finishconflictfilename = CreateNewNameForFilePath(finishconflictfilename, Path.GetFileNameWithoutExtension(finishconflictfilename) + $" copy({iteration})");
					iteration++;
				}
				else
				{
					finishconflictfilename = CreateNewCopyNameForFilePath(finishconflictfilename, iteration);
					iteration++;
				}
			}
			if (FileIsExist) { File.Move(startconflictfilename, finishconflictfilename); }
			File.Move(oldPath, newPath);
			return finishconflictfilename;

		}
		/// <summary>
		/// Get all file names in data base.
		/// </summary>
		/// <returns>list of paths</returns>
		public static List<string> GetAllFileNamesInDataBase()
		{
			var PathsOfCardsInDataBase = new List<string>();
			var paths = Directory.GetFiles($"{ AppContext.BaseDirectory }" + @"CorgiMnemoDataBase\");
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
			List<char> oldpathlist = filepath.ToCharArray().ToList();
			for (int i = oldpathlist.Count - 1; i >= 0; i--)
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
		/// <summary>
		/// Create a new copy name for file path.
		/// </summary>
		/// <param name="filepath">file path</param>
		/// <param name="iteration">iteration</param>
		/// <returns>new copy name for file path</returns>
		public static string CreateNewCopyNameForFilePath(string filepath, int iteration)
		{
			List<char> oldpathlist = filepath.ToCharArray().ToList();
			int buffer = 0;
			for (int i = oldpathlist.Count - 1; i >= 0; i--)
			{
				if (oldpathlist[i] == '.')
				{
					i--;
					while (oldpathlist[i] != '(')
					{
						oldpathlist.RemoveAt(i);
						i--;
					}
					i++;
					for (int j = 0; j < iteration.ToString().Length; j++)
					{
						oldpathlist.Insert(i, iteration.ToString()[j]);
						i++;
					}
					buffer = i;
					break;
				}
			}
			oldpathlist.Insert(buffer, ')');
			var newfilename = new StringBuilder();
			for (int i = 0; i < oldpathlist.Count; i++)
			{
				newfilename.Append(oldpathlist[i]);
			}

			return newfilename.ToString();
		}
	}
}
