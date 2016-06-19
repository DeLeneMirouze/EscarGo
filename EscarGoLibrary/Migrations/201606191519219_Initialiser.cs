namespace EscarGoLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initialiser : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Concurrents",
                c => new
                    {
                        ConcurrentId = c.Int(nullable: false, identity: true),
                        Nom = c.String(),
                        Victoires = c.Int(nullable: false),
                        Defaites = c.Int(nullable: false),
                        IdEntraineur = c.Int(nullable: false),
                        Entraineur_EntraineurId = c.Int(),
                    })
                .PrimaryKey(t => t.ConcurrentId)
                .ForeignKey("dbo.Entraineurs", t => t.Entraineur_EntraineurId)
                .Index(t => t.Entraineur_EntraineurId);
            
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        CourseId = c.Int(nullable: false, identity: true),
                        Label = c.String(),
                        Date = c.DateTime(nullable: false),
                        Pays = c.String(),
                        Ville = c.String(),
                        Likes = c.Int(nullable: false),
                        NbTickets = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CourseId);
            
            CreateTable(
                "dbo.Entraineurs",
                c => new
                    {
                        EntraineurId = c.Int(nullable: false, identity: true),
                        Nom = c.String(),
                    })
                .PrimaryKey(t => t.EntraineurId);
            
            CreateTable(
                "dbo.Paris",
                c => new
                    {
                        PariId = c.Int(nullable: false, identity: true),
                        DateDernierPari = c.DateTime(nullable: false),
                        NbParis = c.Int(nullable: false),
                        CourseId = c.Int(nullable: false),
                        ConcurrentId = c.Int(nullable: false),
                        SC = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.PariId)
                .ForeignKey("dbo.Concurrents", t => t.ConcurrentId, cascadeDelete: true)
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: true)
                .Index(t => t.CourseId)
                .Index(t => t.ConcurrentId);
            
            CreateTable(
                "dbo.Tickets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CourseId = c.Int(nullable: false),
                        AcheteurId = c.Int(nullable: false),
                        DateAchat = c.DateTime(nullable: false),
                        NbPlaces = c.Int(nullable: false),
                        EstConfirme = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Visiteurs", t => t.AcheteurId, cascadeDelete: true)
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: true)
                .Index(t => t.CourseId)
                .Index(t => t.AcheteurId);
            
            CreateTable(
                "dbo.Visiteurs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nom = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ConcurrentCourses",
                c => new
                    {
                        Concurrent_ConcurrentId = c.Int(nullable: false),
                        Course_CourseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Concurrent_ConcurrentId, t.Course_CourseId })
                .ForeignKey("dbo.Concurrents", t => t.Concurrent_ConcurrentId, cascadeDelete: true)
                .ForeignKey("dbo.Courses", t => t.Course_CourseId, cascadeDelete: true)
                .Index(t => t.Concurrent_ConcurrentId)
                .Index(t => t.Course_CourseId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tickets", "CourseId", "dbo.Courses");
            DropForeignKey("dbo.Tickets", "AcheteurId", "dbo.Visiteurs");
            DropForeignKey("dbo.Paris", "CourseId", "dbo.Courses");
            DropForeignKey("dbo.Paris", "ConcurrentId", "dbo.Concurrents");
            DropForeignKey("dbo.Concurrents", "Entraineur_EntraineurId", "dbo.Entraineurs");
            DropForeignKey("dbo.ConcurrentCourses", "Course_CourseId", "dbo.Courses");
            DropForeignKey("dbo.ConcurrentCourses", "Concurrent_ConcurrentId", "dbo.Concurrents");
            DropIndex("dbo.ConcurrentCourses", new[] { "Course_CourseId" });
            DropIndex("dbo.ConcurrentCourses", new[] { "Concurrent_ConcurrentId" });
            DropIndex("dbo.Tickets", new[] { "AcheteurId" });
            DropIndex("dbo.Tickets", new[] { "CourseId" });
            DropIndex("dbo.Paris", new[] { "ConcurrentId" });
            DropIndex("dbo.Paris", new[] { "CourseId" });
            DropIndex("dbo.Concurrents", new[] { "Entraineur_EntraineurId" });
            DropTable("dbo.ConcurrentCourses");
            DropTable("dbo.Visiteurs");
            DropTable("dbo.Tickets");
            DropTable("dbo.Paris");
            DropTable("dbo.Entraineurs");
            DropTable("dbo.Courses");
            DropTable("dbo.Concurrents");
        }
    }
}
