using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Browsely.Modules.Dispatcher.Infrastructure.Database.Migrations;

/// <inheritdoc />
public partial class Initial : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Url",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                HtmlContent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                CurrentState = table.Column<string>(type: "nvarchar(450)", nullable: false),
                Uri = table.Column<string>(type: "nvarchar(max)", nullable: false),
                CreatedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                ModifiedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Url", x => x.Id);
            });

        migrationBuilder.CreateIndex(
            name: "IX_Url_CurrentState",
            table: "Url",
            column: "CurrentState");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Url");
    }
}
