using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ATBM_HTTT_PH2.Service;
using ATBM_HTTT_PH2.Model;

namespace ATBM_HTTT_PH2.Form
{
    public partial class NVPDTForm : System.Windows.Forms.Form
    {
        private readonly INhanVienService _nhanVienService;
        private readonly IPhanCongService _phanCongService;
        private readonly int _currentHocKy = 2; // Giả sử học kỳ hiện tại là 3
        private readonly int _currentNam = 2024; // Năm hiện tại

        public NVPDTForm(INhanVienService nhanVienService, IPhanCongService phanCongService)
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

                if (user.VAITRO != "NV PĐT")
                {
                    MessageBox.Show("Bạn không có quyền truy cập chức năng này! Chỉ nhân viên phòng đào tạo (NV PĐT) được phép.", "Lỗi quyền", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    return;
                }

                LoadPhanCongData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi khởi tạo form: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void LoadPhanCongData()
        {
            try
            {
                List<MoMon> phanCongList = _phanCongService.GetPhanCongHocKyHienTai(_currentHocKy, _currentNam);
                if (phanCongList == null || phanCongList.Count == 0)
                {
                    MessageBox.Show("Không có phân công giảng dạy nào trong học kỳ hiện tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void btnThem_Click(object sender, EventArgs e)
        {
            using (var inputForm = new System.Windows.Forms.Form())
            {
                inputForm.Text = "Thêm phân công giảng dạy";
                inputForm.Size = new System.Drawing.Size(300, 300);
                inputForm.FormBorderStyle = FormBorderStyle.FixedDialog;
                inputForm.MaximizeBox = false;
                inputForm.MinimizeBox = false;
                inputForm.StartPosition = FormStartPosition.CenterParent;

                var lblMAMM = new Label { Text = "Mã môn mở:", Location = new System.Drawing.Point(20, 20), AutoSize = true };
                var txtMAMM = new TextBox { Location = new System.Drawing.Point(120, 20), Width = 150 };
                var lblMAHP = new Label { Text = "Mã học phần:", Location = new System.Drawing.Point(20, 60), AutoSize = true };
                var txtMAHP = new TextBox { Location = new System.Drawing.Point(120, 60), Width = 150 };
                var lblMAGV = new Label { Text = "Mã giảng viên:", Location = new System.Drawing.Point(20, 100), AutoSize = true };
                var txtMAGV = new TextBox { Location = new System.Drawing.Point(120, 100), Width = 150 };
                var lblHK = new Label { Text = "Học kỳ:", Location = new System.Drawing.Point(20, 140), AutoSize = true };
                var txtHK = new TextBox { Location = new System.Drawing.Point(120, 140), Width = 150, Text = _currentHocKy.ToString(), ReadOnly = true };
                var lblNAM = new Label { Text = "Năm học:", Location = new System.Drawing.Point(20, 180), AutoSize = true };
                var txtNAM = new TextBox { Location = new System.Drawing.Point(120, 180), Width = 150, Text = _currentNam.ToString(), ReadOnly = true };
                var btnSave = new Button { Text = "Lưu", Location = new System.Drawing.Point(120, 220), Width = 70 };
                var btnCancel = new Button { Text = "Hủy", Location = new System.Drawing.Point(200, 220), Width = 70 };

                btnSave.Click += (s, ev) =>
                {
                    try
                    {
                        var moMon = new MoMon
                        {
                            MAMM = txtMAMM.Text.Trim(),
                            MAHP = txtMAHP.Text.Trim(),
                            MAGV = txtMAGV.Text.Trim(),
                            HK = _currentHocKy,
                            NAM = _currentNam
                        };
                        _phanCongService.AddMoMon(moMon);
                        MessageBox.Show("Thêm phân công thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        inputForm.Close();
                        LoadPhanCongData();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Lỗi khi thêm phân công: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                };

                btnCancel.Click += (s, ev) => inputForm.Close();

                inputForm.Controls.AddRange(new Control[] { lblMAMM, txtMAMM, lblMAHP, txtMAHP, lblMAGV, txtMAGV, lblHK, txtHK, lblNAM, txtNAM, btnSave, btnCancel });
                inputForm.ShowDialog();
            }
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            if (dgvPhanCong.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một dòng để cập nhật!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedRow = dgvPhanCong.SelectedRows[0].DataBoundItem as MoMon;
            using (var inputForm = new System.Windows.Forms.Form())
            {
                inputForm.Text = "Cập nhật phân công giảng dạy";
                inputForm.Size = new System.Drawing.Size(300, 300);
                inputForm.FormBorderStyle = FormBorderStyle.FixedDialog;
                inputForm.MaximizeBox = false;
                inputForm.MinimizeBox = false;
                inputForm.StartPosition = FormStartPosition.CenterParent;

                var lblMAMM = new Label { Text = "Mã môn mở:", Location = new System.Drawing.Point(20, 20), AutoSize = true };
                var txtMAMM = new TextBox { Location = new System.Drawing.Point(120, 20), Width = 150, Text = selectedRow.MAMM, ReadOnly = true };
                var lblMAHP = new Label { Text = "Mã học phần:", Location = new System.Drawing.Point(20, 60), AutoSize = true };
                var txtMAHP = new TextBox { Location = new System.Drawing.Point(120, 60), Width = 150, Text = selectedRow.MAHP };
                var lblMAGV = new Label { Text = "Mã giảng viên:", Location = new System.Drawing.Point(20, 100), AutoSize = true };
                var txtMAGV = new TextBox { Location = new System.Drawing.Point(120, 100), Width = 150, Text = selectedRow.MAGV };
                var lblHK = new Label { Text = "Học kỳ:", Location = new System.Drawing.Point(20, 140), AutoSize = true };
                var txtHK = new TextBox { Location = new System.Drawing.Point(120, 140), Width = 150, Text = selectedRow.HK.ToString(), ReadOnly = true };
                var lblNAM = new Label { Text = "Năm học:", Location = new System.Drawing.Point(20, 180), AutoSize = true };
                var txtNAM = new TextBox { Location = new System.Drawing.Point(120, 180), Width = 150, Text = selectedRow.NAM.ToString(), ReadOnly = true };
                var btnSave = new Button { Text = "Lưu", Location = new System.Drawing.Point(120, 220), Width = 70 };
                var btnCancel = new Button { Text = "Hủy", Location = new System.Drawing.Point(200, 220), Width = 70 };

                btnSave.Click += (s, ev) =>
                {
                    try
                    {
                        var moMon = new MoMon
                        {
                            MAMM = txtMAMM.Text.Trim(),
                            MAHP = txtMAHP.Text.Trim(),
                            MAGV = txtMAGV.Text.Trim(),
                            HK = selectedRow.HK,
                            NAM = selectedRow.NAM
                        };
                        _phanCongService.UpdateMoMon(moMon);
                        MessageBox.Show("Cập nhật phân công thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        inputForm.Close();
                        LoadPhanCongData();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Lỗi khi cập nhật phân công: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                };

                btnCancel.Click += (s, ev) => inputForm.Close();

                inputForm.Controls.AddRange(new Control[] { lblMAMM, txtMAMM, lblMAHP, txtMAHP, lblMAGV, txtMAGV, lblHK, txtHK, lblNAM, txtNAM, btnSave, btnCancel });
                inputForm.ShowDialog();
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvPhanCong.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một dòng để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedRow = dgvPhanCong.SelectedRows[0].DataBoundItem as MoMon;
            var result = MessageBox.Show($"Bạn có chắc muốn xóa phân công {selectedRow.MAMM}?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                try
                {
                    _phanCongService.DeleteMoMon(selectedRow.MAMM);
                    MessageBox.Show("Xóa phân công thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadPhanCongData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi xóa phân công: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}