using DevExpress.XtraEditors;
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
    public partial class Login : DevExpress.XtraEditors.XtraForm
    {
        #region Method
        private bool userConfirmedExit = false;
        public string Check { get; set; }

        Notification notify = new Notification();
        #endregion Method

        public Login()
        {
            InitializeComponent();
            this.ControlBox = false;
        }

        #region Event
        private void btnLogin_Click(object sender, EventArgs e)
        {
            string userName = txbUserName.Text;
            string passWord = txbPassWord.Text;

            bool loginResult = AccountDAO.Instance.Login(userName, passWord);

            if (loginResult)
            {
                AccountDTO login = AccountDAO.Instance.GetAccountByUserName(userName);
                string accountStatus = login.Status;

                if (accountStatus == "Hoạt động")
                {
                    Home home = new Home(login);
                    notify.Show("Đăng nhập thành công!!!");
                    this.Hide();
                    home.ShowDialog();
                }
                else
                {
                    notify.Show("Tài khoản đã bị khóa!!!");
                }
            }
            else
            {
                notify.Show("Sai tên tài khoản hoặc mật khẩu!!!");
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            MessageBoxYesNo msgyn = new MessageBoxYesNo();
            msgyn.Notify = "Bạn có muốn thoát chương trình không?";
            this.Show();
            msgyn.ShowDialog();
            msgyn.Hide();
            Check = msgyn.Check;
            if (Check == "Không")
            {
                userConfirmedExit = false;
            }
            else
            {
                userConfirmedExit = true;
                Application.Exit();
            }
        }

        private void Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!userConfirmedExit)
            {
                e.Cancel = true;
            }
        }
        #endregion Event
    }
}