namespace CarRental.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class OptimizedOrdersTable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Orders", "User_Id", c => c.String(nullable: false));
            AlterColumn("dbo.Orders", "StartPlace", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Orders", "EndPlace", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Orders", "Status", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Orders", "ManagComment", c => c.String(maxLength: 300));
        }

        public override void Down()
        {
            AlterColumn("dbo.Orders", "ManagComment", c => c.String());
            AlterColumn("dbo.Orders", "Status", c => c.String());
            AlterColumn("dbo.Orders", "EndPlace", c => c.String());
            AlterColumn("dbo.Orders", "StartPlace", c => c.String());
            AlterColumn("dbo.Orders", "User_Id", c => c.String());
        }
    }
}
