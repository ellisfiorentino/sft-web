namespace sft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class index : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Rentals", "UrlName", unique: true, name: "UrLName");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Rentals", "UrLName");
        }
    }
}
