using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Moodle.Server.Migrations
{
    /// <inheritdoc />
    public partial class Update01 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Degrees_DegreeId",
                table: "Courses");

            migrationBuilder.DropTable(
                name: "ApprovedDegree");

            migrationBuilder.DropTable(
                name: "MyCourse");

            migrationBuilder.DropIndex(
                name: "IX_Courses_DegreeId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "DegreeId",
                table: "Courses");

            migrationBuilder.CreateTable(
                name: "CourseDegree",
                columns: table => new
                {
                    CoursesId = table.Column<int>(type: "INTEGER", nullable: false),
                    DegreesId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseDegree", x => new { x.CoursesId, x.DegreesId });
                    table.ForeignKey(
                        name: "FK_CourseDegree_Courses_CoursesId",
                        column: x => x.CoursesId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseDegree_Degrees_DegreesId",
                        column: x => x.DegreesId,
                        principalTable: "Degrees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CourseUser",
                columns: table => new
                {
                    CoursesId = table.Column<int>(type: "INTEGER", nullable: false),
                    UsersId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseUser", x => new { x.CoursesId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_CourseUser_Courses_CoursesId",
                        column: x => x.CoursesId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseUser_Users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourseDegree_DegreesId",
                table: "CourseDegree",
                column: "DegreesId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseUser_UsersId",
                table: "CourseUser",
                column: "UsersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseDegree");

            migrationBuilder.DropTable(
                name: "CourseUser");

            migrationBuilder.AddColumn<int>(
                name: "DegreeId",
                table: "Courses",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ApprovedDegree",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CourseId = table.Column<int>(type: "INTEGER", nullable: false),
                    DegreeId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApprovedDegree", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApprovedDegree_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApprovedDegree_Degrees_DegreeId",
                        column: x => x.DegreeId,
                        principalTable: "Degrees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MyCourse",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CourseId = table.Column<int>(type: "INTEGER", nullable: false),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MyCourse", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MyCourse_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MyCourse_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Courses_DegreeId",
                table: "Courses",
                column: "DegreeId");

            migrationBuilder.CreateIndex(
                name: "IX_ApprovedDegree_CourseId",
                table: "ApprovedDegree",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_ApprovedDegree_DegreeId",
                table: "ApprovedDegree",
                column: "DegreeId");

            migrationBuilder.CreateIndex(
                name: "IX_MyCourse_CourseId",
                table: "MyCourse",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_MyCourse_UserId",
                table: "MyCourse",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Degrees_DegreeId",
                table: "Courses",
                column: "DegreeId",
                principalTable: "Degrees",
                principalColumn: "Id");
        }
    }
}
