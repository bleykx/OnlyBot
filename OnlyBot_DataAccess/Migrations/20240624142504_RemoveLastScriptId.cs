using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlyBot_DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class RemoveLastScriptId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bots_Scripts_ScriptId",
                table: "Bots");

            migrationBuilder.AddColumn<Guid>(
                name: "LastScriptLoadedId",
                table: "Bots",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bots_LastScriptLoadedId",
                table: "Bots",
                column: "LastScriptLoadedId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bots_Scripts_LastScriptLoadedId",
                table: "Bots",
                column: "LastScriptLoadedId",
                principalTable: "Scripts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Bots_Scripts_ScriptId",
                table: "Bots",
                column: "ScriptId",
                principalTable: "Scripts",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bots_Scripts_LastScriptLoadedId",
                table: "Bots");

            migrationBuilder.DropForeignKey(
                name: "FK_Bots_Scripts_ScriptId",
                table: "Bots");

            migrationBuilder.DropIndex(
                name: "IX_Bots_LastScriptLoadedId",
                table: "Bots");

            migrationBuilder.DropColumn(
                name: "LastScriptLoadedId",
                table: "Bots");

            migrationBuilder.AddForeignKey(
                name: "FK_Bots_Scripts_ScriptId",
                table: "Bots",
                column: "ScriptId",
                principalTable: "Scripts",
                principalColumn: "Id");
        }
    }
}
