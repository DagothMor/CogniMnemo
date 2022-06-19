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
		/// <summary>
		/// Return a text after a inputed attribute.
		/// </summary>
		/// <param name="text">text of file</param>
		/// <param name="attributeFlag">the attribute after which you want to read the text</param>
		/// <returns>text which going after a pointed attribute</returns>
		public static string ParsingTextFromManualOrAuthomatedInsertCard(string text, string attributeFlag)
		{
			// 6.4 Было list стало listOfCharsFromCards.
			var listOfCharsFromCards = text.ToCharArray().ToList();
			var listOfAttributes = new List<string>() { "[date of creation]", "[date of last recall]", "[level-]", "[date of next recall]", "[question]", "[answer]", "[!]", "[?]", "[zerolinks]", "[tags]", "[links]" };
			// 6.4 было iteration, стало charIndexInAttribute.
			var charIndexInAttribute = text.IndexOf(attributeFlag);
			bool insideattribute = false;
			var attributeWordBuffer = new StringBuilder();
			// 6.1 Было textout стало parsedTextFromCard.
			var parsedTextFromCard = new StringBuilder();
			for (; charIndexInAttribute < listOfCharsFromCards.Count; charIndexInAttribute++)
			{
				if (text[charIndexInAttribute] == ']')
				{
					charIndexInAttribute++;
					break;
				}
			}
			// 6.4 было indexLetterAfterAtributeFlag стало charIndexInValue
			var charIndexInValue = charIndexInAttribute;
			for (; charIndexInValue < listOfCharsFromCards.Count; charIndexInValue++)
			{
				if (insideattribute == true)
				{
					if (text[charIndexInValue] == ']')
					{
						attributeWordBuffer.Append(listOfCharsFromCards[charIndexInValue]);
						string attributeWord = attributeWordBuffer.ToString();
						if (listOfAttributes.Contains(attributeWord))
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
						attributeWordBuffer.Append(listOfCharsFromCards[charIndexInValue]);
					}
				}
				else
				{
					if (text[charIndexInValue] == '[')
					{
						attributeWordBuffer.Append('[');
						insideattribute = true;
						continue;
					}
					else
					{
						parsedTextFromCard.Append(listOfCharsFromCards[charIndexInValue]);
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
			string path = $"{ AppContext.BaseDirectory }" + @"CorgiMnemoDataBase\"+$"{id}"+".txt";
			// 6.1 было cardtext стало textFromMnemoCard.
			var textFromMnemoCard = File.ReadAllText(path);
			var listOfCharsFromCard = textFromMnemoCard.ToCharArray().ToList();
			var listOfAttributes = new List<string>() { "[date of creation]", "[date of last recall]", "[level-]", "[date of next recall]", "[question]", "[answer]", "[!]", "[?]", "[zerolinks]", "[tags]", "[links]" };
			var iterationOfCardText = textFromMnemoCard.IndexOf(attributeFlag);
			bool insideAnAttribute = false;
			var attributeWordBuffer = new StringBuilder();
			var textout = new StringBuilder();

			var startdeletebufferindex = 0;
			var enddeletebufferindex = 0;

			for (; iterationOfCardText < listOfCharsFromCard.Count; iterationOfCardText++)
			{
				if (listOfCharsFromCard[iterationOfCardText] == ']')
				{
					iterationOfCardText++;
					break;
				}
			}


			for (; iterationOfCardText < listOfCharsFromCard.Count; iterationOfCardText++)
			{
				if (insideAnAttribute == true)
				{
					if (listOfCharsFromCard[iterationOfCardText] == ']')
					{
						attributeWordBuffer.Append(listOfCharsFromCard[iterationOfCardText]);
						enddeletebufferindex++;
						string attributeWord = attributeWordBuffer.ToString();
						if (listOfAttributes.Contains(attributeWord))
						{
							//textout.ToString().Replace("\r\n", string.Empty);//appendig other files
							break;
						}
						listOfCharsFromCard.RemoveRange(startdeletebufferindex, enddeletebufferindex);
						insideAnAttribute = false;
						attributeWordBuffer.Clear();
						startdeletebufferindex = 0;
						enddeletebufferindex = 0;
						continue;
					}
					else
					{
						attributeWordBuffer.Append(listOfCharsFromCard[iterationOfCardText]);
						enddeletebufferindex++;
					}
				}
				else
				{
					if (listOfCharsFromCard[iterationOfCardText] == '[')
					{
						attributeWordBuffer.Append('[');
						startdeletebufferindex = iterationOfCardText;
						enddeletebufferindex = 1;
						insideAnAttribute = true;
						continue;
					}
					else
					{
						listOfCharsFromCard.RemoveAt(iterationOfCardText);
						iterationOfCardText--;
					}
				}
			}
			iterationOfCardText = textFromMnemoCard.IndexOf(attributeFlag);

			for (; iterationOfCardText < listOfCharsFromCard.Count; iterationOfCardText++)
			{
				if (listOfCharsFromCard[iterationOfCardText] == ']')
				{
					iterationOfCardText++;
					break;
				}
			}
			//cardTextList.Insert(iterationOfCardText, ' ');
			//iterationOfCardText++;
			for(int i = replacementText.Length-1; i > -1; i--)
			{
				listOfCharsFromCard.Insert(iterationOfCardText, replacementText[i]);
			}
			foreach (var letter in listOfCharsFromCard)
			{
				textout.Append(letter);
			}
			File.WriteAllText(path, textout.ToString());
		}

		/// <summary>
		/// Checks the text validity of the card.
		/// </summary>
		/// <param name="pathfile">path file</param>
		/// <returns>true if card have all automated attributes</returns>
		public static bool AutomatedInsertCardTextValidation(string pathfile)
		{
			var card = File.ReadAllText(pathfile);
			if (card.Contains("[date of creation]"))
			{
				if (card.Contains("[date of last recall]"))
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
