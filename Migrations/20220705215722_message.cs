using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GerenciamentoEscola.Migrations
{
    public partial class message : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "turma",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "varchar(20)", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_turma", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "aluno",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "varchar(50)", nullable: false),
                    DataNascimento = table.Column<DateTime>(type: "DateTime", nullable: false),
                    Sexo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Faltas = table.Column<int>(type: "int", nullable: true),
                    TurmaId = table.Column<int>(type: "int", nullable: false),
                    AlunoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_aluno", x => x.Id);
                    table.ForeignKey(
                        name: "FK_aluno_aluno_AlunoId",
                        column: x => x.AlunoId,
                        principalTable: "aluno",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_aluno_turma_TurmaId",
                        column: x => x.TurmaId,
                        principalTable: "turma",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_aluno_AlunoId",
                table: "aluno",
                column: "AlunoId");

            migrationBuilder.CreateIndex(
                name: "IX_aluno_TurmaId",
                table: "aluno",
                column: "TurmaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "aluno");

            migrationBuilder.DropTable(
                name: "turma");
        }
    }
}
