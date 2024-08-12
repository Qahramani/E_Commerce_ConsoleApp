using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ORM_Mini_Project.Migrations
{
    public partial class UserConfigurationsChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_Fullname",
                table: "Users");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Users_Fullname",
                table: "Users",
                column: "Fullname",
                unique: true);
        }
    }
}
