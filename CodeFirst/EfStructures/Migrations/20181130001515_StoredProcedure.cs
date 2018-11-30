using Microsoft.EntityFrameworkCore.Migrations;

namespace CodeFirst.EfStructures.Migrations
{
    public partial class StoredProcedure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            const string sp = @"CREATE PROCEDURE [dbo].[GetAllBlogs]
AS 
BEGIN
    SET NOCOUNT ON;
    select * from Blogs
END";
            migrationBuilder.Sql(sp);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP PROCEDURE [dbo].[GetAllBlogs]");
        }
    }
}
