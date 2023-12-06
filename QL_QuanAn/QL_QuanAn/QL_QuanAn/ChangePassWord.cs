using DevExpress.Office.PInvoke;
using DevExpress.XtraEditors;
using DevExpress.XtraRichEdit.Import.OpenXml;
using QL_QuanAn.DAO;
using QL_QuanAn.DTO;
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
    public partial class ChangePassWord : DevExpress.XtraEditors.XtraForm
    {
        #region Mothod
        Notification notitfy = new Notification(); //Thông báo
        public AccountDTO loginAcc; //Constructor để nhận thông tin tài khoản từ form Home
        public AccountDTO LoginAcc
        {
            get { return loginAcc; }
            set
            {
                loginAcc = value;
            }
        }
        public string Check { get; set; }
        #endregion Method


        public ChangePassWord(AccountDTO acc)
        {
            InitializeComponent();
            this.ControlBox = false;
            this.LoginAcc = acc;
        }

        #region EV_ResetPassWord
        private void btnChangePW_Click(object sender, EventArgs e)
        {
            string userName = loginAcc.UserName;
            string passwordold = txbPassWordOld.Text;
            string passwordnew = txbPassWordNew.Text;
            string prepasswordnew = txbPrePassWorkNew.Text;

            MessageBoxYesNo msgyn = new MessageBoxYesNo();
            msgyn.Notify = "Bạn có muốn đổi mật khẩu không?";
            msgyn.ShowDialog();
            msgyn.Hide();
            Check = msgyn.Check;

            if (Check == "Có")
            {
                try
                {
                    if (loginAcc.PassWord != AccountDAO.Instance.HasPassWord(passwordold))
                    {
                        notitfy.Show("Mật khẩu không đúng!!!");
                    }
                    else
                    {
                        if (passwordnew != prepasswordnew)
                        {
                            notitfy.Show("Mật khẩu mới và nhập lại mật khẩu không giống nhau!!!");
                        }
                        else
                        {
                            AccountDAO.Instance.ChangePassWord(userName, passwordold, prepasswordnew);
                            notitfy.Show("Đổi mật khẩu thành công!!!");
                            txbPassWordOld.Clear();
                            txbPassWordNew.Clear();
                            txbPrePassWorkNew.Clear();
                        }
                    }
                }
                catch { }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            MessageBoxYesNo msgyn = new MessageBoxYesNo();
            msgyn.Notify = "Bạn có muốn thoát không?";
            this.Show();
            msgyn.ShowDialog();
            msgyn.Hide();
            Check = msgyn.Check;
            if (Check == "Có")
            {
                this.Close();
            }
        }
        #endregion EV_ResetPassWord
    }
}