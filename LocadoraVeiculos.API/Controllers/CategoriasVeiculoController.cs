using LocadoraVeiculos.API.Data;
using LocadoraVeiculos.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LocadoraVeiculos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriasVeiculoController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public CategoriasVeiculoController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/CategoriasVeiculo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoriaVeiculo>>> GetCategorias()
        {
            return await _context.CategoriasVeiculo.ToListAsync();
        }

        // GET: api/CategoriasVeiculo/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoriaVeiculo>> GetCategoria(int id)
        {
            var categoria = await _context.CategoriasVeiculo
                .FindAsync(id);

            if (categoria == null)
            {
                return NotFound(new
                {
                    mensagem = "Categoria não encontrada."
                });
            }

            return categoria;
        }

        // POST: api/CategoriasVeiculo
        [HttpPost]
        public async Task<ActionResult<CategoriaVeiculo>> PostCategoria(
            CategoriaVeiculo categoria)
        {
            _context.CategoriasVeiculo.Add(categoria);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetCategoria),
                new { id = categoria.Id },
                categoria
            );
        }

        // PUT: api/CategoriasVeiculo/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategoria(
            int id,
            CategoriaVeiculo categoria)
        {
            if (id != categoria.Id)
            {
                return BadRequest(new
                {
                    mensagem = "O ID da URL não corresponde ao ID da categoria."
                });
            }

            _context.Entry(categoria).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.CategoriasVeiculo.AnyAsync(c => c.Id == id))
                {
                    return NotFound(new
                    {
                        mensagem = "Categoria não encontrada."
                    });
                }

                throw;
            }

            return NoContent();
        }

        // DELETE: api/CategoriasVeiculo/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoria(int id)
        {
            var categoria = await _context.CategoriasVeiculo
                .FindAsync(id);

            if (categoria == null)
            {
                return NotFound(new
                {
                    mensagem = "Categoria não encontrada."
                });
            }

            _context.CategoriasVeiculo.Remove(categoria);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}