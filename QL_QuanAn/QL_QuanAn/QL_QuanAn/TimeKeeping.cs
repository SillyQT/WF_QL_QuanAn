using DevExpress.CodeParser;
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
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace QL_QuanAn
{
    public partial class TimeKeeping : DevExpress.XtraEditors.XtraForm
    {
        #region Method
        Notification notitfy = new Notification(); //Thông báo       
        BindingSource employeeList = new BindingSource();
        BindingSource timekeepList = new BindingSource();

        private DateTime selectedDate;
        private string selectedStaffID = "";
        private string selectedTime = "";

        private int selectedMonth = 0;
        private int selectedYear = 0;
        private string selectedShift = string.Empty;
        #endregion Method

        public TimeKeeping()
        {
            InitializeComponent();
            Load();
        }

        private new void Load()
        {
            dtgvEmployee.DataSource = employeeList;
            dtgvTimeKeeping.DataSource = timekeepList;
            LoadListEmployee();
            LoadListTimeKeeping();
            EmployeeBinding();
            LoadWorkShiftIntoCombobox(cbWorkShifts);
            LoadMonthOfWorkTimeKeepingIntoCombobox(cbMonthOfWork);
            LoadYearOfWorkTimeKeepingIntoCombobox(cbYearOfWork);
            LoadWorkShifsTimeKeepingIntoComboBox(cbWorkShiftTimeKeeping);

            dtgvEmployee.CellFormatting += dtgvEmployee_CellFormatting;
            dtgvTimeKeeping.CellFormatting += dtgvWorkShifts_CellFormatting;

            txbEmployeeID.Enabled = false;
            txbName.Enabled = false;
            txbStaffIDTimeKeeping.Enabled = false;
            cbWorkShifts.Enabled = false;

            cbMonthOfWork.DropDownStyle = ComboBoxStyle.DropDownList;
            cbYearOfWork.DropDownStyle = ComboBoxStyle.DropDownList;
            cbWorkShiftTimeKeeping.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        #region Employee
        void LoadListEmployee()
        {
            dtgvEmployee.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

            employeeList.DataSource = EmployeeDAO.Instance.GetListEmployee();
            dtgvEmployee.Columns["Address"].Visible = false;
            dtgvEmployee.Columns["Phone"].Visible = false;
            dtgvEmployee.Columns["DateOfBirth"].Visible = false;
            dtgvEmployee.Columns["BasicSalary"].Visible = false;

            // Đặt các thuộc tính của các cột để canh giữa
            foreach (DataGridViewColumn column in dtgvEmployee.Columns)
            {
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                column.ReadOnly = true;
            }

            dtgvEmployee.Columns[0].HeaderText = "Mã";
            dtgvEmployee.Columns[0].Width = 100;

            dtgvEmployee.Columns[1].HeaderText = "Họ tên";
            dtgvEmployee.Columns[1].Width = 190;

            dtgvEmployee.Columns[2].HeaderText = "Giới tính";
            dtgvEmployee.Columns[2].Width = 98;

            dtgvEmployee.Columns[6].HeaderText = "Ngày vào làm";
            dtgvEmployee.Columns[6].Width = 130;

            // Tô màu tiêu đề cột và thay đổi chiều cao
            dtgvEmployee.EnableHeadersVisualStyles = false;
            dtgvEmployee.ColumnHeadersDefaultCellStyle.BackColor = Color.Bisque; // Tô màu nền tiêu đề
            dtgvEmployee.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing; // Cho phép thay đổi kích thước chiều cao
            dtgvEmployee.ColumnHeadersHeight = 50; // Đặt kích thước chiều cao của tiêu đề cột

            // Tăng chiều cao từng dòng dữ liệu
            dtgvEmployee.RowPrePaint += (sender, e) =>
            {
                e.PaintParts &= ~DataGridViewPaintParts.Focus; // Loại bỏ viền bôi đậm
                if (e.RowIndex >= 0)
                {
                    e.Graphics.FillRectangle(Brushes.White, e.RowBounds); // Tô màu nền dòng
                    e.PaintCells(e.ClipBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.Border);
                }
            };
            dtgvEmployee.RowTemplate.Height = 40; // Đặt kích thước chiều cao của từng dòng
        }

        void EmployeeBinding()
        {
            txbEmployeeID.DataBindings.Add(new Binding("Text", dtgvEmployee.DataSource, "StaffID", true, DataSourceUpdateMode.Never));
            txbName.DataBindings.Add(new Binding("Text", dtgvEmployee.DataSource, "StaffName", true, DataSourceUpdateMode.Never));
        }

        List<EmployeeDTO> SearchEmployeeByName(string name)
        {
            List<EmployeeDTO> listEmployee = EmployeeDAO.Instance.SearchEmployeeByName(name);

            return listEmployee;
        }
        #endregion Employee

        #region TimeKeeping
        void LoadListTimeKeeping()
        {
            dtgvTimeKeeping.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

            timekeepList.DataSource = TimeKeepingDAO.Instance.GetListTimeKeeping();

            // Đặt các thuộc tính của các cột để canh giữa
            foreach (DataGridViewColumn column in dtgvTimeKeeping.Columns)
            {
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                column.ReadOnly = true;
            }

            dtgvTimeKeeping.Columns[0].HeaderText = "Mã nhân viên";
            dtgvTimeKeeping.Columns[0].Width = 150;

            dtgvTimeKeeping.Columns[1].HeaderText = "Ngày làm";
            dtgvTimeKeeping.Columns[1].Width = 200;

            dtgvTimeKeeping.Columns[2].HeaderText = "Ca làm";
            dtgvTimeKeeping.Columns[2].Width = 150;

            // Tô màu tiêu đề cột và thay đổi chiều cao
            dtgvTimeKeeping.EnableHeadersVisualStyles = false;
            dtgvTimeKeeping.ColumnHeadersDefaultCellStyle.BackColor = Color.Bisque; // Tô màu nền tiêu đề
            dtgvTimeKeeping.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing; // Cho phép thay đổi kích thước chiều cao
            dtgvTimeKeeping.ColumnHeadersHeight = 50; // Đặt kích thước chiều cao của tiêu đề cột

            // Tăng chiều cao từng dòng dữ liệu
            dtgvTimeKeeping.RowPrePaint += (sender, e) =>
            {
                e.PaintParts &= ~DataGridViewPaintParts.Focus; // Loại bỏ viền bôi đậm
                if (e.RowIndex >= 0)
                {
                    e.Graphics.FillRectangle(Brushes.White, e.RowBounds); // Tô màu nền dòng
                    e.PaintCells(e.ClipBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.Border);
                }
            };
            dtgvTimeKeeping.RowTemplate.Height = 40; // Đặt kích thước chiều cao của từng dòng
        }

        void LoadWorkShiftIntoCombobox(System.Windows.Forms.ComboBox cb)
        {
            // Thêm các mục vào ComboBox
            cbWorkShifts.Items.Add("Ca sáng");
            cbWorkShifts.Items.Add("Ca chiều");
            cbWorkShifts.Items.Add("Ca tối");

            // Thiết lập mục đầu tiên là mục mặc định
            cbWorkShifts.SelectedIndex = 0;
            SetDefaultShiftBasedOnTime();
        }

        private void SetDefaultShiftBasedOnTime()
        {
            DateTime currentTime = DateTime.Now;
            int hour = currentTime.Hour;

            if (hour >= 7 && hour < 12)
            {
                cbWorkShifts.SelectedIndex = 0; // Ca sáng
            }
            else if (hour >= 12 && hour < 17)
            {
                cbWorkShifts.SelectedIndex = 1; // Ca chiều
            }
            else
            {
                cbWorkShifts.SelectedIndex = 2; // Ca tối
            }
        }

        void LoadWorkShifsTimeKeepingIntoComboBox(System.Windows.Forms.ComboBox cb)
        {
            List<string> allWorkShifts = TimeKeepingDAO.Instance.GetListTimeKeeping().Select(ws => ws.WorkShifts).ToList();

            List<string> uniqueWorkShift = allWorkShifts.Distinct().ToList();

            uniqueWorkShift.Insert(0, "Tất cả");

            cb.DataSource = uniqueWorkShift;
        }

        void LoadMonthOfWorkTimeKeepingIntoCombobox(System.Windows.Forms.ComboBox cb)
        {
            cb.Items.Clear();

            cb.Items.Add("Tất cả");

            for (int month = 1; month <= 12; month++)
            {
                cb.Items.Add(month.ToString());
            }

            cb.SelectedIndex = 0;
        }

        void LoadYearOfWorkTimeKeepingIntoCombobox(System.Windows.Forms.ComboBox cb)
        {
            cb.Items.Clear();

            cb.Items.Add("Tất cả");

            for (int year = 2020; year <= 2025; year++)
            {
                cb.Items.Add(year.ToString());
            }

            cb.SelectedIndex = 0;
        }

        List<TimeKeepingDTO> SearchTimeKeepingByStaffID(string staffid)
        {
            List<TimeKeepingDTO> listTimeKeeping = TimeKeepingDAO.Instance.SearchTimeKeepingByStaffID(staffid);

            return listTimeKeeping;
        }

        List<TimeKeepingDTO> SearchTimeKeepingByWorkShifts(string workshift)
        {
            List<TimeKeepingDTO> listTimeKeeping = TimeKeepingDAO.Instance.SearchTimeKeepingByWorkShifts(workshift);

            return listTimeKeeping;
        }

        List<TimeKeepingDTO> SearchTimeKeepingByMonth(int month)
        {
            List<TimeKeepingDTO> listTimeKeeping = TimeKeepingDAO.Instance.SearchTimeKeepingByMonth(month);

            return listTimeKeeping;
        }

        List<TimeKeepingDTO> SearchTimeKeepingByYear(int year)
        {
            List<TimeKeepingDTO> listTimeKeeping = TimeKeepingDAO.Instance.SearchTimeKeepingByYear(year);

            return listTimeKeeping;
        }

        List<TimeKeepingDTO> SearchTimeKeepingByMonthAndYear(int month, int year)
        {
            List<TimeKeepingDTO> listTimeKeeping = TimeKeepingDAO.Instance.SearchTimeKeepingByMonthAndYear(month, year);

            return listTimeKeeping;
        }

        List<TimeKeepingDTO> SearchTimeKeepingByMonthAndShift(int month, string workshift)
        {
            List<TimeKeepingDTO> listTimeKeeping = TimeKeepingDAO.Instance.SearchTimeKeepingByMonthAndShift(month, workshift);

            return listTimeKeeping;
        }

        List<TimeKeepingDTO> SearchTimeKeepingByYearAndShift(int year, string workshift)
        {
            List<TimeKeepingDTO> listTimeKeeping = TimeKeepingDAO.Instance.SearchTimeKeepingByYearAndShift(year, workshift);

            return listTimeKeeping;
        }

        List<TimeKeepingDTO> SearchTimeKeepingByMonthYearAndShift(int month, int year, string workshift)
        {
            List<TimeKeepingDTO> listTimeKeeping = TimeKeepingDAO.Instance.SearchTimeKeepingByMonthYearAndShift(month, year, workshift);

            return listTimeKeeping;
        }

        private void UpdateSelectedValues()
        {
            if (cbMonthOfWork.SelectedIndex > 0)
            {
                selectedMonth = Convert.ToInt32(cbMonthOfWork.SelectedItem.ToString());
            }
            else
            {
                selectedMonth = 0;
            }

            if (cbYearOfWork.SelectedIndex > 0)
            {
                selectedYear = Convert.ToInt32(cbYearOfWork.SelectedItem.ToString());
            }
            else
            {
                selectedYear = 0;
            }

            if (cbWorkShiftTimeKeeping.SelectedIndex > 0)
            {
                selectedShift = cbWorkShiftTimeKeeping.SelectedItem.ToString();
            }
            else
            {
                selectedShift = string.Empty;
            }
        }

        private void PerformSearch()
        {
            if (selectedMonth == 0 && selectedYear == 0 && string.IsNullOrEmpty(selectedShift))
            {
                // Chưa chọn tháng, năm và ca làm, hiển thị tất cả
                timekeepList.DataSource = TimeKeepingDAO.Instance.GetListTimeKeeping();
            }
            else if (selectedMonth > 0 && selectedYear == 0 && string.IsNullOrEmpty(selectedShift))
            {
                // Chỉ chọn tháng
                timekeepList.DataSource = SearchTimeKeepingByMonth(selectedMonth);
            }
            else if (selectedMonth == 0 && selectedYear > 0 && string.IsNullOrEmpty(selectedShift))
            {
                // Chỉ chọn năm
                timekeepList.DataSource = SearchTimeKeepingByYear(selectedYear);
            }
            else if (selectedMonth > 0 && selectedYear > 0 && string.IsNullOrEmpty(selectedShift))
            {
                // Chọn cả tháng và năm
                timekeepList.DataSource = SearchTimeKeepingByMonthAndYear(selectedMonth, selectedYear);
            }
            else if (selectedMonth == 0 && selectedYear == 0 && !string.IsNullOrEmpty(selectedShift))
            {
                // Chỉ chọn ca làm
                timekeepList.DataSource = SearchTimeKeepingByWorkShifts(selectedShift);
            }
            else if (selectedMonth > 0 && selectedYear == 0 && !string.IsNullOrEmpty(selectedShift))
            {
                // Chọn tháng và ca làm
                timekeepList.DataSource = SearchTimeKeepingByMonthAndShift(selectedMonth, selectedShift);
            }
            else if (selectedMonth == 0 && selectedYear > 0 && !string.IsNullOrEmpty(selectedShift))
            {
                // Chọn năm và ca làm
                timekeepList.DataSource = SearchTimeKeepingByYearAndShift(selectedYear, selectedShift);
            }
            else if (selectedMonth > 0 && selectedYear > 0 && !string.IsNullOrEmpty(selectedShift))
            {
                // Chọn cả tháng, năm và ca làm
                timekeepList.DataSource = SearchTimeKeepingByMonthYearAndShift(selectedMonth, selectedYear, selectedShift);
            }
        }
        #endregion TimeKeeping

        #region EV_Employee
        private void dtgvEmployee_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                // Kiểm tra cột bạn muốn canh giữa, ví dụ: cột 0, 1, 2
                if (e.ColumnIndex == 0 || e.ColumnIndex == 2 || e.ColumnIndex == 6)
                {
                    e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
            }
        }
        #endregion EV_Employee

        #region EV_TimeKeeping
        private void dtgvWorkShifts_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                // Kiểm tra cột bạn muốn canh giữa, ví dụ: cột 0, 1, 2
                if (e.ColumnIndex == 0 || e.ColumnIndex == 1 || e.ColumnIndex == 2)
                {
                    e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
            }
        }

        private void btnAddWork_Click(object sender, EventArgs e)
        {
            string staffid = txbEmployeeID.Text;
            string workshifts = cbWorkShifts.SelectedItem as string;

            try
            {
                if (TimeKeepingDAO.Instance.InsertTimeKeeping(staffid, workshifts))
                {
                    notitfy.Show("Chấm công thành công!!!");
                    LoadListTimeKeeping();
                }
                else
                {
                    notitfy.Show("Chấm công thất bại!!!");
                }
            }
            catch
            {
                notitfy.Show("Lỗi: Nhân viên này đã được chấm công!!!");
            }
        }

        private void btnDeleteWork_Click(object sender, EventArgs e)
        {
            try
            {
                if (TimeKeepingDAO.Instance.DeleteTimeKeeping(selectedStaffID, selectedDate, selectedTime))
                {
                    notitfy.Show("Xóa chấm công của nhân viên " + selectedStaffID + " vào ngày " + selectedDate.ToString("dd/MM/yyyy") + " thành công!!!");
                    LoadListTimeKeeping();
                }
                else
                {
                    notitfy.Show("Xóa chấm công thất bại!!!");
                }
            }
            catch { }
        }

        private void dtgvTimeKeeping_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow selectedRow = dtgvTimeKeeping.Rows[e.RowIndex];
                    selectedStaffID = selectedRow.Cells[0].Value.ToString();
                    if (DateTime.TryParse(selectedRow.Cells[1].Value.ToString(), out selectedDate))
                    {
                        txbStaffIDTimeKeeping.Text = selectedStaffID;
                    }
                    selectedTime = selectedRow.Cells[2].Value.ToString();
                }
            }
            catch { }
        }

        private void txtTimKiem_EditValueChanged(object sender, EventArgs e)
        {
            employeeList.DataSource = SearchEmployeeByName(txbFindName.Text);
        }

        private void txbFindStaffID_EditValueChanged(object sender, EventArgs e)
        {
            timekeepList.DataSource = SearchTimeKeepingByStaffID(txbFindStaffID.Text);
        }
        
        private void cbMonthOfWork_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateSelectedValues();
            PerformSearch();
        }

        private void cbYearOfWork_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateSelectedValues();
            PerformSearch();
        }

        private void cbWorkShiftTimeKeeping_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateSelectedValues();
            PerformSearch();
        }        
        #endregion EV_TimeKeeping
    }
}