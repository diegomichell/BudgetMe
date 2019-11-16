namespace BudgetMe.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v7 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Wallets", "UserId", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Wallets", "UserId", c => c.String(nullable: false));
        }
    }
}
