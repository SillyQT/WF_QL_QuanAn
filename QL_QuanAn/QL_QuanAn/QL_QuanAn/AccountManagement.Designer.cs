namespace QL_QuanAn
{
    partial class AccountManagement
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AccountManagement));
            this.groupControl4 = new DevExpress.XtraEditors.GroupControl();
            this.dtgvAccount = new System.Windows.Forms.DataGridView();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.txbUserName = new System.Windows.Forms.TextBox();
            this.cbStatus = new System.Windows.Forms.ComboBox();
            this.groupControlGrantPermissions = new DevExpress.XtraEditors.GroupControl();
            this.radioEmployee = new System.Windows.Forms.RadioButton();
            this.radioAdmin = new System.Windows.Forms.RadioButton();
            this.groupControl3 = new DevExpress.XtraEditors.GroupControl();
            this.btnRemove = new DevExpress.XtraEditors.SimpleButton();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnReload = new DevExpress.XtraEditors.SimpleButton();
            this.btnEdit = new DevExpress.XtraEditors.SimpleButton();
            this.btnDelete = new DevExpress.XtraEditors.SimpleButton();
            this.btnAdd = new DevExpress.XtraEditors.SimpleButton();
            this.btnResetPassWord = new DevExpress.XtraEditors.SimpleButton();
            this.groupControlAccount = new DevExpress.XtraEditors.GroupControl();
            this.cbStaffName = new System.Windows.Forms.ComboBox();
            this.cbStaffID = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl4)).BeginInit();
            this.groupControl4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvAccount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControlGrantPermissions)).BeginInit();
            this.groupControlGrantPermissions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).BeginInit();
            this.groupControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControlAccount)).BeginInit();
            this.groupControlAccount.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupControl4
            // 
            this.groupControl4.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupControl4.AppearanceCaption.Options.UseFont = true;
            this.groupControl4.CaptionImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("groupControl4.CaptionImageOptions.Image")));
            this.groupControl4.Controls.Add(this.dtgvAccount);
            this.groupControl4.Location = new System.Drawing.Point(435, 7);
            this.groupControl4.Name = "groupControl4";
            this.groupControl4.Size = new System.Drawing.Size(805, 600);
            this.groupControl4.TabIndex = 1;
            this.groupControl4.Text = "Danh sách tài khoản";
            // 
            // dtgvAccount
            // 
            this.dtgvAccount.AllowUserToAddRows = false;
            this.dtgvAccount.AllowUserToDeleteRows = false;
            this.dtgvAccount.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.dtgvAccount.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgvAccount.Location = new System.Drawing.Point(4, 35);
            this.dtgvAccount.Name = "dtgvAccount";
            this.dtgvAccount.ReadOnly = true;
            this.dtgvAccount.RowHeadersWidth = 51;
            this.dtgvAccount.Size = new System.Drawing.Size(796, 560);
            this.dtgvAccount.TabIndex = 0;
            this.dtgvAccount.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtgvAccount_CellClick);
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(27, 51);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(108, 19);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Mã nhân viên";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Location = new System.Drawing.Point(26, 157);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(115, 19);
            this.labelControl2.TabIndex = 1;
            this.labelControl2.Text = "Tên nhân viên";
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl3.Appearance.Options.UseFont = true;
            this.labelControl3.Location = new System.Drawing.Point(27, 104);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(121, 19);
            this.labelControl3.TabIndex = 2;
            this.labelControl3.Text = "Tên đăng nhập";
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl5.Appearance.Options.UseFont = true;
            this.labelControl5.Location = new System.Drawing.Point(27, 210);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(85, 19);
            this.labelControl5.TabIndex = 4;
            this.labelControl5.Text = "Trạng thái";
            // 
            // txbUserName
            // 
            this.txbUserName.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txbUserName.Location = new System.Drawing.Point(162, 96);
            this.txbUserName.Name = "txbUserName";
            this.txbUserName.Size = new System.Drawing.Size(231, 33);
            this.txbUserName.TabIndex = 2;
            // 
            // cbStatus
            // 
            this.cbStatus.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbStatus.FormattingEnabled = true;
            this.cbStatus.Location = new System.Drawing.Point(162, 202);
            this.cbStatus.Name = "cbStatus";
            this.cbStatus.Size = new System.Drawing.Size(231, 33);
            this.cbStatus.TabIndex = 4;
            this.cbStatus.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cbStatus_KeyPress);
            // 
            // groupControlGrantPermissions
            // 
            this.groupControlGrantPermissions.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupControlGrantPermissions.AppearanceCaption.Options.UseFont = true;
            this.groupControlGrantPermissions.CaptionImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("groupControlGrantPermissions.CaptionImageOptions.Image")));
            this.groupControlGrantPermissions.Controls.Add(this.radioEmployee);
            this.groupControlGrantPermissions.Controls.Add(this.radioAdmin);
            this.groupControlGrantPermissions.Location = new System.Drawing.Point(5, 251);
            this.groupControlGrantPermissions.Name = "groupControlGrantPermissions";
            this.groupControlGrantPermissions.Size = new System.Drawing.Size(398, 100);
            this.groupControlGrantPermissions.TabIndex = 10;
            this.groupControlGrantPermissions.Text = "Cấp quyền";
            // 
            // radioEmployee
            // 
            this.radioEmployee.AutoSize = true;
            this.radioEmployee.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioEmployee.Location = new System.Drawing.Point(248, 49);
            this.radioEmployee.Name = "radioEmployee";
            this.radioEmployee.Size = new System.Drawing.Size(124, 27);
            this.radioEmployee.TabIndex = 6;
            this.radioEmployee.TabStop = true;
            this.radioEmployee.Text = "Nhân viên";
            this.radioEmployee.UseVisualStyleBackColor = true;
            // 
            // radioAdmin
            // 
            this.radioAdmin.AutoSize = true;
            this.radioAdmin.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioAdmin.Location = new System.Drawing.Point(56, 49);
            this.radioAdmin.Name = "radioAdmin";
            this.radioAdmin.Size = new System.Drawing.Size(106, 27);
            this.radioAdmin.TabIndex = 5;
            this.radioAdmin.TabStop = true;
            this.radioAdmin.Text = "Quản trị";
            this.radioAdmin.UseVisualStyleBackColor = true;
            // 
            // groupControl3
            // 
            this.groupControl3.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupControl3.AppearanceCaption.Options.UseFont = true;
            this.groupControl3.CaptionImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("groupControl3.CaptionImageOptions.Image")));
            this.groupControl3.Controls.Add(this.btnRemove);
            this.groupControl3.Controls.Add(this.btnSave);
            this.groupControl3.Controls.Add(this.btnReload);
            this.groupControl3.Controls.Add(this.btnEdit);
            this.groupControl3.Controls.Add(this.btnDelete);
            this.groupControl3.Controls.Add(this.btnAdd);
            this.groupControl3.Location = new System.Drawing.Point(5, 419);
            this.groupControl3.Name = "groupControl3";
            this.groupControl3.Size = new System.Drawing.Size(398, 176);
            this.groupControl3.TabIndex = 11;
            this.groupControl3.Text = "Xử lý";
            // 
            // btnRemove
            // 
            this.btnRemove.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRemove.Appearance.Options.UseFont = true;
            this.btnRemove.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnRemove.ImageOptions.Image")));
            this.btnRemove.Location = new System.Drawing.Point(275, 112);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(113, 48);
            this.btnRemove.TabIndex = 13;
            this.btnRemove.Text = "Hủy";
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnSave
            // 
            this.btnSave.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Appearance.Options.UseFont = true;
            this.btnSave.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.ImageOptions.Image")));
            this.btnSave.Location = new System.Drawing.Point(142, 112);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(113, 48);
            this.btnSave.TabIndex = 12;
            this.btnSave.Text = "Lưu";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnReload
            // 
            this.btnReload.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReload.Appearance.Options.UseFont = true;
            this.btnReload.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnReload.ImageOptions.Image")));
            this.btnReload.Location = new System.Drawing.Point(9, 112);
            this.btnReload.Name = "btnReload";
            this.btnReload.Size = new System.Drawing.Size(113, 48);
            this.btnReload.TabIndex = 11;
            this.btnReload.Text = "Tải lại";
            this.btnReload.Click += new System.EventHandler(this.btnReload_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEdit.Appearance.Options.UseFont = true;
            this.btnEdit.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnEdit.ImageOptions.Image")));
            this.btnEdit.Location = new System.Drawing.Point(275, 50);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(113, 48);
            this.btnEdit.TabIndex = 10;
            this.btnEdit.Text = "Sửa";
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.Appearance.Options.UseFont = true;
            this.btnDelete.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnDelete.ImageOptions.Image")));
            this.btnDelete.Location = new System.Drawing.Point(142, 50);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(113, 48);
            this.btnDelete.TabIndex = 9;
            this.btnDelete.Text = "Xóa";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.Appearance.Options.UseFont = true;
            this.btnAdd.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnAdd.ImageOptions.Image")));
            this.btnAdd.Location = new System.Drawing.Point(9, 50);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(113, 48);
            this.btnAdd.TabIndex = 8;
            this.btnAdd.Text = "Thêm";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnResetPassWord
            // 
            this.btnResetPassWord.Appearance.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnResetPassWord.Appearance.Options.UseFont = true;
            this.btnResetPassWord.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnResetPassWord.ImageOptions.Image")));
            this.btnResetPassWord.Location = new System.Drawing.Point(91, 362);
            this.btnResetPassWord.Name = "btnResetPassWord";
            this.btnResetPassWord.Size = new System.Drawing.Size(241, 43);
            this.btnResetPassWord.TabIndex = 7;
            this.btnResetPassWord.Text = "Đặt lại mật khẩu";
            this.btnResetPassWord.Click += new System.EventHandler(this.btnResetPassWord_Click);
            // 
            // groupControlAccount
            // 
            this.groupControlAccount.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupControlAccount.AppearanceCaption.Options.UseFont = true;
            this.groupControlAccount.CaptionImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("groupControlAccount.CaptionImageOptions.Image")));
            this.groupControlAccount.Controls.Add(this.cbStaffName);
            this.groupControlAccount.Controls.Add(this.cbStaffID);
            this.groupControlAccount.Controls.Add(this.btnResetPassWord);
            this.groupControlAccount.Controls.Add(this.groupControl3);
            this.groupControlAccount.Controls.Add(this.groupControlGrantPermissions);
            this.groupControlAccount.Controls.Add(this.cbStatus);
            this.groupControlAccount.Controls.Add(this.txbUserName);
            this.groupControlAccount.Controls.Add(this.labelControl5);
            this.groupControlAccount.Controls.Add(this.labelControl3);
            this.groupControlAccount.Controls.Add(this.labelControl2);
            this.groupControlAccount.Controls.Add(this.labelControl1);
            this.groupControlAccount.Location = new System.Drawing.Point(12, 8);
            this.groupControlAccount.Name = "groupControlAccount";
            this.groupControlAccount.Size = new System.Drawing.Size(408, 600);
            this.groupControlAccount.TabIndex = 0;
            this.groupControlAccount.Text = "Thông tin tài khoản";
            // 
            // cbStaffName
            // 
            this.cbStaffName.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbStaffName.FormattingEnabled = true;
            this.cbStaffName.Location = new System.Drawing.Point(162, 149);
            this.cbStaffName.Name = "cbStaffName";
            this.cbStaffName.Size = new System.Drawing.Size(231, 33);
            this.cbStaffName.TabIndex = 3;
            // 
            // cbStaffID
            // 
            this.cbStaffID.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbStaffID.FormattingEnabled = true;
            this.cbStaffID.Location = new System.Drawing.Point(162, 43);
            this.cbStaffID.Name = "cbStaffID";
            this.cbStaffID.Size = new System.Drawing.Size(231, 33);
            this.cbStaffID.TabIndex = 1;
            this.cbStaffID.SelectedIndexChanged += new System.EventHandler(this.cbStaffID_SelectedIndexChanged);
            this.cbStaffID.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cbStaffID_KeyPress);
            // 
            // AccountManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1252, 618);
            this.Controls.Add(this.groupControl4);
            this.Controls.Add(this.groupControlAccount);
            this.IconOptions.Image = global::QL_QuanAn.Properties.Resources.logo;
            this.Name = "AccountManagement";
            this.Text = "Quản lý tài khoản";
            ((System.ComponentModel.ISupportInitialize)(this.groupControl4)).EndInit();
            this.groupControl4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtgvAccount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControlGrantPermissions)).EndInit();
            this.groupControlGrantPermissions.ResumeLayout(false);
            this.groupControlGrantPermissions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).EndInit();
            this.groupControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControlAccount)).EndInit();
            this.groupControlAccount.ResumeLayout(false);
            this.groupControlAccount.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraEditors.GroupControl groupControl4;
        private System.Windows.Forms.DataGridView dtgvAccount;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private System.Windows.Forms.TextBox txbUserName;
        private System.Windows.Forms.ComboBox cbStatus;
        private DevExpress.XtraEditors.GroupControl groupControlGrantPermissions;
        private System.Windows.Forms.RadioButton radioEmployee;
        private System.Windows.Forms.RadioButton radioAdmin;
        private DevExpress.XtraEditors.GroupControl groupControl3;
        private DevExpress.XtraEditors.SimpleButton btnRemove;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnReload;
        private DevExpress.XtraEditors.SimpleButton btnEdit;
        private DevExpress.XtraEditors.SimpleButton btnDelete;
        private DevExpress.XtraEditors.SimpleButton btnAdd;
        private DevExpress.XtraEditors.SimpleButton btnResetPassWord;
        private DevExpress.XtraEditors.GroupControl groupControlAccount;
        private System.Windows.Forms.ComboBox cbStaffName;
        private System.Windows.Forms.ComboBox cbStaffID;
    }
}