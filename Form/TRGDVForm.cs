using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ATBM_HTTT_PH2.Service;

namespace ATBM_HTTT_PH2.Form
{
    public partial class TRGDVForm : System.Windows.Forms.Form
    {
        private readonly INhanVienService _nhanVienService;

        public TRGDVForm(INhanVienService nhanVienService)
        {
            InitializeComponent();
            _nhanVienService = nhanVienService;
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
                    MessageBox.Show("Bạn không có quyền truy cập chức năng này! Chỉ trưởng đơn vị (TRGDV) được phép.", "Lỗi quyền", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    return;
                }

                LoadNhanVienData(user.MADV);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi khởi tạo form: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void LoadNhanVienData(string madv)
        {
            try
            {
                List<Model.NhanVien> nhanVienList = _nhanVienService.GetByDonVi(madv);
                if (nhanVienList == null || nhanVienList.Count == 0)
                {
                    MessageBox.Show("Không có nhân viên nào trong đơn vị này!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                dgvNhanVien.DataSource = nhanVienList;
                // Ẩn cột LUONG và PHUCAP
                if (dgvNhanVien.Columns["LUONG"] != null)
                    dgvNhanVien.Columns["LUONG"].Visible = false;
                if (dgvNhanVien.Columns["PHUCAP"] != null)
                    dgvNhanVien.Columns["PHUCAP"].Visible = false;

                // Đặt tên hiển thị cho các cột
                dgvNhanVien.Columns["MANV"].HeaderText = "Mã nhân viên";
                dgvNhanVien.Columns["HOTEN"].HeaderText = "Họ tên";
                dgvNhanVien.Columns["PHAI"].HeaderText = "Phái";
                dgvNhanVien.Columns["NGSINH"].HeaderText = "Ngày sinh";
                dgvNhanVien.Columns["DT"].HeaderText = "Số điện thoại";
                dgvNhanVien.Columns["VAITRO"].HeaderText = "Vai trò";
                dgvNhanVien.Columns["MADV"].HeaderText = "Mã đơn vị";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu nhân viên: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
