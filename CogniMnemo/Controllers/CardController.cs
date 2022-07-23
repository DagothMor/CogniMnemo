using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CogniMnemo.Controllers
{
    /// <summary>
    /// Card controller.
    /// </summary>
    public static class CardController
    {
        private const string DATA_BASE_FOLDER = @"CorgiMnemoDataBase\";

        /// <summary>
        /// Added a working card.
        /// </summary>
        /// <param name="question">question</param>
        /// <param name="answer">answer</param>
        public static void AddAuthomatedCard(string question, string answer)
        {

            var dataBaseFolder = $"{AppContext.BaseDirectory}{DATA_BASE_FOLDER}";
            string pathToWrite = dataBaseFolder + $"{GetNumberOfCardsInDataBaseFolder()}.txt";
            try
            {
                using StreamWriter sw = new StreamWriter(pathToWrite);
                sw.Write(FullTemplateCardWithAnswerAndQuestion(question, answer));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        /// <summary>
        /// FullTemplateCardWithAnswerAndQuestion.
        /// </summary>
        /// <param name="path"></param>
        public static string FullTemplateCardWithAnswerAndQuestion(string question, string answer)
        {
            // 6.1 Было text, стало textFromMnemoCard
            var textFromMnemoCard = new StringBuilder();
            textFromMnemoCard.Append("[date of creation]" + DateTime.Now.ToString() + Environment.NewLine);
            textFromMnemoCard.Append("[date of last recall]" + DateTime.Now.ToString() + Environment.NewLine);
            textFromMnemoCard.Append("[level-]0" + Environment.NewLine);
            textFromMnemoCard.Append("[date of next recall]" + EbbinghausCurve.GetTimeRecallByForgettingCurve(DateTime.Now, 0, '+').ToString() + Environment.NewLine);
            textFromMnemoCard.Append("[zerolinks]" + Environment.NewLine);
            textFromMnemoCard.Append("[links]" + Environment.NewLine);
            textFromMnemoCard.Append("[tags]" + Environment.NewLine);
            textFromMnemoCard.Append("[question]" + question + Environment.NewLine);
            textFromMnemoCard.Append("[answer]" + answer + Environment.NewLine);
            return textFromMnemoCard.ToString();
        }
        /// <summary>
        /// Rewriting manual inserted card to working.
        /// </summary>
        public static void CreateWorkCardFromManualInsertedCard(string path)
        {
            try
            {
                File.WriteAllText(path, string.Empty);
                var text = File.ReadAllText(path);
                string question = TextController.ParsingTextFromCardAttribute(text, "[?]");
                string answer = TextController.ParsingTextFromCardAttribute(text, "[!]");
                using (StreamWriter sw = new StreamWriter(path))
                {

                    sw.Write(FullTemplateCardWithAnswerAndQuestion(question, answer));
                }
                Console.WriteLine("recording completed.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        /// <summary>
        /// Get count of cards in database.
        /// </summary>
        /// <returns></returns>
        public static int GetNumberOfCardsInDataBaseFolder()
        {
            // 6.1 Было count, стало countOfValidatedCards
            int countOfValidatedCards = 0;
            // было rawlist стало countOfAllCards.
            var countOfAllCards = Directory.GetFiles($"{AppContext.BaseDirectory}" + DATA_BASE_FOLDER);
            foreach (string path in countOfAllCards)
            {
                if (CardNameIsValid(path)) { countOfValidatedCards++; }
            }
            return countOfValidatedCards;
        }


        /// <summary>
        /// Checking for valid card name.
        /// </summary>
        /// <param name="pathfile">path file</param>
        /// <returns></returns>
        public static bool CardNameIsValid(string pathfile)
        {
            // 7.2 Добавлена bool PathFileIsFound
            var PathFileIsFound = int.TryParse(Path.GetFileNameWithoutExtension(pathfile), out _);
            if (PathFileIsFound)
            {
                if (TextController.AutomatedCardHasAllAttributes(pathfile))
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// needed for pagination
        /// </summary>
        public static void DisplayAllCards()
        {
            var paths = Directory.GetFiles($"{AppContext.BaseDirectory}" + DATA_BASE_FOLDER);
            var cards = new List<string>();
            foreach (var card in paths)
            {
                cards.Add("Card id:" + Path.GetFileNameWithoutExtension(card) + Environment.NewLine + File.ReadAllText(card));
            }
            foreach (var card in cards)
            {
                Console.Write(card);
                Console.WriteLine("_____________");
            }
            Console.WriteLine("Press enter for back to Card menu.");
            Console.ReadLine();
        }
        public static void DisplayCardById(int number)
        {
            try
            {
                // 6.1 Было text, стало textFromMnemoCard
                string textFromMnemoCard = File.ReadAllText($"{AppContext.BaseDirectory}" + DATA_BASE_FOLDER + $"{number}" + ".txt");
                Console.Write(textFromMnemoCard);
                Console.WriteLine("_____________");
                Console.WriteLine("Press enter for back to Card menu.");
                Console.ReadLine();
            }
            catch (Exception)
            {

                Console.WriteLine("Bad request, press enter for back to Card Menu");
                Console.ReadLine();
            }
        }
        public static Card GetCardFromPathFile(string path)
        {
            try
            {
                return CreateCardFromPathWithInputedText(path);
            }
            catch (Exception e)
            {
                throw;
            }
            
        }

        public static Card CreateCardFromPathWithInputedText(string path)
        {
            var textFromMnemoCard = File.ReadAllText(path);
            Card card;
            int id = int.Parse(Path.GetFileNameWithoutExtension(path));
            DateTime dateOfCreation = DateTime.Parse(TextController.ParsingTextFromCardAttribute(textFromMnemoCard, "[date of creation]"));
            DateTime dateOfLastRecall = DateTime.Parse(TextController.ParsingTextFromCardAttribute(textFromMnemoCard, "[date of last recall]"));
            byte level = byte.Parse(TextController.ParsingTextFromCardAttribute(textFromMnemoCard, "[level-]"));
            DateTime dateOfNextRecall = DateTime.Parse(TextController.ParsingTextFromCardAttribute(textFromMnemoCard, "[date of next recall]"));
            string question = TextController.ParsingTextFromCardAttribute(textFromMnemoCard, "[question]");
            string answer = TextController.ParsingTextFromCardAttribute(textFromMnemoCard, "[answer]");
            card = new Card
            {
                Id = id,
                DateOfCreation = dateOfCreation,
                DateOfLastRecall = dateOfLastRecall,
                Level = level,
                DateOfNextRecall = dateOfNextRecall,
                Question = question,
                Answer = answer
            };
            return card;
        }

        // 6.1 Было list стало listOfMnemoCards.
        public static Card GetOldestCard(List<Card> listOfMnemoCards)
        {
            var buffercard = new Card();
            TimeSpan interval = new TimeSpan();
            buffercard.DateOfNextRecall = DateTime.Now;
            var cardWithBiggerInterval = new Card();

            foreach (var card in listOfMnemoCards)
            {
                if (interval < buffercard.DateOfNextRecall - card.DateOfNextRecall)
                {
                    interval = buffercard.DateOfNextRecall - card.DateOfNextRecall;
                    cardWithBiggerInterval = card;
                }
            }
            return cardWithBiggerInterval;
        }

    }
}
