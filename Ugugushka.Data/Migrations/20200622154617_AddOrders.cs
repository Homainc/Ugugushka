using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Ugugushka.Data.Migrations
{
    public partial class AddOrders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false, defaultValueSql: "GETUTCDATE()"),
                    FirstName = table.Column<string>(maxLength: 256, nullable: false),
                    LastName = table.Column<string>(maxLength: 256, nullable: false),
                    Email = table.Column<string>(maxLength: 256, nullable: false),
                    PhoneNumber = table.Column<string>(maxLength: 50, nullable: false),
                    DeliveryType = table.Column<int>(nullable: false),
                    Street = table.Column<string>(maxLength: 256, nullable: true),
                    HouseNumber = table.Column<string>(maxLength: 50, nullable: true),
                    ApartmentNumber = table.Column<string>(maxLength: 50, nullable: true),
                    FloorNumber = table.Column<int>(nullable: true),
                    ExitNumber = table.Column<string>(maxLength: 50, nullable: true),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderToys",
                columns: table => new
                {
                    ToyId = table.Column<int>(nullable: false),
                    OrderId = table.Column<string>(nullable: false),
                    Quantity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderToys", x => new { x.ToyId, x.OrderId });
                    table.ForeignKey(
                        name: "FK_OrderToys_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderToys_Toys_ToyId",
                        column: x => x.ToyId,
                        principalTable: "Toys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderToys_OrderId",
                table: "OrderToys",
                column: "OrderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderToys");

            migrationBuilder.DropTable(
                name: "Orders");
        }
    }
}
