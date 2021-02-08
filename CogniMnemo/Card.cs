using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CogniMnemo
{
	public class Card
	{
		public DateTime DateOfCreation { get; set; }
		public DateTime DateOfLastRecall { get; set; }
		public int Level { get; set; }
		public string Question { get; set; }
		public string Answer { get; set; }
	}
}
