namespace Dvd_Library_API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Dvds",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        title = c.String(nullable: false, storeType: "varchar", maxLength: 50),
                        releaseYear = c.Int(nullable: false),
                        director = c.String(nullable: false, storeType: "varchar", maxLength: 50),
                        rating = c.String(storeType: "varchar", maxLength: 5),
                        notes = c.String(storeType: "varchar", maxLength: 300),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Dvds");
        }
    }
}
