namespace BudgetMe.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v6 : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Transactions", "WalletId");
            AddForeignKey("dbo.Transactions", "WalletId", "dbo.Wallets", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Transactions", "WalletId", "dbo.Wallets");
            DropIndex("dbo.Transactions", new[] { "WalletId" });
        }
    }
}
