using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Clothes.Migrations
{
    public partial class bankData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Status_StatusId",
                table: "Customers");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Status",
                newName: "StatusId");

            migrationBuilder.AlterColumn<int>(
                name: "StatusId",
                table: "Customers",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Status_StatusId",
                table: "Customers",
                column: "StatusId",
                principalTable: "Status",
                principalColumn: "StatusId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Status_StatusId",
                table: "Customers");

            migrationBuilder.RenameColumn(
                name: "StatusId",
                table: "Status",
                newName: "Id");

            migrationBuilder.AlterColumn<int>(
                name: "StatusId",
                table: "Customers",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Status_StatusId",
                table: "Customers",
                column: "StatusId",
                principalTable: "Status",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
