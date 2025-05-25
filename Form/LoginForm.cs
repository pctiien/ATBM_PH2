using Microsoft.Extensions.DependencyInjection;
using ATBM_HTTT_PH2.Util;
using ATBM_HTTT_PH2.Form;
using ATBM_HTTT_PH2.Repository;
using ATBM_HTTT_PH2.Service;
using UniversityManagementSystem.Repositories;
using System.Windows.Forms;
namespace ATBM_HTTT_PH2.Form
{
    public partial class LoginForm : System.Windows.Forms.Form
    {
        private readonly ServiceCollection services;
        public LoginForm()
        {
            InitializeComponent();
            this.services = new ServiceCollection();
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
                services.AddScoped<INhanVienRepository, NhanVienRepository>();
                services.AddScoped<INhanVienService, NhanVienService>();
                services.AddScoped<IPhanCongRepository, PhanCongRepository>();
                services.AddScoped<IPhanCongService, PhanCongService>();
                services.AddScoped<ISinhVienRepository, SinhVienRepository>();
                services.AddScoped<ISinhVienService, SinhVienService>();

                services.AddTransient<SessionContext>();
                services.AddTransient<MainForm>();

                MessageBox.Show("Login successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                var serviceProvider = services.BuildServiceProvider();
                var mainForm = serviceProvider.GetRequiredService<MainForm>();

                Hide();

                mainForm.FormClosed += (s, args) => Close();
                mainForm.Show();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                MessageBox.Show("Login failed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}