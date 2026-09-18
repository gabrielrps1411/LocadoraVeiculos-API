using LocadoraVeiculos.API.Data;
using LocadoraVeiculos.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LocadoraVeiculos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlugueisController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public AlugueisController(ApplicationContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Aluguel>>> GetAlugueis()
        {
            return await _context.Alugueis
                .Include(a => a.Cliente)
                .Include(a => a.Veiculo)
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Aluguel>> GetAluguel(int id)
        {
            var aluguel = await _context.Alugueis
                .Include(a => a.Cliente)
                .Include(a => a.Veiculo)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (aluguel == null)
            {
                return NotFound(new
                {
                    mensagem = "Aluguel não encontrado."
                });
            }

            return aluguel;
        }

        [HttpPost]
        public async Task<ActionResult<Aluguel>> PostAluguel(
            Aluguel aluguel)
        {
            var clienteExiste = await _context.Clientes
                .AnyAsync(c => c.Id == aluguel.ClienteId);

            if (!clienteExiste)
            {
                return BadRequest(new
                {
                    mensagem = "O cliente informado não existe."
                });
            }

            var veiculoExiste = await _context.Veiculos
                .AnyAsync(v => v.Id == aluguel.VeiculoId);

            if (!veiculoExiste)
            {
                return BadRequest(new
                {
                    mensagem = "O veículo informado não existe."
                });
            }

            if (aluguel.DataFim < aluguel.DataInicio)
            {
                return BadRequest(new
                {
                    mensagem = "A data final não pode ser anterior à data inicial."
                });
            }

            _context.Alugueis.Add(aluguel);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetAluguel),
                new { id = aluguel.Id },
                aluguel
            );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAluguel(
            int id,
            Aluguel aluguel)
        {
            if (id != aluguel.Id)
            {
                return BadRequest(new
                {
                    mensagem = "O ID da URL não corresponde ao ID do aluguel."
                });
            }

            _context.Entry(aluguel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Alugueis.AnyAsync(a => a.Id == id))
                {
                    return NotFound(new
                    {
                        mensagem = "Aluguel não encontrado."
                    });
                }

                throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAluguel(int id)
        {
            var aluguel = await _context.Alugueis.FindAsync(id);

            if (aluguel == null)
            {
                return NotFound(new
                {
                    mensagem = "Aluguel não encontrado."
                });
            }

            _context.Alugueis.Remove(aluguel);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}