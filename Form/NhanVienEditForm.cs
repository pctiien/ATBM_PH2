using System;
using System.Globalization;
using System.Windows.Forms;
using ATBM_HTTT_PH2.Model;
using ATBM_HTTT_PH2.Service;

namespace ATBM_HTTT_PH2.Form
{
    public partial class NhanVienEditForm : System.Windows.Forms.Form
    {
        public NhanVien NhanVienData { get; private set; }
        private INhanVienService _INhanVienService;

        private readonly bool _isEditMode;

        public NhanVienEditForm(INhanVienService iNhanVienService,NhanVien nhanVien = null)
        {
            InitializeComponent();
            _isEditMode = nhanVien != null;
            NhanVienData = nhanVien ?? new NhanVien();
            _INhanVienService = iNhanVienService;
            LoadData();
        }

        private void LoadData()
        {
            if (_isEditMode)
            {
                txtMaNV.Text = NhanVienData.MANV;
                txtHoTen.Text = NhanVienData.HOTEN;
                cbPhai.SelectedItem = NhanVienData.PHAI;
                dtpNgaySinh.Value = NhanVienData.NGSINH ?? DateTime.Now;
                txtLuong.Text = NhanVienData.LUONG?.ToString();
                txtPhuCap.Text = NhanVienData.PHUCAP?.ToString();
                txtDienThoai.Text = NhanVienData.DT;
                txtVaiTro.Text = NhanVienData.VAITRO;
                txtMaDV.Text = NhanVienData.MADV;

                txtMaNV.Enabled = false; // Không cho sửa Mã NV
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                if (!_isEditMode && string.IsNullOrWhiteSpace(txtMaNV.Text))
                {
                    MessageBox.Show("Mã nhân viên không được để trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                NhanVienData.MANV = txtMaNV.Text.Trim();
                NhanVienData.HOTEN = txtHoTen.Text.Trim();
                NhanVienData.PHAI = cbPhai.SelectedItem?.ToString();
                NhanVienData.NGSINH = dtpNgaySinh.Value;
                NhanVienData.LUONG = decimal.TryParse(txtLuong.Text, out var luong) ? luong : (decimal?)null;
                NhanVienData.PHUCAP = decimal.TryParse(txtPhuCap.Text, out var phuCap) ? phuCap : (decimal?)null;
                NhanVienData.DT = txtDienThoai.Text.Trim();
                NhanVienData.VAITRO = txtVaiTro.Text.Trim();
                NhanVienData.MADV = txtMaDV.Text.Trim();

                if(_isEditMode)
                    _INhanVienService.UpdateNhanVien(NhanVienData);
                else
                    _INhanVienService.AddNhanVien(NhanVienData);
                
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                MessageBox.Show($"Lỗi khi lưu nhân viên: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
