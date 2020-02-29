using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace KogniMnemo.DAL
{
    public class Account
    {
        public static int AccountId { get; set; }

        public static string Name { get; set; }

        public static Dictionary<byte, long> EbbinghausCurve { get; set; }

        public List<Card> Cards { get; set; }

    }
}

