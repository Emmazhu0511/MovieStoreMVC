﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MovieStore.Infrastructure.Migrations
{
    public partial class CreateUserTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(maxLength: 128, nullable: true),
                    LastName = table.Column<string>(maxLength: 128, nullable: true),
                    DateOfBirth = table.Column<DateTime>(nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    HashedPassword = table.Column<string>(maxLength: 4096, nullable: true),
                    Salt = table.Column<string>(maxLength: 4096, nullable: true),
                    PhoneNumber = table.Column<string>(maxLength: 4096, nullable: true),
                    TwoFactorEnabled = table.Column<bool>(nullable: true),
                    LockoutEndDate = table.Column<DateTime>(nullable: true),
                    LastLoginDateTime = table.Column<DateTime>(nullable: true),
                    IsLocked = table.Column<bool>(nullable: true),
                    AccessFailedCount = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
