using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResturantProject.Migrations
{
    public partial class @in : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Fav",
                table: "ReslinkPlayer",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "tblMapping",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RestaurantId = table.Column<int>(type: "int", nullable: false),
                    PlayerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblMapping", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblMapping_Playertbl_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Playertbl",
                        principalColumn: "PlayerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblMapping_Restauranttbl_RestaurantId",
                        column: x => x.RestaurantId,
                        principalTable: "Restauranttbl",
                        principalColumn: "RestaurantId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblMapping_PlayerId",
                table: "tblMapping",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_tblMapping_RestaurantId",
                table: "tblMapping",
                column: "RestaurantId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblMapping");

            migrationBuilder.DropColumn(
                name: "Fav",
                table: "ReslinkPlayer");
        }
    }
}
