using System;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualBasic.ApplicationServices;
using Oracle.ManagedDataAccess.Client;
using ATBM_HTTT_PH2.Util;
namespace ATBM_HTTT_PH2.Forms
{
    public partial class LoginForm : Form
    {
        private readonly ServiceCollection services;
        public LoginForm(ServiceCollection services)
        {
            InitializeComponent();
            this.services = services;
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            string connectionString;

            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;
            string serviceNameOrSID = txtServiceName.Text;

            try
            {
                var oracleConfig = OracleConfig.GetOracleConfig();
                connectionString = $"User Id={username};Password={password};Data Source={oracleConfig.HostName}:{oracleConfig.OraclePort}/{serviceNameOrSID};";

                if (username.Equals("SYS"))
                    connectionString += "DBA Privilege = SYSDBA;";

                var factory = new OracleConnectionFactory(connectionString);
                var connection = factory.createConnection();

                services.AddSingleton(new OracleConnectionFactory(connectionString));
                services.AddScoped(provider =>
                {
                    var factory = provider.GetRequiredService<OracleConnectionFactory>();
                    return factory.createConnection();
                });

                MessageBox.Show("Login successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Login failed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}