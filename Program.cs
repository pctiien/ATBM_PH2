using System;
using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;
using DotNetEnv;
using ATBM_HTTT_PH2.Forms;
using ATBM_HTTT_PH2.Services;
using ATBM_HTTT_PH2.Service;
using ATBM_HTTT_PH2.Util;

namespace ATBM_HTTT_PH2
{
    internal static class Program
    {
        /// <summary>
        /// Điểm khởi đầu chính của ứng dụng.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Load biến môi trường từ file .env nếu có
            try
            {
                Env.Load();
            }
            catch
            {
                // Không có .env hoặc lỗi – tiếp tục chạy bình thường
            }

            // Khởi tạo DI Container
            var services = new ServiceCollection();

            // Đăng ký các dịch vụ
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<INhanVienService, NhanVienService>();
            services.AddScoped<IPhanCongService, PhanCongService>();
            services.AddScoped<ISinhVienService, SinhVienService>();
            services.AddScoped<SessionContext>();

            // Đăng ký các Form
            services.AddTransient<LoginForm>();
            services.AddTransient<SendNotificationForm>();
            services.AddTransient<ReceiveNotificationForm>();
            services.AddTransient<MainForm>(); // Register MainForm for DI

            var serviceProvider = services.BuildServiceProvider();

            ApplicationConfiguration.Initialize();

            // Chạy form LoginForm đầu tiên
            var loginForm = serviceProvider.GetRequiredService<LoginForm>();
            Application.Run(loginForm);
        }
    }
}