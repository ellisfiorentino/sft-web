namespace sft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userids : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comments", "UserId", c => c.Guid(nullable: false));
            AddColumn("dbo.NewsArticles", "AuthorId", c => c.Guid(nullable: false));
            AddColumn("dbo.NewsArticles", "EditorId", c => c.Guid(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.NewsArticles", "EditorId");
            DropColumn("dbo.NewsArticles", "AuthorId");
            DropColumn("dbo.Comments", "UserId");
        }
    }
}
