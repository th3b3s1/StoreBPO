using System.ComponentModel.DataAnnotations;

namespace StoreBPO.Models
{
    public class VMProduct
    {
        [Required, StringLength(50)]
        public string ProductName { get; set; }= string.Empty;
    }
}
