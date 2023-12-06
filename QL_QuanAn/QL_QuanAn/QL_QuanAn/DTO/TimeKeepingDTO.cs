using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QL_QuanAn.DTO
{
    public class TimeKeepingDTO
    {
        private string staffid;
        private DateTime workday;
        private string workshifts;

        public string StaffID
        {
            get { return staffid; }
            set { staffid = value; }
        }
        public DateTime WorkDay
        {
            get { return workday; }
            set { workday = value; }
        }
        public string WorkShifts
        {
            get { return workshifts; }
            set { workshifts = value; }
        }
        public TimeKeepingDTO() { }

        public TimeKeepingDTO(string staffid, DateTime workday, string workshifts)
        {
            this.staffid = staffid;
            this.workday = workday;
            this.workshifts = workshifts;
        }

        public TimeKeepingDTO(DataRow row)
        {
            this.StaffID = row["MANHANVIEN"].ToString();
            this.WorkDay = (DateTime)row["NGAYLAM"];
            this.WorkShifts = row["CALAM"].ToString();
        }
    }
}
