using CogniMnemo.Controllers;
using System;

namespace CogniMnemo
{
	/// <summary>
	/// New card.
	/// </summary>
	public static class CreateNewCardMenu
	{
		public static void Start()
		{
			string input;
			while (true)
			{
				Console.WriteLine("1 - Create new card" + Environment.NewLine +
				"back - Go to main menu");
				input = Console.ReadLine();
				if (string.IsNullOrEmpty(input) || string.IsNullOrWhiteSpace(input))
				{
					Console.Clear();
					continue;
				}
				if (input.ToLower() == "back")
				{
					break;
				}
				else if (int.Parse(input) == 1)
				{
					AddNewPage();
				}
				Console.Clear();
			}
		}
		public static void AddNewPage()
		{
			string question;
			string answer;
			string input;
			while (true)
			{
				Console.WriteLine("Enter the question");
				question = Console.ReadLine();
				Console.WriteLine("___");
				Console.WriteLine("Enter the answer");
				answer = Console.ReadLine();
				Console.WriteLine("___");
				Console.WriteLine($"Your question is:{question}");
				Console.WriteLine("___");
				Console.WriteLine($"Your answer is:{answer}");
				Console.WriteLine("___");
				Console.WriteLine($"Correct?Y/N");
				input = Console.ReadLine();
				if (input.ToLower() == "y")
				{
					CardController.AddAuthomatedCard(question, answer);
					break;
				}
				else
				{
					Console.Clear();
					break;
				}
			}
		}
	}
}
