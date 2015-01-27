namespace sft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nullableeditor : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.NewsArticles", "EditorId", c => c.Guid());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.NewsArticles", "EditorId", c => c.Guid(nullable: false));
        }
    }
}
