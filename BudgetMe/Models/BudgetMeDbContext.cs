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
        public BudgetMeDbContext():base("BudgetMeDbContext")
        {

        }
        public DbSet<Transaction> Transactions { get; set; }
    }
}