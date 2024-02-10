using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Demo_Project.Models
{
    public class BillDetail
    {
        public int BillDetailId { get; set; } 
        public int BillMasterId { get; set; }
        public int ItemId { get; set; } 
        public int Quantity { get; set; }
        [Required, Column(TypeName = "money")]
        public decimal Amount { get; set; } 
        [ForeignKey("BillMasterId")]
        public BillMaster? BillMaster { get; set; } = default!;
        [ForeignKey("ItemId")] 
        public Item? Item { get; set; } = default!; 
    }
}
