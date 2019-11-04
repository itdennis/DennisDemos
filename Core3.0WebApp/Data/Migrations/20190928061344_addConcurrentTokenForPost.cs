using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Core3._0WebApp.Data.Migrations
{
    public partial class addConcurrentTokenForPost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Timestamp",
                table: "Posts",
                rowVersion: true,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Timestamp",
                table: "Posts");
        }
    }
}
