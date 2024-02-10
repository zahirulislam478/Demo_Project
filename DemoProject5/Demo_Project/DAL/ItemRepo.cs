using System.Data.SqlClient;
using System.Data.SqlTypes;
using Demo_Project.Models;

namespace Demo_Project.DAL
{
    public class ItemRepo : IDataRepo
    {
        private readonly SqlConnection con;
        private readonly IConfiguration Configuration;
        public ItemRepo(IConfiguration configuration)
        {
            this.Configuration = configuration;
            con = new SqlConnection(this.Configuration.GetConnectionString("db"));
        }

        public IEnumerable<Item> GetItems() 
        {
            using (SqlCommand cmd = new SqlCommand("GetItems", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                List<Item> items = new List<Item>();
                while (dr.Read())
                {
                    items.Add(new Item
                    {
                        ItemId = dr.GetInt32(dr.GetOrdinal("ItemId")),
                        ItemName = dr.GetString(dr.GetOrdinal("ItemName")),
                        UnitPrice = dr.GetDecimal(dr.GetOrdinal("UnitPrice"))
                    });
                }
                con.Close();
                return items;
            }
        }

        public IEnumerable<SelectOptionModel> GetSelectOptions()
        {
            using (SqlCommand cmd = new SqlCommand("GetItems", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                List<SelectOptionModel> items = new List<SelectOptionModel>();
                while (dr.Read())
                {
                    items.Add(new SelectOptionModel
                    {
                        ItemId = dr.GetInt32(dr.GetOrdinal("ItemId")),
                        ItemName = dr.GetString(dr.GetOrdinal("ItemName"))
                    });
                }
                con.Close();
                return items;
            }
        }

        public IEnumerable<BillMaster> GetBillMasters()
        {
            using (SqlCommand cmd = new SqlCommand("GetBillMasters", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                List<BillMaster> billMasters = new List<BillMaster>();

                while (dr.Read())
                {
                    billMasters.Add(new BillMaster
                    {
                        BillMasterId = dr.GetInt32(dr.GetOrdinal("BillMasterId")),
                        BillDate = dr.GetDateTime(dr.GetOrdinal("BillDate")),
                        BillTime = dr.GetDateTime(dr.GetOrdinal("BillTime")),
                        VAT = dr.GetDecimal(dr.GetOrdinal("VAT")),
                        Discount = dr.GetDecimal(dr.GetOrdinal("Discount")),
                        SubTotal = dr.GetDecimal(dr.GetOrdinal("SubTotal")),
                        GrandTotal = dr.GetDecimal(dr.GetOrdinal("GrandTotal"))
                    });
                }
                con.Close();
                return billMasters;
            }
        }

        public IEnumerable<BillDetail> GetBillDetails()
        {
            using (SqlCommand cmd = new SqlCommand("GetBillDetails", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                List<BillDetail> billDetails = new List<BillDetail>();

                while (dr.Read())
                {
                    billDetails.Add(new BillDetail
                    {
                        BillDetailId = dr.GetInt32(dr.GetOrdinal("BillDetailId")),
                        BillMasterId = dr.GetInt32(dr.GetOrdinal("BillMasterId")),
                        ItemId = dr.GetInt32(dr.GetOrdinal("ItemId")),
                        Quantity = dr.GetInt32(dr.GetOrdinal("Quantity")),
                        Amount = dr.GetDecimal(dr.GetOrdinal("Amount"))
                    });
                }
                con.Close();
                return billDetails;
            }
        }

        public BillDetail GetBillDetailById(int id)
        {
            using (SqlCommand cmd = new SqlCommand("GetBillDetailById", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);

                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    return new BillDetail
                    {
                        BillDetailId = dr.GetInt32(dr.GetOrdinal("BillDetailId")),
                        BillMasterId = dr.GetInt32(dr.GetOrdinal("BillMasterId")),
                        ItemId = dr.GetInt32(dr.GetOrdinal("ItemId")),
                        Quantity = dr.GetInt32(dr.GetOrdinal("Quantity")),
                        Amount = dr.GetDecimal(dr.GetOrdinal("Amount"))
                    };
                }
                else
                {
                    throw new InvalidOperationException("No bill detail found with the specified ID.");
                }
            }
        }

        public void CreateBillDetail(BillDetail billDetail)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("CreateBillDetail", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@BillMasterId", billDetail.BillMasterId);
                    cmd.Parameters.AddWithValue("@ItemId", billDetail.ItemId);
                    cmd.Parameters.AddWithValue("@Quantity", billDetail.Quantity);
                    cmd.Parameters.AddWithValue("@Amount", billDetail.Amount);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            finally
            {
                con.Close();
            }
        }

        public void UpdateBillDetail(BillDetail updatedBillDetail)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("UpdateBillDetail", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@BillDetailId", updatedBillDetail.BillDetailId);
                    cmd.Parameters.AddWithValue("@BillMasterId", updatedBillDetail.BillMasterId);
                    cmd.Parameters.AddWithValue("@ItemId", updatedBillDetail.ItemId);
                    cmd.Parameters.AddWithValue("@Quantity", updatedBillDetail.Quantity);
                    cmd.Parameters.AddWithValue("@Amount", updatedBillDetail.Amount);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            finally
            {
                con.Close();
            }
        }

        public void SaveBillDetails(BillMaster billMaster)
        {
            using (SqlCommand cmd = new SqlCommand("SaveBillDetails", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                // Add parameters for the stored procedure
                DateTime currentDateTime = DateTime.Now;

                cmd.Parameters.AddWithValue("@BillDate", (billMaster.BillDate != DateTime.MinValue) ? billMaster.BillDate : currentDateTime);
                cmd.Parameters.AddWithValue("@BillTime", (billMaster.BillTime != DateTime.MinValue) ? billMaster.BillTime : currentDateTime);
                cmd.Parameters.AddWithValue("@VAT", billMaster.VAT);
                cmd.Parameters.AddWithValue("@Discount", billMaster.Discount);
                cmd.Parameters.AddWithValue("@SubTotal", billMaster.SubTotal);
                cmd.Parameters.AddWithValue("@GrandTotal", billMaster.GrandTotal);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

    }
}
