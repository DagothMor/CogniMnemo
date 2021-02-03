using CorgiMnemo;
using System;
using System.IO;

namespace KogniMnemo
{
	class Program
	{

		static void Main(string[] args)
		{
			CardController.AddAuthomatedCard("questiontest", "answertest");
			//DataBaseInitialization.Start();

			//CardController.GetAllPathFilesInDataBase();
			//CheckForExistFolder();
			//while (true)
			//{
			//	Console.WriteLine("Welcome to the CorgiMneemo!" +
			//		"The best application for training your skills!");
			//	Console.WriteLine("Main menu:" +
			//		"1-Start new game!" +
			//		"2-Add a new note." +
			//		"3-Exit");
			//	var input = Console.ReadLine();
			//	if (int.Parse(input) == 3)//exit from game.
			//	{
			//		Environment.Exit(0);
			//	}
			//	else if (int.Parse(input) == 1)//Start new game.
			//	{
			//	}
			//	else if (int.Parse(input) == 2)//Add a note.
			//	{
			//		Console.Clear();
			//		NewPage.Start();
			//	}
			//	else if (int.Parse(input) == 4)//Developer menu.
			//	{
			//		Console.Clear();
			//		DeveloperMenu.Start();
			//	}
			//	Console.Clear();
			//}
		}

		
	}

}
