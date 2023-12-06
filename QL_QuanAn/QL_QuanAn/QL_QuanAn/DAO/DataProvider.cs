using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using DevExpress.ClipboardSource.SpreadsheetML;
using DevExpress.Utils.Drawing.Helpers;
using static DevExpress.XtraEditors.Mask.MaskSettings;

namespace QuanLyQuanCafe.DAO
{
    public class DataProvider
    {
        private static DataProvider instance;

        public static DataProvider Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DataProvider();
                }
                return DataProvider.instance;
            }
            private set { DataProvider.instance = value; }
        }

        private DataProvider() { }

        // private string connectionSTR = @"Data Source=.\SQLEXPRESS;Initial Catalog=QL_QuanAn;Integrated Security=True";
        private string connectionSTR = @"Data Source=MSI-TAOPRO;Initial Catalog=QL_QuanAn;Integrated Security=True";
        public DataTable ExecuteQuery(string query, object[] parameter = null) // truy vấn dữ liệu 
        {
            DataTable data = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionSTR)) // kết nói client xuống server // using dùng để giải phóng bộ nhớ
            {
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection); // thực thi câu truy vấn

                if (parameter != null)
                {
                    string[] listPara = query.Split(' ');
                    int i = 0;
                    foreach (string item in listPara)
                    {
                        if (item.Contains('@'))
                        {
                            command.Parameters.AddWithValue(item, parameter[i]);
                            i++;
                        }
                    }
                }

                SqlDataAdapter adapter = new SqlDataAdapter(command); // trung gian thực hiện câu truy vấn

                adapter.Fill(data);

                connection.Close();
            }

            return data;
        }

        public int ExecuteNonQuery(string query, object[] parameter = null) //đếm số dòng chèn thành công dữ liệu
        {
            int data = 0;

            using (SqlConnection connection = new SqlConnection(connectionSTR)) // kết nói client xuống server // using dùng để giải phóng bộ nhớ
            {
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection); // thực thi câu truy vấn

                if (parameter != null)
                {
                    string[] listPara = query.Split(' ');
                    int i = 0;
                    foreach (string item in listPara)
                    {
                        if (item.Contains('@'))
                        {
                            command.Parameters.AddWithValue(item, parameter[i]);
                            i++;
                        }
                    }
                }

                data = command.ExecuteNonQuery();

                connection.Close();
            }

            return data;
        }

        public object ExecuteScalar(string query, object[] parameter = null) //đếm số lượng phần tử
        {
            object data = 0;

            using (SqlConnection connection = new SqlConnection(connectionSTR)) // kết nói client xuống server // using dùng để giải phóng bộ nhớ
            {
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection); // thực thi câu truy vấn

                if (parameter != null)
                {
                    string[] listPara = query.Split(' ');
                    int i = 0;
                    foreach (string item in listPara)
                    {
                        if (item.Contains('@'))
                        {
                            command.Parameters.AddWithValue(item, parameter[i]);
                            i++;
                        }
                    }
                }

                data = command.ExecuteScalar();

                connection.Close();
            }

            return data;
        }
    }
}
