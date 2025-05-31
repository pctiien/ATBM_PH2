using ATBM_HTTT_PH2.Model;
using ATBM_HTTT_PH2.Service;

namespace ATBM_HTTT_PH2.Form
{
    public partial class NVTCHCForm : System.Windows.Forms.Form
    {
        private readonly INhanVienService _nhanVienService;

        public NVTCHCForm(INhanVienService nhanVienService)
        {
            InitializeComponent();
            _nhanVienService = nhanVienService;
            LoadNhanVienData();
        }

        private void LoadNhanVienData()
        {
            try
            {
                List<NhanVien> list = _nhanVienService.GetAll();
                dgvNhanVien.DataSource = list;

                dgvNhanVien.Columns["MANV"].HeaderText = "Mã NV";
                dgvNhanVien.Columns["HOTEN"].HeaderText = "Họ tên";
                dgvNhanVien.Columns["PHAI"].HeaderText = "Phái";
                dgvNhanVien.Columns["NGSINH"].HeaderText = "Ngày sinh";
                dgvNhanVien.Columns["DT"].HeaderText = "SĐT";
                dgvNhanVien.Columns["VAITRO"].HeaderText = "Vai trò";
                dgvNhanVien.Columns["MADV"].HeaderText = "Mã đơn vị";
                dgvNhanVien.Columns["LUONG"].HeaderText = "Lương";
                dgvNhanVien.Columns["PHUCAP"].HeaderText = "Phụ cấp";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var dialog = new NhanVienEditForm(_nhanVienService);
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                LoadNhanVienData();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvNhanVien.CurrentRow?.DataBoundItem is NhanVien nv)
            {
                var dialog = new NhanVienEditForm(_nhanVienService, nv);
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    LoadNhanVienData();
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvNhanVien.CurrentRow?.DataBoundItem is NhanVien nv)
            {
                var confirm = MessageBox.Show($"Xác nhận xóa nhân viên {nv.MANV}?", "Xác nhận", MessageBoxButtons.YesNo);
                if (confirm == DialogResult.Yes)
                {
                    _nhanVienService.DeleteNhanVien(nv.MANV);
                    LoadNhanVienData();
                }
            }
        }
    }
}
