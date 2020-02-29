using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using KogniMnemo.DAL;
using Microsoft.EntityFrameworkCore;

namespace KogniMnemo
{
    public sealed class ApplicationContext : DbContext
    {
        public DbSet<Card> Cards { get; set; }

        public DbSet<Account> Accounts { get; set; }

        public ApplicationContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBilder)
        {
            optionsBilder.UseSqlServer(
                @"Data Source=1-PC;Initial Catalog=3;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }
    }
}
