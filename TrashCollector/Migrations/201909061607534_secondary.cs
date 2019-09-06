namespace TrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class secondary : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "PickupActivity", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Customers", "OneTimePickup", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Customers", "OneTimePickup", c => c.DateTime(nullable: false));
            DropColumn("dbo.Customers", "PickupActivity");
        }
    }
}
