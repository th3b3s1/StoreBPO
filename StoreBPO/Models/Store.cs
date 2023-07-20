using System.ComponentModel.DataAnnotations;

namespace StoreBPO.Models
{
    public class Store
    {
        [Key]
        public int StoreID { get; set; }

        [Required]
        [StringLength(50)]
        public string StoreName { get; set; } = string.Empty;
    }
}
