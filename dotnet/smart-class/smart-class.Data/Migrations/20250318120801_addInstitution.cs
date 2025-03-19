using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace smart_class.Data.Migrations
{
    /// <inheritdoc />
    public partial class addInstitution : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Teacher_InstitutionId",
                table: "Teacher",
                column: "InstitutionId");

            migrationBuilder.CreateIndex(
                name: "IX_Student_InstitutionId",
                table: "Student",
                column: "InstitutionId");

            migrationBuilder.CreateIndex(
                name: "IX_Admin_InstitutionId",
                table: "Admin",
                column: "InstitutionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Admin_Institution_InstitutionId",
                table: "Admin",
                column: "InstitutionId",
                principalTable: "Institution",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Student_Institution_InstitutionId",
                table: "Student",
                column: "InstitutionId",
                principalTable: "Institution",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Teacher_Institution_InstitutionId",
                table: "Teacher",
                column: "InstitutionId",
                principalTable: "Institution",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Admin_Institution_InstitutionId",
                table: "Admin");

            migrationBuilder.DropForeignKey(
                name: "FK_Student_Institution_InstitutionId",
                table: "Student");

            migrationBuilder.DropForeignKey(
                name: "FK_Teacher_Institution_InstitutionId",
                table: "Teacher");

            migrationBuilder.DropIndex(
                name: "IX_Teacher_InstitutionId",
                table: "Teacher");

            migrationBuilder.DropIndex(
                name: "IX_Student_InstitutionId",
                table: "Student");

            migrationBuilder.DropIndex(
                name: "IX_Admin_InstitutionId",
                table: "Admin");
        }
    }
}
