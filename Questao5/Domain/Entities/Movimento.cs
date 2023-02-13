using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Questao5.Domain.Entities
{
    public class Movimento
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("idmovimento")]
        public string IdMovimento { get; set; }

        [Column("idcontacorrente")]
        public int IdContaCorrente { get; set; }

        [Column("datamovimento")]
        public string DataMovimento { get; set; }
        
        [Column("tipomovimento")]
        public string TipoMovimento { get; set; }
        
        [Column("valor ")]
        public decimal Valor { get; set; }
    }
}
