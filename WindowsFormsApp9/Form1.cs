using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using Newtonsoft.Json;

namespace WindowsFormsApp9
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Btn_search_Click(object sender, EventArgs e)
        {
            string baihat = txt_search.Text;
            WebRequest req = HttpWebRequest.Create("http://hack.2k-confessions.com/nhac.php?baihat=" + baihat);
            req.Method = "GET";
            string source;
            StreamReader reader = new StreamReader(req.GetResponse().GetResponseStream());
            source = reader.ReadToEnd();
           
            dynamic stuff = JsonConvert.DeserializeObject(source);
            string tenbh = stuff.ten;
            string tencs = stuff.casi;
            string thumb = stuff.thumbnail;
            string link = stuff.url;
            string noti = stuff.text;
            lb_song.Text = tenbh;
            lb_singer.Text = tencs;
            pic_cs.ImageLocation = thumb;
            axWindowsMediaPlayer1.URL = link;
            axWindowsMediaPlayer1.Ctlcontrols.play();
            trackBar1.Value = 5;
            MessageBox.Show(noti, "notification");
 
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.Ctlcontrols.play();

        }

        private void Button2_Click(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.Ctlcontrols.pause();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.Ctlcontrols.stop();

        }

        private void Button4_Click(object sender, EventArgs e)
        {
            //axWindowsMediaPlayer1.settings.mute = true;
           if(axWindowsMediaPlayer1.settings.mute == true)
            {
                axWindowsMediaPlayer1.settings.mute = false;
               
            }
            else
            {
                axWindowsMediaPlayer1.settings.mute = true;
                button4.BackColor = Color.White;
            }

        }

        private void Button5_Click(object sender, EventArgs e)
        {
            DialogResult noti;
            noti = (MessageBox.Show("Are You Sure ? ", "Notification", MessageBoxButtons.YesNo,MessageBoxIcon.Warning));
            if (noti == DialogResult.Yes)
                Application.Exit();

        }

        private void Button7_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "music *.mp3 | *.mp3";
            dialog.FileName = lb_song.Text;
            DialogResult result = dialog.ShowDialog();
            if(result == DialogResult.OK)
            {
                WebClient webClient = new WebClient();
                webClient.DownloadFile(axWindowsMediaPlayer1.URL, dialog.FileName);
            }
        }

        private void TrackBar1_Scroll(object sender, EventArgs e)
        {
            trackBar1.Minimum = 0;
            trackBar1.Maximum = 100;
            axWindowsMediaPlayer1.settings.volume = trackBar1.Value;

        }

        private void HScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            axWindowsMediaPlayer1.Ctlcontrols.currentPosition = hScrollBar1.Value;
        }
    }
}
