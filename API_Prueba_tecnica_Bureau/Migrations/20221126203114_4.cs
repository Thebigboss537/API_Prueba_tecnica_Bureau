using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIPruebatecnicaBureau.Migrations
{
    /// <inheritdoc />
    public partial class _4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    Idcategoria = table.Column<int>(name: "Id_categoria", type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.Idcategoria);
                });

            migrationBuilder.CreateTable(
                name: "Directores",
                columns: table => new
                {
                    Iddirector = table.Column<int>(name: "Id_director", type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Directores", x => x.Iddirector);
                });

            migrationBuilder.CreateTable(
                name: "Idiomas",
                columns: table => new
                {
                    Ididioma = table.Column<int>(name: "Id_idioma", type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Idiomas", x => x.Ididioma);
                });

            migrationBuilder.CreateTable(
                name: "Peliculas",
                columns: table => new
                {
                    Idpelicula = table.Column<int>(name: "Id_pelicula", type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Idcategoria = table.Column<int>(name: "Id_categoria", type: "int", nullable: false),
                    Iddirector = table.Column<int>(name: "Id_director", type: "int", nullable: false),
                    Ididioma = table.Column<int>(name: "Id_idioma", type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Peliculas", x => x.Idpelicula);
                    table.ForeignKey(
                        name: "FK_Peliculas_Categorias_Id_categoria",
                        column: x => x.Idcategoria,
                        principalTable: "Categorias",
                        principalColumn: "Id_categoria",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Peliculas_Directores_Id_director",
                        column: x => x.Iddirector,
                        principalTable: "Directores",
                        principalColumn: "Id_director",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Peliculas_Idiomas_Id_idioma",
                        column: x => x.Ididioma,
                        principalTable: "Idiomas",
                        principalColumn: "Id_idioma",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Peliculas_Id_categoria",
                table: "Peliculas",
                column: "Id_categoria");

            migrationBuilder.CreateIndex(
                name: "IX_Peliculas_Id_director",
                table: "Peliculas",
                column: "Id_director");

            migrationBuilder.CreateIndex(
                name: "IX_Peliculas_Id_idioma",
                table: "Peliculas",
                column: "Id_idioma");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Peliculas");

            migrationBuilder.DropTable(
                name: "Categorias");

            migrationBuilder.DropTable(
                name: "Directores");

            migrationBuilder.DropTable(
                name: "Idiomas");
        }
    }
}
