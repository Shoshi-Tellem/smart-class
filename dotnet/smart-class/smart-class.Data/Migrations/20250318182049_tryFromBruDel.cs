using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace smart_class.Data.Migrations
{
    /// <inheritdoc />
    public partial class tryFromBruDel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Group_Institution_InstitutionId",
                table: "Group");

            migrationBuilder.AddColumn<int>(
                name: "InstitutionId1",
                table: "Teacher",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "InstitutionId2",
                table: "Teacher",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "InstitutionId1",
                table: "Admin",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "InstitutionId2",
                table: "Admin",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Teacher_InstitutionId1",
                table: "Teacher",
                column: "InstitutionId1");

            migrationBuilder.CreateIndex(
                name: "IX_Admin_InstitutionId1",
                table: "Admin",
                column: "InstitutionId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Admin_Institution_InstitutionId1",
                table: "Admin",
                column: "InstitutionId1",
                principalTable: "Institution",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Group_Institution_InstitutionId",
                table: "Group",
                column: "InstitutionId",
                principalTable: "Institution",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Teacher_Institution_InstitutionId1",
                table: "Teacher",
                column: "InstitutionId1",
                principalTable: "Institution",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Admin_Institution_InstitutionId1",
                table: "Admin");

            migrationBuilder.DropForeignKey(
                name: "FK_Group_Institution_InstitutionId",
                table: "Group");

            migrationBuilder.DropForeignKey(
                name: "FK_Teacher_Institution_InstitutionId1",
                table: "Teacher");

            migrationBuilder.DropIndex(
                name: "IX_Teacher_InstitutionId1",
                table: "Teacher");

            migrationBuilder.DropIndex(
                name: "IX_Admin_InstitutionId1",
                table: "Admin");

            migrationBuilder.DropColumn(
                name: "InstitutionId1",
                table: "Teacher");

            migrationBuilder.DropColumn(
                name: "InstitutionId2",
                table: "Teacher");

            migrationBuilder.DropColumn(
                name: "InstitutionId1",
                table: "Admin");

            migrationBuilder.DropColumn(
                name: "InstitutionId2",
                table: "Admin");

            migrationBuilder.AddForeignKey(
                name: "FK_Group_Institution_InstitutionId",
                table: "Group",
                column: "InstitutionId",
                principalTable: "Institution",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
