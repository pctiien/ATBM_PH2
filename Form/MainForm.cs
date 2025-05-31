using System;
using System.Windows.Forms;
using ATBM_HTTT_PH2.Services;
using ATBM_HTTT_PH2.Service;
using ATBM_HTTT_PH2.Util;
using Oracle.ManagedDataAccess.Client;
using ATBM_HTTT_PH2.Form;

namespace ATBM_HTTT_PH2.Forms
{
    public partial class MainForm : System.Windows.Forms.Form
    {
        private readonly INotificationService _notificationService;
        private readonly INhanVienService _nhanVienService;
        private readonly IPhanCongService _phanCongService;
        private readonly ISinhVienService _sinhVienService;
        private readonly IDangKyService _dangKyService;
        //private readonly IBangDiemService _bangDiemService;
        private readonly SessionContext _sessionContext;
        private readonly OracleConnection _oracleConnection;
        private readonly string _currentUser;

        public MainForm(
            INotificationService notificationService,
            INhanVienService nhanVienService,
            IPhanCongService phanCongService,
            ISinhVienService sinhVienService,
            IDangKyService dangKyService,
            //IBangDiemService bangDiemService,
            SessionContext sessionContext,
            OracleConnection oracleConnection)
        {
            InitializeComponent();
            _notificationService = notificationService;
            _nhanVienService = nhanVienService;
            _phanCongService = phanCongService;
            _sinhVienService = sinhVienService;
            _dangKyService = dangKyService;
           // _bangDiemService = bangDiemService;
            _sessionContext = sessionContext;
            _oracleConnection = oracleConnection;
            _currentUser = sessionContext.CurrentUser;

            ConfigureInterface();
        }

        private void ConfigureInterface()
        {
            lblUserInfo.Text = $"Đăng nhập bởi: {_sessionContext.CurrentUser} - {_sessionContext.GetUserRole()}";
            tabControlMain.TabPages.Clear();

            string role = _sessionContext.GetUserRole()?.Trim().ToUpper() ?? "SINHVIEN";

            switch (role)
            {
                case "NVCB":
                    AddTab("Thông tin cá nhân", new NVCBForm(_nhanVienService));
                    AddTab("Thông báo", new NotificationForm(_notificationService, _currentUser));
                    break;

                case "TRGĐV":
                    AddTab("Thông tin cá nhân", new NVCBForm(_nhanVienService));
                    AddTab("Quản lý đơn vị", new TRGDVForm(_nhanVienService));
                    AddTab("Phân công giảng dạy", new TRGDV_MOMONForm(_nhanVienService, _phanCongService));
                    AddTab("Thông báo", new NotificationForm(_notificationService, _currentUser));
                    break;

                case "NV TCHC":
                    AddTab("Thông tin cá nhân", new NVCBForm(_nhanVienService));
                    AddTab("Quản lý nhân viên", new NVTCHCForm(_nhanVienService));
                    AddTab("Quản lý sinh viên", new SINHVIENForm(_sinhVienService, _phanCongService, _dangKyService, _sessionContext, _oracleConnection, _bangDiemService));
                    AddTab("Thông báo", new NotificationForm(_notificationService, _currentUser));
                    break;

                case "GV":
                    AddTab("Thông tin cá nhân", new NVCBForm(_nhanVienService));
                    AddTab("Phân công giảng dạy", new GVForm(_nhanVienService, _phanCongService));
                    AddTab("Quản lý sinh viên", new SINHVIENForm(_sinhVienService, _phanCongService, _dangKyService, _sessionContext, _oracleConnection, _bangDiemService));
                    AddTab("Thông báo", new NotificationForm(_notificationService, _currentUser));
                    break;

                case "NV PĐT":
                    AddTab("Thông tin cá nhân", new NVCBForm(_nhanVienService));
                    AddTab("Quản lý môn học", new NVPDTForm(_nhanVienService, _phanCongService));
                    AddTab("Quản lý sinh viên", new SINHVIENForm(_sinhVienService, _phanCongService, _dangKyService, _sessionContext, _oracleConnection, _bangDiemService));
                    AddTab("Thông báo", new NotificationForm(_notificationService, _currentUser));
                    break;

                case "NV PKT":
                    AddTab("Thông tin cá nhân", new NVCBForm(_nhanVienService));
                    AddTab("Quản lý sinh viên", new SINHVIENForm(_sinhVienService, _phanCongService, _dangKyService, _sessionContext, _oracleConnection, _bangDiemService));
                    break;

                case "NV CTSV":
                    AddTab("Thông tin cá nhân", new NVCBForm(_nhanVienService));
                    AddTab("Quản lý sinh viên", new SINHVIENForm(_sinhVienService, _phanCongService, _dangKyService, _sessionContext, _oracleConnection, _bangDiemService));
                    break;

                case "SINHVIEN":
                    AddTab("Thông tin cá nhân", new SINHVIENForm(_sinhVienService, _phanCongService, _dangKyService, _sessionContext, _oracleConnection, _bangDiemService));
                    AddTab("Thông báo", new NotificationForm(_notificationService, _currentUser));
                    break;

                default:
                    MessageBox.Show("Vai trò không được hỗ trợ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Close();
                    return;
            }
        }

        private void AddTab(string tabName, System.Windows.Forms.Form form)
        {
            var tabPage = new TabPage(tabName);
            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;
            tabPage.Controls.Add(form);
            tabControlMain.TabPages.Add(tabPage);
            form.Show();
        }

        private void Logout_Click(object sender, EventArgs e) => Close();
        private void Exit_Click(object sender, EventArgs e) => Application.Exit();

        private void About_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Hệ thống Quản lý Trường Đại học\nPhiên bản 1.0\nPhát triển bởi Nhóm X", "Về ứng dụng", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e) => Application.Exit();
    }
}
