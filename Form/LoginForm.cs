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
            username = username.ToUpper();

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
                services.AddScoped<IDangKyRepository, DangKyRepository>();
                services.AddScoped<IDangKyService, DangKyService>();



                services.AddTransient<SessionContext>(provider =>
                {
                    var roleConn = factory.createConnection();
                    if (roleConn.State != System.Data.ConnectionState.Open)
                        roleConn.Open();

                    return new SessionContext(roleConn); // Tự động lấy CurrentUser và role bên trong
                });

                services.AddTransient<MainForm>();

                var serviceProvider = services.BuildServiceProvider();
                try
                {
                    var mainForm = serviceProvider.GetRequiredService<MainForm>();
                    MessageBox.Show("Login successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    Hide();
                    mainForm.FormClosed += (s, args) => Close();
                    mainForm.Show();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi khởi tạo MainForm:\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                MessageBox.Show("Login failed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}