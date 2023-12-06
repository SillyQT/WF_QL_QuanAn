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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using DevExpress.CodeParser;
using System.Net;
using System.Xml.Linq;

namespace QL_QuanAn
{
    public partial class EmployeeManagement : DevExpress.XtraEditors.XtraForm
    {
        #region Method
        BindingSource employeeList = new BindingSource();
        bool Add = false;
        Notification notitfy = new Notification(); //Thông báo
        public string Check { get; set; }
        #endregion Method

        public EmployeeManagement()
        {
            InitializeComponent();
            dtgvEmployee.DataSource = employeeList;
            Load();
        }

        #region EV_EmployeeManagement
        private new void Load()
        {
            LoadListEmployee();
            EmployeeBinding();
            LoadSexEmployeeIntoComboBox(cbSex);
            LoadSortBasicSalaryIntoComboBox(cbSortSalary);
            LoadSortSexIntoComboBox(cbSortSex);

            btnSave.Enabled = false;
            btnRemove.Enabled = false;
            groupControl1.Enabled = false;

            labelControlWorkShilf.Text = "Tổng ca (tháng): 0 ca";
            labelControlSalary.Text = "Tổng tiền: 0 VND";

            cbSortSex.Enabled = true;
            cbSortSalary.Enabled = true;

            cbSortSalary.DropDownStyle = ComboBoxStyle.DropDownList;
            cbSortSex.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        void LoadListEmployee()
        {
            dtgvEmployee.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

            employeeList.DataSource = EmployeeDAO.Instance.GetListEmployee();

            // Đặt các thuộc tính của các cột để canh giữa
            foreach (DataGridViewColumn column in dtgvEmployee.Columns)
            {
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                column.ReadOnly = true;
            }

            dtgvEmployee.Columns[0].HeaderText = "Mã";
            dtgvEmployee.Columns[0].Width = 80;

            dtgvEmployee.Columns[1].HeaderText = "Họ tên";
            dtgvEmployee.Columns[1].Width = 160;

            dtgvEmployee.Columns[2].HeaderText = "Giới tính";
            dtgvEmployee.Columns[2].Width = 70;

            dtgvEmployee.Columns[3].HeaderText = "Ngày sinh";
            dtgvEmployee.Columns[3].Width = 110;

            dtgvEmployee.Columns[4].HeaderText = "Địa chỉ";
            dtgvEmployee.Columns[4].Width = 300;

            dtgvEmployee.Columns[5].HeaderText = "Số điện thoại";
            dtgvEmployee.Columns[5].Width = 120;

            dtgvEmployee.Columns[6].HeaderText = "Ngày vào làm";
            dtgvEmployee.Columns[6].Width = 110;

            dtgvEmployee.Columns[7].HeaderText = "Lương cơ bản";
            dtgvEmployee.Columns[7].Width = 100;

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
            dtpkDateOfBirth.DataBindings.Add(new Binding("Value", dtgvEmployee.DataSource, "DateOfBirth", true, DataSourceUpdateMode.Never));
            txbAddress.DataBindings.Add(new Binding("Text", dtgvEmployee.DataSource, "Address", true, DataSourceUpdateMode.Never));
            dtpkDayOfWork.DataBindings.Add(new Binding("Value", dtgvEmployee.DataSource, "DayOfWork", true, DataSourceUpdateMode.Never));
            txbPhone.DataBindings.Add(new Binding("Text", dtgvEmployee.DataSource, "Phone", true, DataSourceUpdateMode.Never));
            txbBasicSalary.DataBindings.Add(new Binding("Text", dtgvEmployee.DataSource, "BasicSalary", true, DataSourceUpdateMode.Never));
        }

        void LoadSexEmployeeIntoComboBox(System.Windows.Forms.ComboBox cb)
        {
            // Lấy danh sách tất cả các trạng thái từ dữ liệu
            List<string> allStatuses = EmployeeDAO.Instance.GetListEmployee().Select(emp => emp.Sex).ToList();

            // Tạo danh sách duy nhất bằng cách sử dụng Distinct()
            List<string> uniqueStatuses = allStatuses.Distinct().ToList();

            // Gán danh sách duy nhất cho ComboBox
            cb.DataSource = uniqueStatuses;
        }

        void LoadSortBasicSalaryIntoComboBox(System.Windows.Forms.ComboBox cb)
        {
            // Lấy danh sách nhân viên từ EmployeeDAO.Instance.GetListEmployee()
            List<EmployeeDTO> employeeList = EmployeeDAO.Instance.GetListEmployee();

            // Tạo một danh sách mới để chứa giá trị không trùng lặp
            List<EmployeeDTO> uniqueEmployeeList = new List<EmployeeDTO>();

            // Duyệt qua danh sách gốc và thêm các giá trị không trùng lặp vào danh sách mới
            foreach (EmployeeDTO employee in employeeList)
            {
                if (!uniqueEmployeeList.Any(emp => emp.BasicSalary == employee.BasicSalary))
                {
                    uniqueEmployeeList.Add(employee);
                }
            }

            // Tạo một đối tượng EmployeeDTO đại diện cho "Tất cả" với BasicSalary = 0.0
            EmployeeDTO allEmployees = new EmployeeDTO { BasicSalary = 0.0 };

            // Thêm "Tất cả" vào danh sách
            uniqueEmployeeList.Insert(0, allEmployees);

            // Gán danh sách dữ liệu đã loại bỏ giá trị trùng lặp và có thêm "Tất cả" cho ComboBox
            cb.DataSource = uniqueEmployeeList;

            // Đặt thuộc tính hiển thị cho ComboBox
            cb.DisplayMember = "BasicSalary";
        }

        void LoadSortSexIntoComboBox(System.Windows.Forms.ComboBox cb)
        {
            // Lấy danh sách tất cả các trạng thái từ dữ liệu
            List<string> allStatuses = EmployeeDAO.Instance.GetListEmployee().Select(emp => emp.Sex).ToList();

            // Tạo danh sách duy nhất bằng cách sử dụng Distinct()
            List<string> uniqueStatuses = allStatuses.Distinct().ToList();

            // Thêm một mục "Tất cả" vào danh sách
            uniqueStatuses.Insert(0, "Tất cả");

            // Gán danh sách đã chỉnh sửa cho ComboBox
            cb.DataSource = uniqueStatuses;
        }

        List<EmployeeDTO> SearchEmployeeByName(string name)
        {
            List<EmployeeDTO> listEmployee = EmployeeDAO.Instance.SearchEmployeeByName(name);

            return listEmployee;
        }

        List<EmployeeDTO> SearchEmployeeBySalary(double salary)
        {
            List<EmployeeDTO> listEmployee = EmployeeDAO.Instance.SearchEmployeeBySalary((double)salary);

            return listEmployee;
        }

        List<EmployeeDTO> SearchEmployeeBySalaryAndSex(double salary, string sex)
        {
            List<EmployeeDTO> listEmployee = EmployeeDAO.Instance.SearchEmployeeBySalaryAndSex((double)salary, sex);

            return listEmployee;
        }

        List<EmployeeDTO> SearchEmployeeBySex(string sex)
        {
            List<EmployeeDTO> listEmployee = EmployeeDAO.Instance.SearchEmployeeBySex(sex);

            return listEmployee;
        }
        #endregion EV_EmployeeManagement

        #region EV_EmployeeManagement
        private void txbEmployeeID_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (dtgvEmployee.SelectedCells.Count > 0 && dtgvEmployee.SelectedCells[0].OwningRow.Cells["StaffID"].Value != null)
                {
                    string staffID = dtgvEmployee.SelectedCells[0].OwningRow.Cells["StaffID"].Value.ToString();
                    EmployeeDTO employee = EmployeeDAO.Instance.GetEmployeeByStaffID(staffID);

                    // Tạo danh sách duy nhất của trạng thái
                    List<string> uniqueStatuses = EmployeeDAO.Instance.GetListEmployee().Select(emp => emp.Sex).Distinct().ToList();

                    // Gán danh sách duy nhất cho ComboBox
                    cbSex.DataSource = uniqueStatuses;

                    if (uniqueStatuses.Contains(employee.Sex))
                    {
                        cbSex.SelectedItem = employee.Sex;
                    }
                }
            }
            catch { }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Add = true;

            btnAdd.Enabled = false;
            btnEdit.Enabled = false;
            btnDelete.Enabled = false;

            btnReload.Enabled = true;
            btnSave.Enabled = true;
            btnRemove.Enabled = true;

            txbEmployeeID.Clear();
            txbName.Clear();
            dtpkDateOfBirth.Value = DateTime.Now;
            txbAddress.Clear();
            txbPhone.Clear();
            dtpkDayOfWork.Value = DateTime.Now;
            txbBasicSalary.Clear();

            groupControl1.Enabled = true;
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            LoadListEmployee();

            btnAdd.Enabled = true;
            btnEdit.Enabled = true;
            btnDelete.Enabled = true;

            txbEmployeeID.Clear();
            txbName.Clear();
            dtpkDateOfBirth.Value = DateTime.Now;
            txbAddress.Clear();
            txbPhone.Clear();
            dtpkDayOfWork.Value = DateTime.Now;
            txbBasicSalary.Clear();

            labelControlWorkShilf.Text = "Tổng ca (tháng): 0 ca";
            labelControlSalary.Text = "Tổng tiền: 0 VND";

            groupControl1.Enabled = false;
            btnSave.Enabled = false;
            btnRemove.Enabled = false;

            cbSortSalary.SelectedIndex = 0;
            cbSex.SelectedIndex = 0;

            cbSortSex.Enabled = true;
            cbSortSalary.Enabled = true;
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            LoadListEmployee();

            btnAdd.Enabled = true;
            btnEdit.Enabled = true;
            btnDelete.Enabled = true;

            txbEmployeeID.Clear();
            txbName.Clear();
            dtpkDayOfWork.Value = DateTime.Now;
            txbAddress.Clear();
            txbPhone.Clear();
            dtpkDayOfWork.Value = DateTime.Now;
            txbBasicSalary.Clear();

            labelControlWorkShilf.Text = "Tổng ca (tháng): 0 ca";
            labelControlSalary.Text = "Tổng tiền: 0 VND";

            groupControl1.Enabled = false;
            btnSave.Enabled = false;
            btnRemove.Enabled = false;

            cbSortSalary.SelectedIndex = 0;
            cbSex.SelectedIndex = 0;

            cbSortSex.Enabled = true;
            cbSortSalary.Enabled = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string staffid = txbEmployeeID.Text;
            string name = txbName.Text;
            string sex = cbSex.SelectedItem as string;

            DateTime dayofbirth = dtpkDateOfBirth.Value;

            string address = txbAddress.Text;
            string phone = txbPhone.Text;

            DateTime dayofwork = dtpkDayOfWork.Value;

            double bacsicSalary = Convert.ToDouble(txbBasicSalary.Text);

            if (string.IsNullOrEmpty(staffid) || string.IsNullOrEmpty(name) || string.IsNullOrEmpty(sex) || dayofbirth == DateTime.MinValue || string.IsNullOrEmpty(address) || string.IsNullOrEmpty(phone) || dayofwork == DateTime.MinValue || bacsicSalary <= 0)
            {
                notitfy.Show("Vui lòng điền đầy đủ thông tin.");
                return;
            }

            if (!double.TryParse(txbBasicSalary.Text, out bacsicSalary) || bacsicSalary < 0)
            {
                notitfy.Show("Lương cơ bản không hợp lệ. Vui lòng nhập một giá trị số hợp lệ.");
                return;
            }

            if (Add)
            {
                try
                {
                    EmployeeDTO employee = EmployeeDAO.Instance.GetEmployeeByStaffID(staffid);
                    if (employee != null && employee.StaffID == staffid)
                    {
                        notitfy.Show("Mã nhân viên " + staffid + " đã tồn tại trong danh sách tài khoản hoặc danh sách tài khoản đã xóa.\nVui lòng chọn mã nhân viên khác!!!.");
                        return;
                    }

                    if (EmployeeDAO.Instance.InsertEmployee(staffid, name, sex, dayofbirth, address, phone, dayofwork, bacsicSalary))
                    {
                        notitfy.Show("Thêm nhân viên " + name + " thành công!!!");
                        LoadListEmployee();

                        btnAdd.Enabled = true;
                        btnEdit.Enabled = true;
                        btnDelete.Enabled = true;

                        btnSave.Enabled = false;
                        btnRemove.Enabled = false;
                    }
                    else
                    {
                        notitfy.Show("Thêm thất bại!!!");
                    }
                }
                catch
                {
                    notitfy.Show("Lỗi thông tin vui lòng kiểm tra lại!!!");
                }
            }
            else
            {
                try
                {
                    if (EmployeeDAO.Instance.UpdateEmployee(staffid, name, sex, dayofbirth, address, phone, dayofwork, bacsicSalary))
                    {
                        notitfy.Show("Sửa thông tin nhân viên thành công!!!");
                        LoadListEmployee();

                        btnAdd.Enabled = true;
                        btnEdit.Enabled = true;
                        btnDelete.Enabled = true;

                        btnSave.Enabled = false;
                        btnRemove.Enabled = false;
                    }
                    else
                    {
                        notitfy.Show("Sửa thông tin nhân viên thất bại!!!");
                    }
                }
                catch { notitfy.Show("Ngày vào làm phải lớn hơn ngày sinh.\nVui lòng điền lại thông tin!!!"); }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string staffid = txbEmployeeID.Text;

            MessageBoxYesNo msgyn = new MessageBoxYesNo();
            msgyn.Notify = "Bạn có muốn xóa không?";
            msgyn.ShowDialog();
            msgyn.Hide();
            Check = msgyn.Check;
            if (Check == "Có")
            {
                if (EmployeeDAO.Instance.DeleteEmployee(staffid))
                {
                    notitfy.Show("Xóa nhân viên " + txbName.Text + " thành công!!!");
                    LoadListEmployee();
                }
                else
                {
                    notitfy.Show("Xóa thất bại thất bại!!!");
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (txbEmployeeID.Text == "")
            {
                notitfy.Show("Vui lòng chọn nhân viên cần sửa!!!");
                return;
            }

            Add = false;

            btnAdd.Enabled = false;
            btnEdit.Enabled = false;
            btnDelete.Enabled = false;
            txbEmployeeID.Enabled = false;

            btnReload.Enabled = true;
            btnSave.Enabled = true;
            btnRemove.Enabled = true;

            groupControl1.Enabled = true;
        }

        private void txtFind_EditValueChanged(object sender, EventArgs e)
        {
            employeeList.DataSource = SearchEmployeeByName(txbFind.Text);
        }

        private void cbSortSalary_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateEmployeeList();
        }

        private void cbSortSex_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateEmployeeList();
        }

        private void UpdateEmployeeList()
        {
            if (cbSortSalary.SelectedIndex > 0 && cbSortSex.SelectedIndex > 0)
            {
                EmployeeDTO selectedEmployeeSalary = (EmployeeDTO)cbSortSalary.SelectedItem;
                double selectedSalary = selectedEmployeeSalary.BasicSalary;

                string selectedSex = cbSortSex.SelectedItem.ToString();

                employeeList.DataSource = SearchEmployeeBySalaryAndSex(selectedSalary, selectedSex);
            }
            else if (cbSortSalary.SelectedIndex > 0)
            {
                EmployeeDTO selectedEmployeeSalary = (EmployeeDTO)cbSortSalary.SelectedItem;
                double selectedSalary = selectedEmployeeSalary.BasicSalary;

                employeeList.DataSource = SearchEmployeeBySalary(selectedSalary);
            }
            else if (cbSortSex.SelectedIndex > 0)
            {
                string selectedSex = cbSortSex.SelectedItem.ToString();

                employeeList.DataSource = SearchEmployeeBySex(selectedSex);
            }
            else
            {
                employeeList.DataSource = EmployeeDAO.Instance.GetListEmployee();
            }
        }
        private void dtgvEmployee_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                EmployeeDAO.Instance.TakeTheSalary(labelControlWorkShilf, labelControlSalary, txbEmployeeID.Text, double.Parse(txbBasicSalary.Text));
            }
            catch { }
        }
        #endregion EV_EmployeeManagement
    }
}