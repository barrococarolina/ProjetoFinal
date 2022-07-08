using System.Text.Json.Serialization;

namespace GerenciamentoEscola.Models
{
    public class Turma
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public bool? Ativo { get; set; }

        public virtual List<Aluno>? Alunos { internal get; set; }

    }
}
