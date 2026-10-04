using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Server
{
    internal class ServerDBContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<MSG> Messages { get; set; }
        public ServerDBContext() { }
        public ServerDBContext(DbContextOptions<ServerDBContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("jsconfig1.json").Build();
            optionsBuilder.UseSqlServer(config.GetConnectionString("SqlClient"));
            base.OnConfiguring(optionsBuilder);
        }
    }
}
