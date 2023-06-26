namespace LB5_1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Admins",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LastName = c.String(),
                        FirstName = c.String(),
                        MiddleName = c.String(),
                        Phone = c.String(),
                        Age = c.Int(nullable: false),
                        Qualification = c.String(),
                        Password = c.String(),
                        Photo = c.Binary(),
                        DiningRoom_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DiningRooms", t => t.DiningRoom_Id)
                .Index(t => t.DiningRoom_Id);
            
            CreateTable(
                "dbo.СomboSet",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        СaloricСontent = c.Int(nullable: false),
                        Genre = c.String(),
                        Cover = c.Binary(),
                        Admin_Id = c.Int(),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Admins", t => t.Admin_Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.Admin_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Items",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Date = c.DateTime(),
                        Duration = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Number = c.Int(nullable: false),
                        СomboSet_Id = c.Int(),
                        DiningRoom_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.СomboSet", t => t.СomboSet_Id)
                .ForeignKey("dbo.DiningRooms", t => t.DiningRoom_Id)
                .Index(t => t.СomboSet_Id)
                .Index(t => t.DiningRoom_Id);
            
            CreateTable(
                "dbo.DiningRooms",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Address = c.String(),
                        Time = c.String(),
                        Phone = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        MiddleName = c.String(),
                        Phone = c.String(),
                        Email = c.String(),
                        Password = c.String(),
                        Photo = c.Binary(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.СomboSet", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Items", "DiningRoom_Id", "dbo.DiningRooms");
            DropForeignKey("dbo.Admins", "DiningRoom_Id", "dbo.DiningRooms");
            DropForeignKey("dbo.Items", "СomboSet_Id", "dbo.СomboSet");
            DropForeignKey("dbo.СomboSet", "Admin_Id", "dbo.Admins");
            DropIndex("dbo.Items", new[] { "DiningRoom_Id" });
            DropIndex("dbo.Items", new[] { "СomboSet_Id" });
            DropIndex("dbo.СomboSet", new[] { "User_Id" });
            DropIndex("dbo.СomboSet", new[] { "Admin_Id" });
            DropIndex("dbo.Admins", new[] { "DiningRoom_Id" });
            DropTable("dbo.Users");
            DropTable("dbo.DiningRooms");
            DropTable("dbo.Items");
            DropTable("dbo.СomboSet");
            DropTable("dbo.Admins");
        }
    }
}
