using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QL_QuanAn.DTO
{
    public class FoodDTO
    {
        private int foodid;
        private int foodcategoryid;
        private string foodname;
        private string foodunit;
        private double price;
        private string images = "";

        public int FoodID
        {
            get { return foodid; }
            set { foodid = value; }
        }
        public int FoodcategoryID
        {
            get { return foodcategoryid; }
            set { foodcategoryid = value; }
        }

        public string FoodName
        {
            get { return foodname; }
            set { foodname = value; }
        }
        public string FoodUnit
        {
            get { return foodunit; }
            set { foodunit = value; }
        }
        public double Price
        {
            set { price = value; }
            get { return price; }
        }
        public string Images
        {
            get { return images; }
            set { images = value; }
        }
        public FoodDTO() { }

        public FoodDTO(int foodid, int foodcategoryid, string foodname, string foodunit, double price, string images)
        {
            this.foodid = foodid;
            this.foodcategoryid = foodcategoryid;
            this.foodname = foodname;
            this.foodunit = foodunit;
            this.price = price;
            this.images = images;
        }

        public FoodDTO(DataRow row)
        {
            this.FoodID = (int)row["MAMONAN"];
            this.FoodcategoryID = (int)row["MALOAIMONAN"];
            this.FoodName = row["TENMONAN"].ToString();
            this.FoodUnit = row["DVT"].ToString();
            this.price =Convert.ToDouble(row["DONGIA"]);
            this.Images = row["HINHANH"].ToString();
        }
    }
}
