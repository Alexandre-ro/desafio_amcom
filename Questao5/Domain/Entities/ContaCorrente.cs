using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Questao5.Domain.Entities
{
    public class ContaCorrente
    {
        [Column("idcontacorrente")]
        public string IdContaCorrente { get; set; }
        
        [Required]
        [Column("numero")]
        public int Numero { get; set; }

        [Required]
        [Column("nome")]
        public string Nome { get; set; }

        [Required]
        [Column("ativo")]
        public bool Ativo { get; set; }
    }
}
