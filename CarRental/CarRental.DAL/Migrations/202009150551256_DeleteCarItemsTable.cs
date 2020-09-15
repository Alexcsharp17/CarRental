namespace CarRental.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteCarItemsTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Orders", "CarItem_Id", "dbo.CarItems");
            DropIndex("dbo.Orders", new[] { "CarItem_Id" });
            AddColumn("dbo.Cars", "LisencePlate", c => c.String());
            DropColumn("dbo.Orders", "CarItem_Id");
            DropTable("dbo.CarItems");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.CarItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CarId = c.Int(nullable: false),
                        LicencePlate = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Orders", "CarItem_Id", c => c.Int());
            DropColumn("dbo.Cars", "LisencePlate");
            CreateIndex("dbo.Orders", "CarItem_Id");
            AddForeignKey("dbo.Orders", "CarItem_Id", "dbo.CarItems", "Id");
        }
    }
}
