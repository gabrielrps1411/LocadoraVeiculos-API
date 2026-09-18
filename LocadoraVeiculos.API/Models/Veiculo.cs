using System.ComponentModel.DataAnnotations;

namespace LocadoraVeiculos.API.Models
{
    public class Veiculo
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Modelo { get; set; } = string.Empty;

        [Required]
        public int AnoFabricacao { get; set; }

        [Required]
        public int Quilometragem { get; set; }

        [Required]
        public int FabricanteId { get; set; }

        [Required]
        public int CategoriaVeiculoId { get; set; }

        public Fabricante? Fabricante { get; set; }

        public CategoriaVeiculo? CategoriaVeiculo { get; set; }

        public ICollection<Aluguel> Alugueis { get; set; } = new List<Aluguel>();
    }
}