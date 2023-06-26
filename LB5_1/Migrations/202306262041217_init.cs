namespace LB5_1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Items", "Number");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Items", "Number", c => c.Int(nullable: false));
        }
    }
}
