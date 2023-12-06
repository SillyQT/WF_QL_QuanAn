using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QL_QuanAn.DTO
{
    public class FoodRecipesDTO
    {
        private int foodrecipesid;
        private int foodid;
        private int ingredientid;
        private string foodcontent;

        public int FoodRecipesID
        {
            get { return foodrecipesid; }
            set { foodrecipesid = value; }
        }
        public int FoodID
        {
            get { return foodid; }
            set { foodid = value; }
        }
        public int IngredientID
        {
            get { return ingredientid; }
            set { ingredientid = value;}
        }
        public string FoodContent
        {
            get { return foodcontent; }
            set { foodcontent = value; }
        }
        public FoodRecipesDTO() { }

        public FoodRecipesDTO(int foodrecipesid, int foodid, int ingredientid, string foodcontent)
        {
            this.foodrecipesid = foodrecipesid;
            this.foodid = foodid;
            this.ingredientid = ingredientid;
            this.foodcontent = foodcontent;
        }

        public FoodRecipesDTO(DataRow row)
        {
            this.FoodRecipesID = (int)row["MACONGTHUC"];
            this.FoodID = (int)row["MAMONAN"];
            this.IngredientID = (int)row["MANGUYENLIEU"];
            this.FoodContent = row["HAMLUONG"].ToString();
        }
    }
}
