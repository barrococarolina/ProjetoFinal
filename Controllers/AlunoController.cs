using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GerenciamentoEscola.Models;

namespace GerenciamentoEscola.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlunoController : ControllerBase
    {
        private readonly GerenciamentoEscolaContext _context;

        public AlunoController(GerenciamentoEscolaContext context)
        {
            _context = context;
        }

        // GET: api/Aluno
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Aluno>>> GetAluno()
        {
            List<Aluno> AlunosCadastrados = _context.Alunos.ToList();
            List<Turma> TurmasCadastradas = _context.Turmas.ToList();

            if (_context.Alunos == null)
            {
              return NotFound();
            }

            List<Aluno> alunosAtivos = new List<Aluno>();

            foreach (Aluno alunoCadastrado in AlunosCadastrados)
            {
                Turma? TurmaAluno = TurmasCadastradas.Find(x => x.Id == alunoCadastrado.TurmaId);

                if (TurmaAluno.Ativo == true)
                {
                    alunosAtivos.Add(alunoCadastrado);
                }
            }

            return alunosAtivos;

        }


        // GET: api/Aluno/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Aluno>> GetAluno(int id)
        {
            List<Turma> TurmasCadastradas = _context.Turmas.ToList();

          if (_context.Alunos == null)
          {
              return NotFound();
          }
            var aluno = await _context.Alunos.FindAsync(id);

            if (aluno == null)
            {
                return NotFound();
            }

            Turma? TurmaAluno = TurmasCadastradas.Find(x => x.Id == aluno.TurmaId);

            if (TurmaAluno.Ativo == true)
            {
                return aluno;
            }

            return NotFound();
        }

        // PUT: api/Aluno/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAluno(int id, Aluno aluno)
        {
            List<Turma> TurmasCadastradas = _context.Turmas.ToList();

            if (id != aluno.Id)
            {
                return BadRequest();
            }

            _context.Entry(aluno).State = EntityState.Modified;

            Turma? TurmaAluno = TurmasCadastradas.Find(x => x.Id == aluno.TurmaId);

            if (TurmaAluno.Ativo == false)
            {
                return BadRequest();
            }

            else
            {
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AlunosExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

             return NoContent();
        }

        // POST: api/Aluno
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Aluno>> PostAluno(Aluno aluno)
        {
          if (_context.Alunos == null)
          {
              return Problem("Entity set 'GerenciamentoEscolaContext.Aluno'  is null.");
          }
            _context.Alunos.Add(aluno);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAluno", new { id = aluno.Id }, aluno);
        }

        // DELETE: api/Aluno/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAluno(int id)
        {
            if (_context.Alunos == null)
            {
                return NotFound();
            }
            var aluno = await _context.Alunos.FindAsync(id);
            if (aluno == null)
            {
                return NotFound($"O id {id} não existe no banco de dados.");
            }

            _context.Alunos.Remove(aluno);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AlunosExists(int id)
        {
            return (_context.Alunos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        
    }
}
