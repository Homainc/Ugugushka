using Microsoft.EntityFrameworkCore.Migrations;

namespace Ugugushka.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Partitions",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Partitions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: true),
                    PartitionId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categories_Partitions_PartitionId",
                        column: x => x.PartitionId,
                        principalTable: "Partitions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Toys",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    Name = table.Column<string>(maxLength: 40, nullable: true),
                    Description = table.Column<string>(maxLength: 500, nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsOnStock = table.Column<bool>(nullable: false),
                    CategoryId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Toys", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Toys_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_PartitionId",
                table: "Categories",
                column: "PartitionId");

            migrationBuilder.CreateIndex(
                name: "IX_Toys_CategoryId",
                table: "Toys",
                column: "CategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Toys");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Partitions");
        }
    }
}
