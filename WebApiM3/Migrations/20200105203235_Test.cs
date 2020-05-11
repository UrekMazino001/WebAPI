using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApiM3.Migrations
{
    public partial class Test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                   name: "Test",
                   columns: table => new
                   {
                       Id = table.Column<int>(nullable: false)
                           .Annotation("SqlServer:Identity", "1, 1"),
                       Nombre = table.Column<string>(nullable: true)
                   },
                   constraints: table =>
                   {
                       table.PrimaryKey("PK_Tests", x => x.Id);
                   });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Test");
        }
    }
}
