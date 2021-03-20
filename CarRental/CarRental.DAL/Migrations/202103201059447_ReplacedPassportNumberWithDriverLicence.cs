namespace CarRental.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReplacedPassportNumberWithDriverLicence : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "DriverLicenceNumber", c => c.String(nullable: false));
            AddColumn("dbo.AspNetUsers", "DriverLicenceNumber", c => c.String());
            DropColumn("dbo.Orders", "PassportNumb");
            DropColumn("dbo.AspNetUsers", "PassportNumb");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "PassportNumb", c => c.Int(nullable: false));
            AddColumn("dbo.Orders", "PassportNumb", c => c.Int(nullable: false));
            DropColumn("dbo.AspNetUsers", "DriverLicenceNumber");
            DropColumn("dbo.Orders", "DriverLicenceNumber");
        }
    }
}
