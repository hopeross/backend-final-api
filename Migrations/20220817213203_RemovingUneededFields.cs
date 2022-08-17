using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace social_api.Migrations
{
    public partial class RemovingUneededFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "Post");

            migrationBuilder.DropColumn(
                name: "UpdatedOn",
                table: "Post");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DeletedOn",
                table: "Post",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedOn",
                table: "Post",
                type: "TEXT",
                nullable: true);
        }
    }
}
