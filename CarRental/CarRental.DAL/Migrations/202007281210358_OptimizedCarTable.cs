namespace CarRental.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OptimizedCarTable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Cars", "Name", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Cars", "Manufacturer", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Cars", "Capacity", c => c.Byte(nullable: false));
            AlterColumn("dbo.Cars", "DriveUnit", c => c.String(nullable: false, maxLength: 10, unicode: false));
            AlterColumn("dbo.Cars", "Door", c => c.Byte(nullable: false));
            AlterColumn("dbo.Cars", "FuelType", c => c.String(maxLength: 15));
            AlterColumn("dbo.Cars", "FuelConsump", c => c.Byte(nullable: false));
            AlterColumn("dbo.Cars", "EngSize", c => c.Byte(nullable: false));
            AlterColumn("dbo.Cars", "CarType", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Cars", "CarType", c => c.String());
            AlterColumn("dbo.Cars", "EngSize", c => c.Int(nullable: false));
            AlterColumn("dbo.Cars", "FuelConsump", c => c.Int(nullable: false));
            AlterColumn("dbo.Cars", "FuelType", c => c.String());
            AlterColumn("dbo.Cars", "Door", c => c.Int(nullable: false));
            AlterColumn("dbo.Cars", "DriveUnit", c => c.String(nullable: false));
            AlterColumn("dbo.Cars", "Capacity", c => c.Int(nullable: false));
            AlterColumn("dbo.Cars", "Manufacturer", c => c.String(nullable: false));
            AlterColumn("dbo.Cars", "Name", c => c.String(nullable: false));
        }
    }
}
