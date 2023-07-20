using Microsoft.Graph.TermStore;
using System.ComponentModel.DataAnnotations;

namespace StoreBPO.Models
{
    public class VMStore
    {
        [Required]
        [StringLength(50)]
        public string StoreName { get; set; } = string.Empty;
    }
}
