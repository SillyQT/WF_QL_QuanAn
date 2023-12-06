using QL_QuanAn.DTO;
using QuanLyQuanCafe.DAO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QL_QuanAn.DAO
{
    public class TimeKeepingDAO
    {
        private static TimeKeepingDAO instance;
        public static TimeKeepingDAO Instance
        {
            get
            {
                if (instance == null)
                    instance = new TimeKeepingDAO();
                return instance;
            }
            private set { instance = value; }
        }
        private TimeKeepingDAO() { }

        public List<TimeKeepingDTO> GetListTimeKeeping()
        {
            List<TimeKeepingDTO> listtimekeeping = new List<TimeKeepingDTO>();

            string query = "USP_GetListTimeKeeping";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                TimeKeepingDTO timekeeping = new TimeKeepingDTO(item);
                listtimekeeping.Add(timekeeping);
            }
            return listtimekeeping;
        }

        public bool InsertTimeKeeping(string staffid, string workshift)
        {
            string query = string.Format("USP_InsertTimeKeeping @staffid , @workshift");
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { staffid, workshift });

            return result > 0;
        }

        public bool DeleteTimeKeeping(string staffid, DateTime workday, string workshift)
        {
            string query = string.Format("USP_DeleteTimeKeeping @staffid , @workday , @workshift");
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { staffid, workday, workshift });

            return result > 0;
        }

        public List<TimeKeepingDTO> SearchTimeKeepingByStaffID(string staffid)
        {
            List<TimeKeepingDTO> list = new List<TimeKeepingDTO>();

            string query = string.Format("USP_SearchTimeKeepingByStaffID @staffid");

            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { staffid });

            foreach (DataRow item in data.Rows)
            {
                TimeKeepingDTO timekeeping = new TimeKeepingDTO(item);
                list.Add(timekeeping);
            }
            return list;
        }

        public List<TimeKeepingDTO> SearchTimeKeepingByWorkShifts(string workshift)
        {
            List<TimeKeepingDTO> list = new List<TimeKeepingDTO>();

            string query = string.Format("USP_SearchTimeKeepingByWorkShifts @workshift");

            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { workshift });

            foreach (DataRow item in data.Rows)
            {
                TimeKeepingDTO timekeeping = new TimeKeepingDTO(item);
                list.Add(timekeeping);
            }
            return list;
        }

        public List<TimeKeepingDTO> SearchTimeKeepingByMonth(int month)
        {
            List<TimeKeepingDTO> list = new List<TimeKeepingDTO>();

            string query = string.Format("USP_SearchTimeKeepingByMonth @month");

            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { month });

            foreach (DataRow item in data.Rows)
            {
                TimeKeepingDTO timekeeping = new TimeKeepingDTO(item);
                list.Add(timekeeping);
            }
            return list;
        }

        public List<TimeKeepingDTO> SearchTimeKeepingByYear(int year)
        {
            List<TimeKeepingDTO> list = new List<TimeKeepingDTO>();

            string query = string.Format("USP_SearchTimeKeepingByYear @year");

            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { year });

            foreach (DataRow item in data.Rows)
            {
                TimeKeepingDTO timekeeping = new TimeKeepingDTO(item);
                list.Add(timekeeping);
            }
            return list;
        }

        public List<TimeKeepingDTO> SearchTimeKeepingByMonthAndYear(int month, int year)
        {
            List<TimeKeepingDTO> list = new List<TimeKeepingDTO>();

            string query = string.Format("USP_SearchTimeKeepingByMonthAndYear @month , @year");

            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { month, year });

            foreach (DataRow item in data.Rows)
            {
                TimeKeepingDTO timekeeping = new TimeKeepingDTO(item);
                list.Add(timekeeping);
            }
            return list;
        }
        public List<TimeKeepingDTO> SearchTimeKeepingByMonthAndShift(int month, string workshift)
        {
            List<TimeKeepingDTO> list = new List<TimeKeepingDTO>();

            string query = string.Format("USP_SearchTimeKeepingByMonthAndShift @month , @shift");

            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { month, workshift });

            foreach (DataRow item in data.Rows)
            {
                TimeKeepingDTO timekeeping = new TimeKeepingDTO(item);
                list.Add(timekeeping);
            }
            return list;
        }
        public List<TimeKeepingDTO> SearchTimeKeepingByYearAndShift(int year, string workshift)
        {
            List<TimeKeepingDTO> list = new List<TimeKeepingDTO>();

            string query = string.Format("USP_SearchTimeKeepingByYearAndShift @year , @shift");

            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { year, workshift });

            foreach (DataRow item in data.Rows)
            {
                TimeKeepingDTO timekeeping = new TimeKeepingDTO(item);
                list.Add(timekeeping);
            }
            return list;
        }
        public List<TimeKeepingDTO> SearchTimeKeepingByMonthYearAndShift(int month, int year, string workshift)
        {
            List<TimeKeepingDTO> list = new List<TimeKeepingDTO>();

            string query = string.Format("USP_SearchTimeKeepingByMonthYearAndShift @month , @year , @shift");

            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { month, year , workshift });

            foreach (DataRow item in data.Rows)
            {
                TimeKeepingDTO timekeeping = new TimeKeepingDTO(item);
                list.Add(timekeeping);
            }
            return list;
        }
    }
}
