using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectBlog.Core.Migrations
{
    public partial class third : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_PersonalInfo_PersonalInfoId",
                schema: "Auth",
                table: "User");

            migrationBuilder.DropTable(
                name: "PersonalContact",
                schema: "Info");

            migrationBuilder.AddColumn<int>(
                name: "PersonalInfoId",
                schema: "Info",
                table: "Contact",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "PersonalInfoId",
                schema: "Auth",
                table: "User",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Contact_PersonalInfoId",
                schema: "Info",
                table: "Contact",
                column: "PersonalInfoId");

            migrationBuilder.AddForeignKey(
                name: "FK_User_PersonalInfo_PersonalInfoId",
                schema: "Auth",
                table: "User",
                column: "PersonalInfoId",
                principalSchema: "Info",
                principalTable: "PersonalInfo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Contact_PersonalInfo_PersonalInfoId",
                schema: "Info",
                table: "Contact",
                column: "PersonalInfoId",
                principalSchema: "Info",
                principalTable: "PersonalInfo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_PersonalInfo_PersonalInfoId",
                schema: "Auth",
                table: "User");

            migrationBuilder.DropForeignKey(
                name: "FK_Contact_PersonalInfo_PersonalInfoId",
                schema: "Info",
                table: "Contact");

            migrationBuilder.DropIndex(
                name: "IX_Contact_PersonalInfoId",
                schema: "Info",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "PersonalInfoId",
                schema: "Info",
                table: "Contact");

            migrationBuilder.AlterColumn<int>(
                name: "PersonalInfoId",
                schema: "Auth",
                table: "User",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateTable(
                name: "PersonalContact",
                schema: "Info",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ContactId = table.Column<int>(nullable: false),
                    OperationDate = table.Column<DateTime>(nullable: false),
                    PersonalId = table.Column<int>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: false),
                    Status = table.Column<bool>(nullable: false)
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

            migrationBuilder.AddForeignKey(
                name: "FK_User_PersonalInfo_PersonalInfoId",
                schema: "Auth",
                table: "User",
                column: "PersonalInfoId",
                principalSchema: "Info",
                principalTable: "PersonalInfo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
