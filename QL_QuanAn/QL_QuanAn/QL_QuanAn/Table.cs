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
using static DevExpress.Utils.HashCodeHelper;

namespace QL_QuanAn
{
    public partial class Table : DevExpress.XtraEditors.XtraForm
    {
        #region Method
        public class TableManagement
        {
            public SimpleButton btnTable;
            public Label lblTable;
            public Label blTableid;
            public Label lblQuanlity;
            public ToolTip tTipTable;
        }

        DAO.Notification notification = new DAO.Notification();
        public string EmployeeIdSelected { get; set; }
        public List<TabPage> ListTapPage = new List<TabPage>();
        public List<TableManagement> tables = new List<TableManagement>();
        public int index = 0;
        int temp = 0;
        Dictionary<int, string> AreaMap = new Dictionary<int, string>();
        private bool Add = false;
        public string Check { get; set; }
        #endregion Method

        public Table()
        {
            InitializeComponent();
            Load();
        }

        private new void Load()
        {
            LoadTable();

            txbTableID.Enabled = false;
            txbTableName.Enabled = false;
            txbTableQuality.Enabled = false;
            cbTableArea.Enabled = false;
            cbTableStatus.Enabled = false;

            btnSave.Enabled = false;
            btnRemove.Enabled = false;

            LoadAreaNameIntoCombobox(cbTableArea);
            LoadStatusTableIntoComboBox(cbTableStatus);
        }

        public void DeleteTab(TabControl tablistTable)
        {
            tablistTable.TabPages.Clear();
        }

        public void LoadTable()
        {
            DeleteTab(tabControlTable);

            List<AreaDTO> listArea = new List<AreaDTO>();

            listArea = AreaDAO.Instance.GetListArea().OrderBy(row => row.AreaID).ToList();

            foreach (var area in listArea)
            {
                int areaId = area.AreaID;

                TabPage tabPage = new TabPage(areaId.ToString());

                ListTapPage.Add(tabPage);

                tabPage.Width = groupControlListTable.Width - 10;
                tabPage.Text = area.AreaName.Trim();
                tabPage.BackColor = Color.WhiteSmoke;

                List<TableDTO> listTable = new List<TableDTO>();

                listTable = TableDAO.Instance.GetListTableByArea(area.AreaID);

                foreach (var table in listTable)
                {
                    TableManagement item = new TableManagement();

                    tables.Add(item);
                    tables[index].btnTable = new SimpleButton();
                    tables[index].btnTable.Size = new Size(100, 100);
                    tables[index].btnTable.BackColor = Color.Blue;
                    tables[index].btnTable.Name = table.TableID.ToString();
                    tables[index].btnTable.ImageOptions.Location = ImageLocation.MiddleCenter;
                    tables[index].btnTable.AppearanceHovered.BackColor = Color.Gold;
                    tables[index].btnTable.AppearancePressed.BackColor = Color.Yellow;

                    if (table.Status.Trim() == "Trống")
                    {
                        this.tables[index].btnTable.ImageOptions.Image = global::QL_QuanAn.Properties.Resources.icons8_restaurant_table_nobody_96;
                    }
                    else
                    {
                        if (table.Status.Trim() == "Đã đặt")
                        {
                            this.tables[index].btnTable.ImageOptions.Image = global::QL_QuanAn.Properties.Resources.icons8_restaurant_table_96;
                        }
                        else
                        {
                            this.tables[index].btnTable.ImageOptions.Image = global::QL_QuanAn.Properties.Resources.icons8_restaurant_table_haveorder_96_filemoi;
                        }
                    }

                    tables[index].lblTable = new Label();
                    tables[index].lblTable.Name = table.TableID.ToString();
                    tables[index].lblTable.Size = new Size(100, 25);
                    tables[index].lblTable.Text = table.Tablename;
                    tables[index].lblTable.BackColor = Color.WhiteSmoke;
                    tables[index].lblTable.Font = new Font("Tahoma", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    tables[index].btnTable.Click += btn_Click;

                    tabPage.Controls.Add(tables[index].lblTable);
                    tabPage.Controls.Add(tables[index].btnTable);

                    if (index == temp)
                    {
                        tables[index].btnTable.Location = new Point(20, 25);
                    }
                    else
                    {
                        if (tables[index - 1].btnTable.Location.X + 150 > tabPage.Width)
                        {
                            tables[index].btnTable.Location = new Point(20, tables[index - 1].btnTable.Location.Y + 150);
                        }
                        else
                        {
                            tables[index].btnTable.Location = new Point(tables[index - 1].btnTable.Location.X + 140, tables[index - 1].btnTable.Location.Y);
                        }
                    }

                    tables[index].lblTable.Location = new Point(tables[index].btnTable.Location.X, tables[index].btnTable.Location.Y + 100);
                    tables[index].lblTable.TextAlign = ContentAlignment.MiddleCenter;
                    index++;
                }

                temp += listTable.Count();
                tabPage.Refresh();
                tabControlTable.Controls.Add(tabPage);
            }
        }

        public void ChangeColorTable(SimpleButton btntable, List<TableManagement> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (btntable.Name == list[i].btnTable.Name)
                {
                    list[i].lblTable.BackColor = Color.Yellow;
                }
                else
                {
                    list[i].lblTable.BackColor = Color.WhiteSmoke;
                }
            }
        }

        void LoadAreaNameIntoCombobox(System.Windows.Forms.ComboBox cb)
        {
            List<AreaDTO> listArea = AreaDAO.Instance.GetListArea();

            AreaMap.Clear();

            foreach (AreaDTO area in listArea)
            {
                AreaMap.Add(area.AreaID, area.AreaName);
            }

            cb.DataSource = listArea.Select(fc => fc.AreaName).ToList();
        }

        void LoadStatusTableIntoComboBox(System.Windows.Forms.ComboBox cb)
        {
            // Tạo danh sách chứa hai trạng thái "Đã đặt" và "Có người"
            List<string> statusList = new List<string> { "Trống", "Đã Đặt" };

            // Gán danh sách làm nguồn dữ liệu cho ComboBox
            cb.DataSource = statusList;
        }

        int GetAreaIdFromComboBox(System.Windows.Forms.ComboBox cb)
        {
            string selectedFoodCategoryName = cb.SelectedItem as string;

            if (selectedFoodCategoryName != null)
            {
                // Tìm mã loại món ăn từ Dictionary bằng tên loại món ăn
                if (AreaMap.ContainsValue(selectedFoodCategoryName))
                {
                    return AreaMap.FirstOrDefault(x => x.Value == selectedFoodCategoryName).Key;
                }
            }

            return -1; // Hoặc một giá trị khác biểu thị rằng không tìm thấy
        }

        private void btn_Click(object sender, EventArgs e)
        {
            SimpleButton simpleButton = sender as SimpleButton;
            ChangeColorTable(simpleButton, tables);

            LoadAreaNameIntoCombobox(cbTableArea);
            // Trích xuất TableID từ SimpleButton
            int tableID = int.Parse(simpleButton.Name);

            // Tìm thông tin bàn tương ứng trong listTable hoặc từ cơ sở dữ liệu
            TableDTO selectedTable = TableDAO.Instance.GetTableByTableId(tableID); // Điều này phải thay thế bằng cách bạn lấy thông tin từ cơ sở dữ liệu hoặc danh sách

            // Gán giá trị tương ứng vào các ô
            txbTableID.Text = selectedTable.TableID.ToString();
            txbTableName.Text = selectedTable.Tablename;
            txbTableQuality.Text = selectedTable.Quantity.ToString();
            cbTableArea.Text = AreaMap[selectedTable.AreaID]; // Gán tên khu vực thay vì mã
            cbTableStatus.Text = selectedTable.Status;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Add = true;

            LoadAreaNameIntoCombobox(cbTableArea);
            LoadStatusTableIntoComboBox(cbTableStatus);

            txbTableName.Enabled = true;
            txbTableQuality.Enabled = true;
            cbTableArea.Enabled = true;
            cbTableStatus.Enabled = true;

            btnSave.Enabled = true;
            btnRemove.Enabled = true;

            btnAdd.Enabled = false;
            btnEdit.Enabled = false;
            btnDelete.Enabled = false;

            txbTableID.Clear();
            txbTableName.Clear();
            txbTableQuality.Clear();

            cbTableArea.DropDownStyle = ComboBoxStyle.DropDownList;
            cbTableStatus.DropDownStyle = ComboBoxStyle.DropDownList;
        }
        private void btnEdit_Click(object sender, EventArgs e)
        {
            Add = false;

            txbTableName.Enabled = true;
            txbTableQuality.Enabled = true;
            cbTableArea.Enabled = true;
            cbTableStatus.Enabled = true;

            btnSave.Enabled = true;
            btnRemove.Enabled = true;

            btnAdd.Enabled = false;
            btnEdit.Enabled = false;
            btnDelete.Enabled = false;

            cbTableArea.DropDownStyle = ComboBoxStyle.DropDownList;
            cbTableStatus.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int tableid = int.Parse(txbTableID.Text);
            string status = cbTableStatus.SelectedItem as string;

            if (tableid == 0)
            {
                notification.Show("Vui lòng chọn bàn ăn cần xóa!!!");
                return;
            }

            if (status == "Đã đặt")
            {
                notification.Show("Bàn này có người ăn, không thể xóa!!!");
                return;
            }

            MessageBoxYesNo msgyn = new MessageBoxYesNo();
            msgyn.Notify = "Bạn có muốn xóa không?";
            msgyn.ShowDialog();
            msgyn.Hide();
            Check = msgyn.Check;

            if (Check == "Có")
            {
                try
                {
                    if (TableDAO.Instance.DeleteTalbe(tableid))
                    {
                        notification.Show("Xóa bàn ăn thành công!!!");
                        LoadTable();
                        txbTableID.Clear();
                        txbTableName.Clear();
                        txbTableQuality.Clear();
                    }
                    else
                    {
                        notification.Show("Xóa bàn ăn thất bại!!!");
                    }
                }
                catch { }
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            string tablename = txbTableName.Text;
            int quanlity = int.Parse(txbTableQuality.Text);
            int areaId = GetAreaIdFromComboBox(cbTableArea);
            string status = cbTableStatus.SelectedItem as string;

            if (Add)
            {
                try
                {
                    if (TableDAO.Instance.InsertTable(areaId, tablename, quanlity, status))
                    {
                        notification.Show("Thêm bàn ăn thành công!!!");
                        LoadTable();
                        btnAdd.Enabled = true;
                        btnEdit.Enabled = true;
                        btnDelete.Enabled = true;

                        btnSave.Enabled = false;
                        btnRemove.Enabled = false;
                    }
                    else
                    {
                        notification.Show("Thêm bàn ăn thất bại!!!");
                    }
                }
                catch
                {
                    notification.Show("Có lỗi cho việc thêm bàn ăn!!!");
                }
            }
            else
            {
                try
                {
                    int tableid = int.Parse(txbTableID.Text);
                    if (TableDAO.Instance.UpdateTable(tableid, areaId, tablename, quanlity, status))
                    {
                        notification.Show("Sửa thông tin bàn ăn thành công!!!");
                        LoadTable();
                        btnAdd.Enabled = true;
                        btnEdit.Enabled = true;
                        btnDelete.Enabled = true;

                        btnSave.Enabled = false;
                        btnRemove.Enabled = false;
                    }
                    else
                    {
                        notification.Show("Sửa thông tin bàn ăn thất bại!!!");
                    }
                }
                catch { notification.Show("Có lỗi cho việc sửa bàn ăn!!!"); }
            }
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            txbTableID.Enabled = false;
            txbTableName.Enabled = false;
            txbTableQuality.Enabled = false;
            cbTableArea.Enabled = false;
            cbTableStatus.Enabled = false;

            txbTableID.Clear();
            txbTableName.Clear();
            txbTableQuality.Clear();

            btnRemove.Enabled = false;
            btnSave.Enabled = false;

            btnAdd.Enabled = true;
            btnDelete.Enabled = true;
            btnEdit.Enabled = true;
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            txbTableID.Enabled = false;
            txbTableName.Enabled = false;
            txbTableQuality.Enabled = false;
            cbTableArea.Enabled = false;
            cbTableStatus.Enabled = false;

            txbTableID.Clear();
            txbTableName.Clear();
            txbTableQuality.Clear();

            btnRemove.Enabled = false;
            btnSave.Enabled = false;

            btnAdd.Enabled = true;
            btnDelete.Enabled = true;
            btnEdit.Enabled = true;
        }
    }
}