using Microsoft.EntityFrameworkCore.Migrations;

namespace Read.Migrations
{
    public partial class ExtendingArticleReadTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReadId",
                table: "Articles",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReadId",
                table: "Articles");
        }
    }
}
