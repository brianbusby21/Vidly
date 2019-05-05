namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMovieGenreId : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.s", "MovieGenre_Id", "dbo.Genres");
            DropIndex("dbo.s", new[] { "MovieGenre_Id" });
            AddColumn("dbo.s", "MovieGenre_Id1", c => c.Byte());
            AlterColumn("dbo.s", "MovieGenre_Id", c => c.Byte(nullable: false));
            CreateIndex("dbo.s", "MovieGenre_Id1");
            AddForeignKey("dbo.s", "MovieGenre_Id1", "dbo.Genres", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.s", "MovieGenre_Id1", "dbo.Genres");
            DropIndex("dbo.s", new[] { "MovieGenre_Id1" });
            AlterColumn("dbo.s", "MovieGenre_Id", c => c.Byte());
            DropColumn("dbo.s", "MovieGenre_Id1");
            CreateIndex("dbo.s", "MovieGenre_Id");
            AddForeignKey("dbo.s", "MovieGenre_Id", "dbo.Genres", "Id");
        }
    }
}
