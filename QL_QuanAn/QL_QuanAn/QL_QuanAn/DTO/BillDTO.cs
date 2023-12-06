using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QL_QuanAn.DTO
{
    public class BillDTO
    {
        private int billid;
        private int tableid;
        private string staffid;
        private DateTime? datein;
        private DateTime? dateout;
        private int discount;
        private double total;

        public int BillID
        {
            get { return billid; }
            set { billid = value; }
        }
        public int TableID
        {
            get { return tableid; }
            set { tableid = value; }
        }
        public string StaffID
        {
            get { return staffid; }
            set { staffid = value; }
        }
        public DateTime? DateIn
        {
            get { return datein; }
            set { datein = value; }
        }
        public DateTime? DateOut
        {
            get { return dateout; }
            set { dateout = value; }
        }
        public int Discount
        {
            get { return discount; }
            set { discount = value; }
        }
        public double Total
        {
            get { return total; }
            set { total = value; }
        }
        public BillDTO() { }

        public BillDTO(int billid, int tableid, string staffid, DateTime datein, DateTime dateout, int discount, double total)
        {
            this.billid = billid;
            this.tableid = tableid;
            this.staffid = staffid;
            this.datein = datein;
            this.dateout = dateout;
            this.discount = discount;   
            this.total = total;                
        }

        public BillDTO(DataRow row)
        {
            this.BillID = (int)row["MAHOADON"];
            this.TableID = (int)row["MABAN"];
            this.StaffID = row["MANHANVIEN"].ToString();
            this.DateIn = (DateTime?)row["NGAYVAO"];
            
            // T: Sửa thuộc tính DateTime trên thành DateTime?
            // T: và thêm lệnh sau:
            object DateOutObj = row["NGAYRA"];

            if (DateOutObj != DBNull.Value)
            {
                this.DateOut = (DateTime)DateOutObj;
            }
            else
            {
                this.DateOut = null;
            }

            // T: Ép kiểu
            this.discount = Int32.Parse(row["GIAMGIA"].ToString());

            // T: Ép kiểu sang double bằng Parse
            this.Total = double.Parse(row["THANHTIEN"].ToString());
        }
    }
}
