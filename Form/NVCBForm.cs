using System.Windows.Forms;
using ATBM_HTTT_PH2.Service;

namespace ATBM_HTTT_PH2.Form
{
    public partial class NVCBForm : System.Windows.Forms.Form
    {
        private readonly INhanVienService _nhanVienService;

        public NVCBForm(INhanVienService nhanVienService)
        {
            InitializeComponent();
            _nhanVienService = nhanVienService;
            LoadUserData();
        }

        private void LoadUserData()
        {
            try
            {
                var user = _nhanVienService.GetCurrentUser();
                if (user != null)
                {
                    txtMANV.Text = user.MANV;
                    txtHOTEN.Text = user.HOTEN;
                    txtPHAI.Text = user.PHAI;
                    txtNGSINH.Text = user.NGSINH?.ToString("dd/MM/yyyy");
                    txtLUONG.Text = user.LUONG?.ToString("N0");
                    txtPHUCAP.Text = user.PHUCAP?.ToString("N0");
                    txtDT.Text = user.DT;
                    txtVAITRO.Text = user.VAITRO;
                    txtMADV.Text = user.MADV;

                    // Đặt các TextBox ở chế độ chỉ đọc, trừ txtDT
                    txtMANV.ReadOnly = true;
                    txtHOTEN.ReadOnly = true;
                    txtPHAI.ReadOnly = true;
                    txtNGSINH.ReadOnly = true;
                    txtLUONG.ReadOnly = true;
                    txtPHUCAP.ReadOnly = true;
                    txtVAITRO.ReadOnly = true;
                    txtMADV.ReadOnly = true;
                }
                else
                {
                    MessageBox.Show("Không tìm thấy thông tin người dùng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdatePhone_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtDT.Text))
                {
                    MessageBox.Show("Số điện thoại không được để trống!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                _nhanVienService.UpdatePhone(txtDT.Text);
                MessageBox.Show("Cập nhật số điện thoại thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi cập nhật số điện thoại: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
