using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QL_QuanAn.DTO
{
    public class FoodCategoryDTO
    {
        private int foodcategoryid;
        private string foodcategoryname;

        public int FoodCategoryID
        {
            get { return foodcategoryid; }
            set { foodcategoryid = value; }
        }
        public string FoodCategoryName
        {
            get { return foodcategoryname; }
            set { foodcategoryname = value; }
        }
        public FoodCategoryDTO() { }

        public FoodCategoryDTO(int foodcategoryid, string foodcategoryname)
        {
            this.foodcategoryid = foodcategoryid;
            this.foodcategoryname = foodcategoryname;
        }

        public FoodCategoryDTO(DataRow row)
        {
            this.FoodCategoryID = (int)row["MALOAIMONAN"];
            this.FoodCategoryName = row["TENLOAIMONAN"].ToString();
        }
    }
}
