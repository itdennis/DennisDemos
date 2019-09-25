using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Core3._0WebApp.Data.Migrations
{
    public partial class AddPostsModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Slug = table.Column<string>(nullable: true),
                    PostContent = table.Column<string>(nullable: true),
                    CommentEnabled = table.Column<bool>(nullable: false),
                    CreateOnUtc = table.Column<DateTime>(nullable: false),
                    ContentAbstract = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Posts");
        }
    }
}
