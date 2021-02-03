using System;
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
			var listOfAttributes = new List<string>() { "[date of creation]", "[date of last recall]", "[level-]", "[question]", "[answer]"};
			var listout = new List<char>();
			var iteration = text.IndexOf(attributeFlag);
			bool insideattribute = false;
			var attributeWordBuffer = new StringBuilder();
			var textout = new 
			for (; iteration < list.Count(); iteration++)
			{
				if (text[iteration] == ']')
				{
					iteration++;
					break;
				}
			}
			//todo:text can have a [] symbols what i need todo?
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

						}
						attributeWordBuffer.Clear();

					}
					attributeWordBuffer.Append(list[iteration]);
				}
				else
				{
					if (text[iteration] == '[')
					{
						attributeWordBuffer.Append('[');
						insideattribute = true;
						break;
					}
				}


			}
			return "";

		}
	}
}
