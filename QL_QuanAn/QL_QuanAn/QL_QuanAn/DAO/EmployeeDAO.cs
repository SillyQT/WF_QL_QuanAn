using DevExpress.XtraEditors;
using QL_QuanAn.DTO;
using QuanLyQuanCafe.DAO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace QL_QuanAn.DAO
{
    public class EmployeeDAO
    {
        private static EmployeeDAO instance;
        public static EmployeeDAO Instance
        {
            get
            {
                if (instance == null)
                    instance = new EmployeeDAO();
                return instance;
            }
            private set { instance = value; }
        }
        private EmployeeDAO() { }

        public List<EmployeeDTO> GetListEmployee()
        {
            List<EmployeeDTO> listemloyee = new List<EmployeeDTO>();

            string query = "USP_GetListEmployee";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                EmployeeDTO employee = new EmployeeDTO(item);
                listemloyee.Add(employee);
            }
            return listemloyee;
        }

        public EmployeeDTO GetEmployeeByStaffID(string staffid)
        {
            EmployeeDTO employee = null;

            string query = string.Format("USP_GetEmployeeByStaffID @staffid");
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { staffid });

            foreach (DataRow item in data.Rows)
            {
                employee = new EmployeeDTO(item);
                return employee;
            }
            return employee;
        }

        public bool InsertEmployee(string staffid, string name, string sex, DateTime dateofbirth, string address, string phone, DateTime dateofwork, double basicsalary)
        {
            string query = string.Format("USP_InsertEmployee @staffid , @name , @sex , @dateofbirth , @address , @phone , @dateofwork , @basicsalary");
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { staffid, name, sex, dateofbirth, address, phone, dateofwork, basicsalary });

            return result > 0;
        }

        public bool DeleteEmployee(string staffid)
        {
            string query = string.Format("USP_DeleteEmployee @staffid");
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { staffid });

            return result > 0;
        }

        public bool UpdateEmployee(string staffid, string name, string sex, DateTime dateofbirth, string address, string phone, DateTime dateofwork, double basicsalary)
        {
            string query = string.Format("USP_UpdateEmployee @staffid , @name , @sex , @dateofbirth , @address , @phone , @dateofwork , @basicsalary");
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { staffid, name, sex, dateofbirth, address, phone, dateofwork, basicsalary });

            return result > 0;
        }

        public List<EmployeeDTO> SearchEmployeeByName(string name)
        {
            List<EmployeeDTO > list = new List<EmployeeDTO>();

            string query = string.Format("USP_SearchEmployeeByName @name");

            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] {name});

            foreach (DataRow item in data.Rows)
            {
                EmployeeDTO emp = new EmployeeDTO(item);
                list.Add(emp);
            }    
            return list;
        }

        public List<EmployeeDTO> SearchEmployeeBySalary(double basicsalary)
        {
            List<EmployeeDTO> list = new List<EmployeeDTO>();

            string query = string.Format("USP_SearchEmployeeBySalary @basicsalary");

            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { basicsalary });

            foreach(DataRow item in data.Rows)
            {
                EmployeeDTO emp = new EmployeeDTO(item);
                list.Add(emp);
            }    
            return list;
        }

        public List<EmployeeDTO> SearchEmployeeBySex(string sex)
        {
            List<EmployeeDTO> list = new List<EmployeeDTO>();

            string query = string.Format("USP_SearchEmployeeBySex @sex");

            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { sex });

            foreach (DataRow item in data.Rows)
            {
                EmployeeDTO emp = new EmployeeDTO(item);
                list.Add(emp);
            }
            return list;
        }

        public List<EmployeeDTO> SearchEmployeeBySalaryAndSex(double basicsalary, string sex)
        {
            List<EmployeeDTO> list = new List<EmployeeDTO>();

            string query = string.Format("USP_SearchEmployeeBySalaryAndSex @basicsalary , @sex");

            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] {basicsalary, sex });

            foreach (DataRow item in data.Rows)
            {
                EmployeeDTO emp = new EmployeeDTO(item);
                list.Add(emp);
            }
            return list;
        }

        public void TakeTheSalary(LabelControl lbWorkShifts, LabelControl lbSalary, string staffid, double basicsalasy)
        {
            List<EmployeeDTO> list = new List<EmployeeDTO>();
            DateTime day = DateTime.Now;
            double salary;

            string query = string.Format("USP_GetEmployeeByStaffIDMonthYear @staffid , @month , @year");
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { staffid, day.Month, day.Year });

            foreach (DataRow item in data.Rows)
            {
                EmployeeDTO emp = new EmployeeDTO(item);
                list.Add(emp);
            }

            lbWorkShifts.Text = "Tổng ca tháng " + day.Month + ": " + list.Count().ToString() + " ca";
            salary = list.Count() * basicsalasy;
            lbSalary.Text = "Tổng lương: " + string.Format(new CultureInfo("vi-VN"), "{0:#,##0} VND", salary);
        }
    }
}
