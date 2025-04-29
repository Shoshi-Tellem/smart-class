using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace smart_class.Data.Migrations
{
    /// <inheritdoc />
    public partial class changeToOneToMany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lesson_File_FileId",
                table: "Lesson");

            migrationBuilder.DropIndex(
                name: "IX_Lesson_FileId",
                table: "Lesson");

            migrationBuilder.DropColumn(
                name: "FileId",
                table: "Lesson");

            migrationBuilder.RenameColumn(
                name: "FilePath",
                table: "File",
                newName: "Path");

            migrationBuilder.AddColumn<int>(
                name: "LessonId",
                table: "File",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LessonId",
                table: "File");

            migrationBuilder.RenameColumn(
                name: "Path",
                table: "File",
                newName: "FilePath");

            migrationBuilder.AddColumn<int>(
                name: "FileId",
                table: "Lesson",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Lesson_FileId",
                table: "Lesson",
                column: "FileId");

            migrationBuilder.AddForeignKey(
                name: "FK_Lesson_File_FileId",
                table: "Lesson",
                column: "FileId",
                principalTable: "File",
                principalColumn: "Id");
        }
    }
}
