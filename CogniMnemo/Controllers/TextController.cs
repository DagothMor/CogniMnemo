using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CogniMnemo.Controllers
{
	/// <summary>
	/// Text controller.
	/// </summary>
	public static class TextController
	{
        private const string DATA_BASE_FOLDER = @"CorgiMnemoDataBase\";

        /// <summary>
        /// Return a text after a inputed attribute.
        /// </summary>
        /// <param name="cardText">text of file</param>
        /// <param name="attributeFlag">the attribute after which you want to read the text</param>
        /// <returns>text which going after a pointed attribute</returns>
        public static string ParsingTextFromManualOrAuthomatedInsertCard(string cardText, string attributeFlag)
		{
			// 6.4 Было list стало listOfCharsFromCards.
			var сharsFromCard = cardText.ToCharArray().ToList();
			var charIndexInAttribute = cardText.IndexOf(attributeFlag);
			for (; charIndexInAttribute < сharsFromCard.Count; charIndexInAttribute++)
			{
				if (cardText[charIndexInAttribute] == ']')
				{
					charIndexInAttribute++;
					break;
				}
			}
			var attributes = new List<string>() { "[date of creation]", "[date of last recall]", "[level-]", "[date of next recall]", "[question]", "[answer]", "[!]", "[?]", "[zerolinks]", "[tags]", "[links]" };
			// 6.4 было iteration, стало charIndexInAttribute.
			bool insideattribute = false;
			// было insideAnAttribute стало isInAttribute
			var attributeWordBuffer = new StringBuilder();
			// 6.1 Было textout стало parsedTextFromCard.
			var parsedTextFromCard = new StringBuilder();
			// 6.4 было indexLetterAfterAtributeFlag стало charIndexInValue
			var charIndexInValue = charIndexInAttribute;
			for (; charIndexInValue < сharsFromCard.Count; charIndexInValue++)
			{
				if (insideattribute == true)
				{
					if (cardText[charIndexInValue] == ']')
					{
						attributeWordBuffer.Append(сharsFromCard[charIndexInValue]);
						string attributeWord = attributeWordBuffer.ToString();
						if (attributes.Contains(attributeWord))
						{
							return parsedTextFromCard.ToString().Replace("\r\n", string.Empty);
						}
						insideattribute = false;
						parsedTextFromCard.Append(attributeWordBuffer);
						attributeWordBuffer.Clear();
						continue;
					}
					else
					{
						attributeWordBuffer.Append(сharsFromCard[charIndexInValue]);
					}
				}
				else
				{
					if (cardText[charIndexInValue] == '[')
					{
						attributeWordBuffer.Append('[');
						insideattribute = true;
						continue;
					}
					else
					{
						parsedTextFromCard.Append(сharsFromCard[charIndexInValue]);
					}
				}
			}
			return parsedTextFromCard.ToString();
		}

		/// <summary>
		/// rewrite a text after an input attribute.
		/// </summary>
		/// <param name="cardtext">text of file</param>
		/// <param name="attributeFlag">the attribute after which you want to read the text</param>
		/// <returns>text which going after a pointed attribute</returns>
		public static void RewriteTextFromWorkableCard(int id, string attributeFlag,string replacementText) // 6.1 Было newtext стало replacementText
		{
			// Был переименован path в pathToDataBaseFolder
			string pathToDataBaseFolder = $"{AppContext.BaseDirectory}" + DATA_BASE_FOLDER + $"{id}" + ".txt";
			// 6.1 было cardtext стало textFromMnemoCard.
			var textFromMnemoCard = File.ReadAllText(pathToDataBaseFolder);
			var сharsFromCard = textFromMnemoCard.ToCharArray().ToList();
			var endIndexOfAtribute = textFromMnemoCard.IndexOf(attributeFlag);
			for (; endIndexOfAtribute < сharsFromCard.Count; endIndexOfAtribute++)
			{
				if (сharsFromCard[endIndexOfAtribute] == ']')
				{
					endIndexOfAtribute++;
					break;
				}
			}
			var startIndexOfText = endIndexOfAtribute;
			endIndexOfAtribute = -1;
			// 7.1 было insideAnAttribute стало isInAttribute
			bool isInAttribute = false;
			var attributeWordBuffer = new StringBuilder();
			var startdeletebufferindex = 0;
			var enddeletebufferindex = 0;
			var attributes = new List<string>() { "[date of creation]", "[date of last recall]", "[level-]", "[date of next recall]", "[question]", "[answer]", "[!]", "[?]", "[zerolinks]", "[tags]", "[links]" };

			for (; startIndexOfText < сharsFromCard.Count; startIndexOfText++)
			{
				if (isInAttribute == true)
				{
					if (сharsFromCard[startIndexOfText] == ']')
					{
						attributeWordBuffer.Append(сharsFromCard[startIndexOfText]);
						enddeletebufferindex++;
						string attributeWord = attributeWordBuffer.ToString();
						if (attributes.Contains(attributeWord))
						{
							//textout.ToString().Replace("\r\n", string.Empty);//appendig other files
							break;
						}
						сharsFromCard.RemoveRange(startdeletebufferindex, enddeletebufferindex);
						isInAttribute = false;
						attributeWordBuffer.Clear();
						startdeletebufferindex = 0;
						enddeletebufferindex = 0;
						continue;
					}
					else
					{
						attributeWordBuffer.Append(сharsFromCard[startIndexOfText]);
						enddeletebufferindex++;
					}
				}
				else
				{
					if (сharsFromCard[startIndexOfText] == '[')
					{
						attributeWordBuffer.Append('[');
						startdeletebufferindex = startIndexOfText;
						enddeletebufferindex = 1;
						isInAttribute = true;
						continue;
					}
					else
					{
						сharsFromCard.RemoveAt(startIndexOfText);
						startIndexOfText--;
					}
				}
			}
			startIndexOfText = textFromMnemoCard.IndexOf(attributeFlag);

			for (; startIndexOfText < сharsFromCard.Count; startIndexOfText++)
			{
				if (сharsFromCard[startIndexOfText] == ']')
				{
					startIndexOfText++;
					break;
				}
			}
			//cardTextList.Insert(iterationOfCardText, ' ');
			//iterationOfCardText++;
			for(int i = replacementText.Length-1; i > -1; i--)
			{
				сharsFromCard.Insert(startIndexOfText, replacementText[i]);
			}
			var rewritedTextOfCard = new StringBuilder();
			foreach (var letter in сharsFromCard)
			{
				rewritedTextOfCard.Append(letter);
			}
			File.WriteAllText(pathToDataBaseFolder, rewritedTextOfCard.ToString());
		}

		/// <summary>
		/// Checks the text validity of the card.
		/// Нужно проверять первые символы строк до какогото определенного флага.
		/// </summary>
		/// <param name="pathfile">path file</param>
		/// <returns>true if card have all automated attributes</returns>
		public static bool AutomatedInsertCardTextValidation(string pathfile)
		{
			var card = File.ReadAllText(pathfile);
            if (card.Contains("[date of creation]") && card.Contains("[date of last recall]"))
            {
                if (card.Contains("[level-]"))
                {
                    if (card.Contains("[date of next recall]"))
                    {
                        if (card.Contains("[question]"))
                        {
                            if (card.Contains("[answer]"))
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
		}
		/// <summary>
		/// Checks the text validity of the card which inserted manual.
		/// </summary>
		/// <param name="pathfile">path file</param>
		/// <returns>true if card have all manual attributes</returns>
		public static bool ManualInsertCardTextValidation(string pathfile)
		{
			var card = File.ReadAllText(pathfile);
			if (card.Contains("[?]"))
			{
				if (card.Contains("[!]"))
				{
					return true;
				}
			}
			return false;
		}

	}
}
