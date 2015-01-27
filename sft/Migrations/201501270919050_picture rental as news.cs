namespace sft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class picturerentalasnews : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Rentals", "MainPictureLocation", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Rentals", "MainPictureLocation");
        }
    }
}
