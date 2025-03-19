using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace smart_class.Data.Migrations
{
    /// <inheritdoc />
    public partial class addInstitutionInUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InstitutionId",
                table: "Teacher",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "InstitutionId",
                table: "Student",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "InstitutionId",
                table: "Admin",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InstitutionId",
                table: "Teacher");

            migrationBuilder.DropColumn(
                name: "InstitutionId",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "InstitutionId",
                table: "Admin");
        }
    }
}
