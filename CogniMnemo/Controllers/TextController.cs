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
			var list = text.ToCharArray().ToList();
			var listOfAttributes = new List<string>() { "[date of creation]", "[date of last recall]", "[level-]", "[date of next recall]", "[question]", "[answer]", "[!]", "[?]", "[zerolinks]", "[tags]", "[links]" };
			var iteration = text.IndexOf(attributeFlag);
			bool insideattribute = false;
			var attributeWordBuffer = new StringBuilder();
			var textout = new StringBuilder();
			for (; iteration < list.Count; iteration++)
			{
				if (text[iteration] == ']')
				{
					iteration++;
					break;
				}
			}
			for (; iteration < list.Count; iteration++)
			{
				if (insideattribute == true)
				{
					if (text[iteration] == ']')
					{
						attributeWordBuffer.Append(list[iteration]);
						string attributeWord = attributeWordBuffer.ToString();
						if (listOfAttributes.Contains(attributeWord))
						{
							return textout.ToString().Replace("\r\n", string.Empty);
						}
						insideattribute = false;
						textout.Append(attributeWordBuffer);
						attributeWordBuffer.Clear();
						continue;
					}
					else
					{
						attributeWordBuffer.Append(list[iteration]);
					}
				}
				else
				{
					if (text[iteration] == '[')
					{
						attributeWordBuffer.Append('[');
						insideattribute = true;
						continue;
					}
					else
					{
						textout.Append(list[iteration]);
					}
				}
			}
			return textout.ToString();
		}

		/// <summary>
		/// rewrite a text after an input attribute.
		/// </summary>
		/// <param name="cardtext">text of file</param>
		/// <param name="attributeFlag">the attribute after which you want to read the text</param>
		/// <returns>text which going after a pointed attribute</returns>
		public static void RewriteTextFromWorkableCard(int id, string attributeFlag,string newtext)
		{
			string path = $"{ AppContext.BaseDirectory }" + @"CorgiMnemoDataBase\"+$"{id}"+".txt";
			var cardtext = File.ReadAllText(path);
			var cardTextList = cardtext.ToCharArray().ToList();
			var listOfAttributes = new List<string>() { "[date of creation]", "[date of last recall]", "[level-]", "[date of next recall]", "[question]", "[answer]", "[!]", "[?]", "[zerolinks]", "[tags]", "[links]" };
			var iterationOfCardText = cardtext.IndexOf(attributeFlag);
			bool insideAnAttribute = false;
			var attributeWordBuffer = new StringBuilder();
			var textout = new StringBuilder();

			var startdeletebufferindex = 0;
			var enddeletebufferindex = 0;

			for (; iterationOfCardText < cardTextList.Count; iterationOfCardText++)
			{
				if (cardTextList[iterationOfCardText] == ']')
				{
					iterationOfCardText++;
					break;
				}
			}


			for (; iterationOfCardText < cardTextList.Count; iterationOfCardText++)
			{
				if (insideAnAttribute == true)
				{
					if (cardTextList[iterationOfCardText] == ']')
					{
						attributeWordBuffer.Append(cardTextList[iterationOfCardText]);
						enddeletebufferindex++;
						string attributeWord = attributeWordBuffer.ToString();
						if (listOfAttributes.Contains(attributeWord))
						{
							//textout.ToString().Replace("\r\n", string.Empty);//appendig other files
							break;
						}
						cardTextList.RemoveRange(startdeletebufferindex, enddeletebufferindex);
						insideAnAttribute = false;
						attributeWordBuffer.Clear();
						startdeletebufferindex = 0;
						enddeletebufferindex = 0;
						continue;
					}
					else
					{
						attributeWordBuffer.Append(cardTextList[iterationOfCardText]);
						enddeletebufferindex++;
					}
				}
				else
				{
					if (cardTextList[iterationOfCardText] == '[')
					{
						attributeWordBuffer.Append('[');
						startdeletebufferindex = iterationOfCardText;
						enddeletebufferindex = 1;
						insideAnAttribute = true;
						continue;
					}
					else
					{
						cardTextList.RemoveAt(iterationOfCardText);
						iterationOfCardText--;
					}
				}
			}
			iterationOfCardText = cardtext.IndexOf(attributeFlag);

			for (; iterationOfCardText < cardTextList.Count; iterationOfCardText++)
			{
				if (cardTextList[iterationOfCardText] == ']')
				{
					iterationOfCardText++;
					break;
				}
			}
			//cardTextList.Insert(iterationOfCardText, ' ');
			//iterationOfCardText++;
			for(int i = newtext.Length-1; i > -1; i--)
			{
				cardTextList.Insert(iterationOfCardText, newtext[i]);
			}
			foreach (var letter in cardTextList)
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
