using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduHome.App.Migrations
{
    public partial class updatedblog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_CourseAssets_CourseAssetsId",
                table: "Courses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseAssets",
                table: "CourseAssets");

            migrationBuilder.RenameTable(
                name: "CourseAssets",
                newName: "CourseAssetss");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseAssetss",
                table: "CourseAssetss",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_CourseAssetss_CourseAssetsId",
                table: "Courses",
                column: "CourseAssetsId",
                principalTable: "CourseAssetss",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_CourseAssetss_CourseAssetsId",
                table: "Courses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseAssetss",
                table: "CourseAssetss");

            migrationBuilder.RenameTable(
                name: "CourseAssetss",
                newName: "CourseAssets");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseAssets",
                table: "CourseAssets",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_CourseAssets_CourseAssetsId",
                table: "Courses",
                column: "CourseAssetsId",
                principalTable: "CourseAssets",
                principalColumn: "Id");
        }
    }
}
