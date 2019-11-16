namespace BudgetMe.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v51 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Wallets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Description = c.String(),
                        UserId = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Transactions", "WalletId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Transactions", "WalletId");
            DropTable("dbo.Wallets");
        }
    }
}
