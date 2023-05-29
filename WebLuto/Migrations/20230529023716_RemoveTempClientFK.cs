using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebLuto.Migrations
{
    /// <inheritdoc />
    public partial class RemoveTempClientFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Client_ClientToken_Id",
                table: "Client");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "ClientToken");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ClientId",
                table: "ClientToken",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddForeignKey(
                name: "FK_Client_ClientToken_Id",
                table: "Client",
                column: "Id",
                principalTable: "ClientToken",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
