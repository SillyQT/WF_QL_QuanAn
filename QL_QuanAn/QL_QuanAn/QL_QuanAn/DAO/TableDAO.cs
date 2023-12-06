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
    public class TableDAO
    {
        private static TableDAO instance;

        public static TableDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new TableDAO();
                }
                return instance;
            }
            private set { instance = value; }
        }

        private TableDAO() { }

        public List<TableDTO> GetListTable()
        {
            List<TableDTO> list = new List<TableDTO>();

            string query = "USP_GetListTable";

            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow row in data.Rows)
            {
                TableDTO table = new TableDTO(row);
                list.Add(table);
            }
            return list;
        }

        public List<TableDTO> GetListTableByArea(int areaId)
        {
            List<TableDTO> list = new List<TableDTO>();

            string query = "EXEC USP_GetListTableByArea @areaId";

            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { areaId });

            foreach (DataRow row in data.Rows)
            {
                TableDTO table = new TableDTO(row);
                list.Add(table);
            }
            return list;
        }

        public string GetStatusByTable(int tableId)
        {
            string query = "EXEC USP_GetStatusByTable @tableId";

            string result = "";

            result = DataProvider.Instance.ExecuteScalar(query, new object[] { tableId }).ToString();

            return result;
        }

        public bool UpdateStatusTable(int tableId, string status)
        {
            string query = "EXEC USP_UpdateStatusTable @tableId , @status";

            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { tableId, status });

            return result > 0;
        }

        public TableDTO GetTableByTableId(int tableId)
        {
            TableDTO table = null;

            string query = "EXEC USP_GetTableByTableId @tableId";

            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { tableId });

            foreach (DataRow row in data.Rows)
            {
                table = new TableDTO(row);
            }

            return table;
        }

        public int GetTableIdByTableName(string tableName)
        {
            string query = "EXEC USP_GetTableIdByTableName @tableName";

            int result = Int32.Parse(DataProvider.Instance.ExecuteScalar(query, new object[] { tableName }).ToString());

            return result;
        }

        public bool InsertTable(int areaid, string tablename, int quanlity, string status)
        {
            string query = string.Format("USP_InsertTable @areaid , @tablename , @quanlity , @status");
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { areaid, tablename, quanlity, status });

            return result > 0;
        }

        public bool DeleteTalbe(int tableid)
        {
            string query = string.Format("USP_DeleteTable @tableid");
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { tableid });

            return result > 0;
        }

        public bool UpdateTable(int tableid, int areaid, string tablename, int quanlity, string status)
        {
            string query = string.Format("USP_UpdateTable @tableid , @areaid , @tablename , @quanlity , @status");
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { tableid, areaid, tablename, quanlity, status });

            return result > 0;
        }
    }
}
