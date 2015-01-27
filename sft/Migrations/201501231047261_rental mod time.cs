namespace sft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class rentalmodtime : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Rentals", "Created", c => c.DateTime(nullable: false));
            AddColumn("dbo.Rentals", "Modified", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Rentals", "Modified");
            DropColumn("dbo.Rentals", "Created");
        }
    }
}
