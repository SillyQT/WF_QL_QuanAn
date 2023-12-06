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
    public class FoodCategoryDAO
    {
        private static FoodCategoryDAO instance;
        public static FoodCategoryDAO Instance
        {
            get
            {
                if (instance == null)
                    instance = new FoodCategoryDAO();
                return instance;
            }
            private set { instance = value; }
        }
        private FoodCategoryDAO() { }

        public List<FoodCategoryDTO> GetListFoodCategory()
        {
            List<FoodCategoryDTO> listfoodcategory = new List<FoodCategoryDTO>();

            string query = "USP_GetListFoodCategory";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                FoodCategoryDTO foodcategory = new FoodCategoryDTO(item);
                listfoodcategory.Add(foodcategory);
            }
            return listfoodcategory;
        }

        public bool InsertFoodCategory(string foodcatororyname)
        {
            string query = string.Format("USP_InsertFoodCategory @foodcategoryName");
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] {foodcatororyname});

            return result > 0;
        }

        public  bool DeleteFoodCategory(int foodcategoryid)
        {
            string query = string.Format("USP_DeleteFoodCategory @foodcategoryid");
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { foodcategoryid });

            return result > 0;
        }

        public bool UpdateFoodCategory(int foodcategoryid, string foodcatororyname)
        {
            string query = string.Format("USP_UpdateFoodCategory @foodcategoryid , @foodcatororyname");
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { foodcategoryid, foodcatororyname });

            return result > 0;
        }

        public List<FoodCategoryDTO> GetFoodCatogoryByID(int foodcategoryid)
        {
            List<FoodCategoryDTO> listfoodcategory = new List<FoodCategoryDTO>();

            string query = "USP_GetFoodCatogoryByID @foodcategoryid";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] {foodcategoryid});

            foreach (DataRow item in data.Rows)
            {
                FoodCategoryDTO foodcategory = new FoodCategoryDTO(item);
                listfoodcategory.Add(foodcategory);
            }
            return listfoodcategory;
        }
    }
}
