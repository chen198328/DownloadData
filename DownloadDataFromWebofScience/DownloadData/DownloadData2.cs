using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using mshtml;
using DownloadDataFromWebofScience;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Collections.Specialized;
namespace DownloadData
{
    public partial class DownloadData2 : Form
    {
        public DownloadData2()
        {
            InitializeComponent();
        }
        private static string outbountserviceString = string.Empty;
        /// <summary>
        /// 保存文件的前缀
        /// </summary>
        private static string FilePrefix = "";
        /// <summary>
        /// 任务数
        /// </summary>
        private static int TaskCount = 2;
        /// <summary>
        /// 每个文件的论文数量，最大值500
        /// </summary>
        private static int FileCount = 500;
        /// <summary>
        /// 每次下载文件线程休眠的时间
        /// </summary>
        private static int ThreadSleepTime = 2000;
        private static Queue<Item> queueItems = new Queue<Item>();
        /// <summary>
        /// 记录当前已下载的数据数量
        /// </summary>
        static int CurrentCount = 0;
        /// <summary>
        /// 需要下载的总数量
        /// </summary>
        static int TotalCount = 0;
        /// <summary>
        /// QueryId
        /// </summary>
        string Qid = "";
        /// <summary>
        /// SID
        /// </summary>
        string Sid = "";

        private void DownloadData2_Load(object sender, EventArgs e)
        {
            axWebBrowser1.BeforeNavigate2 += axWebBrowser1_BeforeNavigate2;
            axWebBrowser1.DocumentComplete += axWebBrowser1_DocumentComplete;
            axWebBrowser1.Navigate("http://apps.webofknowledge.com");

            AddMessage("【程序已经启动】");
            AddMessage("【操作提示】待左侧浏览器窗口加载完成后，按照正常下载数据流程操作一遍。然后点击下载按钮即可。");

            //设置配置文件路径
            GetDefaultFolderForSaveFiles();

            timer.Tick += timer_Tick;
            timer.Interval = 500;

            btnDownload.Enabled = false;
        }



        void axWebBrowser1_BeforeNavigate2(object sender, AxSHDocVw.DWebBrowserEvents2_BeforeNavigate2Event e)
        {

            if (e.uRL.ToString().Contains("OutboundService.do"))
            {
                if (e.postData != null)
                {
                    outbountserviceString = GetOutboundService(System.Text.Encoding.ASCII.GetString(e.postData as byte[]));
                }
            }
            else {
                TimerStart();
            }

        }
        void axWebBrowser1_DocumentComplete(object sender, AxSHDocVw.DWebBrowserEvents2_DocumentCompleteEvent e)
        {
            TimerStop();
            if (e.uRL.ToString().Contains("search.do") || e.uRL.ToString().Contains("summary.do"))
            {
                GetTotalCount(e);
                btnDownload.Enabled = true;
            }
            else
            {
                btnDownload.Enabled = false;
                tbxFrom.Text = tbxTo.Text = tbxTotalCount.Text = string.Empty;
            }
        }
        #region Timer
        Timer timer = new Timer();
        void TimerStart() {
            toolStripProgressBar1.Value = 1;
            timer.Start();
        }
        void TimerStop() {
            toolStripProgressBar1.Value = 100;
            timer.Stop();
        }
        void timer_Tick(object sender, EventArgs e)
        {
            toolStripProgressBar1.PerformStep();
            if (toolStripProgressBar1.Value ==toolStripProgressBar1.Maximum) {
                toolStripProgressBar1.Value = 0;
            }
        }
        #endregion
        private void GetTotalCount(AxSHDocVw.DWebBrowserEvents2_DocumentCompleteEvent e)
        {
            SHDocVw.WebBrowser browser = e.pDisp as SHDocVw.WebBrowser;
            HTMLDocumentClass html = (HTMLDocumentClass)browser.Document;
            tbxTotalCount.Text = html.getElementById("trueFinalResultCount").innerText;

            tbxFrom.Text = "1";
            tbxTo.Text = tbxTotalCount.Text;

            NameValueCollection queryList = HttpHelper.GetQueryString(e.uRL.ToString());
            Sid = queryList["SID"];
            Qid = queryList["qid"];


        }

        public string GetOutboundService(string content)
        {
            System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex(@"mark_from=\d+");
            content = reg.Replace(content, "mark_from={0}");
            reg = new System.Text.RegularExpressions.Regex(@"mark_to=\d+");
            content = reg.Replace(content, "mark_to={1}");
            reg = new System.Text.RegularExpressions.Regex(@"markFrom=\d+");
            content = reg.Replace(content, "markFrom={0}");
            reg = new System.Text.RegularExpressions.Regex(@"markTo=\d+");
            content = reg.Replace(content, "markTo={1}");
            return content;
        }
        #region Message
        public void AddMessage(string message, bool isAppendLine = true)
        {
            richTextBox1.AppendText(message);
            if (isAppendLine)
            {
                richTextBox1.AppendText("\r\n");
            }
        }
        public void Report()
        {
            toolStripProgressBar1.Value = CurrentCount * 100 / TotalCount;
        }


        #endregion

        private void splitContainer2_Panel1_Paint(object sender, PaintEventArgs e)
        {
            axWebBrowser1.Width = splitContainer2.Panel1.Width;
            axWebBrowser1.Height = splitContainer2.Panel1.Height;
            axWebBrowser1.Top = splitContainer2.Panel1.Top;
            axWebBrowser1.Left = splitContainer2.Panel1.Left;
        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {
        }
        #region 数据下载
        private void btnDownload_Click(object sender, EventArgs e)
        {
            if (ValidatedForm())
            {

                AddMessage("【文件下载开始】");
                Task task = new Task(() =>
                {
                    DownloadAll();
                });
                task.Start();
                task.ContinueWith(t =>
                {
                    Func<string, bool> addMessage = message => { AddMessage(message); return true; };
                    this.Invoke(addMessage, "【文件下载结束】");
                    MessageBox.Show("文件下载结束");
                });
            }
            else
            {
                MessageBox.Show("现有操作无法下载内容");
            }
        }
        /// <summary>
        /// 检验所有内容
        /// </summary>
        /// <returns></returns>
        private bool ValidatedForm()
        {
            //Post content
            if (outbountserviceString.Length == 0)
            {
                return false;
            }
            //下载参数
            if (tbxTotalCount.Text.Length == 0 || tbxFrom.Text.Length == 0 || tbxTo.Text.Length == 0)
            {
                return false;
            }
            else
            {
                int totalCount = 0;
                int from = 0;
                int to = 0;
                int.TryParse(tbxTotalCount.Text, out totalCount);
                int.TryParse(tbxFrom.Text, out from);
                int.TryParse(tbxTo.Text, out to);

                if (totalCount == 0 || from == 0 || to == 0 || from > to || to > totalCount)
                {
                    return false;
                }
            }
            if (!Directory.Exists(tbxFolder.Text.Trim()))
            {
                return false;
            }
            return true;
        }

        public bool DownloadAll()
        {
            //入列
            int ifrom = 0;
            int ito = 0;
            int itotal = 0;
            int.TryParse(tbxTotalCount.Text, out itotal);
            int.TryParse(tbxFrom.Text, out ifrom);
            int.TryParse(tbxTo.Text, out ito);

            for (int i = ifrom; i < ito + 1; i += FileCount)
            {
                if (i + FileCount > ito)
                {
                    AddQueue(new Item(i, ito) { });
                }
                else
                {
                    AddQueue(new Item(i, i + 499));
                }
            }
            TotalCount = ito - ifrom + 1;
            CurrentCount = 0;
            Task[] tasks = new Task[TaskCount];
            for (int i = 0; i < tasks.Length; i++)
            {
                tasks[i] = Task.Factory.StartNew(() =>
                {
                    DownloadFiles();
                });
            }
            Task.WaitAll(tasks);
            return true;

        }
        public void DownloadFiles()
        {
            Item item = null;
            Func<bool> report = () =>
            {
                Report();
                return true;
            };
            Func<string, bool> addMessage = (message) =>
            {
                AddMessage(message);
                return true;
            };
            while ((item = GetItemFromQueue()) != null)
            {
                CookieCollection cookies = new CookieCollection();
                Downloader downloader = new Downloader(Sid, Qid, cookies);
                if (DownloadFile(downloader, item))
                {
                    //文件报告
                    AddCurrentCount(item.Count);
                    this.Invoke(report);
                    this.Invoke(addMessage, string.Format("[{0}]:{1}-{2}-{3}.txt  文件下载完成", DateTime.Now, item.From, item.To, item.Count));
                }
                else
                {
                    AddQueue(item);
                    //文件报告
                }
            }
        }
        public bool DownloadFile(Downloader downloader, Item item)
        {
            string filepath = Path.Combine(tbxFolder.Text.Trim(), GetFileName(item));
            try
            {
                using (StreamWriter writer = new StreamWriter(filepath, false, Encoding.Default))
                {
                    downloader.PostContent = outbountserviceString;
                    writer.Write(downloader.DownloadData(item.From, item.To));
                    writer.Flush();
                    writer.Close();
                    System.Threading.Thread.Sleep(ThreadSleepTime);
                }
                return true;
            }
            catch (IOException)
            {
                return false;
            }
            catch (WebException) {
                return false;
            }
        }
        /// <summary>
        /// 获取保存文件名
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private string GetFileName(Item item)
        {
            if (FilePrefix.Length == 0)
            {
                return string.Format("{0}-{1}-{2}.txt", item.From, item.To, item.Count);
            }
            else
            {
                return string.Format("{0}{1}-{2}-{3}.txt", FilePrefix, item.From, item.To, item.Count);
            }
        }
        object obj1 = new object();
        public Item GetItemFromQueue()
        {
            lock (obj1)
            {
                if (queueItems.Count > 0)
                {
                    return queueItems.Dequeue();
                }
                else
                {
                    return null;
                }
            }
        }
        object obj2 = new object();
        void AddQueue(Item item)
        {
            lock (obj2)
            {
                queueItems.Enqueue(item);
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
        #endregion
        #region 配置文件
        private void tbxPrefix_TextChanged(object sender, EventArgs e)
        {
            FilePrefix = tbxPrefix.Text.Trim();
        }

        private void tbxTaskCount_TextChanged(object sender, EventArgs e)
        {
            int count = 0;
            int.TryParse(tbxTaskCount.Text.Trim(), out count);
            if (count != 0)
            {
                TaskCount = count;
            }
        }
        private void tbxThreadSleepTime_TextChanged(object sender, EventArgs e)
        {
            int count = 0;
            int.TryParse(tbxThreadSleepTime.Text.Trim(), out count);
            if (count != 0)
            {
                TaskCount = count;
            }
        }
        private void tbxFileCount_TextChanged(object sender, EventArgs e)
        {
            int count = 0;
            int.TryParse(tbxFileCount.Text.Trim(), out count);
            if (count != 0)
            {
                FileCount = count;
            }
        }
        private void GetDefaultFolderForSaveFiles()
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
        private void btnOpenFolder_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowDialog();
            tbxFolder.Text = folderBrowserDialog1.SelectedPath;
        }

        private void openFolder_Click(object sender, EventArgs e)
        {
            if (tbxFolder.Text.Trim().Length > 0)
            {
                System.Diagnostics.Process.Start("explorer.exe", tbxFolder.Text.Trim());
            }
        }
        
        #endregion

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            linkLabel1.Links[0].LinkData = "http://hi.baidu.com/chen198328/item/784ff51f43bcb40ab98a1af9";
            System.Diagnostics.Process.Start(e.Link.LinkData.ToString());
        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {

        }

     



    }
    public class Item
    {
        public int From { set; get; }
        public int To { set; get; }
        public int Count { set; get; }
        public Item(int from, int to)
        {
            From = from;
            To = to;
            Count = to + 1 - from;
        }
    }
}
