using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace StoreBPO.Models
{
    public class StoreProductMapping
    {
        [Key]
        public int MappingID { get; set; }
        
        [Required]
        public int StoreID { get; set; }
        
        [Required]
        public int ProductID { get; set; }
        
        [Required]
        public int Stock { get; set; }

        public Store? Store { get; set; }
        public Product? Product { get; set; }
    }
}
