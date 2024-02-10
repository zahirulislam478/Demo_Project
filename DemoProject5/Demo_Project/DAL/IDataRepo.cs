using Demo_Project.Models;

namespace Demo_Project.DAL
{
    public interface IDataRepo
    {
        IEnumerable<Item> GetItems();
        IEnumerable<SelectOptionModel> GetSelectOptions(); 
        IEnumerable<BillMaster> GetBillMasters();
        BillDetail GetBillDetailById(int id); 
        IEnumerable<BillDetail> GetBillDetails();
        void CreateBillDetail(BillDetail billDetail);
        void UpdateBillDetail(BillDetail updatedBillDetail);
        void SaveBillDetails(BillMaster billMaster);
    }
}
