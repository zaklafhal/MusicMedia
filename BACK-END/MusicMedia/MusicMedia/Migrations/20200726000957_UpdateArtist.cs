using Microsoft.EntityFrameworkCore.Migrations;

namespace MusicMedia.Migrations
{
    public partial class UpdateArtist : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Artist_AspNetUsers_ApplicationUserId",
                table: "Artist");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Artist",
                table: "Artist");

            migrationBuilder.RenameTable(
                name: "Artist",
                newName: "Artists");

            migrationBuilder.RenameIndex(
                name: "IX_Artist_ApplicationUserId",
                table: "Artists",
                newName: "IX_Artists_ApplicationUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Artists",
                table: "Artists",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Artists_AspNetUsers_ApplicationUserId",
                table: "Artists",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Artists_AspNetUsers_ApplicationUserId",
                table: "Artists");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Artists",
                table: "Artists");

            migrationBuilder.RenameTable(
                name: "Artists",
                newName: "Artist");

            migrationBuilder.RenameIndex(
                name: "IX_Artists_ApplicationUserId",
                table: "Artist",
                newName: "IX_Artist_ApplicationUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Artist",
                table: "Artist",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Artist_AspNetUsers_ApplicationUserId",
                table: "Artist",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
