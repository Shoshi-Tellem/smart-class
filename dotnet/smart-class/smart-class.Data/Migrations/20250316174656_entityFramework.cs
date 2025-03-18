using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace smart_class.Data.Migrations
{
    /// <inheritdoc />
    public partial class entityFramework : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Teacher_InstitutionId",
                table: "Teacher",
                column: "InstitutionId");

            migrationBuilder.CreateIndex(
                name: "IX_Student_GroupId",
                table: "Student",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Student_InstitutionId",
                table: "Student",
                column: "InstitutionId");

            migrationBuilder.CreateIndex(
                name: "IX_Lesson_CourseId",
                table: "Lesson",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Group_InstitutionId",
                table: "Group",
                column: "InstitutionId");

            migrationBuilder.CreateIndex(
                name: "IX_File_LessonId",
                table: "File",
                column: "LessonId");

            migrationBuilder.CreateIndex(
                name: "IX_Course_TeacherId",
                table: "Course",
                column: "TeacherId");

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
                name: "FK_Course_Teacher_TeacherId",
                table: "Course",
                column: "TeacherId",
                principalTable: "Teacher",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_File_Lesson_LessonId",
                table: "File",
                column: "LessonId",
                principalTable: "Lesson",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Group_Institution_InstitutionId",
                table: "Group",
                column: "InstitutionId",
                principalTable: "Institution",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Lesson_Course_CourseId",
                table: "Lesson",
                column: "CourseId",
                principalTable: "Course",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Student_Group_GroupId",
                table: "Student",
                column: "GroupId",
                principalTable: "Group",
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
                name: "FK_Course_Teacher_TeacherId",
                table: "Course");

            migrationBuilder.DropForeignKey(
                name: "FK_File_Lesson_LessonId",
                table: "File");

            migrationBuilder.DropForeignKey(
                name: "FK_Group_Institution_InstitutionId",
                table: "Group");

            migrationBuilder.DropForeignKey(
                name: "FK_Lesson_Course_CourseId",
                table: "Lesson");

            migrationBuilder.DropForeignKey(
                name: "FK_Student_Group_GroupId",
                table: "Student");

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
                name: "IX_Student_GroupId",
                table: "Student");

            migrationBuilder.DropIndex(
                name: "IX_Student_InstitutionId",
                table: "Student");

            migrationBuilder.DropIndex(
                name: "IX_Lesson_CourseId",
                table: "Lesson");

            migrationBuilder.DropIndex(
                name: "IX_Group_InstitutionId",
                table: "Group");

            migrationBuilder.DropIndex(
                name: "IX_File_LessonId",
                table: "File");

            migrationBuilder.DropIndex(
                name: "IX_Course_TeacherId",
                table: "Course");

            migrationBuilder.DropIndex(
                name: "IX_Admin_InstitutionId",
                table: "Admin");
        }
    }
}
