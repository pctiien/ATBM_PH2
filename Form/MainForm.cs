using System;
using System.Windows.Forms;
using ATBM_HTTT_PH2.Services;

namespace ATBM_HTTT_PH2.Forms
{
    public partial class MainForm : Form
    {
        private readonly INotificationService _notificationService;
        private string _currentUser;

        public MainForm(INotificationService notificationService)
        {
            InitializeComponent();
            _notificationService = notificationService;
        }

        public void SetUser(string username)
        {
            _currentUser = username;
            LoadNotifications();
        }

        private void LoadNotifications()
        {
            var notifications = _notificationService.GetNotificationsForUser(_currentUser);
            dgvNotifications.DataSource = notifications;
        }
    }
}
