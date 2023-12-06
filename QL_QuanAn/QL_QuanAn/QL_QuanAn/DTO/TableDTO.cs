using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QL_QuanAn.DTO
{
    public class TableDTO
    {
        private int tableid;
        private int areaid;
        private string tablename;
        private int quantity;
        private string status;

        public int TableID
        {
            get { return tableid; } 
            set { tableid = value; }
        }
        public int AreaID
        {
            get { return areaid; }
            set { areaid = value; }
        }
        public string Tablename
        {
            get { return tablename; }
            set { tablename = value; }
        }
        public int Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }
        public string Status
        {
            get { return status; }
            set { status = value; }
        }
        public TableDTO() { }

        public TableDTO(int tableid, int areaid, string tablename, int quantity, string status)
        {
            this.tableid = tableid;
            this.areaid = areaid;
            this.tablename = tablename;
            this.quantity = quantity;
            this.status = status;
        }

        public TableDTO(DataRow row)
        {
            this.TableID = (int)row["MABAN"];
            this.AreaID = (int)row["MAKHUVUC"];
            this.Tablename = row["TENBAN"].ToString();
            this.Quantity = (int)row["SOLUONGNGUOI"];
            this.Status = row["TRANGTHAI"].ToString();
        }
    }
}
