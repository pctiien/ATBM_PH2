using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ATBM_HTTT_PH2.Service;
using ATBM_HTTT_PH2.Model;

namespace ATBM_HTTT_PH2.Form
{
    public partial class SINHVIENForm : System.Windows.Forms.Form
    {
        private readonly ISinhVienService _sinhVienService;
        private readonly IPhanCongService _phanCongService;

        public SINHVIENForm(ISinhVienService sinhVienService, IPhanCongService phanCongService)
        {
            InitializeComponent();
            _sinhVienService = sinhVienService;
            _phanCongService = phanCongService;
            InitializeForm();
        }

        private void InitializeForm()
        {
            try
            {
                var sinhVien = _sinhVienService.GetCurrentSinhVien();
                if (sinhVien == null)
                {
                    MessageBox.Show("Không tìm thấy thông tin sinh viên!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    return;
                }

                LoadPhanCongData(sinhVien.MASV);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi khởi tạo form: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void LoadPhanCongData(string masv)
        {
            try
            {
                List<MoMon> phanCongList = _phanCongService.GetPhanCongSinhVien(masv);
                if (phanCongList == null || phanCongList.Count == 0)
                {
                    MessageBox.Show("Không có phân công giảng dạy nào thuộc khoa của bạn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                dgvPhanCong.DataSource = phanCongList;
                // Đặt tên hiển thị cho các cột
                dgvPhanCong.Columns["MAMM"].HeaderText = "Mã môn mở";
                dgvPhanCong.Columns["MAHP"].HeaderText = "Mã học phần";
                dgvPhanCong.Columns["HK"].HeaderText = "Học kỳ";
                dgvPhanCong.Columns["NAM"].HeaderText = "Năm học";
                dgvPhanCong.Columns["MAGV"].HeaderText = "Mã giảng viên";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu phân công: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}