using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiSGCubos.Models
{

    [Table("COMPRACUBOS")]
    public class CompraCubos
    {
        [Key]
        [Column("id_pedido")]
        public int Id_Pedido { get; set; }
        [Column("id_cubo")]
        public int Id_Cubo { get; set; }
        [Column("id_usuario")]

        public int Id_Usuario { get; set; }
        [Column("fechapedido")]

        public DateTime FechaPedido { get; set; }



    }
}
