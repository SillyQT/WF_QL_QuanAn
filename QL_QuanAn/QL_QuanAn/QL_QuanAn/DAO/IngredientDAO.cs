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
    public class IngredientDAO
    {
        private static IngredientDAO instance;
        public static IngredientDAO Instance
        {
            get
            {
                if (instance == null)
                    instance = new IngredientDAO();
                return instance;
            }
            private set { instance = value; }
        }
        private IngredientDAO() { }

        public List<IngredientDTO> GetListIngredient()
        {
            List<IngredientDTO> listingredient = new List<IngredientDTO>();

            string query = "USP_GetListIngredient";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                IngredientDTO ingredient = new IngredientDTO(item);
                listingredient.Add(ingredient);
            }
            return listingredient;
        }

        public bool InsertIngredient(string ingredientName)
        {
            string query = string.Format("USP_InsertIngredient @ingerdientName");
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] {ingredientName});

            return result > 0;
        }

        public bool DeleteIngredient(int ingredientID)
        {
            string query = string.Format("USP_DeleteIngredient @ingredientID");
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { ingredientID });

            return result > 0;
        }

        public bool UpdateIngredient(int ingredientID, string ingredientName)
        {
            string query = string.Format("USP_UpdateIngredient @ingredientID , @ingerdientName");
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { ingredientID, ingredientName });

            return result > 0;
        }
    }
}
