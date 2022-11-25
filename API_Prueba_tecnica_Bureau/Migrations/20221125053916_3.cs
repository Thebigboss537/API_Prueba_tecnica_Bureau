using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIPruebatecnicaBureau.Migrations
{
    /// <inheritdoc />
    public partial class _3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Usuarios_autenticacion_Id_usuario_autenticacion",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "Contraseña",
                table: "Usuarios");

            migrationBuilder.AlterColumn<int>(
                name: "Id_usuario_autenticacion",
                table: "Usuarios",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Usuarios_autenticacion_Id_usuario_autenticacion",
                table: "Usuarios",
                column: "Id_usuario_autenticacion",
                principalTable: "Usuarios_autenticacion",
                principalColumn: "Id_usuario_autenticacion",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Usuarios_autenticacion_Id_usuario_autenticacion",
                table: "Usuarios");

            migrationBuilder.AlterColumn<int>(
                name: "Id_usuario_autenticacion",
                table: "Usuarios",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "Contraseña",
                table: "Usuarios",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Usuarios_autenticacion_Id_usuario_autenticacion",
                table: "Usuarios",
                column: "Id_usuario_autenticacion",
                principalTable: "Usuarios_autenticacion",
                principalColumn: "Id_usuario_autenticacion");
        }
    }
}
