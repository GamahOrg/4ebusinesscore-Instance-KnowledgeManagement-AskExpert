using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AskExperts.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class add_userId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Experts",
                type: "uniqueidentifier",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Experts");
        }
    }
}
