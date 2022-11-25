using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIPruebatecnicaBureau.Migrations
{
    /// <inheritdoc />
    public partial class _1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Idrol = table.Column<int>(name: "Id_rol", type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Idrol);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios_autenticacion",
                columns: table => new
                {
                    Idusuarioautenticacion = table.Column<int>(name: "Id_usuario_autenticacion", type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<int>(type: "int", nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    Idrol = table.Column<int>(name: "Id_rol", type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios_autenticacion", x => x.Idusuarioautenticacion);
                    table.ForeignKey(
                        name: "FK_Usuarios_autenticacion_Roles_Id_rol",
                        column: x => x.Idrol,
                        principalTable: "Roles",
                        principalColumn: "Id_rol",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Idusuario = table.Column<int>(name: "Id_usuario", type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Correoelectronico = table.Column<string>(name: "Correo_electronico", type: "nvarchar(max)", nullable: false),
                    Contraseña = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Idusuarioautenticacion = table.Column<int>(name: "Id_usuario_autenticacion", type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Idusuario);
                    table.ForeignKey(
                        name: "FK_Usuarios_Usuarios_autenticacion_Id_usuario_autenticacion",
                        column: x => x.Idusuarioautenticacion,
                        principalTable: "Usuarios_autenticacion",
                        principalColumn: "Id_usuario_autenticacion");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_Id_usuario_autenticacion",
                table: "Usuarios",
                column: "Id_usuario_autenticacion");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_autenticacion_Id_rol",
                table: "Usuarios_autenticacion",
                column: "Id_rol");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Usuarios_autenticacion");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
