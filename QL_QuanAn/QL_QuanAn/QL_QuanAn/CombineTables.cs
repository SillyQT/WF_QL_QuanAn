using DevExpress.Utils.MVVM.Services;
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
    public partial class CombineTables : DevExpress.XtraEditors.XtraForm
    {
        #region Method
        DAO.Notification notification = new DAO.Notification();
        public string OldTableId { get; set; }
        public string CombineTableId { get; set; }

        public List<TabPage> listTabPage = new List<TabPage>();

        public class Table
        {
            public SimpleButton btn;
            public Label lbl;
        }

        public List<Table> tables = new List<Table>();

        public int i = 0;
        int temp = 0;
        #endregion Method
        
        public CombineTables()
        {
            InitializeComponent();
        }

        #region CombineTables
        public void LoadTable()
        {
            RemoveAll(tabControlCombineTable);

            List<AreaDTO> listArea = new List<AreaDTO>();

            listArea = AreaDAO.Instance.GetListArea().OrderBy(row => row.AreaID).ToList();

            foreach (var area in listArea)
            {
                string areaId = area.AreaID.ToString();

                TabPage tabPage = new TabPage(areaId);

                listTabPage.Add(tabPage);

                tabPage.Width = groupControl3.Width - 100;
                tabPage.Text = area.AreaName.Trim();
                tabPage.BackColor = Color.WhiteSmoke;

                List<TableDTO> listTableByArea = new List<TableDTO>();

                listTableByArea = TableDAO.Instance.GetListTableByArea(area.AreaID);

                List<TableDTO> listTable = listTableByArea.Where(row => row.TableID.ToString().Trim() != OldTableId && row.Status == "Đã đặt").ToList();

                foreach (var item in listTable)
                {
                    Table table = new Table();

                    tables.Add(table);

                    tables[i].btn = new SimpleButton();
                    tables[i].btn.Size = new Size(100, 100);
                    tables[i].btn.BackColor = Color.Blue;
                    tables[i].btn.Name = item.TableID.ToString().Trim();
                    tables[i].btn.ImageOptions.Location = ImageLocation.MiddleCenter;
                    tables[i].btn.AppearanceHovered.BackColor = Color.Gold;
                    tables[i].btn.AppearancePressed.BackColor = Color.Yellow;
                    this.tables[i].btn.ImageOptions.Image = global::QL_QuanAn.Properties.Resources.icons8_restaurant_table_96;

                    tables[i].lbl = new Label();
                    tables[i].lbl.Name = item.AreaID.ToString().Trim();
                    tables[i].lbl.Size = new Size(100, 25);
                    tables[i].lbl.Text = item.Tablename.ToString().Trim();
                    tables[i].lbl.BackColor = Color.WhiteSmoke;
                    tables[i].lbl.Font = new Font(tables[i].lbl.Font, FontStyle.Bold);

                    tables[i].btn.Click += Btn_Click;

                    tabPage.Controls.Add(tables[i].lbl);
                    tabPage.Controls.Add(tables[i].btn);

                    if (temp == i)
                    {
                        tables[i].btn.Location = new Point(20, 25);
                    }
                    else
                    {
                        if (tables[i - 1].btn.Location.X + 150 > tabPage.Width)
                        {
                            tables[i].btn.Location = new Point(30, tables[i - 1].btn.Location.Y + 150);
                        }
                        else
                        {
                            tables[i].btn.Location = new Point(tables[i - 1].btn.Location.X + 140, tables[i - 1].btn.Location.Y);
                        }
                    }

                    tables[i].lbl.Location = new Point(tables[i].btn.Location.X, tables[i].btn.Location.Y + 100);
                    tables[i].lbl.TextAlign = ContentAlignment.MiddleCenter;

                    i++;
                }

                temp += tables.Count;

                tabPage.Refresh();

                tabControlCombineTable.Controls.Add(tabPage);
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

        public void ChangeColorTable(SimpleButton btnTable, List<Table> list)
        {
            for (int i = 0; i <= list.Count - 1; i++)
            {
                if (btnTable.Name == list[i].btn.Name)
                {
                    list[i].lbl.BackColor = Color.Yellow;
                }
                else
                {
                    list[i].lbl.BackColor = Color.WhiteSmoke;
                }
            }
        }

        public void GetOldTable(string currentTableId, System.Windows.Forms.TextBox txbTableId, System.Windows.Forms.TextBox txbTableName, System.Windows.Forms.TextBox txbAreaName)
        {
            List<AreaDTO> listArea = new List<AreaDTO>();

            listArea = AreaDAO.Instance.GetListArea().OrderBy(row => row.AreaID).ToList();

            foreach (var area in listArea)
            {
                List<TableDTO> listTable = new List<TableDTO>();

                listTable = TableDAO.Instance.GetListTableByArea(area.AreaID);

                foreach (var table in listTable)
                {
                    if (table.TableID.ToString().Trim() == currentTableId.Trim())
                    {
                        txbTableId.Text = table.TableID.ToString().Trim();
                        txbTableName.Text = table.Tablename.Trim();
                        txbAreaName.Text = area.AreaName.Trim();

                        return;
                    }
                }
            }
        }

        public void HideTable(string tableId, List<Table> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (tableId.Trim() == list[i].btn.Name.Trim())
                {
                    list[i].btn.Visible = false;
                    list[i].lbl.Visible = false;
                }
            }
        }

        public void ShowTable(string tableId, List<Table> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (tableId.Trim() == list[i].btn.Name.Trim())
                {
                    list[i].btn.Visible = true;
                    list[i].lbl.Visible = true;
                }
            }
        }

        public void TestList()
        {
            if (listBoxControlCombineTable.Items.Count != 0)
            {
                btnSave.Enabled = true;
            }
            else
            {
                btnSave.Enabled = false;
            }
        }

        public void CombineTable(string transferTableId, string destinationTableId)
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
                BillDTO bill1 = new BillDTO();

                bill1 = BillDAO.Instance.GetBillByTable(Int32.Parse(destinationTableId));

                List<BillInfoDTO> listDestination = new List<BillInfoDTO>();

                listDestination = BillInfoDAO.Instance.GetBillInfoByBill(bill1.BillID);

                BillDTO bill2 = new BillDTO();

                bill2 = BillDAO.Instance.GetBillByTable(Int32.Parse(transferTableId));

                List<BillInfoDTO> listTransfer = new List<BillInfoDTO>();

                listTransfer = BillInfoDAO.Instance.GetBillInfoByBill(bill2.BillID);

                foreach (var x in listTransfer)
                {
                    int check = 0;
                    int billId = 0;
                    int foodId = 0;
                    int count = 0;
                    int temp = 0;

                    foreach (var y in listDestination)
                    {
                        if (y.FoodID == x.FoodID)
                        {
                            check = 1;
                            temp = Int32.Parse(y.Quanlity.ToString());
                        }
                        billId = y.BillID;
                    }

                    foodId = x.FoodID;
                    count = Int32.Parse(x.Quanlity.ToString());

                    try
                    {
                        if (check == 0)
                        {
                            BillInfoDAO.Instance.InsertBillInfo(billId, foodId, count);
                            BillDAO.Instance.DeleteBill(x.BillID);
                        }
                        else
                        {
                            int sumCount = temp + count;
                            BillInfoDAO.Instance.UpdateCountInBillInfo(billId, foodId, sumCount);
                            BillDAO.Instance.DeleteBill(x.BillID);
                        }
                    }
                    catch { }
                }

                TableDAO.Instance.UpdateStatusTable(Int32.Parse(transferTableId), "Trống");
                TableDAO.Instance.UpdateStatusTable(Int32.Parse(destinationTableId), "Đã đặt");

                //try
                //{
                //    BillDTO billTransfer = new BillDTO();

                //    billTransfer = BillDAO.Instance.GetBillByTable(Int32.Parse(transferTableId));

                //    BillDTO billDestination = new BillDTO();

                //    billDestination = BillDAO.Instance.GetBillByTable(Int32.Parse(destinationTableId));

                //    BillDAO.Instance.UpdateTableIdForBill(Int32.Parse(destinationTableId), billTransfer.BillID);

                //    BillDAO.Instance.UpdateTableIdForBill(Int32.Parse(transferTableId), billDestination.BillID);
                //}
                //catch { }
            }
        }
        #endregion CombineTable

        private void Btn_Click(object sender, EventArgs e)
        {
            try
            {
                SimpleButton simpleButton = sender as SimpleButton;

                ChangeColorTable(simpleButton, tables);

                CombineTableId = simpleButton.Name.Trim();

                btnRemoveCombine.Enabled = false;
            }
            catch { }
        }

        private void CombineTables_Load_1(object sender, EventArgs e)
        {
            LoadTable();
            GetOldTable(OldTableId, txbTableID, txbTableName, txbAreaName);
        }

        private void btnRemoveCombine_Click(object sender, EventArgs e)
        {
            try
            {
                string str = listBoxControlCombineTable.SelectedItem.ToString();

                int tableId = TableDAO.Instance.GetTableIdByTableName(str);

                listBoxControlCombineTable.Items.RemoveAt(listBoxControlCombineTable.SelectedIndices[0]);

                ShowTable(tableId.ToString().Trim(), tables);

                btnAddCombine.Enabled = true;

                TestList();
            }
            catch { }
        }

        private void btnAddCombine_Click(object sender, EventArgs e)
        {
            try
            {
                TableDTO table = new TableDTO();

                table = TableDAO.Instance.GetTableByTableId(Int32.Parse(CombineTableId));

                listBoxControlCombineTable.Items.Add(table.Tablename);

                HideTable(CombineTableId, tables);

                btnRemoveCombine.Enabled = true;

                TestList();
            }
            catch { }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string str = "";
            try
            {
                foreach (var i in listBoxControlCombineTable.Items)
                {
                    str += i.ToString().Trim();

                    int tableId = TableDAO.Instance.GetTableIdByTableName(i.ToString().Trim());

                    CombineTable(tableId.ToString().Trim(), txbTableID.Text.ToString());
                }

                TableDTO table = new TableDTO();

                table = TableDAO.Instance.GetTableByTableId(Int32.Parse(txbTableID.Text.Trim()));

                notification.Show("Gộp thành công bàn " + str + " sang bàn " + table.Tablename);

                this.Close();
            }
            catch { }
        }

        private void listBoxControlCombineTable_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnAddCombine.Enabled = false;
        }

        private void listBoxControlCombineTable_Click(object sender, EventArgs e)
        {
            btnRemoveCombine.Enabled = true;
        }
    }
}