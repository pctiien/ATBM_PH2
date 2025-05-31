
using System;
using System.Data;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;

namespace OLSNotificationApp
{

 public partial class ReceiveNotificationForm : Form
    {
        public ReceiveNotificationForm()
        {
            InitializeComponent();
        }

        private void ReceiveNotificationForm_Load(object sender, EventArgs e)
        {
            using (OracleConnection conn = new OracleConnection("User Id=" + LoginForm.CurrentUser + ";Password=userpwd;Data Source=orcl;"))
            {
                conn.Open();
                OracleCommand cmd = new OracleCommand("SELECT NOIDUNG FROM THONGBAO", conn);
                OracleDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lstNotifications.Items.Add(reader.GetString(0));
                }
            }
        }
    }
}
