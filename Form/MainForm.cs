using ATBM_HTTT_PH2.Service;
using ATBM_HTTT_PH2.Util;


namespace ATBM_HTTT_PH2.Form
{

    public partial class MainForm : System.Windows.Forms.Form
    {
        private readonly INhanVienService _nhanVienService;
        private readonly SessionContext _sessionContext;

        public MainForm(INhanVienService nhanVienService, SessionContext sessionContext)
        {
            InitializeComponent();
            _nhanVienService = nhanVienService;
            _sessionContext = sessionContext;
            ConfigureInterface();
        }

        private void ConfigureInterface()
        {
            // Cập nhật thông tin người dùng trên StatusStrip
            lblUserInfo.Text = $"Đăng nhập bởi: {_sessionContext.CurrentUser} - {_sessionContext.GetUserRole()}";
            tabControlMain.TabPages.Clear(); // Xóa các tab mặc định

            // Thêm các tab dựa trên vai trò
            string role = _sessionContext.GetUserRole();
            switch (role)
            {
                case "NVCB":
                    AddTab("Thông tin cá nhân", new NVCBForm(_nhanVienService));
                    break;
                case "TRGĐV":
                    AddTab("Thông tin cá nhân", new NVCBForm(_nhanVienService));
                    AddTab("Quản lý đơn vị", new TRGDVForm(_nhanVienService));
                    break;
                //case "NV TCHC":
                //    AddTab("Thông tin cá nhân", new NVCBForm(_nhanVienService));
                //    AddTab("Quản lý nhân viên", new NVTCHCForm(_nhanVienService));
                //    break;
                //case "GV":
                //    AddTab("Thông tin cá nhân", new NVCBForm(_nhanVienService));
                //    AddTab("Phân công giảng dạy", new GVForm(_nhanVienService));
                //    break;
                //case "NV PĐT":
                //    AddTab("Thông tin cá nhân", new NVCBForm(_nhanVienService));
                //    AddTab("Quản lý môn học", new NVPDTForm(_nhanVienService));
                //    break;
                //case "NV PKT":
                //    AddTab("Thông tin cá nhân", new NVCBForm(_nhanVienService));
                //    AddTab("Quản lý điểm", new NVPKTForm(_nhanVienService));
                //    break;
                //case "NV CTSV":
                //    AddTab("Thông tin cá nhân", new NVCBForm(_nhanVienService));
                //    AddTab("Quản lý sinh viên", new NVCTSVForm(_sinhVienService));
                //    break;
                //case "SINHVIEN":
                //    AddTab("Thông tin sinh viên", new SinhVienForm(_sinhVienService));
                //    break;
                default:
                    MessageBox.Show("Vai trò không được hỗ trợ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    return;
            }
        }

        private void AddTab(string tabName, System.Windows.Forms.Form form)
        {
            TabPage tabPage = new TabPage(tabName);
            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;
            tabPage.Controls.Add(form);
            tabControlMain.TabPages.Add(tabPage);
            form.Show();
        }

        private void Logout_Click(object sender, EventArgs e)
        {
            // Mở lại LoginForm và đóng MainForm
            //loginForm.Show();
            this.Close();
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void About_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Hệ thống Quản lý Trường Đại học\nPhiên bản 1.0\nPhát triển bởi Nhóm X", "Về ứng dụng", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }
    }
}
