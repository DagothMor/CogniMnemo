using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CogniMnemo.Controllers
{
	public static class MockController
	{
        private const string DATA_BASE_FOLDER = @"CorgiMnemoDataBase\";

        public static void CreateListOfTemplateCards()
		{
			CardController.AddAuthomatedCard("kek?", "kek!");
			CardController.AddAuthomatedCard("kak?", "kak!");
			CardController.AddAuthomatedCard("kyk?", "kyk!");
			CardController.AddAuthomatedCard("kuk?", "kuk!");
			CardController.AddAuthomatedCard("kik?", "kik!");
			CardController.AddAuthomatedCard("kok?", "kok!");

		}
		public static void DeleteAllCardsInDataBase() {
			var paths = Directory.GetFiles($"{ AppContext.BaseDirectory }" + DATA_BASE_FOLDER);
			foreach (var path in paths)
			{
				File.Delete(path);
			}
		}
	}
}
