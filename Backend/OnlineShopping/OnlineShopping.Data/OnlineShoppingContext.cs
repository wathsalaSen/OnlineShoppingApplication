using Microsoft.EntityFrameworkCore;
using OnlineShopping.Common.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShopping.Data
{
    public class OnlineShoppingContext : DbContext
    {
        public DbSet<User> user { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.;Database=OnlineShopping;Trusted_Connection=True;");
        }
    }
}
