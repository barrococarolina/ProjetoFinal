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
    public class TurmaController : ControllerBase
    {
        private readonly GerenciamentoEscolaContext _context;

        public TurmaController(GerenciamentoEscolaContext context)
        {
            _context = context;
        }

        // GET: api/Turma
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Turma>>> GetTurmas()
        {
            List <Turma> TurmasCadastradas = _context.Turmas.ToList();

          if (_context.Turmas == null)
          {
              return NotFound("Não há nenhuma turma cadastrada.");
          }

            List<Turma> TurmasAtivas = new List <Turma>();

            foreach (Turma TurmaCadastrada in TurmasCadastradas)
            {
                if (TurmaCadastrada.Ativo == true)
                {
                    TurmasAtivas.Add(TurmaCadastrada);
                }
            }

            return TurmasAtivas;
        }

        // GET: api/Turma/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Turma>> GetTurma(int id)
        {
          if (_context.Turmas == null)
          {
              return NotFound("Não há nenhuma turma cadastrada.");
          }
            var turma = await _context.Turmas.FindAsync(id);

            if (turma == null)
            {
                return NotFound($"A busca não retornou resultados para a turma de ID: {id}.");
            
            }

            if (turma.Ativo == true)
            {
                return turma;
            }

            return NotFound();
        }

        // PUT: api/Turma/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTurma(int id, Turma turma)
        {
            if (id != turma.Id)
            {
                return BadRequest();
            }

            _context.Entry(turma).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TurmasExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok("Alteração realizada");
        }

        // POST: api/Turma
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Turma>> PostTurma(Turma turma)
        {
          if (_context.Turmas == null)
          {
              return Problem("Entity set 'GerenciamentoEscolaContext.Turmas'  is null.");
          }
            _context.Turmas.Add(turma);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTurma", new { id = turma.Id }, turma);
        }

        // DELETE: api/Turma/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTurma(int id)
        {
            if (_context.Turmas == null)
            {
                return NotFound();
            }
            var turma = await _context.Turmas.FindAsync(id);
            if (turma == null)
            {
                return NotFound($"O id {id} não existe no banco de dados.");
            }

            if (!turma.Alunos.Any())
            {
                _context.Turmas.Remove(turma);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            else
            {
                throw new Exception("Operação inválida! Há alunos cadastrados nessa turma.");
            }

            /*_context.Turmas.Remove(turma);
            await _context.SaveChangesAsync();

            return Ok("O registro foi removido.");*/

        }

        private bool TurmasExists(int id)
        {
            return (_context.Turmas?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
