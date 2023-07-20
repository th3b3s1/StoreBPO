using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace StoreBPO.Models
{
    public class VMStoreProductMapping
    {
        [Required]
        public int StoreID { get; set; }
        
        [Required]
        public int ProductID { get; set; }
        
        [Required]
        public int Stock { get; set; }
    }
}
