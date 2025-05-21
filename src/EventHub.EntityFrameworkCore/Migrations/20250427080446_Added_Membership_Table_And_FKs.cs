using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventHub.Migrations
{
    /// <inheritdoc />
    public partial class Added_Membership_Table_And_FKs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OrganizationMembership",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrganizationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizationMembership", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrganizationMembership_AbpUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OrganizationMembership_Organization_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organization",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Organization_OwnerUserId",
                table: "Organization",
                column: "OwnerUserId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationMembership_OrganizationId_UserId",
                table: "OrganizationMembership",
                columns: new[] { "OrganizationId", "UserId" });

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationMembership_UserId",
                table: "OrganizationMembership",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Organization_AbpUsers_OwnerUserId",
                table: "Organization",
                column: "OwnerUserId",
                principalTable: "AbpUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Organization_AbpUsers_OwnerUserId",
                table: "Organization");

            migrationBuilder.DropTable(
                name: "OrganizationMembership");

            migrationBuilder.DropIndex(
                name: "IX_Organization_OwnerUserId",
                table: "Organization");
        }
    }
}
