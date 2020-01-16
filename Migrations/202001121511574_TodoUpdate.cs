namespace ToDoApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TodoUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ToDoes", "CreatedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.ToDoes", "DueDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.ToDoes", "Priority", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ToDoes", "Priority");
            DropColumn("dbo.ToDoes", "DueDate");
            DropColumn("dbo.ToDoes", "CreatedDate");
        }
    }
}
