namespace TrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class getcustomers : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Employees", "AreaZipCode", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Employees", "AreaZipCode", c => c.Int(nullable: false));
        }
    }
}
