namespace CustomerManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LS20CustomerDataStorageContextv1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LS2Bestellungen",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BranchOfficeCode = c.String(),
                        OrderStatus = c.Int(nullable: false),
                        OrderNumber = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.LS2Bestellungen");
        }
    }
}
