using Microsoft.EntityFrameworkCore.Migrations;

namespace CodeFirst.EfStructures.Migrations
{
    public partial class Indexes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Posts_Title",
                table: "Posts",
                column: "Title");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_Make_Model",
                table: "Cars",
                columns: new[] { "Make", "Model" });

            migrationBuilder.CreateIndex(
                name: "IX_Blogs_BlogUrl",
                table: "Blogs",
                column: "BlogUrl",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Posts_Title",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Cars_Make_Model",
                table: "Cars");

            migrationBuilder.DropIndex(
                name: "IX_Blogs_BlogUrl",
                table: "Blogs");
        }
    }
}
