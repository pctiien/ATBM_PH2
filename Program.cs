using System;
using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;
using DotNetEnv;
using ATBM_HTTT_PH2.Forms;

namespace ATBM_HTTT_PH2
{
    internal static class Program
    {
        /// <summary>
        ///  Điểm khởi đầu chính của ứng dụng.
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

          
            // services.AddScoped<INotificationService, NotificationService>();

            // Đăng ký các Form nếu bạn muốn quản lý Form bằng DI
            services.AddTransient<LoginForm>();
            services.AddTransient<SendNotificationForm>();
            services.AddTransient<ReceiveNotificationForm>();

            var serviceProvider = services.BuildServiceProvider();

            ApplicationConfiguration.Initialize();

            // Chạy form LoginForm đầu tiên
            var loginForm = serviceProvider.GetRequiredService<LoginForm>();
            Application.Run(loginForm);
        }
    }
}
