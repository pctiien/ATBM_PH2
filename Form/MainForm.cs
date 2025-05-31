using System;
using System.Windows.Forms;
using ATBM_HTTT_PH2.Services;
using ATBM_HTTT_PH2.Service;
using ATBM_HTTT_PH2.Util;

namespace ATBM_HTTT_PH2.Forms
{
    public partial class MainForm : Form
    {
        private readonly INotificationService _notificationService;
        private readonly INhanVienService _nhanVienService;
        private readonly IPhanCongService _phanCongService;
        private readonly ISinhVienService _sinhVienService;
        private readonly SessionContext _sessionContext;
        private string _currentUser;

        public MainForm(INotificationService notificationService, INhanVienService nhanVienService, IPhanCongService phanCongService, ISinhVienService sinhVienService, SessionContext sessionContext)
        {
            InitializeComponent();
            _notificationService = notificationService;
            _nhanVienService = nhanVienService;
            _phanCongService = phanCongService;
            _sinhVienService = sinhVienService;
            _sessionContext = sessionContext;
        }

        public void SetUser(string username)
        {
            _currentUser = username;
            LoadNotifications();
            ConfigureInterface();
        }

        private void LoadNotifications()
        {
            var notifications = _notificationService.GetNotificationsForUser(_currentUser);
            dgvNotifications.DataSource = notifications;
        }

        private void ConfigureInterface()
        {
            lblUserInfo.Text = $"Đăng nhập bởi: {_sessionContext.CurrentUser} - {_sessionContext.GetUserRole()}";
            tabControlMain.TabPages.Clear();

            string role = _sessionContext.GetUserRole();
            switch (role)
            {
                case "NVCB":
                    AddTab("Thông tin cá nhân", new NVCBForm(_nhanVienService));
                    AddTab("Thông báo", new NotificationForm(_notificationService, _currentUser));
                    break;
                case "TRGĐV":
                    AddTab("Thông tin cá nhân", new NVCBForm(_nhanVienService));
                    AddTab("Quản lý đơn vị", new TRGDVForm(_nhanVienService));
                    AddTab("Quản lý phân công giảng dạy trong đơn vị", new TRGDV_MOMONForm(_nhanVienService, _phanCongService));
                    AddTab("Thông báo", new NotificationForm(_notificationService, _currentUser));
                    break;
                case "NV TCHC":
                    AddTab("Thông tin cá nhân", new NVCBForm(_nhanVienService));
                    AddTab("Quản lý nhân viên", new NVTCHCForm(_nhanVienService));
                    AddTab("Thông báo", new NotificationForm(_notificationService, _currentUser));
                    break;
                case "GV":
                    AddTab("Thông tin cá nhân", new NVCBForm(_nhanVienService));
                    AddTab("Phân công giảng dạy", new GVForm(_nhanVienService, _phanCongService));
                    AddTab("Thông báo", new NotificationForm(_notificationService, _currentUser));
                    break;
                case "NV PĐT":
                    AddTab("Thông tin cá nhân", new NVCBForm(_nhanVienService));
                    AddTab("Quản lý môn học", new NVPDTForm(_nhanVienService, _phanCongService));
                    AddTab("Thông báo", new NotificationForm(_notificationService, _currentUser));
                    break;
                case "SINHVIEN":
                    AddTab("Môn mở của khoa", new SINHVIENForm(_sinhVienService, _phanCongService));
                    AddTab("Thông báo", new NotificationForm(_notificationService, _currentUser));
                    break;
                default:
                    MessageBox.Show("Vai trò không được hỗ trợ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    return;
            }
        }

        private void AddTab(string tabName, Form form)
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
            this.Close();
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void About_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Hệ thống Quản lý Trường Đại học\nPhiên bản 1.0\nPhát triển bởi Nhóm X", "Về ứng dụng", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
