using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Demo_Project.Models
{
    public class BillMaster
    {
        public int BillMasterId { get; set; }
        [Required, Column(TypeName = "date")]
        public DateTime BillDate { get; set; }
        [Required, Column(TypeName = "date")]
        public DateTime BillTime { get; set; }
        [Required, Column(TypeName = "money")]
        public decimal SubTotal { get; set; } 
        [Required, Column(TypeName = "money")]
        public decimal Discount { get; set; }
        [Required, Column(TypeName = "money")]
        public decimal VAT { get; set; }
        [Required, Column(TypeName = "money")]
        public decimal GrandTotal { get; set; }
        public ICollection<BillDetail> BillDetails { get; set; } = new List<BillDetail>();
    }
}
