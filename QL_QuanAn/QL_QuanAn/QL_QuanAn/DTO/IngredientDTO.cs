using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QL_QuanAn.DTO
{
    public class IngredientDTO
    {
        private int ingredientid;
        private string ingredientname;

        public int IngredientID
        {
            get { return ingredientid; }
            set { ingredientid = value; }
        }
        public string IngredientName
        {
            get { return ingredientname; }
            set { ingredientname = value; }
        }
        public IngredientDTO() { }

        public IngredientDTO(int ingredientid, string ingredientname)
        {
            this.ingredientid = ingredientid;
            this.ingredientname = ingredientname;
        }

        public IngredientDTO(DataRow row)
        {
            this.IngredientID = (int)row["MANGUYENLIEU"];
            this.IngredientName = row["TENNGUYENLIEU"].ToString();
        }
    }
}
