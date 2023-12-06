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
    public class FoodDAO
    {

        private static FoodDAO instance;
        public static FoodDAO Instance
        {
            get
            {
                if (instance == null)
                    instance = new FoodDAO();
                return instance;
            }
            private set { instance = value; }
        }
        private FoodDAO() { }

        public List<FoodDTO> GetListFood()
        {
            List<FoodDTO> list = new List<FoodDTO>();

            string query = string.Format("USP_GetListFood");

            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                FoodDTO food = new FoodDTO(item);
                list.Add(food);
            }
            return list;
        }

        public string GetFoodNameByFoodId(int foodId)
        {
            string query = "EXEC USP_GetFoodNameByFoodId @foodId";

            string result = null;

            result = DataProvider.Instance.ExecuteScalar(query, new object[] { foodId }).ToString();

            return result;
        }

        public List<FoodDTO> SearchFood(string keyword)
        {
            List<FoodDTO> foods = new List<FoodDTO>();

            string query = "SELECT * FROM UF_SearchFood( @foodName )";

            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { keyword });

            foreach (DataRow row in data.Rows)
            {
                FoodDTO food = new FoodDTO(row);
                foods.Add(food);
            }

            return foods;
        }

        public List<FoodDTO> SearchFoodByCategoty(string keyword, string category)
        {
            List<FoodDTO> foods = new List<FoodDTO>();

            string query = "SELECT * FROM UF_SearchFoodByCategoty( @foodName ,  @category )";

            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { keyword, category });

            foreach (DataRow row in data.Rows)
            {
                FoodDTO food = new FoodDTO(row);
                foods.Add(food);
            }

            return foods;
        }

        public double GetUnitPriceByFoodId(int foodId)
        {
            string query = "EXEC USP_GetUnitPriceByFoodId @foodId";

            double result = 0;

            result = double.Parse(DataProvider.Instance.ExecuteScalar(query, new object[] { foodId }).ToString());

            return result;
        }

        public List<FoodDTO> SearchFoodByFoodCategoryName(int foodcategoryid)
        {
            List<FoodDTO> list = new List<FoodDTO>();

            string query = string.Format("USP_SearchFoodByFoodCategoryName @foodcategoryid");

            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { foodcategoryid });

            foreach (DataRow item in data.Rows)
            {
                FoodDTO food = new FoodDTO(item);
                list.Add(food);
            }
            return list;
        }

        public bool InsertFood(int foodcategoryid, string foodName, string unit, double price, string images)
        {
            string query = string.Format("USP_InsertFood @foodcategoryid , @foodName , @unit , @price , @images");
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { foodcategoryid, foodName, unit, price, images });

            return result > 0;
        }

        public bool UpdateFood(int foodid, int foodcategoryid, string foodName, string unit, double price, string images)
        {
            string query = string.Format("USP_UpdateFood @foodid , @foodcategoryid , @foodName , @unit , @price , @images");
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] {foodid, foodcategoryid, foodName, unit, price, images });

            return result > 0;
        }

        public bool DeleteFood(int foodid)
        {
            string query = string.Format("USP_DeleteFood @foodid");
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { foodid });

            return result > 0;
        }

        public bool DeleteFoodByCategoryID(int categoryid)
        {
            string query = string.Format("USP_DeleteFoodByCategoryID @categoryid");
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { categoryid });

            return result > 0;
        }

        public FoodDTO GetFoodByFoodId(int foodId)
        {
            List<FoodDTO> list = new List<FoodDTO>();

            string query = "EXEC USP_GetFoodByFoodId @foodId";

            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { foodId });

            foreach (DataRow row in data.Rows)
            {
                FoodDTO food = new FoodDTO(row);

                list.Add(food);
            }
            
            return list.Where(row => row.FoodID == foodId).SingleOrDefault();
        }
    }
}
