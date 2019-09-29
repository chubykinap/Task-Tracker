namespace TaskTracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class edit_1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Projects", "AccountId", "dbo.Accounts");
            DropIndex("dbo.Projects", new[] { "AccountId" });
            RenameColumn(table: "dbo.Projects", name: "AccountId", newName: "Account_Id");
            AlterColumn("dbo.Projects", "Account_Id", c => c.Int());
            CreateIndex("dbo.Projects", "Account_Id");
            AddForeignKey("dbo.Projects", "Account_Id", "dbo.Accounts", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Projects", "Account_Id", "dbo.Accounts");
            DropIndex("dbo.Projects", new[] { "Account_Id" });
            AlterColumn("dbo.Projects", "Account_Id", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Projects", name: "Account_Id", newName: "AccountId");
            CreateIndex("dbo.Projects", "AccountId");
            AddForeignKey("dbo.Projects", "AccountId", "dbo.Accounts", "Id", cascadeDelete: true);
        }
    }
}
