using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Clothes.Migrations
{
    public partial class CreditCardAndAddress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Locale",
                table: "Laundries");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "Laundries");

            migrationBuilder.RenameColumn(
                name: "Location",
                table: "Customers",
                newName: "Telphone");

            migrationBuilder.RenameColumn(
                name: "Locale",
                table: "Customers",
                newName: "RG");

            migrationBuilder.RenameColumn(
                name: "Gender",
                table: "Customers",
                newName: "Celphone");

            migrationBuilder.AddColumn<int>(
                name: "AddressId",
                table: "Laundries",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "CPF",
                table: "Customers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    AddressId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    City = table.Column<string>(nullable: true),
                    Complement = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true),
                    CustomerId = table.Column<int>(nullable: true),
                    Neighborhood = table.Column<string>(nullable: true),
                    Number = table.Column<string>(nullable: true),
                    State = table.Column<string>(nullable: true),
                    Street = table.Column<string>(nullable: true),
                    ZipCode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.AddressId);
                    table.ForeignKey(
                        name: "FK_Addresses_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CreditCards",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CustomerId = table.Column<int>(nullable: true),
                    Hash = table.Column<string>(nullable: true),
                    NameOnCard = table.Column<string>(nullable: true),
                    Number = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreditCards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CreditCards_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Laundries_AddressId",
                table: "Laundries",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_CustomerId",
                table: "Addresses",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CreditCards_CustomerId",
                table: "CreditCards",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Laundries_Addresses_AddressId",
                table: "Laundries",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "AddressId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Laundries_Addresses_AddressId",
                table: "Laundries");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "CreditCards");

            migrationBuilder.DropIndex(
                name: "IX_Laundries_AddressId",
                table: "Laundries");

            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "Laundries");

            migrationBuilder.DropColumn(
                name: "CPF",
                table: "Customers");

            migrationBuilder.RenameColumn(
                name: "Telphone",
                table: "Customers",
                newName: "Location");

            migrationBuilder.RenameColumn(
                name: "RG",
                table: "Customers",
                newName: "Locale");

            migrationBuilder.RenameColumn(
                name: "Celphone",
                table: "Customers",
                newName: "Gender");

            migrationBuilder.AddColumn<string>(
                name: "Locale",
                table: "Laundries",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Laundries",
                nullable: true);
        }
    }
}
