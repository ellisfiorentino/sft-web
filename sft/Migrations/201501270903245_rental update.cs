namespace sft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class rentalupdate : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Pictures", "Rental_Id", "dbo.Rentals");
            DropIndex("dbo.Pictures", new[] { "Rental_Id" });
            DropIndex("dbo.Rentals", "RentalName");
            AddColumn("dbo.Rentals", "UrlName", c => c.String(maxLength: 255));
            AlterColumn("dbo.Rentals", "Name", c => c.String());
            DropColumn("dbo.Pictures", "Rental_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Pictures", "Rental_Id", c => c.Guid());
           
            AlterColumn("dbo.Rentals", "Name", c => c.String(maxLength: 255));
            DropColumn("dbo.Rentals", "UrlName");
            CreateIndex("dbo.Rentals", "Name", unique: true, name: "RentalName");
            CreateIndex("dbo.Pictures", "Rental_Id");
            AddForeignKey("dbo.Pictures", "Rental_Id", "dbo.Rentals", "Id");
        }
    }
}
