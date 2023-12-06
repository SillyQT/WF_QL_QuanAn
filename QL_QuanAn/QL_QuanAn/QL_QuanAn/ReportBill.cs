using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;

namespace QL_QuanAn
{
    public partial class ReportBill : DevExpress.XtraReports.UI.XtraReport
    {
        public string GTText { get; set; }
        public string Total { get; set; }
        public string Exchange { get; set; }
        public string VAT { get; set; }
        public string MoneySentByCustomers { get; set; }
        public double GT { get; set; }

        public ReportBill()
        {
            InitializeComponent();
        }
        private void ReportBill_BeforePrint(object sender, CancelEventArgs e)
        {
            InfomationTable infomationTable = new InfomationTable();

            GT = Double.Parse(GTText);

            lblVAT.Text = VAT;
            lblTienTraLai.Text = Exchange;
            lblTienNhan.Text = MoneySentByCustomers;
            lblTongTienHD.Text = Total;
            lblBangChu.Text = infomationTable.NumberToWords(GT);
        }
    }
}
