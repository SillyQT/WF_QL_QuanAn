using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QL_QuanAn
{
    public partial class MessageBoxYesNo : DevExpress.XtraEditors.XtraForm
    {
        public MessageBoxYesNo()
        {
            InitializeComponent();
            this.ControlBox = false;
        }
        public string Check { get; set; }
        public string Notify { get; set; }

        private void btnYes_Click(object sender, EventArgs e)
        {
            Check = "Có";
            this.Close();
        }

        private void btnNo_Click(object sender, EventArgs e)
        {
            Check = "Không";
            this.Close();
        }

        private void MessageBoxYesNo_Load(object sender, EventArgs e)
        {
            labelcontrolNotify.Text = Notify;
        }
    }
}