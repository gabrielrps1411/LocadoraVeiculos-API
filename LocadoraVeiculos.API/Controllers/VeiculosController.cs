using LocadoraVeiculos.API.Data;
using LocadoraVeiculos.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LocadoraVeiculos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VeiculosController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public VeiculosController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/Veiculos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Veiculo>>> GetVeiculos()
        {
            return await _context.Veiculos
                .Include(v => v.Fabricante)
                .Include(v => v.CategoriaVeiculo)
                .ToListAsync();
        }

        // GET: api/Veiculos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Veiculo>> GetVeiculo(int id)
        {
            var veiculo = await _context.Veiculos
                .Include(v => v.Fabricante)
                .Include(v => v.CategoriaVeiculo)
                .FirstOrDefaultAsync(v => v.Id == id);

            if (veiculo == null)
            {
                return NotFound(new
                {
                    mensagem = "Veículo não encontrado."
                });
            }

            return veiculo;
        }

        // POST: api/Veiculos
        [HttpPost]
        public async Task<ActionResult<Veiculo>> PostVeiculo(Veiculo veiculo)
        {
            var fabricanteExiste = await _context.Fabricantes
                .AnyAsync(f => f.Id == veiculo.FabricanteId);

            if (!fabricanteExiste)
            {
                return BadRequest(new
                {
                    mensagem = "O fabricante informado não existe."
                });
            }

            var categoriaExiste = await _context.CategoriasVeiculo
                .AnyAsync(c => c.Id == veiculo.CategoriaVeiculoId);

            if (!categoriaExiste)
            {
                return BadRequest(new
                {
                    mensagem = "A categoria informada não existe."
                });
            }

            _context.Veiculos.Add(veiculo);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetVeiculo),
                new { id = veiculo.Id },
                veiculo
            );
        }

        // PUT: api/Veiculos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVeiculo(
            int id,
            Veiculo veiculo)
        {
            if (id != veiculo.Id)
            {
                return BadRequest(new
                {
                    mensagem = "O ID da URL não corresponde ao ID do veículo."
                });
            }

            var fabricanteExiste = await _context.Fabricantes
                .AnyAsync(f => f.Id == veiculo.FabricanteId);

            if (!fabricanteExiste)
            {
                return BadRequest(new
                {
                    mensagem = "O fabricante informado não existe."
                });
            }

            var categoriaExiste = await _context.CategoriasVeiculo
                .AnyAsync(c => c.Id == veiculo.CategoriaVeiculoId);

            if (!categoriaExiste)
            {
                return BadRequest(new
                {
                    mensagem = "A categoria informada não existe."
                });
            }

            _context.Entry(veiculo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Veiculos.AnyAsync(v => v.Id == id))
                {
                    return NotFound(new
                    {
                        mensagem = "Veículo não encontrado."
                    });
                }

                throw;
            }

            return NoContent();
        }

        // DELETE: api/Veiculos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVeiculo(int id)
        {
            var veiculo = await _context.Veiculos
                .FindAsync(id);

            if (veiculo == null)
            {
                return NotFound(new
                {
                    mensagem = "Veículo não encontrado."
                });
            }

            _context.Veiculos.Remove(veiculo);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}