using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectBlog.Core.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Auth");

            migrationBuilder.EnsureSchema(
                name: "Info");

            migrationBuilder.CreateTable(
                name: "Contact",
                schema: "Info",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Status = table.Column<bool>(nullable: false),
                    OperationDate = table.Column<DateTime>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: false),
                    Type = table.Column<int>(nullable: false),
                    Value = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contact", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PersonalInfo",
                schema: "Info",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Status = table.Column<bool>(nullable: false),
                    OperationDate = table.Column<DateTime>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: true),
                    Surname = table.Column<string>(maxLength: 50, nullable: true),
                    Gender = table.Column<int>(nullable: false),
                    Birthdate = table.Column<DateTime>(nullable: false),
                    NationalNumber = table.Column<string>(maxLength: 12, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonalInfo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                schema: "Auth",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Status = table.Column<bool>(nullable: false),
                    OperationDate = table.Column<DateTime>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: false),
                    Username = table.Column<string>(maxLength: 50, nullable: false),
                    PasswordHash = table.Column<byte[]>(nullable: false),
                    PasswordSalt = table.Column<byte[]>(nullable: false),
                    Confirmation = table.Column<bool>(nullable: false),
                    ApplicationType = table.Column<int>(nullable: false),
                    PersonalInfoId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_PersonalInfo_PersonalInfoId",
                        column: x => x.PersonalInfoId,
                        principalSchema: "Info",
                        principalTable: "PersonalInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PersonalContact",
                schema: "Info",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Status = table.Column<bool>(nullable: false),
                    OperationDate = table.Column<DateTime>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: false),
                    PersonalId = table.Column<int>(nullable: false),
                    ContactId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonalContact", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonalContact_Contact_ContactId",
                        column: x => x.ContactId,
                        principalSchema: "Info",
                        principalTable: "Contact",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonalContact_PersonalInfo_PersonalId",
                        column: x => x.PersonalId,
                        principalSchema: "Info",
                        principalTable: "PersonalInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRole",
                schema: "Auth",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Status = table.Column<bool>(nullable: false),
                    OperationDate = table.Column<DateTime>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: false),
                    Role = table.Column<string>(maxLength: 50, nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRole", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserRole_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "Auth",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_User_PersonalInfoId",
                schema: "Auth",
                table: "User",
                column: "PersonalInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_UserId",
                schema: "Auth",
                table: "UserRole",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PersonalContact_ContactId",
                schema: "Info",
                table: "PersonalContact",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalContact_PersonalId",
                schema: "Info",
                table: "PersonalContact",
                column: "PersonalId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserRole",
                schema: "Auth");

            migrationBuilder.DropTable(
                name: "PersonalContact",
                schema: "Info");

            migrationBuilder.DropTable(
                name: "User",
                schema: "Auth");

            migrationBuilder.DropTable(
                name: "Contact",
                schema: "Info");

            migrationBuilder.DropTable(
                name: "PersonalInfo",
                schema: "Info");
        }
    }
}
