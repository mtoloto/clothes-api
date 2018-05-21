using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Clothes.Migrations
{
    public partial class bankData3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BankData_Laundries_LaundryId",
                table: "BankData");

            migrationBuilder.DropIndex(
                name: "IX_BankData_LaundryId",
                table: "BankData");

            migrationBuilder.DropColumn(
                name: "LaundryId",
                table: "BankData");

            migrationBuilder.AddColumn<int>(
                name: "BankDataId",
                table: "Laundries",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Laundries_BankDataId",
                table: "Laundries",
                column: "BankDataId");

            migrationBuilder.AddForeignKey(
                name: "FK_Laundries_BankData_BankDataId",
                table: "Laundries",
                column: "BankDataId",
                principalTable: "BankData",
                principalColumn: "BankDataId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Laundries_BankData_BankDataId",
                table: "Laundries");

            migrationBuilder.DropIndex(
                name: "IX_Laundries_BankDataId",
                table: "Laundries");

            migrationBuilder.DropColumn(
                name: "BankDataId",
                table: "Laundries");

            migrationBuilder.AddColumn<int>(
                name: "LaundryId",
                table: "BankData",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_BankData_LaundryId",
                table: "BankData",
                column: "LaundryId");

            migrationBuilder.AddForeignKey(
                name: "FK_BankData_Laundries_LaundryId",
                table: "BankData",
                column: "LaundryId",
                principalTable: "Laundries",
                principalColumn: "LaundryId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
