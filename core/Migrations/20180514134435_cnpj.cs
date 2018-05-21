using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Clothes.Migrations
{
    public partial class cnpj : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Laundries_Status_StatusId",
                table: "Laundries");

            migrationBuilder.AlterColumn<int>(
                name: "StatusId",
                table: "Laundries",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CNPJ",
                table: "Laundries",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IE",
                table: "Laundries",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NomeFantasia",
                table: "Laundries",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RazaoSocial",
                table: "Laundries",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Laundries_Status_StatusId",
                table: "Laundries",
                column: "StatusId",
                principalTable: "Status",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Laundries_Status_StatusId",
                table: "Laundries");

            migrationBuilder.DropColumn(
                name: "CNPJ",
                table: "Laundries");

            migrationBuilder.DropColumn(
                name: "IE",
                table: "Laundries");

            migrationBuilder.DropColumn(
                name: "NomeFantasia",
                table: "Laundries");

            migrationBuilder.DropColumn(
                name: "RazaoSocial",
                table: "Laundries");

            migrationBuilder.AlterColumn<int>(
                name: "StatusId",
                table: "Laundries",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Laundries_Status_StatusId",
                table: "Laundries",
                column: "StatusId",
                principalTable: "Status",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
