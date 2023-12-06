using DevExpress.XtraCharts;
using DevExpress.XtraEditors;
using QL_QuanAn.DAO;
using QL_QuanAn.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QL_QuanAn
{
    public partial class InfomationTable : DevExpress.XtraEditors.XtraForm
    {
        #region Method
        public class Table
        {
            public SimpleButton btnTable;
            public Label lblTable;
            public ToolTip tTipTable;
        }

        DAO.Notification notification = new DAO.Notification();
        public string EmployeeIdSelected { get; set; }
        public List<TabPage> ListTapPage = new List<TabPage>();
        public List<Table> tables = new List<Table>();
        public int index = 0;
        public string check { get; set; }
        public string TotalAmount { get; set; }
        public string NameChoosing { get; set; }
        public double gt = 0;
        public string CurrentlySelectedTableId { get; set; }
        int temp = 0;

        #endregion Method

        public InfomationTable()
        {
            InitializeComponent();
        }

        #region TableInfo
        public void DeleteTab(TabControl tablistTable)
        {
            tablistTable.TabPages.Clear();
        }

        public void LoadTable()
        {
            DeleteTab(tabctrlTable);
            List<AreaDTO> listArea = new List<AreaDTO>();

            listArea = AreaDAO.Instance.GetListArea().OrderBy(row =>row.AreaID).ToList();
            
            foreach (var area in listArea)
            {
                int areaId = area.AreaID;

                TabPage tabPage = new TabPage(areaId.ToString());

                ListTapPage.Add(tabPage);

                tabPage.Width = grctrlDSBan.Width - 10;
                tabPage.Text = area.AreaName.Trim();
                tabPage.BackColor = Color.WhiteSmoke;

                List<TableDTO> listTable = new List<TableDTO>();

                listTable = TableDAO.Instance.GetListTableByArea(area.AreaID);

                foreach (var table in listTable)
                {
                    Table item = new Table();

                    tables.Add(item);
                    tables[index].btnTable = new SimpleButton();
                    tables[index].btnTable.Size = new Size(60, 60);
                    tables[index].btnTable.BackColor = Color.Blue;
                    tables[index].btnTable.Name = table.TableID.ToString();
                    tables[index].btnTable.ImageOptions.Location = ImageLocation.MiddleCenter;
                    tables[index].btnTable.AppearanceHovered.BackColor = Color.Gold;
                    tables[index].btnTable.AppearancePressed.BackColor = Color.Yellow;

                    if (table.Status.Trim() == "Trống")
                    {
                        this.tables[index].btnTable.ImageOptions.Image = global::QL_QuanAn.Properties.Resources.icons8_restaurant_table_nobody_50;
                    }
                    else
                    {
                        if (table.Status.Trim() == "Đã đặt")
                        {
                            this.tables[index].btnTable.ImageOptions.Image = global::QL_QuanAn.Properties.Resources.icons8_restaurant_table_50;
                        }
                        else
                        {
                            this.tables[index].btnTable.ImageOptions.Image = global::QL_QuanAn.Properties.Resources.icons8_restaurant_table_haveorder_50;
                        }
                    }

                    tables[index].lblTable = new Label();
                    tables[index].lblTable.Name = table.TableID.ToString();
                    tables[index].lblTable.Size = new Size(60, 25);
                    tables[index].lblTable.Text = table.Tablename;
                    tables[index].lblTable.BackColor = Color.WhiteSmoke;
                    tables[index].lblTable.Font = new Font(tables[index].lblTable.Font, FontStyle.Bold);
                    tables[index].btnTable.Click += btnTable_Click;

                    tabPage.Controls.Add(tables[index].lblTable);
                    tabPage.Controls.Add(tables[index].btnTable);

                    if (index == temp)
                    {
                        tables[index].btnTable.Location = new Point(20, 15);
                    }
                    else
                    {
                        if (tables[index - 1].btnTable.Location.X + 150 > tabPage.Width)
                        {
                            tables[index].btnTable.Location = new Point(20, tables[index - 1].btnTable.Location.Y + 100);
                        }
                        else
                        {
                            tables[index].btnTable.Location = new Point(tables[index - 1].btnTable.Location.X + 100, tables[index - 1].btnTable.Location.Y);
                        }
                    }

                    tables[index].lblTable.Location = new Point(tables[index].btnTable.Location.X, tables[index].btnTable.Location.Y + 60);
                    tables[index].lblTable.TextAlign = ContentAlignment.MiddleCenter;
                    index++;
                }

                temp += listTable.Count();
                tabPage.Refresh();
                tabctrlTable.Controls.Add(tabPage);
            }
        }

        public void ChangeColorTable(SimpleButton btnTable, List<Table> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (btnTable.Name == list[i].btnTable.Name)
                {
                    list[i].lblTable.BackColor = Color.Yellow;
                    lblHienThi.Text = list[i].lblTable.Text;
                }
                else
                {
                    list[i].lblTable.BackColor = Color.WhiteSmoke;
                }
            }
        }

        public void SeeTableInformation(string tableId, DataGridView dataGridView)
        {
            BillDTO bill = new BillDTO();

            bill = BillDAO.Instance.GetBillByTable(Int32.Parse(tableId));

            List<BillInfoDTO> listBillInfo = new List<BillInfoDTO>();

            listBillInfo = BillInfoDAO.Instance.GetBillInfoByBill(bill.BillID);

            DataTable data = new DataTable();

            data.Columns.Add("Tên món");
            data.Columns.Add("Số lượng");
            data.Columns.Add("Thành tiền");

            foreach (var info in listBillInfo)
            {
                data.Rows.Add(FoodDAO.Instance.GetFoodNameByFoodId(info.FoodID), info.Quanlity, string.Format(new CultureInfo("vi-VN"), "{0:#,##0} VND", FoodDAO.Instance.GetUnitPriceByFoodId(info.FoodID) * info.Quanlity));
            }

            dataGridView.DataSource = data;

            // Đặt các thuộc tính của các cột để canh giữa và in đậm
            foreach (DataGridViewColumn column in dataGridView.Columns)
            {
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                column.ReadOnly = true;
            }

            //dataGridView.Columns[0].Width = 100;
            //dataGridView.Columns[1].Width = 40;
            //dataGridView.Columns[2].Width = 60;

            // Tô màu tiêu đề cột và thay đổi chiều cao
            dataGridView.EnableHeadersVisualStyles = false;
            dataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.Bisque; // Tô màu nền tiêu đề
            dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing; // Cho phép thay đổi kích thước chiều cao
            dataGridView.ColumnHeadersHeight = 50; // Đặt kích thước chiều cao của tiêu đề cột

            // Tăng chiều cao từng dòng dữ liệu
            dataGridView.RowPrePaint += (sender, e) =>
            {
                e.PaintParts &= ~DataGridViewPaintParts.Focus; // Loại bỏ viền bôi đậm
                if (e.RowIndex >= 0)
                {
                    e.Graphics.FillRectangle(Brushes.White, e.RowBounds); // Tô màu nền dòng
                    e.PaintCells(e.ClipBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.Border);
                }
            };
            dataGridView.RowTemplate.Height = 40; // Đặt kích thước chiều cao của từng dòng
        }

        public double CalculateTotalAmount(DataGridView dataGridView)
        {
            double total = 0;

            for (int i = 0; i <= dataGridView.Rows.Count - 1; i++)
            {
                string str = dataGridView.Rows[i].Cells[2].Value.ToString();

                str = str.Replace(" VND", "");

                CultureInfo ci = new CultureInfo("vi-VN");

                total += Double.Parse(str, ci);
            }

            return total;
        }

        public string NumberToWords(double gNum)
        {
            if (gNum == 0)
                return "Không đồng";

            string words = "";
            string separatedMod = "";
            string separatedRemainder = "";
            double Num = Math.Round(gNum, 0);
            string gN = Convert.ToString(Num);
            int m = Convert.ToInt32(gN.Length / 3);
            int mod = gN.Length - m * 3;
            string sign = "[+]";

            if (gNum < 0)
                sign = "[-]";
            sign = "";

            // Separate the largest segment
            if (mod.Equals(1))
                separatedMod = "00" + Convert.ToString(Num.ToString().Trim().Substring(0, 1)).Trim();
            if (mod.Equals(2))
                separatedMod = "0" + Convert.ToString(Num.ToString().Trim().Substring(0, 2)).Trim();
            if (mod.Equals(0))
                separatedMod = "000";

            // Separate the remaining part after the modulus
            if (Num.ToString().Length > 2)
                separatedRemainder = Convert.ToString(Num.ToString().Trim().Substring(mod, Num.ToString().Length - mod)).Trim();

            // Unit of the modulus
            int unit = m + 1;
            if (mod > 0)
                words = Split(separatedMod).ToString().Trim() + " " + Units(unit.ToString().Trim());

            // Split the remaining part into sets of 3
            int i = m;
            int _m = m;
            int j = 1;
            string separated3 = "";
            string separated3_ = "";

            while (i > 0)
            {
                separated3 = separatedRemainder.Trim().Substring(0, 3).Trim();
                separated3_ = separated3;
                words = words.Trim() + " " + Split(separated3.Trim()).Trim();
                m = _m + 1 - j;

                if (!separated3_.Equals("000"))
                    words = words.Trim() + " " + Units(m.ToString().Trim()).Trim();
                separatedRemainder = separatedRemainder.Trim().Substring(3, separatedRemainder.Trim().Length - 3);

                i = i - 1;
                j = j + 1;
            }

            if (words.Trim().Substring(0, 1).Equals("k"))
                words = words.Trim().Substring(10, words.Trim().Length - 10).Trim();
            if (words.Trim().Substring(0, 1).Equals("l"))
                words = words.Trim().Substring(2, words.Trim().Length - 2).Trim();
            if (words.Trim().Length > 0)
                words = sign.Trim() + " " + words.Trim().Substring(0, 1).Trim().ToUpper() + words.Trim().Substring(1, words.Trim().Length - 1).Trim() + " đồng.";
            return words.ToString().Trim();
        }

        public string Split(string segment)
        {
            string result = "";
            if (segment.Equals("000"))
                return "";

            if (segment.Length == 3)
            {
                string hundreds = segment.Trim().Substring(0, 1).ToString().Trim();
                string tens = segment.Trim().Substring(1, 1).ToString().Trim();
                string ones = segment.Trim().Substring(2, 1).ToString().Trim();

                if (hundreds.Equals("0") && tens.Equals("0"))
                    result = " không trăm lẻ " + ReadNumber(ones.ToString().Trim()) + " ";

                if (!hundreds.Equals("0") && tens.Equals("0") && ones.Equals("0"))
                    result = ReadNumber(hundreds.ToString().Trim()).Trim() + " trăm ";

                if (!hundreds.Equals("0") && tens.Equals("0") && !ones.Equals("0"))
                    result = ReadNumber(hundreds.ToString().Trim()).Trim() + " trăm lẻ " + ReadNumber(ones.Trim()).Trim() + " ";

                if (hundreds.Equals("0") && Convert.ToInt32(tens) > 1 && Convert.ToInt32(ones) > 0 && !ones.Equals("5"))
                    result = " không trăm " + ReadNumber(tens.Trim()).Trim() + " mươi " + ReadNumber(ones.Trim()).Trim() + " ";

                if (hundreds.Equals("0") && Convert.ToInt32(tens) > 1 && ones.Equals("0"))
                    result = " không trăm " + ReadNumber(tens.Trim()).Trim() + " mươi ";

                if (hundreds.Equals("0") && Convert.ToInt32(tens) > 1 && ones.Equals("5"))
                    result = " không trăm " + ReadNumber(tens.Trim()).Trim() + " mươi lăm ";

                if (hundreds.Equals("0") && tens.Equals("1") && Convert.ToInt32(ones) > 0 && !ones.Equals("5"))
                    result = " không trăm mười " + ReadNumber(ones.Trim()).Trim() + " ";

                if (hundreds.Equals("0") && tens.Equals("1") && ones.Equals("0"))
                    result = " không trăm mười ";

                if (hundreds.Equals("0") && tens.Equals("1") && ones.Equals("5"))
                    result = " không trăm mười lăm ";
                if (Convert.ToInt32(hundreds) > 0 && Convert.ToInt32(tens) > 1 && Convert.ToInt32(ones) > 0 && !ones.Equals("5"))
                    result = ReadNumber(hundreds.Trim()).Trim() + " trăm " + ReadNumber(tens.Trim()).Trim() + " mươi " + ReadNumber(ones.Trim()).Trim() + " ";

                if (Convert.ToInt32(hundreds) > 0 && Convert.ToInt32(tens) > 1 && ones.Equals("0"))
                    result = ReadNumber(hundreds.Trim()).Trim() + " trăm " + ReadNumber(tens.Trim()).Trim() + " mươi ";

                if (Convert.ToInt32(hundreds) > 0 && Convert.ToInt32(tens) > 1 && ones.Equals("5"))
                    result = ReadNumber(hundreds.Trim()).Trim() + " trăm " + ReadNumber(tens.Trim()).Trim() + " mươi lăm ";

                if (Convert.ToInt32(hundreds) > 0 && tens.Equals("1") && Convert.ToInt32(ones) > 0 && !ones.Equals("5"))
                    result = ReadNumber(hundreds.Trim()).Trim() + " trăm mười " + ReadNumber(ones.Trim()).Trim() + " ";
                if (Convert.ToInt32(hundreds) > 0 && tens.Equals("1") && ones.Equals("0"))
                    result = ReadNumber(hundreds.Trim()).Trim() + " trăm mười ";
                if (Convert.ToInt32(hundreds) > 0 && tens.Equals("1") && ones.Equals("5"))
                    result = ReadNumber(hundreds.Trim()).Trim() + " trăm mười lăm ";
            }
            return result;
        }

        public string ReadNumber(string numericDigit)
        {
            string result = "";
            switch (numericDigit)
            {
                case "0":
                    result = "không";
                    break;
                case "1":
                    result = "một";
                    break;
                case "2":
                    result = "hai";
                    break;
                case "3":
                    result = "ba";
                    break;
                case "4":
                    result = "bốn";
                    break;
                case "5":
                    result = "năm";
                    break;
                case "6":
                    result = "sáu";
                    break;
                case "7":
                    result = "bảy";
                    break;
                case "8":
                    result = "tám";
                    break;
                case "9":
                    result = "chín";
                    break;
            }
            return result;
        }

        public string Units(string number)
        {
            string result = "";

            if (number.Equals("1"))
                result = "";
            if (number.Equals("2"))
                result = "nghìn";
            if (number.Equals("3"))
                result = "triệu";
            if (number.Equals("4"))
                result = "tỷ";
            if (number.Equals("5"))
                result = "nghìn tỷ";
            if (number.Equals("6"))
                result = "triệu tỷ";
            if (number.Equals("7"))
                result = "tỷ tỷ";

            return result;
        }

        public void ReloadTable()
        {
            try
            {
                RemoveAll(tabctrlTable);
                LoadTable();
            }
            catch { }
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

        public void ReloadWhenDone()
        {
            SeeTableInformation(CurrentlySelectedTableId, dtgvTableDetails);

            txbTotal.Text = string.Format(new CultureInfo("vi-VN"), "{0:#,##0} VND", Double.Parse(CalculateTotalAmount(dtgvTableDetails).ToString()));

            TotalAmount = CalculateTotalAmount(dtgvTableDetails).ToString();

            lblBangChu.Text = "Bằng chữ: " + NumberToWords(Double.Parse(CalculateTotalAmount(dtgvTableDetails).ToString()));
            btnInBill.Enabled = false;
        }
        #endregion TableInfo

        #region EV_TableInfo

        private void btnTable_Click(object sender, EventArgs e)
        {
            SimpleButton simpleButton = sender as SimpleButton;

            CurrentlySelectedTableId = simpleButton.Name.Trim();

            SeeTableInformation(simpleButton.Name, dtgvTableDetails);

            ChangeColorTable(simpleButton, tables);

            TotalAmount = CalculateTotalAmount(dtgvTableDetails).ToString();

            txbTotal.Text = CalculateTotalAmount(dtgvTableDetails).ToString();

            Label lb = new Label();

            NameChoosing = lb.Text;

            btnAddFood.Enabled = true;
            btnOrderTable.Enabled = true;

            gt = double.Parse(txbTotal.Text.ToString());
            txbTotal.Text = string.Format(new CultureInfo("vi-VN"), "{0:#,##0} VND", gt);

            if (TableDAO.Instance.GetStatusByTable(Int32.Parse(CurrentlySelectedTableId)) == "Đã đặt" || TableDAO.Instance.GetStatusByTable(Int32.Parse(CurrentlySelectedTableId)) == "Trống")
            {
                lblTTKhachDT.Text = "KHÔNG CÓ THÔNG TIN";
                btnRemove.Enabled = false;

                if (TableDAO.Instance.GetStatusByTable(Int32.Parse(CurrentlySelectedTableId)) == "Đã đặt")
                {
                    btnOrderTable.Enabled = false;
                    btnRemove.Enabled = false;
                    btnInBill.Enabled = true;
                    btnSwitchTable.Enabled = true;
                    btnCombineTable.Enabled = true;
                }
                else
                {
                    if (TableDAO.Instance.GetStatusByTable(Int32.Parse(CurrentlySelectedTableId)) == "Trống")
                    {
                        btnOrderTable.Enabled = true;
                        btnRemove.Enabled = false;
                        btnInBill.Enabled = false;
                        btnSwitchTable.Enabled = false;
                        btnCombineTable.Enabled = false;
                    }
                }
            }
            else
            {
                lblTTKhachDT.Text = TableDAO.Instance.GetStatusByTable(Int32.Parse(CurrentlySelectedTableId));

                btnRemove.Enabled = true;
                btnOrderTable.Enabled = false;
                btnInBill.Enabled = false;
                btnSwitchTable.Enabled = false;
                btnCombineTable.Enabled = false;
            }

            lblBangChu.Text = "Bằng chữ: " + NumberToWords(gt);
        }

        private void InfomationTable_Load(object sender, EventArgs e)
        {
            LoadTable();
            btnAddFood.Enabled = false;
            btnOrderTable.Enabled = false;
            btnCombineTable.Enabled = false;
            btnSwitchTable.Enabled = false;
            btnInBill.Enabled = false;
            btnRemove.Enabled = false;
        }

        private void btnAddFood_Click(object sender, EventArgs e)
        {
            OrderFood orderFood = new OrderFood();

            orderFood.CurrentlySelectedTableId = CurrentlySelectedTableId;
            orderFood.EmployeeId = EmployeeIdSelected;

            orderFood.ShowDialog();
            orderFood.Hide();

            ReloadTable();
            ReloadWhenDone();
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            try
            {
                ReloadTable();
                SeeTableInformation(CurrentlySelectedTableId, dtgvTableDetails);
            }
            catch { }
        }

        private void btnSwitchTable_Click(object sender, EventArgs e)
        {
            SwitchTables switchTables = new SwitchTables();

            switchTables.OldTableId = CurrentlySelectedTableId;
            switchTables.ShowDialog();
            switchTables.Hide();

            ReloadTable();
            ReloadWhenDone();

            lblHienThi.Text = "Chưa chọn bàn";
        }

        private void btnCombineTable_Click_1(object sender, EventArgs e)
        {
            CombineTables combineTables = new CombineTables();

            combineTables.OldTableId = CurrentlySelectedTableId;
            combineTables.ShowDialog();
            combineTables.Hide();

            ReloadTable();
            ReloadWhenDone();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            string status = TableDAO.Instance.GetStatusByTable(Int32.Parse(CurrentlySelectedTableId));

            MessageBoxYesNo messageBoxYesNo = new MessageBoxYesNo();

            messageBoxYesNo.Notify = "Bạn có muốn hủy bàn " + TableDAO.Instance.GetTableByTableId(Int32.Parse(CurrentlySelectedTableId)).Tablename + " không?";
            messageBoxYesNo.ShowDialog();
            messageBoxYesNo.Hide();

            check = messageBoxYesNo.Check;

            if (check == "Có")
            {
                try
                {
                    if (status.Trim() != "Đã đặt" && status.Trim() != "Trống")
                    {
                        TableDAO.Instance.UpdateStatusTable(Int32.Parse(CurrentlySelectedTableId), "Trống");

                        notification.Show("Bạn đã hủy bàn thành công.");

                        ReloadTable();
                    }
                    else
                    {
                        if (status.Trim() == "Đã đặt")
                        {
                            BillDAO.Instance.DeleteBill(BillDAO.Instance.GetBillByTable(Int32.Parse(CurrentlySelectedTableId)).BillID);

                            TableDAO.Instance.UpdateStatusTable(Int32.Parse(CurrentlySelectedTableId), "Trống");

                            notification.Show("Bạn đã hủy bàn thành công.");

                            ReloadTable();
                            ReloadWhenDone();
                        }
                        else
                        {
                            lblTTKhachDT.Text = "Thông tin khác đặt trước: ";
                        }
                    }
                }
                catch { }
            }
        }

        private void btnInBill_Click(object sender, EventArgs e)
        {
            ExportBill exportBill = new ExportBill();

            exportBill.TableName = CurrentlySelectedTableId;
            exportBill.TableName2 = NameChoosing;
            exportBill.Nummerial = txbTotal.Text.Trim();
            exportBill.Text = lblBangChu.Text;
            exportBill.Total = gt.ToString();
            exportBill.BillId = BillDAO.Instance.GetBillByTable(Int32.Parse(CurrentlySelectedTableId)).BillID.ToString();
            exportBill.Money = TotalAmount;
            exportBill.ShowDialog();
            exportBill.Hide();

            ReloadTable();
            ReloadWhenDone();

            lblHienThi.Text = "Chưa chọn bàn";
        }

        private void btnOrderTable_Click_1(object sender, EventArgs e)
        {
            OrderTable orderTable = new OrderTable();

            orderTable.CurrentTableId = CurrentlySelectedTableId;
            orderTable.ShowDialog();
            orderTable.Hide();

            ReloadTable();
            ReloadWhenDone();

            lblTTKhachDT.Text = TableDAO.Instance.GetStatusByTable(Int32.Parse(CurrentlySelectedTableId));

            btnRemove.Enabled = true;
        }
        #endregion EV_TableInfo
    }
}