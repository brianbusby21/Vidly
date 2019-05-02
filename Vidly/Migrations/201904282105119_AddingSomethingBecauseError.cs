namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingSomethingBecauseError : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "Birthdate", c => c.DateTime());
            DropColumn("dbo.Customers", "Birtdate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Customers", "Birtdate", c => c.DateTime());
            DropColumn("dbo.Customers", "Birthdate");
        }
    }
}
