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
    public class AreaDAO
    {
        private static AreaDAO instance;

        public static AreaDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AreaDAO();
                }
                return instance;
            }
            private set { instance = value; }
        }

        private AreaDAO() { }

        public List<AreaDTO> GetListArea()
        {
            List<AreaDTO> list = new List<AreaDTO>();

            string query = "EXEC USP_GetListArea";

            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow row in data.Rows)
            {
                AreaDTO area = new AreaDTO(row);
                list.Add(area);
            }

            return list;
        }

        public int GetNumberOfArea()
        {
            int result = 0;

            string query = "EXEC USP_GetNumberOfArea";

            result = Int32.Parse(DataProvider.Instance.ExecuteScalar(query).ToString());

            return result;
        }

        public List<AreaDTO> GetAreaNameByAreaID(int areaid)
        {
            List<AreaDTO> list = new List<AreaDTO>();

            string query = "USP_GetAreaNameByAreaID @areaid";

            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] {areaid});

            foreach (DataRow row in data.Rows)
            {
                AreaDTO area = new AreaDTO(row);
                list.Add(area);
            }

            return list;
        }
    }
}
