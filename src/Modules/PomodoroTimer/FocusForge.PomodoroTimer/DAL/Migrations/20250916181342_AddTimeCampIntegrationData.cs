using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FocusForge.PomodoroTimer.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddTimeCampIntegrationData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Settings",
                columns: new[] { "Id", "SettingsKey", "SettingsValue" },
                values: new object[] { 4, "ApiKey", "" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
