namespace RecLeagueBlog.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class postAndPagesModelChanges : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StaticPages", "Status_StatusId", c => c.Int());
            CreateIndex("dbo.StaticPages", "Status_StatusId");
            AddForeignKey("dbo.StaticPages", "Status_StatusId", "dbo.Status", "StatusId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StaticPages", "Status_StatusId", "dbo.Status");
            DropIndex("dbo.StaticPages", new[] { "Status_StatusId" });
            DropColumn("dbo.StaticPages", "Status_StatusId");
        }
    }
}
