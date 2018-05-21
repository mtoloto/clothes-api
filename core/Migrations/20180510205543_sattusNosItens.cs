using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Clothes.Migrations
{
    public partial class sattusNosItens : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StatusId",
                table: "Laundries",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StatusId",
                table: "Customers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StatusId",
                table: "CreditCards",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StatusId",
                table: "Administrators",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Laundries_StatusId",
                table: "Laundries",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_StatusId",
                table: "Customers",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_CreditCards_StatusId",
                table: "CreditCards",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Administrators_StatusId",
                table: "Administrators",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Administrators_Status_StatusId",
                table: "Administrators",
                column: "StatusId",
                principalTable: "Status",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CreditCards_Status_StatusId",
                table: "CreditCards",
                column: "StatusId",
                principalTable: "Status",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Status_StatusId",
                table: "Customers",
                column: "StatusId",
                principalTable: "Status",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Laundries_Status_StatusId",
                table: "Laundries",
                column: "StatusId",
                principalTable: "Status",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Administrators_Status_StatusId",
                table: "Administrators");

            migrationBuilder.DropForeignKey(
                name: "FK_CreditCards_Status_StatusId",
                table: "CreditCards");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Status_StatusId",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Laundries_Status_StatusId",
                table: "Laundries");

            migrationBuilder.DropIndex(
                name: "IX_Laundries_StatusId",
                table: "Laundries");

            migrationBuilder.DropIndex(
                name: "IX_Customers_StatusId",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_CreditCards_StatusId",
                table: "CreditCards");

            migrationBuilder.DropIndex(
                name: "IX_Administrators_StatusId",
                table: "Administrators");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "Laundries");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "CreditCards");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "Administrators");
        }
    }
}
