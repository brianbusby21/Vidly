namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveMovieGenre_Id : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.s", "MovieGenre_Id");
        }
        
        public override void Down()
        {
        }
    }
}
