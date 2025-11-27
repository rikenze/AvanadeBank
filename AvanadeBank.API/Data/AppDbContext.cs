using AvanadeBank.API.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace AvanadeBank.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<BankAccount> Accounts => Set<BankAccount>();
    }

}
