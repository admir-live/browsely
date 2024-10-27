using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Browsely.Modules.Dispatcher.Infrastructure.Database.Migrations;

/// <inheritdoc />
public partial class AddedConstraints : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<string>(
            name: "Uri",
            table: "Url",
            type: "nvarchar(500)",
            maxLength: 500,
            nullable: false,
            oldClrType: typeof(string),
            oldType: "nvarchar(max)");

        migrationBuilder.AlterColumn<string>(
            name: "HtmlContent",
            table: "Url",
            type: "nvarchar(max)",
            nullable: true,
            oldClrType: typeof(string),
            oldType: "nvarchar(max)");

        migrationBuilder.AlterColumn<string>(
            name: "CurrentState",
            table: "Url",
            type: "nvarchar(20)",
            maxLength: 20,
            nullable: false,
            oldClrType: typeof(string),
            oldType: "nvarchar(450)");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<string>(
            name: "Uri",
            table: "Url",
            type: "nvarchar(max)",
            nullable: false,
            oldClrType: typeof(string),
            oldType: "nvarchar(500)",
            oldMaxLength: 500);

        migrationBuilder.AlterColumn<string>(
            name: "HtmlContent",
            table: "Url",
            type: "nvarchar(max)",
            nullable: false,
            defaultValue: "",
            oldClrType: typeof(string),
            oldType: "nvarchar(max)",
            oldNullable: true);

        migrationBuilder.AlterColumn<string>(
            name: "CurrentState",
            table: "Url",
            type: "nvarchar(450)",
            nullable: false,
            oldClrType: typeof(string),
            oldType: "nvarchar(20)",
            oldMaxLength: 20);
    }
}
