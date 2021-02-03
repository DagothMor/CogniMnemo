﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorgiMnemo
{
	public static class TextController
	{
		/// <summary>
		/// Return a text 
		/// </summary>
		/// <param name="text"></param>
		/// <param name="attributeFlag"></param>
		/// <returns></returns>
		public static string ParsingTextFromManualOrAuthomatedInsertCard(string text, string attributeFlag)
		{
			var list = text.ToCharArray().ToList();
			var listOfAttributes = new List<string>() { "[date of creation]", "[date of last recall]", "[level-]", "[question]", "[answer]","[!]","[?]"};
			var listout = new List<char>();
			var iteration = text.IndexOf(attributeFlag);
			bool insideattribute = false;
			var attributeWordBuffer = new StringBuilder();
			var textout = new StringBuilder();
			for (; iteration < list.Count(); iteration++)
			{
				if (text[iteration] == ']')
				{
					iteration++;
					break;
				}
			}
			for (; iteration < list.Count(); iteration++)
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
	}
}
