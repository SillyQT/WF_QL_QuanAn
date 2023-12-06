using QL_QuanAn.DTO;
using QuanLyQuanCafe.DAO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QL_QuanAn.DAO
{
    public class BillInfoDAO
    {
        private static BillInfoDAO instance;

        public static BillInfoDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new BillInfoDAO();
                }
                return instance;
            }
            private set { instance = value; }
        }

        private BillInfoDAO() { }

        public List<BillInfoDTO> GetBillInfoByBill(int billId)
        {
            List<BillInfoDTO> list = new List<BillInfoDTO>();

            string query = "EXEC USP_GetBillInfoByBill @billId";

            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { billId });

            foreach (DataRow row in data.Rows)
            {
                BillInfoDTO bill = new BillInfoDTO(row);
                list.Add(bill);
            }

            return list;
        }

        public bool InsertBillInfo(int billId, int foodId, int count)
        {
            string query = "EXEC USP_InsertBillInfo @foodId , @billId , @count";

            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { foodId, billId, count });

            return result > 0;
        }

        public bool DeleteBillInfoByBillIdAndFoodId(int billId, int foodId)
        {
            string query = "EXEC USP_DeleteBillInfoByBillIdAndFoodId @billId , @foodId";

            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { billId, foodId });

            return result > 0;
        }

        public bool UpdateCountInBillInfo(int billId, int foodId, int count)
        {
            string query = "EXEC USP_UpdateCountInBillInfo @billId , @foodId , @count";

            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { billId, foodId, count });

            return result > 0;
        }

        public bool DeleteBillInfoByFoodId(int foodId)
        {
            string query = "EXEC USP_DeleteBillInfoByFoodId @foodId";

            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { foodId });

            return result > 0;
        }
    }
}
