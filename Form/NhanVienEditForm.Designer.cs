namespace ATBM_HTTT_PH2.Form
{
    partial class NhanVienEditForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.txtMaNV = new System.Windows.Forms.TextBox();
            this.txtHoTen = new System.Windows.Forms.TextBox();
            this.cbPhai = new System.Windows.Forms.ComboBox();
            this.dtpNgaySinh = new System.Windows.Forms.DateTimePicker();
            this.txtLuong = new System.Windows.Forms.TextBox();
            this.txtPhuCap = new System.Windows.Forms.TextBox();
            this.txtDienThoai = new System.Windows.Forms.TextBox();
            this.txtVaiTro = new System.Windows.Forms.TextBox();
            this.txtMaDV = new System.Windows.Forms.TextBox();
            this.btnLuu = new System.Windows.Forms.Button();
            this.btnHuy = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // Các textbox và label bố trí lần lượt theo trật tự

            this.txtMaNV.PlaceholderText = "Mã nhân viên";
            this.txtHoTen.PlaceholderText = "Họ tên";
            this.cbPhai.Items.AddRange(new object[] { "Nam", "Nữ" });
            this.txtLuong.PlaceholderText = "Lương";
            this.txtPhuCap.PlaceholderText = "Phụ cấp";
            this.txtDienThoai.PlaceholderText = "Điện thoại";
            this.txtVaiTro.PlaceholderText = "Vai trò";
            this.txtMaDV.PlaceholderText = "Mã đơn vị";

            this.btnLuu.Text = "Lưu";
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);

            this.btnHuy.Text = "Hủy";
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);

            // Add controls to form
            var controls = new System.Windows.Forms.Control[] {
                txtMaNV, txtHoTen, cbPhai, dtpNgaySinh,
                txtLuong, txtPhuCap, txtDienThoai,
                txtVaiTro, txtMaDV, btnLuu, btnHuy
            };

            int y = 10;
            foreach (var ctrl in controls)
            {
                ctrl.Left = 20;
                ctrl.Top = y;
                ctrl.Width = 250;
                this.Controls.Add(ctrl);
                y += 35;
            }

            // Form
            this.Text = "Thông tin nhân viên";
            this.ClientSize = new System.Drawing.Size(300, y + 20);
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.TextBox txtMaNV;
        private System.Windows.Forms.TextBox txtHoTen;
        private System.Windows.Forms.ComboBox cbPhai;
        private System.Windows.Forms.DateTimePicker dtpNgaySinh;
        private System.Windows.Forms.TextBox txtLuong;
        private System.Windows.Forms.TextBox txtPhuCap;
        private System.Windows.Forms.TextBox txtDienThoai;
        private System.Windows.Forms.TextBox txtVaiTro;
        private System.Windows.Forms.TextBox txtMaDV;
        private System.Windows.Forms.Button btnLuu;
        private System.Windows.Forms.Button btnHuy;
    }
}
