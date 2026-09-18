using System.ComponentModel.DataAnnotations;

namespace LocadoraVeiculos.API.Models
{
    public class Pagamento
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int AluguelId { get; set; }

        [Required]
        public DateTime DataPagamento { get; set; }

        [Required]
        public decimal Valor { get; set; }

        [Required]
        [StringLength(50)]
        public string FormaPagamento { get; set; } = string.Empty;

        [Required]
        [StringLength(30)]
        public string Status { get; set; } = string.Empty;

        public Aluguel? Aluguel { get; set; }
    }
}