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
    public partial class MessageBoxNotify : DevExpress.XtraEditors.XtraForm
    {
        public string Notify { get; set; }
        
        public MessageBoxNotify()
        {
            InitializeComponent(); 
            this.ControlBox = false;
        }

        #region EV
        private void btnOke_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void MessageBoxNotify_Load(object sender, EventArgs e)
        {
            labelcontrolNotify.Text = Notify;
        }
        #endregion EV
    }
}