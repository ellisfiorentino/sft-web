using System.Data.Entity.Migrations;

namespace sft.Migrations
{
  public partial class basesite : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        IsApproved = c.Boolean(nullable: false),
                        Text = c.String(),
                        Created = c.DateTime(nullable: false),
                        Modified = c.DateTime(nullable: false),
                        User = c.String(),
                        IsSubComment = c.Boolean(nullable: false),
                        SubCommentFrom = c.Guid(),
                        Comment_Id = c.Guid(),
                        NewsArticle_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Comments", t => t.Comment_Id)
                .ForeignKey("dbo.NewsArticles", t => t.NewsArticle_Id)
                .Index(t => t.Comment_Id)
                .Index(t => t.NewsArticle_Id);
            
            CreateTable(
                "dbo.NewsArticles",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        UrlName = c.String(maxLength: 255),
                        Name = c.String(),
                        Text = c.String(),
                        HasPicture = c.Boolean(nullable: false),
                        MainPictureLocation = c.String(),
                        Created = c.DateTime(nullable: false),
                        Modified = c.DateTime(nullable: false),
                        Author = c.String(),
                        Editor = c.String(),
                        IsCommentingEnabled = c.Boolean(nullable: false),
                        IsHighLighted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UrlName, unique: true, name: "UrlName");
            
            CreateTable(
                "dbo.Pictures",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Name = c.String(),
                        FileName = c.String(),
                        Rental_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Rentals", t => t.Rental_Id)
                .Index(t => t.Rental_Id);
            
            CreateTable(
                "dbo.Rentals",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        SimId = c.Guid(nullable: false),
                        Name = c.String(maxLength: 255),
                        Description = c.String(),
                        AllowedPrim = c.Int(nullable: false),
                        WeekPrice = c.Int(nullable: false),
                        Xcord = c.Int(nullable: false),
                        Ycord = c.Int(nullable: false),
                        Zcord = c.Int(nullable: false),
                        Available = c.Boolean(nullable: false),
                        StreetName = c.String(),
                        HouseNumber = c.String(),
                        IsHighLighted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Sims", t => t.SimId, cascadeDelete: true)
                .Index(t => t.SimId)
                .Index(t => t.Name, unique: true, name: "RentalName");
            
            CreateTable(
                "dbo.Sims",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        SimName = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.SimName, unique: true, name: "SimName");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Rentals", "SimId", "dbo.Sims");
            DropForeignKey("dbo.Pictures", "Rental_Id", "dbo.Rentals");
            DropForeignKey("dbo.Comments", "NewsArticle_Id", "dbo.NewsArticles");
            DropForeignKey("dbo.Comments", "Comment_Id", "dbo.Comments");
            DropIndex("dbo.Sims", "SimName");
            DropIndex("dbo.Rentals", "RentalName");
            DropIndex("dbo.Rentals", new[] { "SimId" });
            DropIndex("dbo.Pictures", new[] { "Rental_Id" });
            DropIndex("dbo.NewsArticles", "UrlName");
            DropIndex("dbo.Comments", new[] { "NewsArticle_Id" });
            DropIndex("dbo.Comments", new[] { "Comment_Id" });
            DropTable("dbo.Sims");
            DropTable("dbo.Rentals");
            DropTable("dbo.Pictures");
            DropTable("dbo.NewsArticles");
            DropTable("dbo.Comments");
        }
    }
}
