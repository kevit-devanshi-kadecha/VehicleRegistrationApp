﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VehicleRegistration.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ColumnForImagePath : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProfileImagepath",
                table: "Users",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfileImagepath",
                table: "Users");
        }
    }
}
