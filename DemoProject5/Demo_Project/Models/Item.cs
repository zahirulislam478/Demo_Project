using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Demo_Project.Models
{
    public class Item
    {
        public int ItemId { get; set; }
        [Required, StringLength(100)]
        public string ItemName { get; set; } = default!;
        [Required, Column(TypeName = "money")]
        public decimal UnitPrice { get; set; } 
        public ICollection<BillDetail> BillDetails { get; set; } = new List<BillDetail>(); 
    }
}
