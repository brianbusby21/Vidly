namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPropertiesToMovie : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.s", "ReleaseDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.s", "DateAdded", c => c.DateTime(nullable: false));
            AddColumn("dbo.s", "NumberInStock", c => c.Int(nullable: false));
            AddColumn("dbo.s", "MovieGenre_Id", c => c.Byte());
            AlterColumn("dbo.s", "Name", c => c.String(nullable: false, maxLength: 255));
            CreateIndex("dbo.s", "MovieGenre_Id");
            AddForeignKey("dbo.s", "MovieGenre_Id", "dbo.Genres", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.s", "MovieGenre_Id", "dbo.Genres");
            DropIndex("dbo.s", new[] { "MovieGenre_Id" });
            AlterColumn("dbo.s", "Name", c => c.String());
            DropColumn("dbo.s", "MovieGenre_Id");
            DropColumn("dbo.s", "NumberInStock");
            DropColumn("dbo.s", "DateAdded");
            DropColumn("dbo.s", "ReleaseDate");
        }
    }
}
