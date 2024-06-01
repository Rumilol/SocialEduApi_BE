using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocialEduApi.Migrations
{
    /// <inheritdoc />
    public partial class Comments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ForumPostLikes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ForumPostID = table.Column<int>(type: "INTEGER", nullable: false),
                    UserId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ForumPostLikes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ForumPostLikes_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ForumPostLikes_ForumPosts_ForumPostID",
                        column: x => x.ForumPostID,
                        principalTable: "ForumPosts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectSubmissionComments",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Text = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UserID = table.Column<string>(type: "TEXT", nullable: false),
                    ProjectSubmissionID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectSubmissionComments", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ProjectSubmissionComments_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectSubmissionComments_ProjectSubmissions_ProjectSubmissionID",
                        column: x => x.ProjectSubmissionID,
                        principalTable: "ProjectSubmissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectSubmissionLikes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProjectSubmissionID = table.Column<int>(type: "INTEGER", nullable: false),
                    UserId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectSubmissionLikes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectSubmissionLikes_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectSubmissionLikes_ProjectSubmissions_ProjectSubmissionID",
                        column: x => x.ProjectSubmissionID,
                        principalTable: "ProjectSubmissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ForumPostLikes_ForumPostID",
                table: "ForumPostLikes",
                column: "ForumPostID");

            migrationBuilder.CreateIndex(
                name: "IX_ForumPostLikes_UserId",
                table: "ForumPostLikes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectSubmissionComments_ProjectSubmissionID",
                table: "ProjectSubmissionComments",
                column: "ProjectSubmissionID");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectSubmissionComments_UserID",
                table: "ProjectSubmissionComments",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectSubmissionLikes_ProjectSubmissionID",
                table: "ProjectSubmissionLikes",
                column: "ProjectSubmissionID");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectSubmissionLikes_UserId",
                table: "ProjectSubmissionLikes",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ForumPostLikes");

            migrationBuilder.DropTable(
                name: "ProjectSubmissionComments");

            migrationBuilder.DropTable(
                name: "ProjectSubmissionLikes");
        }
    }
}
