namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMovieGenreId : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Movies", "MovieGenre_Id", "dbo.Genres");
            DropIndex("dbo.Movies", new[] { "MovieGenre_Id" });
            AddColumn("dbo.Movies", "MovieGenre_Id1", c => c.Byte());
            AlterColumn("dbo.Movies", "MovieGenre_Id", c => c.Byte(nullable: false));
            CreateIndex("dbo.Movies", "MovieGenre_Id1");
            AddForeignKey("dbo.Movies", "MovieGenre_Id1", "dbo.Genres", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Movies", "MovieGenre_Id1", "dbo.Genres");
            DropIndex("dbo.Movies", new[] { "MovieGenre_Id1" });
            AlterColumn("dbo.Movies", "MovieGenre_Id", c => c.Byte());
            DropColumn("dbo.Movies", "MovieGenre_Id1");
            CreateIndex("dbo.Movies", "MovieGenre_Id");
            AddForeignKey("dbo.Movies", "MovieGenre_Id", "dbo.Genres", "Id");
        }
    }
}
