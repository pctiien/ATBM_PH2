using Microsoft.Extensions.DependencyInjection;
using ATBM_HTTT_PH2.Form;

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

            ApplicationConfiguration.Initialize();

            Application.Run(new LoginForm());
        }
    }
}