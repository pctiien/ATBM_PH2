using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ATBM_HTTT_PH2.Service;
using ATBM_HTTT_PH2.Model;
using ATBM_HTTT_PH2.Util;
using Oracle.ManagedDataAccess.Client;

namespace ATBM_HTTT_PH2.Form
{
    public partial class SINHVIENForm : System.Windows.Forms.Form
    {
        private readonly ISinhVienService _sinhVienService;
        private readonly IPhanCongService _phanCongService;
        private readonly IDangKyService _dangKyService;
        //private readonly IBangDiemService _bangDiemService;
        private readonly string _currentUser;
        private readonly SessionContext _sessionContext;
        private readonly OracleConnection _oracleConnection;

        public SINHVIENForm(ISinhVienService sinhVienService, IPhanCongService phanCongService, IDangKyService dangKyService, SessionContext sessionContext, OracleConnection oracleConnection)
        {
            InitializeComponent();
            _sinhVienService = sinhVienService;
            _phanCongService = phanCongService;
            _dangKyService = dangKyService;
            _currentUser = sessionContext.CurrentUser;
            _sessionContext = sessionContext;
            _oracleConnection = oracleConnection;
            //_bangDiemService = bangDiemService;

            LoadThongTinSinhVien();
            LoadMonMo();
            LoadDanhSachMonMo();
            LoadDanhSachMonDaDangKy();
            LoadBangDiem();

        }

        private void LoadBangDiem()
        {
            string role = _sessionContext.GetUserRole()?.ToUpper();
            if (role == "SINHVIEN")
            {
                dgvBangDiem.DataSource = _phanCongService.GetBangDiemBySinhVien(_currentUser);
            }
            else if (role == "GV")
            {
                dgvBangDiem.DataSource = _phanCongService.GetBangDiemByGiangVien(_currentUser);
            }
            else if (role == "NV PKT")
            {
                dgvBangDiem.DataSource = _phanCongService.GetAllBangDiem();
                dgvBangDiem.ReadOnly = false;
                dgvBangDiem.CellValueChanged += DgvBangDiem_CellValueChanged;
            }
        }

        private void DgvBangDiem_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var row = dgvBangDiem.Rows[e.RowIndex];
                var model = new BangDiemModel
                {
                    MaSV = row.Cells["MaSV"].Value.ToString(),
                    MaMM = row.Cells["MaMM"].Value.ToString(),
                    DiemQT = Convert.ToInt32(row.Cells["DiemQT"].Value),
                    DiemCK = Convert.ToInt32(row.Cells["DiemCK"].Value),
                    DiemTH = Convert.ToInt32(row.Cells["DiemTH"].Value),
                    DiemTK = Convert.ToInt32(row.Cells["DiemTK"].Value)
                };
                _phanCongService.UpdateDiem(model.MaSV, model.MaMM, model.DiemQT, model.DiemCK, model.DiemTH);
            }
        }

        private bool IsInFirst14Days()
        {
            string query = "SELECT TRUNC(SYSDATE) - TO_DATE('01-09-2024', 'DD-MM-YYYY') FROM DUAL";
            using (var cmd = new OracleCommand(query, _oracleConnection))
            {
                var result = Convert.ToInt32(cmd.ExecuteScalar());
                return result >= 0 && result <= 14;
            }
        }

        private void LoadThongTinSinhVien()
        {
            try
            {
                string currentUser = _sessionContext.CurrentUser;
                string role = _sessionContext.GetUserRole();

                var list = _sinhVienService.GetAllSinhVien(currentUser, role);
                dgvSinhVien.DataSource = list;
                dgvSinhVien.Columns["MASV"].HeaderText = "Mã nhân viên";
                dgvSinhVien.Columns["HOTEN"].HeaderText = "Họ tên";
                dgvSinhVien.Columns["PHAI"].HeaderText = "Phái";
                dgvSinhVien.Columns["NGSINH"].HeaderText = "Ngày sinh";
                dgvSinhVien.Columns["DCHI"].HeaderText = "Địa chỉ";
                dgvSinhVien.Columns["SDT"].HeaderText = "Số điện thoại";
                dgvSinhVien.Columns["KHOA"].HeaderText = "Mã khoa";
                dgvSinhVien.Columns["TINHTRANG"].HeaderText = "Tình trạng";
                dgvSinhVien.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách nhân viên: " + ex.Message);
            }
        }
        private void dgvSinhVien_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvSinhVien.CurrentRow != null && dgvSinhVien.CurrentRow.DataBoundItem is SinhVien sv)
            {
                txtMaSV.Text = sv.MASV;
                txtHoTen.Text = sv.HOTEN;
                txtPhai.Text = sv.PHAI;
                dateTimePicker1.Value = sv.NGSINH;
                txtDiaChi.Text = sv.DCHI;
                txtSDT.Text = sv.SDT;
                txtKhoa.Text = sv.KHOA;
                txtTinhTrang.Text = sv.TINHTRANG;

                // Disable tất cả trước
                txtMaSV.Enabled = false;
                txtHoTen.Enabled = false;
                txtPhai.Enabled = false;
                dateTimePicker1.Enabled = false;
                txtKhoa.Enabled = false;
                txtTinhTrang.Enabled = false;
                txtDiaChi.Enabled = false;
                txtSDT.Enabled = false;

                string role = _sessionContext.GetUserRole()?.Trim().ToUpper() ?? "";
                string currentUser = _sessionContext.CurrentUser?.Trim().ToUpper() ?? "";

                switch (role)
                {
                    case "SINHVIEN":
                        if (sv.MASV.Equals(currentUser, StringComparison.OrdinalIgnoreCase))
                        {
                            txtDiaChi.Enabled = true;
                            txtSDT.Enabled = true;
                        }
                        break;

                    case "NV PCTSV":
                        txtHoTen.Enabled = true;
                        txtPhai.Enabled = true;
                        dateTimePicker1.Enabled = true;
                        txtDiaChi.Enabled = true;
                        txtSDT.Enabled = true;
                        txtKhoa.Enabled = true;
                        // TINHTRANG không được sửa bởi NV PCTSV
                        break;

                    case "NV PĐT":
                        txtTinhTrang.Enabled = true; // chỉ được sửa tình trạng
                        break;

                    case "GV":
                        // Không được sửa gì cả, chỉ xem
                        break;

                    case "NV TCHC":
                        // Admin được sửa toàn bộ
                        txtHoTen.Enabled = true;
                        txtPhai.Enabled = true;
                        dateTimePicker1.Enabled = true;
                        txtDiaChi.Enabled = true;
                        txtSDT.Enabled = true;
                        txtKhoa.Enabled = true;
                        txtTinhTrang.Enabled = true;
                        break;
                }
            }
        }

        private void btnCapNhatSV_Click(object sender, EventArgs e)
        {
            try
            {
                bool result = _sinhVienService.UpdateSinhVienInfo(
                    txtMaSV.Text,
                    txtDiaChi.Text,
                    txtSDT.Text
                );

                if (result)
                {
                    MessageBox.Show("Cập nhật thông tin thành công.");
                    LoadThongTinSinhVien();
                }

                else
                    MessageBox.Show("Không thể cập nhật thông tin.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi cập nhật: " + ex.Message);
            }
        }

        private void LoadMonMo()
        {
            var list = _phanCongService.GetDanhSachMonMo();
            dgvPhanCong.DataSource = list;
        }

        private void LoadDanhSachMonMo()
        {
            var monMoList = _phanCongService.GetDanhSachMonMo();

            dgvMonMoDangKy.Columns.Clear(); // Xóa cũ để thêm lại checkbox

            dgvMonMoDangKy.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            var checkBoxColumn = new DataGridViewCheckBoxColumn
            {
                Name = "Chon",
                HeaderText = "Chọn",
                Width = 50
            };
            dgvMonMoDangKy.Columns.Add(checkBoxColumn);

            dgvMonMoDangKy.DataSource = monMoList;

        }

        private void btnDangKy_Click_1(object sender, EventArgs e)
        {
            string role = _sessionContext.GetUserRole().ToUpper();
            if ((role == "SINHVIEN" || role == "NV PĐT") && !IsInFirst14Days())
            {
                MessageBox.Show("Chỉ được đăng ký trong 14 ngày đầu học kỳ.");
                return;
            }

            try
            {
                foreach (DataGridViewRow row in dgvMonMoDangKy.Rows)
                {
                    if (Convert.ToBoolean(row.Cells["Chon"].Value) == true)
                    {
                        string maMonMo = row.Cells["MAMM"].Value.ToString();
                        _dangKyService.DangKyMonHoc(_currentUser, maMonMo);
                    }
                }
                MessageBox.Show("Đăng ký thành công.");
                LoadDanhSachMonDaDangKy();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi đăng ký: " + ex.Message);
            }
        }

        private void LoadDanhSachMonDaDangKy()
        {
            var ds = _dangKyService.GetDangKyBySinhVien(_currentUser);
            dgvDaDangKy.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvDaDangKy.DataSource = ds;

            dgvDaDangKy.Columns["MaMonMo"].HeaderText = "Mã môn mở";
            dgvDaDangKy.Columns["TenHocPhan"].HeaderText = "Tên học phần";
            dgvDaDangKy.Columns["MaGV"].HeaderText = "Mã GV";
            dgvDaDangKy.Columns["HoTenGV"].HeaderText = "Họ tên GV";
            dgvDaDangKy.Columns["SoTinChi"].HeaderText = "Số tín chỉ";
        }

      
        private void btnHuyDangKy_Click_1(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in dgvDaDangKy.SelectedRows)
                {
                    string maMonMo = row.Cells["MAMM"].Value.ToString();
                    _dangKyService.HuyDangKyMonHoc(_currentUser, maMonMo);
                }

                MessageBox.Show("Hủy đăng ký thành công.");
                LoadDanhSachMonDaDangKy();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hủy đăng ký: " + ex.Message);
            }
        }


    }
}