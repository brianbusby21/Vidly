namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedMovieClass : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.s",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                        MovieGenreId = c.Byte(nullable: false),
                        ReleaseDate = c.DateTime(nullable: false),
                        DateAdded = c.DateTime(nullable: false),
                        NumberInStock = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Genres", t => t.MovieGenreId, cascadeDelete: true)
                .Index(t => t.MovieGenreId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.s", "MovieGenreId", "dbo.Genres");
            DropIndex("dbo.s", new[] { "MovieGenreId" });
            DropTable("dbo.s");
        }
    }
}
