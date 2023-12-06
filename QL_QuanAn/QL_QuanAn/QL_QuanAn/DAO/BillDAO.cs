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
    public class BillDAO
    {
        private static BillDAO instance;

        public static BillDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new BillDAO();
                }
                return instance;
            }
            private set { instance = value; }
        }

        private BillDAO() { }

        public BillDTO GetBillByTable(int tableId)
        {
            BillDTO bill = new BillDTO();

            string query = "EXEC USP_GetBillByTable @tableId";

            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { tableId });

            foreach (DataRow row in data.Rows)
            {
                bill = new BillDTO(row);
            }

            return bill;
        }

        public bool InsertBill(DateTime billCreateDate, int tableId, string employeeId)
        {
            string query = "EXEC USP_InsertBill @tableId , @employeeId , @dateIn";

            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { tableId, employeeId, billCreateDate });

            return result > 0;
        }

        public bool DeleteBill(int billId)
        {
            string query = "EXEC USP_DeleteBill @billId";

            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { billId });

            return result > 0;
        }

        public bool UpdateTableIdForBill(int tableId, int billId)
        {
            string query = "EXEC USP_UpdateTableIdForBill @tableId , @billId";

            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { tableId, billId });

            return result > 0;
        }

        public bool UpdateTotalForBill(int billId, int gt)
        {
            string query = "USP_UpdateTotalForBill @billId , @total";

            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { billId, gt });

            return result > 0;
        }

        public bool UpdateBill(int billId, int gt)
        {
            string query = "USP_UpdateTotalForBill @billId , @total";

            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { billId, gt });

            return result > 0;
        }

        public List<BillDTO> GetListBillByDateOut(DateTime dateStart, DateTime dateEnd)
        {
            List<BillDTO> list = new List<BillDTO>();

            string query = "EXEC USP_GetListBillByDateOut @dateStart , @dateEnd";

            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { dateStart, dateEnd });

            foreach (DataRow row in data.Rows)
            {
                BillDTO bill = new BillDTO(row);

                list.Add(bill);
            }

            return list;
        }

        public List<BillDTO> GetListBillByDateOutAndStaff(DateTime dateStart, DateTime dateEnd, string staffId)
        {
            List<BillDTO> list = new List<BillDTO>();

            string query = "EXEC USP_GetListBillByDateOutAndStaff @dateStart , @dateEnd , @staffId";

            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { dateStart, dateEnd, staffId });

            foreach (DataRow row in data.Rows)
            {
                BillDTO bill = new BillDTO(row);

                list.Add(bill);
            }

            return list;
        }
    }
}
