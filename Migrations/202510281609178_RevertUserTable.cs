namespace JewelryGolden.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RevertUserTable : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Users", newName: "AspNetUsers");
            AlterColumn("dbo.NewsCategories", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.NewsCategories", "Alias", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.NewsCategories", "Alias", c => c.String(maxLength: 256));
            AlterColumn("dbo.NewsCategories", "Name", c => c.String(nullable: false, maxLength: 256));
            RenameTable(name: "dbo.AspNetUsers", newName: "Users");
        }
    }
}
