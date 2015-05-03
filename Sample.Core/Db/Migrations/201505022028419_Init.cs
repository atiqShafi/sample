namespace Sample.Core.Db.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Password = c.String(),
                        Created = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            // nlog
            CreateTable("SystemLog", c => new
            {
                Id = c.Long(identity: true),
                Date = c.DateTime(),
                User = c.String(),
                Level = c.String(),
                Exception = c.String(),
                Message = c.String(),
            });

            
        }
        
        public override void Down()
        {
            DropTable("dbo.Users");
        }
    }
}
