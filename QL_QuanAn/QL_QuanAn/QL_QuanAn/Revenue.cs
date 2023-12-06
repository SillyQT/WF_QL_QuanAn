using DevExpress.XtraEditors;
using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QL_QuanAn
{
    public partial class Revenue : DevExpress.XtraEditors.XtraForm
    {
        public Revenue()
        {
            InitializeComponent();
        }

        #region Methods
        public void AddMonth(ComboBoxEdit cbbMonth)
        {
            for (int i = 1; i <= 12; i++)
            {
                cbbMonth.Properties.Items.Add(i);
            }
        }

        public void AddYear(ComboBoxEdit cbbYear)
        {
            for (int i = 2020; i <= 2030; i++)
            {
                cbbYear.Properties.Items.Add(i);
            }
        }
        #endregion

        #region Events
        private void Revenue_Load(object sender, EventArgs e)
        {
            AddMonth(cbMonth);
            AddYear(cbYear);

            DateTime day = DateTime.Now;

            cbMonth.Text = (day.Month - 1).ToString();
            cbYear.Text = day.Year.ToString();
        }

        private void btnPrintStatistics_Click(object sender, EventArgs e)
        {
            string st = @"Data Source=MSI-TAOPRO;Initial Catalog=QL_QuanAn;Integrated Security=True";

            SqlConnection sql = new SqlConnection(st);

            sql.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM DOANHTHU WHERE THANG = '" + cbMonth.Text + "' AND NAM = '" + cbYear.Text + "'", sql);

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();

            da.Fill(dt);

            sql.Close();

            ReportRevenue reportRevenue = new ReportRevenue();

            reportRevenue.DataSource = dt;
            reportRevenue.DataMember = "DOANHTHU";
            reportRevenue.ShowPreview();

            this.Hide();
        }
        #endregion
    }
}