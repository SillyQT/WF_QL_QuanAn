using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QL_QuanAn.DTO
{
    public class BillInfoDTO
    {
        private int billinfoid;
        private int foodid;
        private int billid;
        private int quanlity;

        public int BillInfoID
        {
            get { return billinfoid; }
            set { billinfoid = value; }
        }
        public int FoodID
        {
            get { return foodid; }
            set { foodid = value; }
        }
        public int BillID
        {
            get { return billid; }
            set { billid = value; }
        }
        public int Quanlity
        {
            get { return quanlity; }
            set { quanlity = value; }
        }
        public BillInfoDTO() { }

        public BillInfoDTO(int billinfoid, int foodid, int billid, int quanlity)
        {
            this.billinfoid = billinfoid;
            this.foodid = foodid;
            this.billid = billid;
            this.quanlity = quanlity;
        }

        public BillInfoDTO(DataRow row)
        {
            this.BillInfoID = (int)row["MACHITIETHD"];
            this.FoodID = (int)row["MAMONAN"];
            this.BillID = (int)row["MAHOADON"];
            this.Quanlity = (int)row["SOLUONG"];
        }
    }
}
