using Microsoft.EntityFrameworkCore;
using GerenciamentoEscola.Models;

namespace GerenciamentoEscola.Models
{
    public class GerenciamentoEscolaContext : DbContext
    {
        public GerenciamentoEscolaContext(DbContextOptions<GerenciamentoEscolaContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Aluno>().ToTable("aluno");

            modelBuilder.Entity<Aluno>()
                .HasOne(c => c.Turma) // 1 tem 1 endereço
                .WithMany(e => e.Alunos) // 1 endereço tem vários contatos
                .HasForeignKey(e => e.TurmaId); // campo da FK

            modelBuilder.Entity<Turma>().ToTable("turma");

            //modelBuilder.Entity<Turma>()
              //  .HasMany(e => e.Alunos)
                //.WithOne(e => e.Turma);
        }

        public DbSet<Turma> Turmas { get; set; }

        public DbSet<Aluno> Alunos { get; set; }
    }
}
