using Microsoft.EntityFrameworkCore.Migrations;

namespace BeerDotApi.Migrations
{
    public partial class AddCreaterUserModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CreaterId",
                table: "Beer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Beer_CreaterId",
                table: "Beer",
                column: "CreaterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Beer_User_CreaterId",
                table: "Beer",
                column: "CreaterId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Beer_User_CreaterId",
                table: "Beer");

            migrationBuilder.DropIndex(
                name: "IX_Beer_CreaterId",
                table: "Beer");

            migrationBuilder.DropColumn(
                name: "CreaterId",
                table: "Beer");
        }
    }
}
