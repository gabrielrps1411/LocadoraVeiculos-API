using System.ComponentModel.DataAnnotations;

namespace LocadoraVeiculos.API.Models
{
    public class Cliente
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        public string Nome { get; set; } = string.Empty;

        [Required]
        [StringLength(14)]
        public string CPF { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [StringLength(150)]
        public string Email { get; set; } = string.Empty;

        public ICollection<Aluguel> Alugueis { get; set; } = new List<Aluguel>();
    }
}