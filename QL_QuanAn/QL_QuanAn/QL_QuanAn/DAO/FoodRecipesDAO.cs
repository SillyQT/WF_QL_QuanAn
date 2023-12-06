using DevExpress.ReportServer.ServiceModel.DataContracts;
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
    public class FoodRecipesDAO
    {
        private static FoodRecipesDAO instance;
        public static FoodRecipesDAO Instance
        {
            get
            {
                if (instance == null)
                    instance = new FoodRecipesDAO();
                return instance;
            }
            private set { instance = value; }
        }
        private FoodRecipesDAO() { }

        public List<FoodRecipesDTO> GetListFoodRecipes()
        {
            List<FoodRecipesDTO> listfoodrecipes = new List<FoodRecipesDTO>();

            string query = "USP_GetListFoodRecipes";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                FoodRecipesDTO foodrecipes = new FoodRecipesDTO(item);
                listfoodrecipes.Add(foodrecipes);
            }
            return listfoodrecipes;
        }

        public bool InsertFoodRecipes(int foodid, int ingredientid, string content)
        {
            string query = string.Format("USP_InsertFoodRecipes @foodid , @ingredientid , @content");
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { foodid, ingredientid, content });

            return result > 0;
        }

        public int CheckIngredientExists(int foodid, int ingredientid)
        {
            return (int)DataProvider.Instance.ExecuteScalar("USP_CheckIngredientExists @foodid , @IngredientID", new object[] { foodid, ingredientid });
        }

        public bool DeleteFoodRecipes(int foodid, int ingredientid)
        {
            string query = string.Format("USP_DeleteFoodRecipes @foodid , @ingredientid");
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { foodid, ingredientid});

            return result > 0;
        }

        public bool UpdateFoodRecipes(int foodrecipesid, int foodid, int ingredientid, string content)
        {
            string query = string.Format("USP_UpdateFoodRecipes @foodrecipesid , @foodid , @@IngredientID , @content");
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] {foodrecipesid, foodid, ingredientid, content });

            return result > 0;
        }

        public bool DeleteFoodRecipesByFoodId(int foodid)
        {
            string query = string.Format("USP_DeleteFoodRecipesByFoodid @foodid");
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { foodid });

            return result > 0;
        }

        public List<FoodRecipesDTO> GetFoodRecipesByFoodID(int foodid)
        {
            List<FoodRecipesDTO> listfoodrecipes = new List<FoodRecipesDTO>();

            string query = "USP_GetFoodRecipesByFoodID @foodid";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { foodid });

            foreach (DataRow item in data.Rows)
            {
                FoodRecipesDTO foodrecipes = new FoodRecipesDTO(item);
                listfoodrecipes.Add(foodrecipes);
            }
            return listfoodrecipes;
        }
    }
}
