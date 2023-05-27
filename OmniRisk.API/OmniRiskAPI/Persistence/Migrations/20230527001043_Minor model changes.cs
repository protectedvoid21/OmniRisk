using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OmniRiskAPI.Migrations
{
    /// <inheritdoc />
    public partial class Minormodelchanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Alerts");

            migrationBuilder.DropTable(
                name: "AlertTypes");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "EventType",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<Guid>(
                name: "AuthorId",
                table: "Events",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Events",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsAccepted",
                table: "Events",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Events_AuthorId",
                table: "Events",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_AspNetUsers_AuthorId",
                table: "Events",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_AspNetUsers_AuthorId",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Events_AuthorId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "IsAccepted",
                table: "Events");

            migrationBuilder.AlterColumn<int>(
                name: "Name",
                table: "EventType",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "AlertTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlertTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Alerts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AlertTypeId = table.Column<int>(type: "int", nullable: false),
                    AuthorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsAccepted = table.Column<bool>(type: "bit", nullable: false),
                    Latitude = table.Column<float>(type: "real", nullable: false),
                    Longitude = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alerts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Alerts_AlertTypes_AlertTypeId",
                        column: x => x.AlertTypeId,
                        principalTable: "AlertTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Alerts_AspNetUsers_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Alerts_AlertTypeId",
                table: "Alerts",
                column: "AlertTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Alerts_AuthorId",
                table: "Alerts",
                column: "AuthorId");
        }
    }
}
