using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using DotNetEnv;
using Oracle.ManagedDataAccess.Client;
using Microsoft.Extensions.DependencyInjection;
using ATBM_HTTT_PH2.Forms;

namespace ATBM_HTTT_PH2
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            var services = new ServiceCollection();

            //services.AddScoped<IUserService, UserService>();
            //services.AddScoped<IRoleService, RoleService>();
            //services.AddScoped<IObjectService, ObjectService>();

            //services.AddTransient<UserForm>();
            //services.AddTransient<RoleForm>();
            //services.AddTransient<ObjectForm>();

            //services.AddTransient<MainForm>();

            var serviceProvider = services.BuildServiceProvider();

            ApplicationConfiguration.Initialize();

            Application.Run(new LoginForm(services));
        }
    }
}