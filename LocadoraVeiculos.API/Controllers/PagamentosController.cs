using LocadoraVeiculos.API.Data;
using LocadoraVeiculos.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LocadoraVeiculos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PagamentosController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public PagamentosController(ApplicationContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pagamento>>> GetPagamentos()
        {
            return await _context.Pagamentos
                .Include(p => p.Aluguel)
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Pagamento>> GetPagamento(int id)
        {
            var pagamento = await _context.Pagamentos
                .Include(p => p.Aluguel)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (pagamento == null)
            {
                return NotFound(new
                {
                    mensagem = "Pagamento não encontrado."
                });
            }

            return pagamento;
        }

        [HttpPost]
        public async Task<ActionResult<Pagamento>> PostPagamento(
            Pagamento pagamento)
        {
            var aluguelExiste = await _context.Alugueis
                .AnyAsync(a => a.Id == pagamento.AluguelId);

            if (!aluguelExiste)
            {
                return BadRequest(new
                {
                    mensagem = "O aluguel informado não existe."
                });
            }

            _context.Pagamentos.Add(pagamento);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetPagamento),
                new { id = pagamento.Id },
                pagamento
            );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPagamento(
            int id,
            Pagamento pagamento)
        {
            if (id != pagamento.Id)
            {
                return BadRequest(new
                {
                    mensagem = "O ID da URL não corresponde ao ID do pagamento."
                });
            }

            _context.Entry(pagamento).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Pagamentos.AnyAsync(p => p.Id == id))
                {
                    return NotFound(new
                    {
                        mensagem = "Pagamento não encontrado."
                    });
                }

                throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePagamento(int id)
        {
            var pagamento = await _context.Pagamentos.FindAsync(id);

            if (pagamento == null)
            {
                return NotFound(new
                {
                    mensagem = "Pagamento não encontrado."
                });
            }

            _context.Pagamentos.Remove(pagamento);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}