namespace ATBM_HTTT_PH2.Form
{
    partial class SINHVIENForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabThongTin;
        private System.Windows.Forms.TabPage tabMonMo;
        private System.Windows.Forms.DataGridView dgvPhanCong;


        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            tabControl = new TabControl();
            tabThongTin = new TabPage();
            dgvSinhVien = new DataGridView();
            panel1 = new Panel();
            txtPhai = new TextBox();
            txtKhoa = new TextBox();
            dateTimePicker1 = new DateTimePicker();
            txtHoTen = new TextBox();
            txtMaSV = new TextBox();
            txtSDT = new TextBox();
            txtDiaChi = new TextBox();
            txtTinhTrang = new TextBox();
            label8 = new Label();
            label7 = new Label();
            label6 = new Label();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            btnCapNhatSV = new Button();
            tabMonMo = new TabPage();
            dgvPhanCong = new DataGridView();
            tabDangKy = new TabPage();
            label9 = new Label();
            dgvDaDangKy = new DataGridView();
            panel2 = new Panel();
            dgvMonMoDangKy = new DataGridView();
            btnHuyDangKy = new Button();
            btnDangKy = new Button();
            label10 = new Label();
            dgvBangDiem = new DataGridView();
            tabControl.SuspendLayout();
            tabThongTin.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvSinhVien).BeginInit();
            panel1.SuspendLayout();
            tabMonMo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvPhanCong).BeginInit();
            tabDangKy.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvDaDangKy).BeginInit();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvMonMoDangKy).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvBangDiem).BeginInit();
            SuspendLayout();
            // 
            // tabControl
            // 
            tabControl.Controls.Add(tabThongTin);
            tabControl.Controls.Add(tabMonMo);
            tabControl.Controls.Add(tabDangKy);
            tabControl.Dock = DockStyle.Fill;
            tabControl.Location = new Point(0, 0);
            tabControl.Name = "tabControl";
            tabControl.SelectedIndex = 0;
            tabControl.Size = new Size(992, 665);
            tabControl.TabIndex = 0;
            // 
            // tabThongTin
            // 
            tabThongTin.Controls.Add(dgvSinhVien);
            tabThongTin.Controls.Add(panel1);
            tabThongTin.Location = new Point(4, 29);
            tabThongTin.Name = "tabThongTin";
            tabThongTin.Padding = new Padding(3);
            tabThongTin.Size = new Size(984, 632);
            tabThongTin.TabIndex = 0;
            tabThongTin.Text = "Thông tin sinh viên";
            // 
            // dgvSinhVien
            // 
            dgvSinhVien.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvSinhVien.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvSinhVien.Location = new Point(8, 274);
            dgvSinhVien.Name = "dgvSinhVien";
            dgvSinhVien.RowHeadersWidth = 51;
            dgvSinhVien.Size = new Size(968, 334);
            dgvSinhVien.TabIndex = 1;
            dgvSinhVien.SelectionChanged += dgvSinhVien_SelectionChanged;
            // 
            // panel1
            // 
            panel1.Controls.Add(txtPhai);
            panel1.Controls.Add(txtKhoa);
            panel1.Controls.Add(dateTimePicker1);
            panel1.Controls.Add(txtHoTen);
            panel1.Controls.Add(txtMaSV);
            panel1.Controls.Add(txtSDT);
            panel1.Controls.Add(txtDiaChi);
            panel1.Controls.Add(txtTinhTrang);
            panel1.Controls.Add(label8);
            panel1.Controls.Add(label7);
            panel1.Controls.Add(label6);
            panel1.Controls.Add(label5);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(btnCapNhatSV);
            panel1.Location = new Point(8, 6);
            panel1.Name = "panel1";
            panel1.Size = new Size(968, 251);
            panel1.TabIndex = 0;
            // 
            // txtPhai
            // 
            txtPhai.Location = new Point(97, 148);
            txtPhai.Name = "txtPhai";
            txtPhai.Size = new Size(144, 27);
            txtPhai.TabIndex = 20;
            // 
            // txtKhoa
            // 
            txtKhoa.Location = new Point(788, 24);
            txtKhoa.Name = "txtKhoa";
            txtKhoa.Size = new Size(144, 27);
            txtKhoa.TabIndex = 19;
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.Format = DateTimePickerFormat.Short;
            dateTimePicker1.Location = new Point(422, 24);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(218, 27);
            dateTimePicker1.TabIndex = 18;
            // 
            // txtHoTen
            // 
            txtHoTen.Location = new Point(97, 87);
            txtHoTen.Name = "txtHoTen";
            txtHoTen.Size = new Size(151, 27);
            txtHoTen.TabIndex = 15;
            // 
            // txtMaSV
            // 
            txtMaSV.Location = new Point(97, 21);
            txtMaSV.Name = "txtMaSV";
            txtMaSV.Size = new Size(158, 27);
            txtMaSV.TabIndex = 14;
            // 
            // txtSDT
            // 
            txtSDT.Location = new Point(422, 148);
            txtSDT.Name = "txtSDT";
            txtSDT.Size = new Size(218, 27);
            txtSDT.TabIndex = 13;
            // 
            // txtDiaChi
            // 
            txtDiaChi.Location = new Point(422, 84);
            txtDiaChi.Name = "txtDiaChi";
            txtDiaChi.Size = new Size(218, 27);
            txtDiaChi.TabIndex = 12;
            // 
            // txtTinhTrang
            // 
            txtTinhTrang.Location = new Point(788, 84);
            txtTinhTrang.Name = "txtTinhTrang";
            txtTinhTrang.Size = new Size(144, 27);
            txtTinhTrang.TabIndex = 11;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(686, 87);
            label8.Name = "label8";
            label8.Size = new Size(76, 20);
            label8.TabIndex = 10;
            label8.Text = "Tình trạng";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(686, 24);
            label7.Name = "label7";
            label7.Size = new Size(43, 20);
            label7.TabIndex = 9;
            label7.Text = "Khoa";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(321, 155);
            label6.Name = "label6";
            label6.Size = new Size(36, 20);
            label6.TabIndex = 8;
            label6.Text = "SĐT";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(321, 87);
            label5.Name = "label5";
            label5.Size = new Size(55, 20);
            label5.TabIndex = 7;
            label5.Text = "Địa chỉ";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(321, 24);
            label4.Name = "label4";
            label4.Size = new Size(74, 20);
            label4.TabIndex = 6;
            label4.Text = "Ngày sinh";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(24, 155);
            label3.Name = "label3";
            label3.Size = new Size(37, 20);
            label3.TabIndex = 5;
            label3.Text = "Phái";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(24, 87);
            label2.Name = "label2";
            label2.Size = new Size(54, 20);
            label2.TabIndex = 4;
            label2.Text = "Họ tên";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(24, 24);
            label1.Name = "label1";
            label1.Size = new Size(51, 20);
            label1.TabIndex = 3;
            label1.Text = "Mã SV";
            // 
            // btnCapNhatSV
            // 
            btnCapNhatSV.Location = new Point(435, 219);
            btnCapNhatSV.Name = "btnCapNhatSV";
            btnCapNhatSV.Size = new Size(94, 29);
            btnCapNhatSV.TabIndex = 1;
            btnCapNhatSV.Text = "Cập nhật";
            btnCapNhatSV.UseVisualStyleBackColor = true;
            btnCapNhatSV.Click += btnCapNhatSV_Click;
            // 
            // tabMonMo
            // 
            tabMonMo.Controls.Add(dgvPhanCong);
            tabMonMo.Location = new Point(4, 29);
            tabMonMo.Name = "tabMonMo";
            tabMonMo.Padding = new Padding(3);
            tabMonMo.Size = new Size(984, 632);
            tabMonMo.TabIndex = 1;
            tabMonMo.Text = "Môn mở";
            // 
            // dgvPhanCong
            // 
            dgvPhanCong.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvPhanCong.ColumnHeadersHeight = 29;
            dgvPhanCong.Dock = DockStyle.Fill;
            dgvPhanCong.Location = new Point(3, 3);
            dgvPhanCong.Name = "dgvPhanCong";
            dgvPhanCong.RowHeadersWidth = 51;
            dgvPhanCong.Size = new Size(978, 626);
            dgvPhanCong.TabIndex = 0;
            // 
            // tabDangKy
            // 
            tabDangKy.Controls.Add(dgvBangDiem);
            tabDangKy.Controls.Add(label10);
            tabDangKy.Controls.Add(label9);
            tabDangKy.Controls.Add(dgvDaDangKy);
            tabDangKy.Controls.Add(panel2);
            tabDangKy.Location = new Point(4, 29);
            tabDangKy.Name = "tabDangKy";
            tabDangKy.Padding = new Padding(3);
            tabDangKy.Size = new Size(984, 632);
            tabDangKy.TabIndex = 2;
            tabDangKy.Text = "Đăng ký môn";
            tabDangKy.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI Black", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 163);
            label9.ForeColor = SystemColors.ActiveCaptionText;
            label9.Location = new Point(85, 233);
            label9.Name = "label9";
            label9.Size = new Size(298, 25);
            label9.TabIndex = 3;
            label9.Text = "DANH SÁCH MÔN ĐÃ ĐĂNG KÝ";
            // 
            // dgvDaDangKy
            // 
            dgvDaDangKy.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvDaDangKy.Location = new Point(13, 261);
            dgvDaDangKy.Name = "dgvDaDangKy";
            dgvDaDangKy.RowHeadersWidth = 51;
            dgvDaDangKy.Size = new Size(478, 363);
            dgvDaDangKy.TabIndex = 1;
            // 
            // panel2
            // 
            panel2.Controls.Add(dgvMonMoDangKy);
            panel2.Controls.Add(btnHuyDangKy);
            panel2.Controls.Add(btnDangKy);
            panel2.Location = new Point(8, 6);
            panel2.Name = "panel2";
            panel2.Size = new Size(968, 212);
            panel2.TabIndex = 0;
            // 
            // dgvMonMoDangKy
            // 
            dgvMonMoDangKy.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvMonMoDangKy.Location = new Point(3, 3);
            dgvMonMoDangKy.Name = "dgvMonMoDangKy";
            dgvMonMoDangKy.RowHeadersWidth = 51;
            dgvMonMoDangKy.Size = new Size(823, 206);
            dgvMonMoDangKy.TabIndex = 3;
            // 
            // btnHuyDangKy
            // 
            btnHuyDangKy.Location = new Point(842, 89);
            btnHuyDangKy.Name = "btnHuyDangKy";
            btnHuyDangKy.Size = new Size(111, 48);
            btnHuyDangKy.TabIndex = 2;
            btnHuyDangKy.Text = "Hủy đăng ký";
            btnHuyDangKy.UseVisualStyleBackColor = true;
            btnHuyDangKy.Click += btnHuyDangKy_Click_1;
            // 
            // btnDangKy
            // 
            btnDangKy.Location = new Point(842, 15);
            btnDangKy.Name = "btnDangKy";
            btnDangKy.Size = new Size(111, 48);
            btnDangKy.TabIndex = 1;
            btnDangKy.Text = "Đăng ký";
            btnDangKy.UseVisualStyleBackColor = true;
            btnDangKy.Click += btnDangKy_Click_1;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Segoe UI Black", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 163);
            label10.ForeColor = SystemColors.ActiveCaptionText;
            label10.Location = new Point(697, 233);
            label10.Name = "label10";
            label10.Size = new Size(119, 25);
            label10.TabIndex = 4;
            label10.Text = "BẢNG ĐIỂM";
            // 
            // dgvBangDiem
            // 
            dgvBangDiem.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvBangDiem.Location = new Point(500, 261);
            dgvBangDiem.Name = "dgvBangDiem";
            dgvBangDiem.RowHeadersWidth = 51;
            dgvBangDiem.Size = new Size(478, 363);
            dgvBangDiem.TabIndex = 5;
            // 
            // SINHVIENForm
            // 
            BackgroundImageLayout = ImageLayout.Center;
            ClientSize = new Size(992, 665);
            Controls.Add(tabControl);
            Name = "SINHVIENForm";
            Text = "Sinh viên - Thông tin & môn học";
            tabControl.ResumeLayout(false);
            tabThongTin.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvSinhVien).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            tabMonMo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvPhanCong).EndInit();
            tabDangKy.ResumeLayout(false);
            tabDangKy.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvDaDangKy).EndInit();
            panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvMonMoDangKy).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvBangDiem).EndInit();
            ResumeLayout(false);
        }

        private DataGridView dgvSinhVien;
        private Panel panel1;
        private Label label8;
        private Label label7;
        private Label label6;
        private Label label5;
        private Label label4;
        private Label label3;
        private Label label2;
        private Label label1;
        private Button button3;
        private Button btnCapNhatSV;
        private Button button1;
        private DateTimePicker dateTimePicker1;
        private TextBox txtHoTen;
        private TextBox txtMaSV;
        private TextBox txtSDT;
        private TextBox txtDiaChi;
        private TextBox txtTinhTrang;
        private TabPage tabDangKy;
        private DataGridView dgvDaDangKy;
        private Panel panel2;
        private Button btnHuyDangKy;
        private Button btnDangKy;
        private Label label9;
        private DataGridView dgvMonMoDangKy;
        private TextBox txtKhoa;
        private TextBox txtPhai;
        private DataGridView dgvBangDiem;
        private Label label10;
    }
}
