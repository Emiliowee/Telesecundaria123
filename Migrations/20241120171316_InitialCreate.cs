using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyTelesecundaria.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Alumnos",
                columns: table => new
                {
                    Matricula = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Apellido = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FechaNacimiento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Correo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Grado = table.Column<int>(type: "int", nullable: false),
                    Grupo = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    NombreTutor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TelefonoTutor = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    CorreoTutor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alumnos", x => x.Matricula);
                });

            migrationBuilder.CreateTable(
                name: "Aulas",
                columns: table => new
                {
                    IDAula = table.Column<int>(type: "int", nullable: false),
                    Capacidad = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aulas", x => x.IDAula);
                });

            migrationBuilder.CreateTable(
                name: "Materias",
                columns: table => new
                {
                    IDMateria = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Materias", x => x.IDMateria);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    IDUsuario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Correo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Contraseña = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TipoUsuario = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.IDUsuario);
                });

            migrationBuilder.CreateTable(
                name: "Maestros",
                columns: table => new
                {
                    IdMaestro = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Apellidos = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Correo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Horario = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IdAula = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Maestros", x => x.IdMaestro);
                    table.ForeignKey(
                        name: "FK_Maestros_Aulas_IdAula",
                        column: x => x.IdAula,
                        principalTable: "Aulas",
                        principalColumn: "IDAula",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AlumnoMateria",
                columns: table => new
                {
                    Matricula = table.Column<int>(type: "int", nullable: false),
                    IDMateria = table.Column<int>(type: "int", nullable: false),
                    PeriodoBimestre = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Calificacion = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlumnoMateria", x => new { x.Matricula, x.IDMateria, x.PeriodoBimestre });
                    table.ForeignKey(
                        name: "FK_AlumnoMateria_Alumnos_Matricula",
                        column: x => x.Matricula,
                        principalTable: "Alumnos",
                        principalColumn: "Matricula",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AlumnoMateria_Materias_IDMateria",
                        column: x => x.IDMateria,
                        principalTable: "Materias",
                        principalColumn: "IDMateria",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MaestroMateria",
                columns: table => new
                {
                    IDMaestro = table.Column<int>(type: "int", nullable: false),
                    IDMateria = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaestroMateria", x => new { x.IDMaestro, x.IDMateria });
                    table.ForeignKey(
                        name: "FK_MaestroMateria_Maestros_IDMaestro",
                        column: x => x.IDMaestro,
                        principalTable: "Maestros",
                        principalColumn: "IdMaestro",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MaestroMateria_Materias_IDMateria",
                        column: x => x.IDMateria,
                        principalTable: "Materias",
                        principalColumn: "IDMateria",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AlumnoMateria_IDMateria",
                table: "AlumnoMateria",
                column: "IDMateria");

            migrationBuilder.CreateIndex(
                name: "IX_MaestroMateria_IDMateria",
                table: "MaestroMateria",
                column: "IDMateria");

            migrationBuilder.CreateIndex(
                name: "IX_Maestros_IdAula",
                table: "Maestros",
                column: "IdAula");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlumnoMateria");

            migrationBuilder.DropTable(
                name: "MaestroMateria");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Alumnos");

            migrationBuilder.DropTable(
                name: "Maestros");

            migrationBuilder.DropTable(
                name: "Materias");

            migrationBuilder.DropTable(
                name: "Aulas");
        }
    }
}
