using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlyBot_DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDeleteBehavior : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bots_Proxies_ProxyId",
                table: "Bots");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProxyId",
                table: "Bots",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Bots_Proxies_ProxyId",
                table: "Bots",
                column: "ProxyId",
                principalTable: "Proxies",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bots_Proxies_ProxyId",
                table: "Bots");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProxyId",
                table: "Bots",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Bots_Proxies_ProxyId",
                table: "Bots",
                column: "ProxyId",
                principalTable: "Proxies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
