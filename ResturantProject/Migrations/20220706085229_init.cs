using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResturantProject.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Playertbl",
                columns: table => new
                {
                    PlayerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    dob = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    primaryaddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    alternateaddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    officeaddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    mobilenumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    driverslicense = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    passport = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    pstreetnumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    paddressline1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    paddressline2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    pCity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    pState = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    pPostal = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    pcountry = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Playertbl", x => x.PlayerId);
                });

            migrationBuilder.CreateTable(
                name: "ReslinkPlayer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RestaurantId = table.Column<int>(type: "int", nullable: false),
                    PlayerId = table.Column<int>(type: "int", nullable: false),
                    Fav = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReslinkPlayer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Restauranttbl",
                columns: table => new
                {
                    RestaurantId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    hoursofoperation = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Restauranttbl", x => x.RestaurantId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Playertbl");

            migrationBuilder.DropTable(
                name: "ReslinkPlayer");

            migrationBuilder.DropTable(
                name: "Restauranttbl");
        }
    }
}
