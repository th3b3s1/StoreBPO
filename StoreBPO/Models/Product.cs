using System.ComponentModel.DataAnnotations;

namespace StoreBPO.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set;}

        [Required, StringLength(50)]
        public string ProductName { get; set; }= string.Empty;
    }
}
