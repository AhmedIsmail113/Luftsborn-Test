﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Luftsborn.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fixrelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notes_Tags_TagId",
                table: "Notes");

            migrationBuilder.AlterColumn<Guid>(
                name: "TagId",
                table: "Notes",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_Tags_TagId",
                table: "Notes",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notes_Tags_TagId",
                table: "Notes");

            migrationBuilder.AlterColumn<Guid>(
                name: "TagId",
                table: "Notes",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_Tags_TagId",
                table: "Notes",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "Id");
        }
    }
}
