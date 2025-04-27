using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventHub.Migrations
{
    /// <inheritdoc />
    public partial class Added_MemberCount_and_IsSendPaidEnrollmentReminderEmail_Columns_To_Organization : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TrialPeriod",
                table: "Organization");

            migrationBuilder.AddColumn<bool>(
                name: "IsSendPaidEnrollmentReminderEmail",
                table: "Organization",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "MemberCount",
                table: "Organization",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSendPaidEnrollmentReminderEmail",
                table: "Organization");

            migrationBuilder.DropColumn(
                name: "MemberCount",
                table: "Organization");

            migrationBuilder.AddColumn<DateTime>(
                name: "TrialPeriod",
                table: "Organization",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
