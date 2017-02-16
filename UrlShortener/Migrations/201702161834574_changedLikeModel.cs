namespace UrlShortener.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedLikeModel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Likes", "TargetId", "dbo.AspNetUsers");
            DropIndex("dbo.Likes", new[] { "TargetId" });
            AlterColumn("dbo.Likes", "TargetId", c => c.Int(nullable: false));
            CreateIndex("dbo.Likes", "TargetId");
            AddForeignKey("dbo.Likes", "TargetId", "dbo.Bookmarks", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Likes", "TargetId", "dbo.Bookmarks");
            DropIndex("dbo.Likes", new[] { "TargetId" });
            AlterColumn("dbo.Likes", "TargetId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Likes", "TargetId");
            AddForeignKey("dbo.Likes", "TargetId", "dbo.AspNetUsers", "Id");
        }
    }
}
