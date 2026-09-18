using System.ComponentModel.DataAnnotations;

namespace LocadoraVeiculos.API.Models
{
    public class Aluguel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ClienteId { get; set; }

        [Required]
        public int VeiculoId { get; set; }

        [Required]
        public DateTime DataInicio { get; set; }

        [Required]
        public DateTime DataFim { get; set; }

        public DateTime? DataDevolucao { get; set; }

        [Required]
        public int QuilometragemInicial { get; set; }

        public int? QuilometragemFinal { get; set; }

        [Required]
        public decimal ValorDiaria { get; set; }

        public decimal? ValorTotal { get; set; }

        public Cliente? Cliente { get; set; }

        public Veiculo? Veiculo { get; set; }

        public ICollection<Pagamento> Pagamentos { get; set; } = new List<Pagamento>();
    }
}