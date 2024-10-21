using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace day2.Migrations
{
    /// <inheritdoc />
    public partial class addthreetable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.CreateTable(
                name: "labAuthors",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Authorid = table.Column<int>(type: "int", nullable: true),
                    labid = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_labAuthors", x => x.id);
                    table.ForeignKey(
                        name: "FK_labAuthors_Authors_Authorid",
                        column: x => x.Authorid,
                        principalTable: "Authors",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_labAuthors_lab_labid",
                        column: x => x.labid,
                        principalTable: "lab",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_labAuthors_Authorid",
                table: "labAuthors",
                column: "Authorid");

            migrationBuilder.CreateIndex(
                name: "IX_labAuthors_labid",
                table: "labAuthors",
                column: "labid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "labAuthors");

            migrationBuilder.CreateTable(
                name: "Authorlab",
                columns: table => new
                {
                    AuthorsId = table.Column<int>(type: "int", nullable: false),
                    labsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authorlab", x => new { x.AuthorsId, x.labsId });
                    table.ForeignKey(
                        name: "FK_Authorlab_Authors_AuthorsId",
                        column: x => x.AuthorsId,
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Authorlab_lab_labsId",
                        column: x => x.labsId,
                        principalTable: "lab",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Authorlab_labsId",
                table: "Authorlab",
                column: "labsId");
        }
    }
}
