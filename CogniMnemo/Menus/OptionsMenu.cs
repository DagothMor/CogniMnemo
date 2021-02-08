using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CogniMnemo.Menus
{
	public static class OptionsMenu
	{
		public static void Start()
		{
			while (true)
			{
				Console.WriteLine("You are in Options!");
				Console.WriteLine(
					"1 - something" + Environment.NewLine +
					"2 - something" + Environment.NewLine +
					"back - Back to main menu");
				var input = Console.ReadLine();
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
					Console.Clear();
				}
				else if (int.Parse(input) == 2)
				{
					Console.Clear();
				}
				Console.Clear();
			}
		}
	}
}
