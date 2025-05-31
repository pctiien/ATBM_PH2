using ATBM_HTTT_PH2.Service;
using ATBM_HTTT_PH2.Util;
using Oracle.ManagedDataAccess.Client;


namespace ATBM_HTTT_PH2.Form
{

    public partial class MainForm : System.Windows.Forms.Form
    {
        private readonly INhanVienService _nhanVienService;
        private readonly IPhanCongService _phanCongService;
        private readonly ISinhVienService _sinhVienService;
        private readonly IDangKyService _dangKyService;
        private readonly SessionContext _sessionContext;
        private readonly OracleConnection _oracleConnection;

        public MainForm(INhanVienService nhanVienService,IPhanCongService phanCongService, ISinhVienService sinhVienService, IDangKyService dangKyService, SessionContext sessionContext, OracleConnection oracleConnection)
        {
            InitializeComponent();
            _nhanVienService = nhanVienService;
            _phanCongService = phanCongService;
            _sinhVienService = sinhVienService;
            _dangKyService = dangKyService;
            _sessionContext = sessionContext;
            _oracleConnection = oracleConnection;
            ConfigureInterface();
        }

        private void ConfigureInterface()
        {
            
            // Cập nhật thông tin người dùng trên StatusStrip
            lblUserInfo.Text = $"Đăng nhập bởi: {_sessionContext.CurrentUser} - {_sessionContext.GetUserRole()}";
            tabControlMain.TabPages.Clear(); // Xóa các tab mặc định

            // Thêm các tab dựa trên vai trò
            string currentUser = _sessionContext.CurrentUser;
            string role = _sessionContext.GetUserRole()?.Trim().ToUpper() ?? "SINHVIEN";


            switch (role)
            {
                case "NVCB":
                    AddTab("Thông tin cá nhân", new NVCBForm(_nhanVienService));
                    break;
                case "TRGĐV":
                    AddTab("Thông tin cá nhân", new NVCBForm(_nhanVienService));
                    AddTab("Quản lý đơn vị", new TRGDVForm(_nhanVienService));
                    AddTab("Quản lý phân công giảng dạy trong đơn vị", new TRGDV_MOMONForm(_nhanVienService,_phanCongService));
                    break;
                case "NV TCHC":                 
                    AddTab("Thông tin cá nhân", new NVCBForm(_nhanVienService));
                    AddTab("Quản lý nhân viên", new NVTCHCForm(_nhanVienService));
                    AddTab("Quản lý sinh viên", new SINHVIENForm(_sinhVienService, _phanCongService, _dangKyService, _sessionContext, _oracleConnection));
                    break;
                case "GV":
                    AddTab("Thông tin cá nhân", new NVCBForm(_nhanVienService));
                    AddTab("Phân công giảng dạy", new GVForm(_nhanVienService,_phanCongService));
                    AddTab("Quản lý sinh viên", new SINHVIENForm(_sinhVienService, _phanCongService, _dangKyService, _sessionContext, _oracleConnection));
                    break;
                case "NV PĐT":
                    AddTab("Thông tin cá nhân", new NVCBForm(_nhanVienService));
                    AddTab("Quản lý môn học", new NVPDTForm(_nhanVienService, _phanCongService));
                    AddTab("Quản lý sinh viên", new SINHVIENForm(_sinhVienService, _phanCongService, _dangKyService, _sessionContext, _oracleConnection));
                    break;
                case "NV PKT":
                    
                    AddTab("Thông tin cá nhân", new NVCBForm(_nhanVienService));
                    AddTab("Quản lý sinh viên", new SINHVIENForm(_sinhVienService, _phanCongService, _dangKyService, _sessionContext, _oracleConnection));
                    //AddTab("Quản lý điểm", new NVPKTForm(_nhanVienService));
                    break;
                case "NV CTSV":
                    
                    AddTab("Thông tin cá nhân", new NVCBForm(_nhanVienService));
                    AddTab("Quản lý sinh viên", new SINHVIENForm(_sinhVienService, _phanCongService, _dangKyService, _sessionContext, _oracleConnection));
                    //AddTab("Quản lý sinh viên", new NVCTSVForm(_sinhVienService));
                    break;
                case "SINHVIEN":
                    AddTab("Thông tin cá nhân", new SINHVIENForm(_sinhVienService, _phanCongService, _dangKyService, _sessionContext, _oracleConnection));
                    break;
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
