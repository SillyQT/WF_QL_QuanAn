using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QL_QuanAn.DTO
{
    public class EmployeeDTO
    {
        private string staffid;
        private string staffname;
        private string sex;
        private DateTime? dateofbirth;
        private string address;
        private string phone;
        private DateTime? dayofwork;
        private double basicsalary;

        public string StaffID
        {
            get { return staffid; }
            set { staffid = value; }
        }
        public string StaffName
        {
            get { return staffname; }
            set { staffname = value; }
        }
        public string Sex
        {
            get { return sex; }
            set { sex = value; }
        }
        public DateTime? DateOfBirth
        {
            get { return dateofbirth; }
            set { dateofbirth = value; }
        }
        public string Address
        {
            get { return address; }
            set { address = value; }
        }
        public string Phone
        {
            get { return phone; }
            set { phone = value; }
        }
        public DateTime? DayOfWork
        {
            get { return dayofwork; }
            set { dayofwork = value; }
        }
        public double BasicSalary
        {
            get { return basicsalary; }
            set { basicsalary = value; }
        }
        public EmployeeDTO() { }

        public EmployeeDTO(string staffid, string staffname, string sex, DateTime? dateofbirth, string address, string phone, DateTime? dayofwork, double basicsalary)
        {
            this.staffid = staffid;
            this.staffname = staffname;
            this.sex = sex;
            this.dateofbirth = dateofbirth;
            this.address = address;
            this.phone = phone;
            this.dayofwork = dayofwork;
            this.basicsalary = basicsalary;
        }

        public EmployeeDTO(DataRow row)
        {
            this.StaffID = row["MANHANVIEN"].ToString();
            this.StaffName = row["HOTEN"].ToString();
            this.Sex = row["PHAI"].ToString();
            this.DateOfBirth = (DateTime?)row["NGAYSINH"];
            this.Address = row["DIACHI"].ToString();
            this.Phone = row["SDT"].ToString();
            this.DayOfWork = (DateTime?)row["NGAYVAOLAM"];
            this.BasicSalary = Convert.ToDouble(row["LUONGCOBAN"]);
        }
    }
}
