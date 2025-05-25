﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ATBM_HTTT_PH2.Service;
using ATBM_HTTT_PH2.Model;

namespace ATBM_HTTT_PH2.Form
{
    public partial class TRGDV_MOMONForm : System.Windows.Forms.Form
    {
        private readonly INhanVienService _nhanVienService;
        private readonly IPhanCongService _phanCongService;

        public TRGDV_MOMONForm(INhanVienService nhanVienService, IPhanCongService phanCongService)
        {
            InitializeComponent();
            _nhanVienService = nhanVienService;
            _phanCongService = phanCongService;
            InitializeForm();
        }

        private void InitializeForm()
        {
            try
            {
                var user = _nhanVienService.GetCurrentUser();
                if (user == null)
                {
                    MessageBox.Show("Không tìm thấy thông tin người dùng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    return;
                }

                if (user.VAITRO != "TRGĐV")
                {
                    MessageBox.Show("Bạn không có quyền truy cập chức năng này! Chỉ trưởng đơn vị (TRGĐV) được phép.", "Lỗi quyền", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    return;
                }

                LoadPhanCongData(user.MANV);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi khởi tạo form: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void LoadPhanCongData(string manv)
        {
            try
            {
                List<MoMon> phanCongList = _phanCongService.GetPhanCongTRGDV(manv);
                if (phanCongList == null || phanCongList.Count == 0)
                {
                    MessageBox.Show("Không có phân công giảng dạy nào trong đơn vị này!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                dgvPhanCong.DataSource = phanCongList;
                // Đặt tên hiển thị cho các cột
                dgvPhanCong.Columns["MAHP"].HeaderText = "Mã học phần";
                dgvPhanCong.Columns["HK"].HeaderText = "Học kỳ";
                dgvPhanCong.Columns["NAM"].HeaderText = "Năm học";
                dgvPhanCong.Columns["MAGV"].HeaderText = "Mã giảng viên";
                dgvPhanCong.Columns["MAMM"].HeaderText = "Mã mở môn";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu phân công: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}