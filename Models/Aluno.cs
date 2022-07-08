using System.Text.Json.Serialization;

namespace GerenciamentoEscola.Models
{
    public class Aluno
    {

        public int Id { get; set; }
        public string Nome { get; set; }

        public DateTime DataNascimento { get; set; }

        public string Sexo { get; set; }

        public int? Faltas { get; set; } 


        public int TurmaId { get; set; }


        public virtual Turma? Turma { internal get; set; }
    }

    
}
