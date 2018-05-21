using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Clothes.Migrations
{
    public partial class bankData2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BankData",
                columns: table => new
                {
                    BankDataId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Agency = table.Column<string>(nullable: true),
                    BankId = table.Column<int>(nullable: false),
                    CheckingAccount = table.Column<string>(nullable: true),
                    DigitAgency = table.Column<string>(nullable: true),
                    DigitCheckingAccount = table.Column<string>(nullable: true),
                    LaundryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankData", x => x.BankDataId);
                    table.ForeignKey(
                        name: "FK_BankData_Banks_BankId",
                        column: x => x.BankId,
                        principalTable: "Banks",
                        principalColumn: "BankId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BankData_Laundries_LaundryId",
                        column: x => x.LaundryId,
                        principalTable: "Laundries",
                        principalColumn: "LaundryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BankData_BankId",
                table: "BankData",
                column: "BankId");

            migrationBuilder.CreateIndex(
                name: "IX_BankData_LaundryId",
                table: "BankData",
                column: "LaundryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BankData");
        }
    }
}
