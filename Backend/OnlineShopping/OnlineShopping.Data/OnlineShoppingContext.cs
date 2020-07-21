using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using OnlineShopping.Common.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShopping.Data
{
    public class OnlineShoppingContext : DbContext
    {
        public OnlineShoppingContext(DbContextOptions<OnlineShoppingContext> options, IHostEnvironment host) : base(options)
        {
            if (host.IsDevelopment() || host.IsEnvironment("Test"))
            {
                return;
            }
            var conn = (SqlConnection)Database.GetDbConnection();
        }

        public DbSet<User> user { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer(@"Server=.;Database=OnlineShopping;Trusted_Connection=True;");
        }
    }
}
