using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QL_QuanAn.DTO
{
    public class AreaDTO
    {
        private int areaid;
        private string areaname;

        public int AreaID
        {
            get { return areaid; }
            set { areaid = value; }
        }
        public string AreaName
        {
            get { return areaname; }
            set { areaname = value; }
        }
        public AreaDTO() { }

        public AreaDTO(int areaid, string areaname)
        {
            this.areaid = areaid;
            this.areaname = areaname;
        }

        public AreaDTO(DataRow row)
        {
            this.AreaID = (int)row["MAKHUVUC"];
            this.AreaName = row["TENKHUVUC"].ToString();
        }
    }
}
