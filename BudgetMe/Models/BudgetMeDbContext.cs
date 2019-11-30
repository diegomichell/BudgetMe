using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BudgetMe.Models
{
    public class BudgetMeDbContext : IdentityDbContext<User>
    {
        public BudgetMeDbContext():base("DefaultConnection")
        {
        }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Wallet> Wallets { get; set; }
    }
}