using System;
using System.Collections.Generic;
using System.Text;

namespace KogniMnemo.DAL
{
    public class Card
    {
        public static int CardId { get; set; }
        public static byte Stage { get; set; }
        public static DateTime Created { get; set; }
        public static DateTime Edited { get; set; }
        public static DateTime LastCall { get; set; }
        public static string Question { get; set; }
        public static string Answer { get; set; }
        public static string Theme { get; set; }
        public static int AccountId { get; set; }
        public Account Account { get; set; }
    }
}
