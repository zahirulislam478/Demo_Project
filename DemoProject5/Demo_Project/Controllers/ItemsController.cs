using Demo_Project.DAL;
using Demo_Project.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Demo_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly IDataRepo repo;
        public ItemsController(IDataRepo repo)
        {
            this.repo = repo;
        }

        [HttpGet("items")]
        public ActionResult<IEnumerable<Item>> GetItems()
        {
            return Ok(this.repo.GetItems());
        }

        [HttpGet("SelectOptions")]
        public ActionResult<IEnumerable<SelectOptionModel>> GetOptions()
        {
            return Ok(this.repo.GetSelectOptions());
        }

        [HttpGet("billMasters")]
        public ActionResult<IEnumerable<BillMaster>> GetBillMasters()
        {
            return Ok(this.repo.GetBillMasters());
        }

        [HttpGet("billDetails/{id}")]
        public ActionResult<BillDetail> GetBillDetailById(int id) 
        {
            var billDetail = this.repo.GetBillDetailById(id);
            return Ok(billDetail);
        }

        [HttpGet("billDetails")]
        public ActionResult<IEnumerable<BillDetail>> GetBillDetails()
        {
            return Ok(this.repo.GetBillDetails());
        }

        [HttpPost("billDetails")]
        public ActionResult CreateBillDetail([FromBody] BillDetail billDetail)
        {
            this.repo.CreateBillDetail(billDetail);
            return CreatedAtAction(nameof(GetBillDetailById), new { id = billDetail.BillDetailId }, billDetail);
        }


        [HttpPut("billDetails/{id}")]
        public ActionResult UpdateBillDetail(int id, [FromBody] BillDetail billDetail)
        {
            if (id != billDetail.BillDetailId)
            {
                return BadRequest();
            }

            var existingBillDetail = this.repo.GetBillDetailById(id);

            if (existingBillDetail == null)
            {
                return NotFound();
            }

            if (billDetail.Item != null)
            {
                existingBillDetail.Item = existingBillDetail.Item ?? new Item(); 
                existingBillDetail.Item.ItemName = billDetail.Item.ItemName;
                existingBillDetail.Item.UnitPrice = billDetail.Item.UnitPrice;
            }

            existingBillDetail.Quantity = billDetail.Quantity;
            existingBillDetail.Amount = billDetail.Amount;

            this.repo.UpdateBillDetail(existingBillDetail);

            return NoContent();
        }


        [HttpPost("saveBillDetails")]
        public ActionResult SaveBillDetails([FromBody] BillMaster billMaster)
        {
            this.repo.SaveBillDetails(billMaster);

            var response = new { message = "Bill details saved successfully" };

            return new JsonResult(response);

        }

    }
}
