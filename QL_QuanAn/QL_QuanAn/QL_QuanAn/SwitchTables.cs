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
    public partial class SwitchTables : DevExpress.XtraEditors.XtraForm
    {
        #region Method
        Notification notification = new Notification();

        public string OldTableId { get; set; }
        public string NewTableId { get; set; }

        public class TableTabPage
        {
            public SimpleButton btn;
            public Label lbl;
        }

        public List<TabPage> listTabPage = new List<TabPage>();
        public List<TableTabPage> listTableTabPage = new List<TableTabPage>();

        public int i = 0;
        int temp = 0;

        #endregion Method

        public SwitchTables()
        {
            InitializeComponent();
        }

        #region SwitchTable
        public void LoadData()
        {
            LoadTable();
        }
        public void LoadTable()
        {
            RemoveAll(tabControlSwitchTable);

            List<AreaDTO> listArea = new List<AreaDTO>();

            listArea = AreaDAO.Instance.GetListArea().OrderBy(row => row.AreaID).ToList();

            foreach (var area in listArea)
            {
                int areaId = area.AreaID;

                TabPage tabPage = new TabPage(areaId.ToString().Trim());

                listTabPage.Add(tabPage);

                tabPage.Width = groupControl3.Width - 100;
                tabPage.Text = area.AreaName.Trim();
                tabPage.BackColor = Color.WhiteSmoke;

                List<TableDTO> listTableByArea = new List<TableDTO>();

                listTableByArea = TableDAO.Instance.GetListTableByArea(areaId);

                List<TableDTO> listTable = listTableByArea.Where(row => row.TableID.ToString().Trim() != OldTableId /*&& row.Status == "Trống"*/ && row.Quantity >= numericClientOld.Value && (row.Status == "Trống" || row.Status == "Đã đặt")).ToList();

                foreach (var table in listTable)
                {
                    TableTabPage tableTabPage = new TableTabPage();

                    listTableTabPage.Add(tableTabPage);
                    listTableTabPage[i].btn = new SimpleButton();
                    listTableTabPage[i].btn.Size = new Size(100, 100);
                    listTableTabPage[i].btn.BackColor = Color.Blue;
                    listTableTabPage[i].btn.Name = table.TableID.ToString();
                    listTableTabPage[i].btn.ImageOptions.Location = ImageLocation.MiddleCenter;
                    listTableTabPage[i].btn.AppearanceHovered.BackColor = Color.Gold;
                    listTableTabPage[i].btn.AppearancePressed.BackColor = Color.Yellow;

                    if (table.Status.Trim() == "Trống")
                    {
                        this.listTableTabPage[i].btn.ImageOptions.Image = global::QL_QuanAn.Properties.Resources.icons8_restaurant_table_nobody_96;
                    }
                    else
                    {
                        if (table.Status.Trim() == "Đã đặt")
                        {
                            this.listTableTabPage[i].btn.ImageOptions.Image = global::QL_QuanAn.Properties.Resources.icons8_restaurant_table_96;
                        }
                        else
                        {
                            this.listTableTabPage[i].btn.ImageOptions.Image = global::QL_QuanAn.Properties.Resources.icons8_restaurant_table_haveorder_96_filemoi;
                        }
                    }

                    listTableTabPage[i].lbl = new Label();
                    listTableTabPage[i].lbl.Name = table.TableID.ToString();
                    listTableTabPage[i].lbl.Size = new Size(100, 25);
                    listTableTabPage[i].lbl.Text = table.Tablename;
                    listTableTabPage[i].lbl.BackColor = Color.WhiteSmoke;
                    listTableTabPage[i].lbl.Font = new Font("Tahoma", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

                    listTableTabPage[i].btn.Click += Btn_Click;

                    tabPage.Controls.Add(listTableTabPage[i].lbl);
                    tabPage.Controls.Add(listTableTabPage[i].btn);

                    if (i == temp)
                    {
                        listTableTabPage[i].btn.Location = new Point(20, 25);
                    }
                    else
                    {
                        if (listTableTabPage[i - 1].btn.Location.X + 150 > tabPage.Width)
                        {
                            listTableTabPage[i].btn.Location = new Point(20, listTableTabPage[i - 1].btn.Location.Y + 150);
                        }
                        else
                        {
                            listTableTabPage[i].btn.Location = new Point(listTableTabPage[i - 1].btn.Location.X + 140, listTableTabPage[i - 1].btn.Location.Y);
                        }
                    }

                    listTableTabPage[i].lbl.Location = new Point(listTableTabPage[i].btn.Location.X, listTableTabPage[i].btn.Location.Y + 100);
                    listTableTabPage[i].lbl.TextAlign = ContentAlignment.MiddleCenter;
                    i++;
                }

                temp += listTable.Count();

                tabPage.Refresh();

                tabControlSwitchTable.Controls.Add(tabPage);
            }
        }
        public void RemoveAll(TabControl tabListTable)
        {
            try
            {
                for (int i = AreaDAO.Instance.GetNumberOfArea() - 1; i >= 0; i--)
                {
                    tabListTable.Controls.RemoveAt(i);
                }
            }
            catch { }
        }

        public void GetTable(string CurrentTableId, System.Windows.Forms.TextBox tableId, System.Windows.Forms.TextBox tableName, System.Windows.Forms.TextBox numberSeats, System.Windows.Forms.TextBox areaId, System.Windows.Forms.TextBox areaName, System.Windows.Forms.TextBox status)
        {
            List<AreaDTO> listArea = new List<AreaDTO>();

            listArea = AreaDAO.Instance.GetListArea().OrderBy(row => row.AreaID).ToList();

            foreach (var area in listArea)
            {
                List<TableDTO> listTable = new List<TableDTO>();

                listTable = TableDAO.Instance.GetListTableByArea(area.AreaID);

                foreach (var table in listTable)
                {
                    if (table.TableID.ToString().Trim() == CurrentTableId.Trim())
                    {
                        tableId.Text = table.TableID.ToString().Trim();
                        tableName.Text = table.Tablename.ToString().Trim();
                        numberSeats.Text = table.Quantity.ToString().Trim();
                        areaId.Text = area.AreaID.ToString().Trim();
                        areaName.Text = area.AreaName.ToString().Trim();

                        if (table.Status.Trim() == "Trống")
                        {
                            status.Text = "Không có khách";
                        }
                        else
                        {
                            status.Text = "Đã có khách";
                        }

                        return;
                    }
                }
            }
        }

        public void GetOldTable(string tableId, System.Windows.Forms.TextBox txbTableId, System.Windows.Forms.TextBox txbTableName, System.Windows.Forms.TextBox txbAreaName)
        {
            List<AreaDTO> listArea = new List<AreaDTO>();

            listArea = AreaDAO.Instance.GetListArea();

            foreach (var area in listArea)
            {
                List<TableDTO> listTable = new List<TableDTO>();

                listTable = TableDAO.Instance.GetListTableByArea(area.AreaID);

                foreach (var table in listTable)
                {
                    if (table.TableID.ToString().Trim() == tableId.Trim())
                    {
                        txbTableId.Text = table.TableID.ToString().Trim();
                        txbTableName.Text = table.Tablename.ToString().Trim();
                        txbAreaName.Text = area.AreaName.ToString().Trim();

                        return;
                    }
                }
            }
        }

        public void SwitchTable(string transferTableId, string destinationTableId)
        {
            List<BillDTO> item1 = new List<BillDTO>();

            item1.Add(BillDAO.Instance.GetBillByTable(Int32.Parse(destinationTableId)));

            if (item1.Where(row => row.TableID.ToString().Trim() == destinationTableId).SingleOrDefault() == null)
            {
                List<BillDTO> item2 = new List<BillDTO>();

                item2.Add(BillDAO.Instance.GetBillByTable(Int32.Parse(transferTableId)));

                try
                {
                    if (item2.Where(row => row.TableID.ToString().Trim() == transferTableId).SingleOrDefault() != null)
                    {
                        BillDAO.Instance.UpdateTableIdForBill(Int32.Parse(destinationTableId), item2[0].BillID);
                        TableDAO.Instance.UpdateStatusTable(Int32.Parse(transferTableId), "Trống");
                        TableDAO.Instance.UpdateStatusTable(Int32.Parse(destinationTableId), "Đã đặt");
                    }
                }
                catch { }
            }
            else
            {
                //BillDTO bill1 = new BillDTO();

                //bill1 = BillDAO.Instance.GetBillByTable(Int32.Parse(destinationTableId));

                //List<BillInfoDTO> listDestination = new List<BillInfoDTO>();

                //listDestination = BillInfoDAO.Instance.GetBillInfoByBill(bill1.BillID);

                //BillDTO bill2 = new BillDTO();

                //bill2 = BillDAO.Instance.GetBillByTable(Int32.Parse(transferTableId));

                //List<BillInfoDTO> listTransfer = new List<BillInfoDTO>();

                //listTransfer = BillInfoDAO.Instance.GetBillInfoByBill(bill2.BillID);

                //foreach(var x in listTransfer)
                //{
                //    int check = 0;
                //    int billId = 0;
                //    int foodId = 0;
                //    int count = 0;
                //    int temp = 0;

                //    foreach (var y in listDestination)
                //    {
                //        if (y.Foodid == x.Foodid)
                //        {
                //            check = 1;
                //            temp = Int32.Parse(y.Quanlity.ToString());
                //        }
                //        billId = y.BillID;
                //    }

                //    foodId = x.Foodid;
                //    count = Int32.Parse(x.Quanlity.ToString());

                //    try
                //    {
                //        if (check == 0)
                //        {
                //            BillInfoDAO.Instance.InsertBillInfo(billId, foodId, count);
                //            BillDAO.Instance.DeleteBill(x.BillID);
                //        }
                //        else
                //        {
                //            int sumCount = temp + count;
                //            BillInfoDAO.Instance.UpdateCountInBillInfo(billId, foodId, sumCount);
                //            BillDAO.Instance.DeleteBill(x.BillID);
                //        }
                //    }
                //    catch { }
                //}

                //TableDAO.Instance.UpdateStatusTable(Int32.Parse(transferTableId), "Trống");
                //TableDAO.Instance.UpdateStatusTable(Int32.Parse(destinationTableId), "Đã đặt");

                try
                {
                    BillDTO billTransfer = new BillDTO();

                    billTransfer = BillDAO.Instance.GetBillByTable(Int32.Parse(transferTableId));

                    BillDTO billDestination = new BillDTO();

                    billDestination = BillDAO.Instance.GetBillByTable(Int32.Parse(destinationTableId));

                    BillDAO.Instance.UpdateTableIdForBill(Int32.Parse(destinationTableId), billTransfer.BillID);

                    BillDAO.Instance.UpdateTableIdForBill(Int32.Parse(transferTableId), billDestination.BillID);
                }
                catch { }
            }
        }
        #endregion SwitchTable

        #region EV_SwitchTable
        private void Btn_Click(object sender, EventArgs e)
        {
            SimpleButton btn = sender as SimpleButton;

            NewTableId = btn.Name.Trim();

            GetTable(NewTableId, txbTableIDNew, txbTableNameNew, txbSeatNumberNew, txbAreaIDNew, txbAreaNew, txbStatus);
        }

        private void SwitchTables_Load(object sender, EventArgs e)
        {
            LoadData();

            GetOldTable(OldTableId, txbTableIDOld, txbTableNameOld, txbAreaNameOld);
        }

        private void btnSwichTable_Click(object sender, EventArgs e)
        {
            try
            {
                SwitchTable(txbTableIDOld.Text.Trim(), txbTableIDNew.Text.Trim());

                notification.Show("Đã chuyển bàn " + txbTableNameOld.Text.Trim() + " sang bàn " + txbTableNameNew.Text.Trim() + " thành công.");

                this.Hide();
            }
            catch
            {
                this.Hide();
            }
        }

        private void numericClientOld_ValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }
        #endregion EV_SwitchTable
    }
}