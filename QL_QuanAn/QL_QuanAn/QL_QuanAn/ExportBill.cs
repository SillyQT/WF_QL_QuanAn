using DevExpress.XtraEditors;
using DevExpress.XtraReports.UI;
using QL_QuanAn.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QL_QuanAn
{
    public partial class ExportBill : DevExpress.XtraEditors.XtraForm
    {
        public string TableName { get; set; }
        public string TableName2 { get; set; }
        public string Nummerial { get; set; }
        public string Text { get; set; }
        public string Total { get; set; }
        public string BillId { get; set; }
        public string Money { get; set; }

        public ExportBill()
        {
            InitializeComponent();
        }

        #region ExportBill
        public int TotalBill()
        {
            try
            {
                int t = Int32.Parse(Money) + VAT(Money);

                return t;
            }
            catch { }

            return 0;
        }

        public int VAT(string total)
        {
            try
            {
                int t = Int32.Parse(Money) * 10 / 100;

                return t;
            }
            catch { }
            return 0;
        }

        public int Exchange(string moneySentByCustomers)
        {
            try
            {
                int t = Int32.Parse(moneySentByCustomers) - TotalBill();

                return t;
            }
            catch { }

            return 0;
        }
        #endregion ExportBill

        #region EV_ExportBill
        private void ExportBill_Load(object sender, EventArgs e)
        {
            btnExportBill.Enabled = false;
            lblBan.Text = "Bàn: " + TableDAO.Instance.GetTableByTableId(Int32.Parse(TableName)).Tablename;
            lblTien.Text = "Tiền: " + Nummerial;
            lblTongTien.Text = "Tổng tiền: " + string.Format(new CultureInfo("vi-VN"), "{0:#,##0} VND", TotalBill());
            lblVAT.Text = "VAT: " + string.Format(new CultureInfo("vi-VN"), "{0:#,##0} VND", VAT(Money));
            txbMoney.Focus();
            if (Exchange(txbMoney.Text) > 0)
            {
                btnExportBill.Enabled = true;
            }
        }
        private void txbMoney_EditValueChanged(object sender, EventArgs e)
        {
            this.txbMoney.ForeColor = Color.Black;

            if (txbMoney.Text == "" || Exchange(txbMoney.Text) < 0)
            {
                btnExportBill.Enabled = false;
            }
            else
            {
                btnExportBill.Enabled = true;
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnExportBill_Click(object sender, EventArgs e)
        {
            string st = @"Data Source=MSI-TAOPRO;Initial Catalog=QL_QuanAn;Integrated Security=True";

            SqlConnection sql = new SqlConnection(st);

            sql.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM V_HOADON WHERE MAHOADON = " + BillId, sql);

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();

            da.Fill(dt);

            sql.Close();

            ReportBill reportBill = new ReportBill();

            reportBill.DataSource = dt;
            reportBill.DataMember = "V_HOADON";
            reportBill.GTText = TotalBill().ToString();
            reportBill.Total = string.Format(new CultureInfo("vi-VN"), "{0:#,##0} VND", TotalBill());
            reportBill.Exchange = string.Format(new CultureInfo("vi-VN"), "{0:#,##0} VND", Exchange(txbMoney.Text));
            reportBill.VAT = string.Format(new CultureInfo("vi-VN"), "{0:#,##0} VND", VAT(Money));
            reportBill.MoneySentByCustomers = string.Format(new CultureInfo("vi-VN"), "{0:#,##0} VND", Int32.Parse(txbMoney.Text));
            reportBill.ShowPreview();

            TableDAO.Instance.UpdateStatusTable(Int32.Parse(TableName), "Trống");
            BillDAO.Instance.UpdateTotalForBill(Int32.Parse(BillId), TotalBill());
            this.Hide();
        }
        #endregion EV_ExportBill
    }
}