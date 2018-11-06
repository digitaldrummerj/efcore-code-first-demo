using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CodeFirst.EfStructures.Migrations
{
    public partial class DefaultValues : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "Created",
                table: "Posts",
                nullable: false,
                defaultValueSql: "getutcdate()",
                oldClrType: typeof(DateTimeOffset));

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "Created",
                table: "Blogs",
                nullable: false,
                defaultValueSql: "getutcdate()",
                oldClrType: typeof(DateTimeOffset));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "Created",
                table: "Posts",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldDefaultValueSql: "getutcdate()");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "Created",
                table: "Blogs",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldDefaultValueSql: "getutcdate()");
        }
    }
}
