using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Net;
using System.Collections.Specialized;
using DownloadDataFromWebofScience;
using System.IO;
namespace DownloadData
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 记录当前已下载的数据数量
        /// </summary>
        static int CurrentCount = 0;
        /// <summary>
        /// 需要下载的总数量
        /// </summary>
        static int TotalCount = 0;
        Timer timer = new Timer();
        private void Form1_Load(object sender, EventArgs e)
        {
            GetDefaultFolderForSaveFiles();


            timer.Interval = 750;
            timer.Tick += timer_Tick;

            string url = "apps.webofknowledge.com";
            AddLog("程序已经启动");
            wbsMain.Navigating += wbsMain_Navigating;
            wbsMain.Navigate(url);
            wbsMain.DocumentCompleted += wbsMain_DocumentCompleted;

        }

        void timer_Tick(object sender, EventArgs e)
        {
            toolStripProgressBar1.PerformStep();
        }
        private void TimerStart()
        {
            toolStripProgressBar1.Value = 1;
            timer.Start();
        }
        private void TimerStop()
        {
            timer.Stop();
        }
        void wbsMain_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            TimerStart();
        }
        public void GetDefaultFolderForSaveFiles()
        {
            try
            {
                string folder = Environment.CurrentDirectory + "\\" + "DownloadData";
                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }
                tbxFolder.Text = folder;
            }
            catch (IOException) { }
        }
        void wbsMain_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            TimerStop();
            toolStripProgressBar1.Value = 100;

        }
        void AddLog(string message)
        {
            rtbLoglist.AppendText(message + "\r\n");
        }

        private void btnDownloadData_Click(object sender, EventArgs e)
        {
            //获取SID和queryId
            AddLog("下载任务开始");
            Task task = new Task(() => {
                Download();
            });
            task.Start();

           
        }
        void Download() {
            int from = 0;
            int to = 0;
            int total = 0;
            int.TryParse(tbxFrom.Text, out from);
            int.TryParse(tbxTo.Text, out to);
            int.TryParse(tbxTotalCount.Text, out total);
            if (to - from + 1 <= total && from <= total && to <= total && from <= to)
            {
                TotalCount = to - from + 1;
                if (to - from + 1 > 500)
                {
                    Task task = new Task(() =>
                    {
                        DownloadAll(from, to, tbxSID.Text.Trim(), tbxQueryid.Text.Trim());
                    });
                    task.Start();
                    Task.WaitAll(task);
                    AddLog("下载完成");
                }
                else
                {

                    CookieCollection cookies = new CookieCollection();
                    Downloader downloader = new Downloader(tbxSID.Text, tbxQueryid.Text, cookies);
                    SaveFile(downloader, to - from + 1, from, to);
                    AddLog("下载完成");

                }
            }
       }

        private void button1_Click(object sender, EventArgs e)
        {
            string url = wbsMain.Url.ToString();
            NameValueCollection queryList = HttpHelper.GetQueryString(url);
            tbxSID.Text = queryList["SID"];
            tbxQueryid.Text = queryList["qid"];

            if (wbsMain.Document != null)
            {
                tbxTotalCount.Text = wbsMain.Document.GetElementById("trueFinalResultCount").InnerText;
            }
        }

        private void btnOpenFolder_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowDialog();
            tbxFolder.Text = folderBrowserDialog1.SelectedPath;
        }
        private string GetFileName(int from, int to)
        {
            return string.Format("{0}-{1}-{2}.txt", from, to, to - from + 1);
        }
        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="downloader"></param>
        /// <param name="total">需要保存的文件总数</param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        private bool SaveFile(Downloader downloader, int total, int from, int to)
        {
            string filepath = Path.Combine(tbxFolder.Text.Trim(), GetFileName(from, to));
            try
            {
                using (StreamWriter writer = new StreamWriter(filepath, false, Encoding.Default))
                {
                    writer.Write(downloader.DownloadData(from, to));
                    writer.Flush();
                    writer.Close();
                }
                AddCurrentCount(to - from + 1);
                Func<bool> report = () =>
                {
                    ReportProcess();
                    return true;
                };
                this.Invoke(report);
                return true;
            }
            catch (IOException)
            {
                return false;
            }
        }
        object obj = new object();
        void AddCurrentCount(int count)
        {
            lock (obj)
            {
                CurrentCount += count;
            }
        }
        void ReportProcess()
        {
            toolStripProgressBar1.Value = CurrentCount * 100 / TotalCount;
        }
        public void DownloadAll(int from, int to, string sid, string qid)
        {
            int temp = (to - from) / 2;
            Task task1 = Task.Factory.StartNew(() =>
            {
                Download(from, temp, sid, qid);
            });
            Task task2 = Task.Factory.StartNew(() =>
            {
                Download(temp + 1, to, sid, qid);
            });
            Task.WaitAll(task1, task2);


        }
        public void Download(int from, int to, string sid, string qid)
        {
            CookieCollection cookies = new CookieCollection();
            Downloader downloader = new Downloader(sid, qid, cookies);
            if (to - from + 1 > 500)
            {
                for (int i = from; i < to+1; i += 500)
                {
                    if (i + 500> to)
                    {
                        SaveFile(downloader, to - from + 1, i, to);
                    }
                    else
                    {
                        SaveFile(downloader, to - from + 1, i, i + 499);
                    }

                }
            }
        }

        private void openFolder_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("explorer.exe", tbxFolder.Text.Trim());
        }
    }
}
