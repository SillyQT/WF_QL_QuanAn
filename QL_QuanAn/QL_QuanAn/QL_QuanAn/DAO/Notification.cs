using DevExpress.Utils.Drawing.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QL_QuanAn.DAO
{
    public class Notification
    {
       public void Show(string notify)
        {
            MessageBoxNotify msg = new MessageBoxNotify();
            msg.Notify = notify;
            msg.ShowDialog();
        }       
    }
}
