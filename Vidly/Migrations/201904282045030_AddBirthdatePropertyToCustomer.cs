namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBirthdatePropertyToCustomer : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "Birtdate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Customers", "Birtdate");
        }
    }
}
