using LocadoraVeiculos.API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LocadoraVeiculos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FiltrosController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public FiltrosController(ApplicationContext context)
        {
            _context = context;
        }

        // =========================================================
        // 1 - VEÍCULOS POR FABRICANTE
        // INNER JOIN
        // =========================================================

        [HttpGet("veiculos/fabricante/{fabricanteId}")]
        public async Task<IActionResult> VeiculosPorFabricante(int fabricanteId)
        {
            var resultado = await (
                from v in _context.Veiculos
                join f in _context.Fabricantes
                    on v.FabricanteId equals f.Id
                where f.Id == fabricanteId
                select new
                {
                    VeiculoId = v.Id,
                    Modelo = v.Modelo,
                    AnoFabricacao = v.AnoFabricacao,
                    Quilometragem = v.Quilometragem,
                    FabricanteId = f.Id,
                    Fabricante = f.Nome
                }
            ).ToListAsync();

            return Ok(resultado);
        }


        // =========================================================
        // 2 - VEÍCULOS POR CATEGORIA
        // INNER JOIN
        // =========================================================

        [HttpGet("veiculos/categoria/{categoriaId}")]
        public async Task<IActionResult> VeiculosPorCategoria(int categoriaId)
        {
            var resultado = await (
                from v in _context.Veiculos
                join c in _context.CategoriasVeiculo
                    on v.CategoriaVeiculoId equals c.Id
                where c.Id == categoriaId
                select new
                {
                    VeiculoId = v.Id,
                    Modelo = v.Modelo,
                    AnoFabricacao = v.AnoFabricacao,
                    Quilometragem = v.Quilometragem,
                    CategoriaId = c.Id,
                    Categoria = c.Nome
                }
            ).ToListAsync();

            return Ok(resultado);
        }


        // =========================================================
        // 3 - ALUGUÉIS POR CLIENTE
        // INNER JOIN
        // =========================================================

        [HttpGet("alugueis/cliente/{clienteId}")]
        public async Task<IActionResult> AlugueisPorCliente(int clienteId)
        {
            var resultado = await (
                from a in _context.Alugueis
                join c in _context.Clientes
                    on a.ClienteId equals c.Id
                join v in _context.Veiculos
                    on a.VeiculoId equals v.Id
                where c.Id == clienteId
                select new
                {
                    AluguelId = a.Id,

                    ClienteId = c.Id,
                    Cliente = c.Nome,

                    VeiculoId = v.Id,
                    Veiculo = v.Modelo,

                    DataInicio = a.DataInicio,
                    DataFim = a.DataFim,
                    DataDevolucao = a.DataDevolucao,

                    QuilometragemInicial = a.QuilometragemInicial,
                    QuilometragemFinal = a.QuilometragemFinal,

                    ValorDiaria = a.ValorDiaria,
                    ValorTotal = a.ValorTotal
                }
            ).ToListAsync();

            return Ok(resultado);
        }


        // =========================================================
        // 4 - ALUGUÉIS POR VEÍCULO
        // INNER JOIN
        // =========================================================

        [HttpGet("alugueis/veiculo/{veiculoId}")]
        public async Task<IActionResult> AlugueisPorVeiculo(int veiculoId)
        {
            var resultado = await (
                from a in _context.Alugueis
                join v in _context.Veiculos
                    on a.VeiculoId equals v.Id
                join c in _context.Clientes
                    on a.ClienteId equals c.Id
                where v.Id == veiculoId
                select new
                {
                    AluguelId = a.Id,

                    VeiculoId = v.Id,
                    Veiculo = v.Modelo,

                    ClienteId = c.Id,
                    Cliente = c.Nome,

                    DataInicio = a.DataInicio,
                    DataFim = a.DataFim,
                    DataDevolucao = a.DataDevolucao,

                    QuilometragemInicial = a.QuilometragemInicial,
                    QuilometragemFinal = a.QuilometragemFinal,

                    ValorDiaria = a.ValorDiaria,
                    ValorTotal = a.ValorTotal
                }
            ).ToListAsync();

            return Ok(resultado);
        }


        // =========================================================
        // 5 - HISTÓRICO COMPLETO DO CLIENTE
        // LEFT JOIN
        // =========================================================

        [HttpGet("clientes/historico/{clienteId}")]
        public async Task<IActionResult> HistoricoCliente(int clienteId)
        {
            var resultado = await (
                from c in _context.Clientes

                join a in _context.Alugueis
                    on c.Id equals a.ClienteId into alugueisGroup

                from a in alugueisGroup.DefaultIfEmpty()

                join v in _context.Veiculos
                    on a.VeiculoId equals v.Id into veiculosGroup

                from v in veiculosGroup.DefaultIfEmpty()

                join f in _context.Fabricantes
                    on v.FabricanteId equals f.Id into fabricantesGroup

                from f in fabricantesGroup.DefaultIfEmpty()

                where c.Id == clienteId

                select new
                {
                    ClienteId = c.Id,
                    Cliente = c.Nome,
                    CPF = c.CPF,
                    Email = c.Email,

                    AluguelId = a != null ? a.Id : (int?)null,

                    VeiculoId = v != null ? v.Id : (int?)null,
                    Veiculo = v != null ? v.Modelo : null,

                    FabricanteId = f != null ? f.Id : (int?)null,
                    Fabricante = f != null ? f.Nome : null,

                    DataInicio = a != null ? a.DataInicio : (DateTime?)null,
                    DataFim = a != null ? a.DataFim : (DateTime?)null,

                    ValorDiaria = a != null ? a.ValorDiaria : (decimal?)null,
                    ValorTotal = a != null ? a.ValorTotal : (decimal?)null
                }
            ).ToListAsync();

            return Ok(resultado);
        }
    }
}