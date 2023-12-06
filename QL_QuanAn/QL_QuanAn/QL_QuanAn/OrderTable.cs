using DevExpress.XtraEditors;
using QL_QuanAn.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace QL_QuanAn
{
    public partial class OrderTable : DevExpress.XtraEditors.XtraForm
    {
        #region Method
        Notification notification = new Notification();

        public string CurrentTableId { get; set; }

        private bool isProgrammaticChange = false;

        public string info { get; set; }
        #endregion Method
        public OrderTable()
        {
            InitializeComponent();
        }

        #region EV_OrtherTable

        #endregion EV_OrtherTable

        private void OrderTable_Load(object sender, EventArgs e)
        {
            txbHours.ForeColor = Color.Black;
            txbPhone.ForeColor = Color.Black;
            txbName.ForeColor = Color.Black;
        }

        private void btnOrther_Click(object sender, EventArgs e)
        {
            try
            {
                info = "Tên: " + txbName.Text + ", SĐT: " + txbPhone.Text + ", Thời gian: " + txbHours.Text;

                TableDAO.Instance.UpdateStatusTable(Int32.Parse(CurrentTableId), info);

                notification.Show("Đặt thành công: " + info);

                this.Hide();
            }
            catch { }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txbPhone_TextChanged(object sender, EventArgs e)
        {
            if (!isProgrammaticChange)
            {
                isProgrammaticChange = true;

                string phoneNumber = txbPhone.Text;

                phoneNumber = Regex.Replace(phoneNumber, "[^0-9]", "");

                if (phoneNumber.Length > 3)
                {
                    phoneNumber = phoneNumber.Insert(3, "-");
                }

                if (phoneNumber.Length > 7)
                {
                    phoneNumber = phoneNumber.Insert(7, "-");
                }

                txbPhone.Text = phoneNumber;

                txbPhone.SelectionStart = txbPhone.Text.Length;

                isProgrammaticChange = false;
            }
        }
    }
}