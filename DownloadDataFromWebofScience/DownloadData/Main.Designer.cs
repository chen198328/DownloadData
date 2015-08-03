namespace DownloadData
{
    partial class Main
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.wbsMain = new System.Windows.Forms.WebBrowser();
            this.rtbLoglist = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbxFolder = new System.Windows.Forms.TextBox();
            this.btnOpenFolder = new System.Windows.Forms.Button();
            this.btnDownloadData = new System.Windows.Forms.Button();
            this.btnPause = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.helpProvider1 = new System.Windows.Forms.HelpProvider();
            this.tbxSID = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tbxQueryid = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.tbxTotalCount = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbxFrom = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tbxTo = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.openFolder = new System.Windows.Forms.Button();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // wbsMain
            // 
            this.wbsMain.Location = new System.Drawing.Point(12, 12);
            this.wbsMain.MinimumSize = new System.Drawing.Size(20, 20);
            this.wbsMain.Name = "wbsMain";
            this.wbsMain.Size = new System.Drawing.Size(747, 577);
            this.wbsMain.TabIndex = 0;
            // 
            // rtbLoglist
            // 
            this.rtbLoglist.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbLoglist.Location = new System.Drawing.Point(6, 20);
            this.rtbLoglist.Name = "rtbLoglist";
            this.rtbLoglist.Size = new System.Drawing.Size(201, 352);
            this.rtbLoglist.TabIndex = 1;
            this.rtbLoglist.Text = "";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(4, 610);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(130, 23);
            this.label1.TabIndex = 2;
            this.label1.Text = "保存文件夹：";
            // 
            // tbxFolder
            // 
            this.tbxFolder.Location = new System.Drawing.Point(130, 610);
            this.tbxFolder.Name = "tbxFolder";
            this.tbxFolder.Size = new System.Drawing.Size(443, 21);
            this.tbxFolder.TabIndex = 3;
            // 
            // btnOpenFolder
            // 
            this.btnOpenFolder.Location = new System.Drawing.Point(586, 13);
            this.btnOpenFolder.Name = "btnOpenFolder";
            this.btnOpenFolder.Size = new System.Drawing.Size(75, 23);
            this.btnOpenFolder.TabIndex = 4;
            this.btnOpenFolder.Text = "更改";
            this.btnOpenFolder.UseVisualStyleBackColor = true;
            this.btnOpenFolder.Click += new System.EventHandler(this.btnOpenFolder_Click);
            // 
            // btnDownloadData
            // 
            this.btnDownloadData.Location = new System.Drawing.Point(784, 568);
            this.btnDownloadData.Name = "btnDownloadData";
            this.btnDownloadData.Size = new System.Drawing.Size(177, 23);
            this.btnDownloadData.TabIndex = 5;
            this.btnDownloadData.Text = "开始下载";
            this.btnDownloadData.UseVisualStyleBackColor = true;
            this.btnDownloadData.Click += new System.EventHandler(this.btnDownloadData_Click);
            // 
            // btnPause
            // 
            this.btnPause.Location = new System.Drawing.Point(19, 92);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(75, 23);
            this.btnPause.TabIndex = 6;
            this.btnPause.Text = "暂停";
            this.btnPause.UseVisualStyleBackColor = true;
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(886, 600);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 23);
            this.btnStop.TabIndex = 7;
            this.btnStop.Text = "终止";
            this.btnStop.UseVisualStyleBackColor = true;
            // 
            // tbxSID
            // 
            this.tbxSID.Location = new System.Drawing.Point(815, 396);
            this.tbxSID.Name = "tbxSID";
            this.tbxSID.Size = new System.Drawing.Size(154, 21);
            this.tbxSID.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(772, 399);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(23, 12);
            this.label2.TabIndex = 9;
            this.label2.Text = "SID";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(772, 436);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(23, 12);
            this.label3.TabIndex = 11;
            this.label3.Text = "Qid";
            // 
            // tbxQueryid
            // 
            this.tbxQueryid.Location = new System.Drawing.Point(815, 433);
            this.tbxQueryid.Name = "tbxQueryid";
            this.tbxQueryid.Size = new System.Drawing.Size(34, 21);
            this.tbxQueryid.TabIndex = 10;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(864, 469);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(105, 23);
            this.button1.TabIndex = 12;
            this.button1.Text = "提取页面信息";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(870, 436);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 14;
            this.label4.Text = "记录数";
            // 
            // tbxTotalCount
            // 
            this.tbxTotalCount.Location = new System.Drawing.Point(919, 433);
            this.tbxTotalCount.Name = "tbxTotalCount";
            this.tbxTotalCount.Size = new System.Drawing.Size(50, 21);
            this.tbxTotalCount.TabIndex = 13;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(787, 535);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(17, 12);
            this.label5.TabIndex = 16;
            this.label5.Text = "从";
            // 
            // tbxFrom
            // 
            this.tbxFrom.Location = new System.Drawing.Point(828, 532);
            this.tbxFrom.Name = "tbxFrom";
            this.tbxFrom.Size = new System.Drawing.Size(34, 21);
            this.tbxFrom.TabIndex = 15;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(886, 535);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(17, 12);
            this.label6.TabIndex = 18;
            this.label6.Text = "到";
            // 
            // tbxTo
            // 
            this.tbxTo.Location = new System.Drawing.Point(927, 532);
            this.tbxTo.Name = "tbxTo";
            this.tbxTo.Size = new System.Drawing.Size(34, 21);
            this.tbxTo.TabIndex = 17;
            // 
            // groupBox1
            // 
            this.groupBox1.Location = new System.Drawing.Point(765, 379);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(214, 123);
            this.groupBox1.TabIndex = 19;
            this.groupBox1.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnPause);
            this.groupBox2.Location = new System.Drawing.Point(765, 508);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(214, 131);
            this.groupBox2.TabIndex = 20;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "下载信息";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.rtbLoglist);
            this.groupBox3.Location = new System.Drawing.Point(766, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(213, 378);
            this.groupBox3.TabIndex = 21;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "操作日志";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.openFolder);
            this.groupBox4.Controls.Add(this.btnOpenFolder);
            this.groupBox4.Location = new System.Drawing.Point(8, 595);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(751, 44);
            this.groupBox4.TabIndex = 22;
            this.groupBox4.TabStop = false;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripProgressBar1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 646);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(991, 22);
            this.statusStrip1.TabIndex = 23;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(246, 17);
            this.toolStripStatusLabel1.Text = "世界科学前沿分析中心--中科院文献情报中心";
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(500, 16);
            // 
            // openFolder
            // 
            this.openFolder.Location = new System.Drawing.Point(672, 11);
            this.openFolder.Name = "openFolder";
            this.openFolder.Size = new System.Drawing.Size(75, 23);
            this.openFolder.TabIndex = 5;
            this.openFolder.Text = "打开";
            this.openFolder.UseVisualStyleBackColor = true;
            this.openFolder.Click += new System.EventHandler(this.openFolder_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(991, 668);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.tbxTo);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tbxFrom);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tbxTotalCount);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbxQueryid);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbxSID);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnDownloadData);
            this.Controls.Add(this.tbxFolder);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.wbsMain);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox4);
            this.Name = "Main";
            this.Text = "Web of Science数据下载器";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.WebBrowser wbsMain;
        private System.Windows.Forms.RichTextBox rtbLoglist;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbxFolder;
        private System.Windows.Forms.Button btnOpenFolder;
        private System.Windows.Forms.Button btnDownloadData;
        private System.Windows.Forms.Button btnPause;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.HelpProvider helpProvider1;
        private System.Windows.Forms.TextBox tbxSID;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbxQueryid;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbxTotalCount;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbxFrom;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbxTo;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.Button openFolder;
    }
}

