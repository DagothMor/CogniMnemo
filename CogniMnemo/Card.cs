using System;

namespace CogniMnemo
{
	public class Card
	{
		public int Id { get; set; }
		public DateTime DateOfCreation { get; set; }
		public DateTime DateOfLastRecall { get; set; }
		public DateTime DateOfNextRecall { get; set; }
		public byte Level { get; set; }
		public string Zerolinks { get; set; }
		public string Links { get; set; }
		public string Tags { get; set; }
		public string Question { get; set; }
		public string Answer { get; set; }

		public Card()
		{
		}

		public Card(int Id,DateTime DateOfCreation, DateTime DateOfLastRecall, byte Level, string Question, string Answer)
		{
			this.Id = Id;
			this.DateOfCreation = DateOfCreation;
			this.DateOfLastRecall = DateOfLastRecall;
			this.Level = Level;
			this.Question = Question;
			this.Answer = Answer;
		}
	}
}
