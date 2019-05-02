namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedMovies : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Movies", "MovieGenre_Id1", "dbo.Genres");
            DropIndex("dbo.Movies", new[] { "MovieGenre_Id1" });
            DropTable("dbo.Movies");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Movies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                        MovieGenre_Id = c.Byte(nullable: false),
                        ReleaseDate = c.DateTime(nullable: false),
                        DateAdded = c.DateTime(nullable: false),
                        NumberInStock = c.Int(nullable: false),
                        MovieGenre_Id1 = c.Byte(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.Movies", "MovieGenre_Id1");
            AddForeignKey("dbo.Movies", "MovieGenre_Id1", "dbo.Genres", "Id");
        }
    }
}
