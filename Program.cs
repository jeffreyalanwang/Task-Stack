using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Extensions.Configuration;

namespace Task_Stack
{

    internal static class Program
    {
        internal static IConfiguration Config;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Config = new ConfigurationBuilder()
                            .AddJsonFile("appsettings.json")
                            .Build();
            
            Application.SetHighDpiMode(HighDpiMode.PerMonitorV2);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Form mainForm = new GUI.Window();
            Application.Run(mainForm);
        }
    }
}