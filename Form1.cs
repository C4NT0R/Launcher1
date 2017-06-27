using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Launcher
{
    public partial class Form1 : Form
    {
        private string serverBase = "http://heroes.frozenms.com:6974/bin/";
        private string clientBase = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "FrozenMS");
        private string clientExecutable = "MapleStory.exe";
        //private string clientArgs = "frozenms.com 8484";
        
        private List<string> filesToUpdate = new List<string>();
        private Dictionary<string, long> sizes = new Dictionary<string, long>();
        private long totalSize = 0;

        public Form1()
        {
            InitializeComponent();
            Application.Idle += VerifyFiles;
        }

        private void VerifyFiles(object sender, EventArgs e)
        {
            Application.Idle -= VerifyFiles;

            try
            {
                StreamReader reader = new StreamReader(new WebClient().OpenRead(new Uri(serverBase + "hash.json")));
                JArray j = JArray.Parse(reader.ReadToEnd());
                Dictionary<string, string> hashes = JsonConvert.DeserializeObject<Dictionary<string, string>>(j[0].ToString());
                sizes = JsonConvert.DeserializeObject<Dictionary<string, long>>(j[1].ToString());

                foreach(var key in sizes)
                {
                    totalSize += key.Value;
                }

                Directory.CreateDirectory(clientBase);
                Directory.CreateDirectory(clientBase + "\\Data");
                Directory.CreateDirectory(clientBase + "\\Data\\Base");
                Directory.CreateDirectory(clientBase + "\\Data\\Character");
                Directory.CreateDirectory(clientBase + "\\Data\\Character\\Accessory");
                Directory.CreateDirectory(clientBase + "\\Data\\Character\\Afterimage");
                Directory.CreateDirectory(clientBase + "\\Data\\Character\\Cap");
                Directory.CreateDirectory(clientBase + "\\Data\\Character\\Cape");
                Directory.CreateDirectory(clientBase + "\\Data\\Character\\Coat");
                Directory.CreateDirectory(clientBase + "\\Data\\Character\\Face");
                Directory.CreateDirectory(clientBase + "\\Data\\Character\\Glove");
                Directory.CreateDirectory(clientBase + "\\Data\\Character\\Hair");
                Directory.CreateDirectory(clientBase + "\\Data\\Character\\Longcoat");
                Directory.CreateDirectory(clientBase + "\\Data\\Character\\Pants");
                Directory.CreateDirectory(clientBase + "\\Data\\Character\\PetEquip");
                Directory.CreateDirectory(clientBase + "\\Data\\Character\\Ring");
                Directory.CreateDirectory(clientBase + "\\Data\\Character\\Shield");
                Directory.CreateDirectory(clientBase + "\\Data\\Character\\Shoes");
                Directory.CreateDirectory(clientBase + "\\Data\\Character\\TamingMob");
                Directory.CreateDirectory(clientBase + "\\Data\\Character\\Weapon");
                Directory.CreateDirectory(clientBase + "\\Data\\Effect");
                Directory.CreateDirectory(clientBase + "\\Data\\Etc");
                Directory.CreateDirectory(clientBase + "\\Data\\Item");
                Directory.CreateDirectory(clientBase + "\\Data\\Item\\Cash");
                Directory.CreateDirectory(clientBase + "\\Data\\Item\\Consume");
                Directory.CreateDirectory(clientBase + "\\Data\\Item\\Etc");
                Directory.CreateDirectory(clientBase + "\\Data\\Item\\Install");
                Directory.CreateDirectory(clientBase + "\\Data\\Item\\Pet");
                Directory.CreateDirectory(clientBase + "\\Data\\Item\\Special");
                Directory.CreateDirectory(clientBase + "\\Data\\Map");
                Directory.CreateDirectory(clientBase + "\\Data\\Map\\Back");
                Directory.CreateDirectory(clientBase + "\\Data\\Map\\Map");
                Directory.CreateDirectory(clientBase + "\\Data\\Map\\Map\\Map0");
                Directory.CreateDirectory(clientBase + "\\Data\\Map\\Map\\Map1");
                Directory.CreateDirectory(clientBase + "\\Data\\Map\\Map\\Map2");
                Directory.CreateDirectory(clientBase + "\\Data\\Map\\Map\\Map3");
                Directory.CreateDirectory(clientBase + "\\Data\\Map\\Map\\Map5");
                Directory.CreateDirectory(clientBase + "\\Data\\Map\\Map\\Map6");
                Directory.CreateDirectory(clientBase + "\\Data\\Map\\Map\\Map7");
                Directory.CreateDirectory(clientBase + "\\Data\\Map\\Map\\Map8");
                Directory.CreateDirectory(clientBase + "\\Data\\Map\\Map\\Map9");
                Directory.CreateDirectory(clientBase + "\\Data\\Map\\Obj");
                Directory.CreateDirectory(clientBase + "\\Data\\Map\\Tile");
                Directory.CreateDirectory(clientBase + "\\Data\\Map\\WorldMap");
                Directory.CreateDirectory(clientBase + "\\Data\\Mob");
                Directory.CreateDirectory(clientBase + "\\Data\\Morph");
                Directory.CreateDirectory(clientBase + "\\Data\\Npc");
                Directory.CreateDirectory(clientBase + "\\Data\\Quest");
                Directory.CreateDirectory(clientBase + "\\Data\\Quest\\QuestData");
                Directory.CreateDirectory(clientBase + "\\Data\\Reactor");
                Directory.CreateDirectory(clientBase + "\\Data\\Skill");
                Directory.CreateDirectory(clientBase + "\\Data\\Sound");
                Directory.CreateDirectory(clientBase + "\\Data\\String");
                Directory.CreateDirectory(clientBase + "\\Data\\TamingMob");
                Directory.CreateDirectory(clientBase + "\\Data\\UI");


                BackgroundWorker bw = new BackgroundWorker();
                bw.DoWork += delegate
                {
                    foreach (var key in hashes)
                    {
                        this.Invoke(new Action(() =>
                        {
                            UpdatePart.Text = "Verifying " + key.Key;
                        }));

                        using (var md5 = System.Security.Cryptography.MD5.Create())
                        {
                            string file = Path.Combine(clientBase, key.Key);
                            if (!File.Exists(file))
                            {
                                filesToUpdate.Add(key.Key);
                            }
                            else
                            {
                                using (var stream = File.OpenRead(file))
                                {
                                    string hash = BitConverter.ToString(md5.ComputeHash(stream)).Replace("-", "").ToLower();
                                    if (!key.Value.Equals(hash))
                                    {
                                        filesToUpdate.Add(key.Key);
                                    }
                                }
                            }
                        }
                        UpdateTotalProgress.Value += (int)(100 * sizes[key.Key]/totalSize);
                    }
                    reader.Close();
                };
                bw.RunWorkerCompleted += delegate
                {
                    UpdateTotalProgress.Value = 0;
                    UpdateTotal.Text = "";
                    UpdatePart.Text = "";

                    if (filesToUpdate.Count == 0)
                    {
                        LaunchButton.BackgroundImage = Properties.Resources.GameStart_btn;
                    }
                    else
                    {
                        LaunchButton.BackgroundImage = Properties.Resources.UpdateClient_btn;
                    }

                    LaunchButton.Enabled = true;
                };
                bw.RunWorkerAsync();
            }
            catch
            {
                MessageBox.Show("서버가 응답하지 않거나 파일을 사용할수 없습니다.", "FROZEN", MessageBoxButtons.OK, MessageBoxIcon.Error);

                Application.Exit();
            }
        }

        private void Launch(object sender, EventArgs e)
        {
            LaunchButton.Enabled = false;

            if(filesToUpdate.Count == 0)
            {
                try
                {
                    Process.Start(Path.Combine(clientBase, clientExecutable));//, clientArgs);
                }
                catch { }
                Application.Exit();
            }
            else
            {
                LaunchButton.Enabled = false;
                LaunchButton.BackgroundImage = Properties.Resources.UpdateClientDisabled_btn;
                ThreadPool.QueueUserWorkItem(new WaitCallback((_) => { UpdateClient(); }), null);
            }
        }

        private void UpdateClient()
        {
            AutoResetEvent notifier = new AutoResetEvent(false);
            long totalBytes = 0;
            int idx = 0;
            totalSize = 0;
            foreach(var key in filesToUpdate)
            {
                totalSize += sizes[key];
            }

            foreach (string filename in filesToUpdate)
            {
                idx++;
                using (WebClient wc = new WebClient())
                {
                    wc.DownloadProgressChanged += (o, f) =>
                    {
                        try
                        {
                            this.Invoke(new Action(() =>
                            {
                                long bytesIn = f.BytesReceived;
                                long partBytes = f.TotalBytesToReceive;
                                int partPercentage = (int)(100 * bytesIn / partBytes);
                                int totalPercentage = (int)(100 * (totalBytes + bytesIn) / totalSize);

                                UpdateTotal.Text = String.Format("{0}/{1} ({2}KB/{3}KB)", idx, filesToUpdate.Count, (totalBytes + bytesIn) / 1024, totalSize / 1024);
                                UpdateTotalProgress.Value = totalPercentage;
                                UpdatePart.Text = String.Format("Downloading {0} {1}% ({2}KB/{3}KB)", filename, partPercentage, bytesIn / 1024, partBytes / 1024);
                                UpdatePartProgress.Value = partPercentage;
                            }));
                        }
                        catch
                        {
                            Application.Exit();
                        }
                    };

                    wc.DownloadFileCompleted += (o, f) =>
                    {
                        totalBytes += sizes[filename];
                        notifier.Set();
                    };

                    wc.DownloadFileAsync(new Uri(serverBase + "test/" + filename), Path.Combine(clientBase, filename));
                    notifier.WaitOne();
                }
            }

            this.Invoke(new Action(() =>
            {
                UpdateTotal.Text = "";
                UpdateTotalProgress.Value = 0;
                UpdatePart.Text = "";
                UpdatePartProgress.Value = 0;

                filesToUpdate = new List<string>();

                LaunchButton.Enabled = true;
                LaunchButton.BackgroundImage = Properties.Resources.GameStart_btn;
            }));
        }

        protected override void WndProc(ref Message m)
        {
            const int WM_NCHITTEST = 0x84;
            const int HTCLIENT = 0x1;
            const int HTCAPTION = 0x2;

            switch (m.Msg)
            {
                case WM_NCHITTEST:
                    base.WndProc(ref m);
                    if ((int)m.Result == HTCLIENT)
                    {
                        m.Result = (IntPtr)HTCAPTION;
                    }

                    return;
            }

            base.WndProc(ref m);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        /*       
       private void label1_Click(object sender, EventArgs e)
       {
          webBrowser1.Url = new Uri("http://frozenms.com/notice");
       }

       private void label2_Click(object sender, EventArgs e)
       {
          webBrowser1.Url = new Uri("http://frozenms.com/event");
       }*/

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
{

        }
    }


    public class CustomProgressBar : Control
    {
        private int min = 0;
        private int max = 100;
        private int val = 0;

        public CustomProgressBar()
        {
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.BackColor = Color.Transparent;
            this.DoubleBuffered = true;
        }

        protected override void OnResize(EventArgs e)
        {
            this.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBilinear;
            e.Graphics.CompositingQuality = CompositingQuality.HighQuality;
            e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            TextureBrush brush = new TextureBrush(Properties.Resources.Download_Graph);

            Rectangle rect = this.ClientRectangle;

            float percent = (float)(val - min) / (float)(max - min);
            rect.Width = (int)((float)rect.Width * percent);

            using (GraphicsPath gp = new GraphicsPath())
            {
                if (rect.Width >= this.Height)
                {
                    gp.AddArc(new Rectangle(new Point(0, 0), new Size(this.Height, this.Height)), 90, 180);
                    gp.AddLine(new Point(this.Height / 2, 0), new Point(rect.Width - (this.Height / 2), 0));
                    gp.AddArc(new Rectangle(new Point(rect.Width - this.Height, 0), new Size(this.Height, this.Height)), -90, 180);
                }
                e.Graphics.FillPath(brush, gp);
            }

            brush.Dispose();
        }

        public int Value
        {
            get
            {
                return val;
            }

            set
            {
                int oldValue = val;
                
                if (value < min)
                {
                    val = min;
                }
                else if (value > max)
                {
                    val = max;
                }
                else
                {
                    val = value;
                }

                float percent;
                Rectangle newValueRect = this.ClientRectangle;
                Rectangle oldValueRect = this.ClientRectangle;

                percent = (float)(val - min) / (float)(max - min);
                newValueRect.Width = (int)((float)newValueRect.Width * percent);

                percent = (float)(oldValue - min) / (float)(max - min);
                oldValueRect.Width = (int)((float)oldValueRect.Width * percent);

                Rectangle updateRect = new Rectangle();

                if (newValueRect.Width > oldValueRect.Width)
                {
                    updateRect.X = oldValueRect.Size.Width - this.Height;
                    updateRect.Width = newValueRect.Width - oldValueRect.Width + this.Height;
                }
                else
                {
                    updateRect.X = newValueRect.Size.Width - this.Height;
                    updateRect.Width = oldValueRect.Width - newValueRect.Width + this.Height;
                }

                updateRect.Height = this.Height;

                this.Invalidate(updateRect);
            }
        }
    }
}
