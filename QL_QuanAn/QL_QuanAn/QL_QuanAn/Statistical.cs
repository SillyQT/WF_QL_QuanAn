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
using System.Windows.Forms.VisualStyles;

namespace QL_QuanAn
{
    public partial class Statistical : DevExpress.XtraEditors.XtraForm
    {
        Notification notification = new Notification();

        private int pageSize = 9;
        private int currentPage = 1;

        private AccountDTO account = new AccountDTO();

        public Statistical(AccountDTO account)
        {
            InitializeComponent();

            this.account = account;
        }

        public void LoadBill()
        {
            try
            {
                DateTime dateStart = dtpkStart.Value;
                DateTime dateEnd = dtpkEnd.Value;

                if (dateStart <= dateEnd)
                {
                    dtgvBill.Rows.Clear();
                    dtgvBill.Columns.Clear();

                    dtgvBill.Font = new System.Drawing.Font("Tahoma", 12.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

                    dtgvBill.Columns.Add("billId", "Mã hóa đơn");
                    dtgvBill.Columns.Add("tableName", "Tên bàn");
                    dtgvBill.Columns.Add("staff", "Tên nhân viên");
                    dtgvBill.Columns.Add("dateIn", "Ngày vào");
                    dtgvBill.Columns.Add("dateOut", "Ngày ra");
                    dtgvBill.Columns.Add("total", "Tổng tiền");

                    int exceptRows = (currentPage - 1) * pageSize;

                    List<BillDTO> listBill = BillDAO.Instance.GetListBillByDateOutAndStaff(dateStart, dateEnd, account.StaffID).Skip(exceptRows).Take(pageSize).ToList();

                    foreach (BillDTO bill in listBill)
                    {
                        string formattedDateIn = bill.DateIn.HasValue ? bill.DateIn.Value.ToString("dd/MM/yyyy") : "N/A";
                        string formattedDateOut = bill.DateOut.HasValue ? bill.DateOut.Value.ToString("dd/MM/yyyy") : "N/A";

                        dtgvBill.Rows.Add(bill.BillID, TableDAO.Instance.GetTableByTableId(bill.TableID).Tablename, EmployeeDAO.Instance.GetEmployeeByStaffID(bill.StaffID).StaffName, formattedDateIn, formattedDateOut, string.Format(new CultureInfo("vi-VN"), "{0:#,##0} VND", bill.Total));
                    }

                    txbDoanhThu.Text = string.Format(new CultureInfo("vi-VN"), "{0:#,##0} VND", BillDAO.Instance.GetListBillByDateOutAndStaff(dateStart, dateEnd, account.StaffID).Sum(row => row.Total));

                    // Đặt các thuộc tính của các cột để canh giữa và in đậm
                    foreach (DataGridViewColumn column in dtgvBill.Columns)
                    {
                        column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        column.ReadOnly = true;
                    }

                    dtgvBill.Columns[0].Width = 80;
                    dtgvBill.Columns[1].Width = 100;
                    dtgvBill.Columns[2].Width = 176;
                    dtgvBill.Columns[3].Width = 100;
                    dtgvBill.Columns[4].Width = 100;
                    dtgvBill.Columns[5].Width = 135;

                    // Tô màu tiêu đề cột và thay đổi chiều cao
                    dtgvBill.EnableHeadersVisualStyles = false;
                    dtgvBill.ColumnHeadersDefaultCellStyle.BackColor = Color.Bisque; // Tô màu nền tiêu đề
                    dtgvBill.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing; // Cho phép thay đổi kích thước chiều cao
                    dtgvBill.ColumnHeadersHeight = 50; // Đặt kích thước chiều cao của tiêu đề cột

                    // Tăng chiều cao từng dòng dữ liệu
                    dtgvBill.RowPrePaint += (sender, e) =>
                    {
                        e.PaintParts &= ~DataGridViewPaintParts.Focus; // Loại bỏ viền bôi đậm
                        if (e.RowIndex >= 0)
                        {
                            e.Graphics.FillRectangle(Brushes.White, e.RowBounds); // Tô màu nền dòng
                            e.PaintCells(e.ClipBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.Border);
                        }
                    };
                    dtgvBill.RowTemplate.Height = 40; // Đặt kích thước chiều cao của từng dòng
                }
                else
                {
                    notification.Show("Ngày bắt đầu phải bé hơn hoặc bằng ngày kết thúc!");
                }
            }
            catch { }
        }

        void FormatFoodBillInfo()
        {
            // Đặt các thuộc tính của các cột để canh giữa và in đậm
            foreach (DataGridViewColumn column in dtgvBillInfo.Columns)
            {
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                column.ReadOnly = true;
            }

            dtgvBillInfo.Columns[0].Width = 150;
            dtgvBillInfo.Columns[1].Width = 153;
            dtgvBillInfo.Columns[2].Width = 100;
            dtgvBillInfo.Columns[3].Width = 150;

            // Tô màu tiêu đề cột và thay đổi chiều cao
            dtgvBillInfo.EnableHeadersVisualStyles = false;
            dtgvBillInfo.ColumnHeadersDefaultCellStyle.BackColor = Color.Bisque; // Tô màu nền tiêu đề
            dtgvBillInfo.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing; // Cho phép thay đổi kích thước chiều cao
            dtgvBillInfo.ColumnHeadersHeight = 50; // Đặt kích thước chiều cao của tiêu đề cột

            // Tăng chiều cao từng dòng dữ liệu
            dtgvBillInfo.RowPrePaint += (sender, e) =>
            {
                e.PaintParts &= ~DataGridViewPaintParts.Focus; // Loại bỏ viền bôi đậm
                if (e.RowIndex >= 0)
                {
                    e.Graphics.FillRectangle(Brushes.White, e.RowBounds); // Tô màu nền dòng
                    e.PaintCells(e.ClipBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.Border);
                }
            };
            dtgvBillInfo.RowTemplate.Height = 40; // Đặt kích thước chiều cao của từng dòng
        }

        private void Statistical_Load_1(object sender, EventArgs e)
        {
            // Lấy ngày đầu tiên của tháng hiện tại
            DateTime firstDayOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

            // Thiết lập giá trị cho dtpkStart
            dtpkStart.Value = firstDayOfMonth;

            // Lấy ngày cuối cùng của tháng tiếp theo
            DateTime lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

            // Thiết lập giá trị cho dtpkEnd
            dtpkEnd.Value = lastDayOfMonth;
        }

        private void dtpkStart_ValueChanged(object sender, EventArgs e)
        {
            LoadBill();
        }

        private void btnFirst_Click_1(object sender, EventArgs e)
        {
            txbPage.Text = "1";
        }

        private void btnLast_Click_1(object sender, EventArgs e)
        {
            int num = BillDAO.Instance.GetListBillByDateOut(dtpkStart.Value, dtpkEnd.Value).Count();

            int lastPage = num / pageSize;

            if (num % pageSize != 0)
            {
                lastPage++;
            }

            txbPage.Text = lastPage.ToString();
        }

        private void txbPage_TextChanged_1(object sender, EventArgs e)
        {
            currentPage = Int32.Parse(txbPage.Text.Trim());

            LoadBill();
        }

        private void btnPrevious_Click_1(object sender, EventArgs e)
        {
            int page = Int32.Parse(txbPage.Text);

            if (page > 1)
            {
                page--;
            }

            txbPage.Text = page.ToString();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            int page = Int32.Parse(txbPage.Text);

            int num = BillDAO.Instance.GetListBillByDateOut(dtpkStart.Value, dtpkEnd.Value).Count();

            int lastPage = num / pageSize;

            if (num % pageSize != 0)
            {
                lastPage++;
            }

            if (page < lastPage)
            {
                page++;
            }

            txbPage.Text = page.ToString();
        }

        private void dtgvBill_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    dtgvBillInfo.Rows.Clear();
                    dtgvBillInfo.Columns.Clear();

                    dtgvBillInfo.Font = new System.Drawing.Font("Tahoma", 12.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

                    dtgvBillInfo.Columns.Add("foodName", "Món ăn");
                    dtgvBillInfo.Columns.Add("price", "Đơn giá");
                    dtgvBillInfo.Columns.Add("quantity", "Số lượng");
                    dtgvBillInfo.Columns.Add("total", "Thành tiền");

                    DataGridViewRow selectedRow = dtgvBill.Rows[e.RowIndex];

                    int billId = Int32.Parse(selectedRow.Cells[0].Value.ToString());

                    List<BillInfoDTO> listBillInfo = BillInfoDAO.Instance.GetBillInfoByBill(billId);

                    foreach (BillInfoDTO billInfo in listBillInfo)
                    {
                        dtgvBillInfo.Rows.Add(FoodDAO.Instance.GetFoodNameByFoodId(billInfo.FoodID), string.Format(new CultureInfo("vi-VN"), "{0:#,##0} VND", FoodDAO.Instance.GetUnitPriceByFoodId(billInfo.FoodID)), billInfo.Quanlity, string.Format(new CultureInfo("vi-VN"), "{0:#,##0} VND", billInfo.Quanlity * FoodDAO.Instance.GetUnitPriceByFoodId(billInfo.FoodID)));
                    }
                    FormatFoodBillInfo();
                }
            }
            catch { }
        }

        private void dtgvBill_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                // Đặt giá trị canh giữa cho các ô trong từng dòng
                dtgvBill.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }
    }
}