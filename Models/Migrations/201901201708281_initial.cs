namespace TaskTracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accounts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Login = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AccountId = c.Int(nullable: false),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Accounts", t => t.AccountId, cascadeDelete: true)
                .Index(t => t.AccountId);
            
            CreateTable(
                "dbo.Tasks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Shedule = c.DateTime(),
                        Status = c.Int(nullable: false),
                        Account_Id = c.Int(),
                        Project_Id = c.Int(nullable:true),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Accounts", t => t.Account_Id)
                .ForeignKey("dbo.Projects", t => t.Project_Id)
                .Index(t => t.Account_Id)
                .Index(t => t.Project_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tasks", "Project_Id", "dbo.Projects");
            DropForeignKey("dbo.Tasks", "Account_Id", "dbo.Accounts");
            DropForeignKey("dbo.Projects", "AccountId", "dbo.Accounts");
            DropIndex("dbo.Tasks", new[] { "Project_Id" });
            DropIndex("dbo.Tasks", new[] { "Account_Id" });
            DropIndex("dbo.Projects", new[] { "AccountId" });
            DropTable("dbo.Tasks");
            DropTable("dbo.Projects");
            DropTable("dbo.Accounts");
        }
    }
}
