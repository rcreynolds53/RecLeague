namespace RecLeagueBlog.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BlogPosts",
                c => new
                    {
                        BlogPostId = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 100),
                        BlogContent = c.String(),
                        DateCreated = c.DateTime(nullable: false),
                        StatusId = c.Int(nullable: false),
                        EmployeeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BlogPostId)
                .ForeignKey("dbo.Employees", t => t.EmployeeId, cascadeDelete: true)
                .ForeignKey("dbo.Status", t => t.StatusId, cascadeDelete: true)
                .Index(t => t.StatusId)
                .Index(t => t.EmployeeId);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(nullable: false, maxLength: 25),
                    })
                .PrimaryKey(t => t.CategoryId);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        EmployeeId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 30),
                        LastName = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.EmployeeId);
            
            CreateTable(
                "dbo.Status",
                c => new
                    {
                        StatusId = c.Int(nullable: false, identity: true),
                        StatusName = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.StatusId);
            
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        TagId = c.Int(nullable: false, identity: true),
                        TagName = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.TagId);
            
            CreateTable(
                "dbo.StaticPages",
                c => new
                    {
                        StaticPageId = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 50),
                        PageContent = c.String(),
                    })
                .PrimaryKey(t => t.StaticPageId);
            
            CreateTable(
                "dbo.CategoryBlogPosts",
                c => new
                    {
                        Category_CategoryId = c.Int(nullable: false),
                        BlogPost_BlogPostId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Category_CategoryId, t.BlogPost_BlogPostId })
                .ForeignKey("dbo.Categories", t => t.Category_CategoryId, cascadeDelete: true)
                .ForeignKey("dbo.BlogPosts", t => t.BlogPost_BlogPostId, cascadeDelete: true)
                .Index(t => t.Category_CategoryId)
                .Index(t => t.BlogPost_BlogPostId);
            
            CreateTable(
                "dbo.TagBlogPosts",
                c => new
                    {
                        Tag_TagId = c.Int(nullable: false),
                        BlogPost_BlogPostId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Tag_TagId, t.BlogPost_BlogPostId })
                .ForeignKey("dbo.Tags", t => t.Tag_TagId, cascadeDelete: true)
                .ForeignKey("dbo.BlogPosts", t => t.BlogPost_BlogPostId, cascadeDelete: true)
                .Index(t => t.Tag_TagId)
                .Index(t => t.BlogPost_BlogPostId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TagBlogPosts", "BlogPost_BlogPostId", "dbo.BlogPosts");
            DropForeignKey("dbo.TagBlogPosts", "Tag_TagId", "dbo.Tags");
            DropForeignKey("dbo.BlogPosts", "StatusId", "dbo.Status");
            DropForeignKey("dbo.BlogPosts", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.CategoryBlogPosts", "BlogPost_BlogPostId", "dbo.BlogPosts");
            DropForeignKey("dbo.CategoryBlogPosts", "Category_CategoryId", "dbo.Categories");
            DropIndex("dbo.TagBlogPosts", new[] { "BlogPost_BlogPostId" });
            DropIndex("dbo.TagBlogPosts", new[] { "Tag_TagId" });
            DropIndex("dbo.CategoryBlogPosts", new[] { "BlogPost_BlogPostId" });
            DropIndex("dbo.CategoryBlogPosts", new[] { "Category_CategoryId" });
            DropIndex("dbo.BlogPosts", new[] { "EmployeeId" });
            DropIndex("dbo.BlogPosts", new[] { "StatusId" });
            DropTable("dbo.TagBlogPosts");
            DropTable("dbo.CategoryBlogPosts");
            DropTable("dbo.StaticPages");
            DropTable("dbo.Tags");
            DropTable("dbo.Status");
            DropTable("dbo.Employees");
            DropTable("dbo.Categories");
            DropTable("dbo.BlogPosts");
        }
    }
}
