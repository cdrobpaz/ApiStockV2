using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiStockV2.Models
{
    [Table("Stock")]
    public class Stock
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Column("PRODUCTO")]
        public string Producto { get; set; }
        [Column("TIPO")]
        public string Tipo { get; set; }
        [Column("MARCA")]
        public string Marca { get; set; }
        [Column("UNIDADES")]
        public int Unidades { get; set; }
        [Column("PRECIO_DE_COMPRA")]
        public decimal PrecioCompra { get; set; }
        [Column("PRECIO_DE_VENTA")]
        public decimal PrecioVenta { get; set; }
    }
}
