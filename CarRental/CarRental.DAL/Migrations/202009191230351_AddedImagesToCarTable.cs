namespace CarRental.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedImagesToCarTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cars", "Image2", c => c.String());
            AddColumn("dbo.Cars", "Image3", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Cars", "Image3");
            DropColumn("dbo.Cars", "Image2");
        }
    }
}
