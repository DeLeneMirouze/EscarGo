namespace EscarGo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Concurrents",
                c => new
                    {
                        IdConcurrent = c.Int(nullable: false, identity: true),
                        Nom = c.String(),
                        Victoires = c.Int(nullable: false),
                        Defaites = c.Int(nullable: false),
                        Entraineur = c.String(),
                        Pari_IdPari = c.Int(),
                    })
                .PrimaryKey(t => t.IdConcurrent)
                .ForeignKey("dbo.Paris", t => t.Pari_IdPari)
                .Index(t => t.Pari_IdPari);
            
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        IdCourse = c.Int(nullable: false, identity: true),
                        Label = c.String(),
                        Date = c.DateTime(nullable: false),
                        Pays = c.String(),
                        Ville = c.String(),
                    })
                .PrimaryKey(t => t.IdCourse);
            
            CreateTable(
                "dbo.Paris",
                c => new
                    {
                        IdPari = c.Int(nullable: false, identity: true),
                        SC = c.Double(nullable: false),
                        DateDernierPari = c.DateTime(nullable: false),
                        NbParis = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdPari);
            
            CreateTable(
                "dbo.ConcurrentCourses",
                c => new
                    {
                        Concurrent_IdConcurrent = c.Int(nullable: false),
                        Course_IdCourse = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Concurrent_IdConcurrent, t.Course_IdCourse })
                .ForeignKey("dbo.Concurrents", t => t.Concurrent_IdConcurrent, cascadeDelete: true)
                .ForeignKey("dbo.Courses", t => t.Course_IdCourse, cascadeDelete: true)
                .Index(t => t.Concurrent_IdConcurrent)
                .Index(t => t.Course_IdCourse);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Concurrents", "Pari_IdPari", "dbo.Paris");
            DropForeignKey("dbo.ConcurrentCourses", "Course_IdCourse", "dbo.Courses");
            DropForeignKey("dbo.ConcurrentCourses", "Concurrent_IdConcurrent", "dbo.Concurrents");
            DropIndex("dbo.ConcurrentCourses", new[] { "Course_IdCourse" });
            DropIndex("dbo.ConcurrentCourses", new[] { "Concurrent_IdConcurrent" });
            DropIndex("dbo.Concurrents", new[] { "Pari_IdPari" });
            DropTable("dbo.ConcurrentCourses");
            DropTable("dbo.Paris");
            DropTable("dbo.Courses");
            DropTable("dbo.Concurrents");
        }
    }
}
