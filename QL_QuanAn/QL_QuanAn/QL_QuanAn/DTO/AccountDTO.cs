using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QL_QuanAn.DTO
{
    public class AccountDTO
    {
        private string username;
        private string staffid;
        private string displayname;
        private string password;
        private int permissionGroupId;
        private string status;

        public string UserName 
        { 
            get { return username; }
            set { username = value; } 
        }
        public string StaffID
        {
            get { return staffid; }
            set { staffid = value; }
        }
        public string DisplayName
        {
            get { return displayname; }
            set { displayname = value; }
        }
        public string PassWord
        {
            get { return password; }
            set { password = value; }
        }
        public int PermissionGroupId
        {
            get { return permissionGroupId; }
            set { permissionGroupId = value; }
        }
        public string Status
        {
            get { return status; }
            set { status = value; }
        }
        public AccountDTO() { }

        public AccountDTO(string username, string staffid, string displayname, string password, int permissionGroupId, string status)
        {
            this.username = username;
            this.staffid = staffid;
            this.displayname = displayname;
            this.password = password;
            this.permissionGroupId = permissionGroupId;
            this.status = status;
        }

        public AccountDTO(DataRow row)
        {
            this.UserName = row["TENDANGNHAP"].ToString();
            this.StaffID = row["MANHANVIEN"].ToString();
            this.DisplayName = row["TENHIENTHI"].ToString();
            this.PassWord = row["MATKHAU"].ToString();
            this.PermissionGroupId = Int32.Parse(row["MaNhom"].ToString());
            this.Status = row["TRANGTHAI"].ToString();
        }
    }
}
