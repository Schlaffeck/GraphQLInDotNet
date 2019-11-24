using Microsoft.EntityFrameworkCore.Migrations;

namespace GraphQlInDotNet.Data.EntityFramework.Migrations
{
    public partial class MusicDb_more_indices : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ExternalId",
                table: "Tracks",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ExternalId",
                table: "Artists",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ExternalId",
                table: "Albums",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UrlLink",
                table: "Albums",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tracks_ExternalId",
                table: "Tracks",
                column: "ExternalId",
                unique: true,
                filter: "[ExternalId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Artists_ExternalId",
                table: "Artists",
                column: "ExternalId",
                unique: true,
                filter: "[ExternalId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Albums_ExternalId",
                table: "Albums",
                column: "ExternalId",
                unique: true,
                filter: "[ExternalId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Tracks_ExternalId",
                table: "Tracks");

            migrationBuilder.DropIndex(
                name: "IX_Artists_ExternalId",
                table: "Artists");

            migrationBuilder.DropIndex(
                name: "IX_Albums_ExternalId",
                table: "Albums");

            migrationBuilder.DropColumn(
                name: "UrlLink",
                table: "Albums");

            migrationBuilder.AlterColumn<string>(
                name: "ExternalId",
                table: "Tracks",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ExternalId",
                table: "Artists",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ExternalId",
                table: "Albums",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
