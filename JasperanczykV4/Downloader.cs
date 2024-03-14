using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.WebRequestMethods;

namespace JasperanczykV4
{
    public partial class Downloader : Form
    {
        public Downloader()
        {
            WebClient client;
            InitializeComponent();
            client = new WebClient();
            client.DownloadProgressChanged += Client_DownloadProgressChanged;
            Download(client);
            Hide();
        }
        private void Client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            progressBar1.Maximum = (int)e.TotalBytesToReceive;
            progressBar1.Value = (int)e.BytesReceived;
            progressBar1.Update();
        }

        private async void Download(WebClient client)
        {
            Directory.CreateDirectory(DownloadPath);
            await client.DownloadFileTaskAsync(new Uri($"{bin}/manifest.txt"), DownloadPath + "\\manifest.txt");
            await Console.Out.WriteLineAsync(DownloadPath + "\\manifest.txt");
            StreamReader manifestReader = new StreamReader(DownloadPath + "\\manifest.txt");
            string line = manifestReader.ReadLine();
            while (line != null)
            {
                await Console.Out.WriteLineAsync(line);
                Uri uri = new Uri($"{bin}/{line}");
                string filename = DownloadPath + "\\" + line;
                if (line.EndsWith(".wav"))
                {
                    byte[] data = await client.DownloadDataTaskAsync(uri);
                    if (line.StartsWith("sfx_")) Payloads.sfx.Add(new MemoryStream(data));
                    else Payloads.music.Add(new MemoryStream(data));
                    Payloads.music.Add(new MemoryStream(data));
                } else
                {
                    Payloads.images.Add(bin + "/" + line);
                }
                line = manifestReader.ReadLine();
            }
            client.Dispose();
            manifestReader.Close();
            Close();
        }

        private static readonly string bin = "https://raw.githubusercontent.com/YasInvolved/JasperanczykAssets/main";
        private static readonly string DownloadPath = Environment.ExpandEnvironmentVariables("%temp%\\JasperanczykV4\\media");
    }
}
