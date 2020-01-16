namespace ToDoApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TodoUpdateDates : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ToDoes", "CreatedDate", c => c.String());
            AlterColumn("dbo.ToDoes", "DueDate", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ToDoes", "DueDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.ToDoes", "CreatedDate", c => c.DateTime(nullable: false));
        }
    }
}
