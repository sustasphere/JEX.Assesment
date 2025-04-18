using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JEX.Assessment.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CompanySet",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyGuid = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanySet", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VacancySet",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyGuid = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VacancyGuid = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VacancySet", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CompanyAddressSet",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    CompanySetId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyAddressSet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyAddressSet_CompanySet_CompanySetId",
                        column: x => x.CompanySetId,
                        principalTable: "CompanySet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CompanyNameSet",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    CompanySetId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyNameSet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyNameSet_CompanySet_CompanySetId",
                        column: x => x.CompanySetId,
                        principalTable: "CompanySet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompanyAddressSet_CompanySetId",
                table: "CompanyAddressSet",
                column: "CompanySetId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyNameSet_CompanySetId",
                table: "CompanyNameSet",
                column: "CompanySetId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompanyAddressSet");

            migrationBuilder.DropTable(
                name: "CompanyNameSet");

            migrationBuilder.DropTable(
                name: "VacancySet");

            migrationBuilder.DropTable(
                name: "CompanySet");
        }
    }
}
