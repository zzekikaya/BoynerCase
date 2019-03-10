using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Boyner.Core.Migrations
{
    public partial class init1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Config",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    ApplicationName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Config", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Config",
                columns: new[] { "Id", "ApplicationName", "IsActive", "Name", "Type", "Value" },
                values: new object[] { 1, "SERVICE-A", true, "SiteName", "String", "Boyner.com.tr" });

            migrationBuilder.InsertData(
                table: "Config",
                columns: new[] { "Id", "ApplicationName", "IsActive", "Name", "Type", "Value" },
                values: new object[] { 2, "SERVICE-B", true, "IsBasketEnabled", "Boolean", "1" });

            migrationBuilder.InsertData(
                table: "Config",
                columns: new[] { "Id", "ApplicationName", "IsActive", "Name", "Type", "Value" },
                values: new object[] { 3, "SERVICE-A", false, "MaxItemCount", "Int", "50" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Config");
        }
    }
}
