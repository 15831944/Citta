﻿namespace C2.Controls.Left
{
    partial class DataSourceControl
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.dataSourceFrame = new System.Windows.Forms.Panel();
            this.externalDataLabel = new System.Windows.Forms.Label();
            this.localDataLabel = new System.Windows.Forms.Label();
            this.localFrame = new System.Windows.Forms.Panel();
            this.externalFrame = new System.Windows.Forms.Panel();
            this.addConnectLabel = new System.Windows.Forms.Label();
            this.dataSourceFrame.SuspendLayout();
            this.externalFrame.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataSourceFrame
            // 
            this.dataSourceFrame.BackColor = System.Drawing.Color.White;
            this.dataSourceFrame.Controls.Add(this.externalDataLabel);
            this.dataSourceFrame.Controls.Add(this.localDataLabel);
            this.dataSourceFrame.Dock = System.Windows.Forms.DockStyle.Top;
            this.dataSourceFrame.Location = new System.Drawing.Point(0, 0);
            this.dataSourceFrame.Name = "dataSourceFrame";
            this.dataSourceFrame.Size = new System.Drawing.Size(185, 30);
            this.dataSourceFrame.TabIndex = 0;
            // 
            // externalDataLabel
            // 
            this.externalDataLabel.BackColor = System.Drawing.Color.Transparent;
            this.externalDataLabel.Dock = System.Windows.Forms.DockStyle.Right;
            this.externalDataLabel.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.externalDataLabel.Location = new System.Drawing.Point(93, 0);
            this.externalDataLabel.Name = "externalDataLabel";
            this.externalDataLabel.Size = new System.Drawing.Size(92, 30);
            this.externalDataLabel.TabIndex = 1;
            this.externalDataLabel.Text = "外部数据";
            this.externalDataLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.externalDataLabel.Click += new System.EventHandler(this.ExternalData_Click);
            // 
            // localDataLabel
            // 
            this.localDataLabel.BackColor = System.Drawing.Color.Transparent;
            this.localDataLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.localDataLabel.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.localDataLabel.Location = new System.Drawing.Point(0, 0);
            this.localDataLabel.Name = "localDataLabel";
            this.localDataLabel.Size = new System.Drawing.Size(99, 30);
            this.localDataLabel.TabIndex = 0;
            this.localDataLabel.Text = "本地数据";
            this.localDataLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.localDataLabel.Click += new System.EventHandler(this.LocalData_Click);
            // 
            // localFrame
            // 
            this.localFrame.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.localFrame.AutoScroll = true;
            this.localFrame.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.localFrame.BackColor = System.Drawing.Color.White;
            this.localFrame.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.localFrame.Location = new System.Drawing.Point(3, 35);
            this.localFrame.Name = "localFrame";
            this.localFrame.Size = new System.Drawing.Size(179, 622);
            this.localFrame.TabIndex = 1;
            this.localFrame.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DataSourceControl_MouseDown);
            // 
            // externalFrame
            // 
            this.externalFrame.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.externalFrame.AutoScroll = true;
            this.externalFrame.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.externalFrame.BackColor = System.Drawing.Color.White;
            this.externalFrame.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.externalFrame.Controls.Add(this.addConnectLabel);
            this.externalFrame.Location = new System.Drawing.Point(3, 35);
            this.externalFrame.Name = "externalFrame";
            this.externalFrame.Size = new System.Drawing.Size(179, 621);
            this.externalFrame.TabIndex = 2;
            this.externalFrame.Visible = false;
            // 
            // addConnectLabel
            // 
            this.addConnectLabel.AutoSize = true;
            this.addConnectLabel.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.addConnectLabel.ForeColor = System.Drawing.SystemColors.Highlight;
            this.addConnectLabel.Location = new System.Drawing.Point(4, 4);
            this.addConnectLabel.Name = "label1";
            this.addConnectLabel.Size = new System.Drawing.Size(77, 14);
            this.addConnectLabel.TabIndex = 0;
            this.addConnectLabel.Text = "+ 添加连接";
            this.addConnectLabel.Click += new System.EventHandler(this.AddConnectLabel_Click);
            // 
            // DataSourceControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.externalFrame);
            this.Controls.Add(this.localFrame);
            this.Controls.Add(this.dataSourceFrame);
            this.Name = "DataSourceControl";
            this.Size = new System.Drawing.Size(185, 660);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.DataSourceControl_Paint);
            this.dataSourceFrame.ResumeLayout(false);
            this.externalFrame.ResumeLayout(false);
            this.externalFrame.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel dataSourceFrame;
        private System.Windows.Forms.Panel localFrame;
        private System.Windows.Forms.Panel externalFrame;
        private System.Windows.Forms.Label externalDataLabel;
        private System.Windows.Forms.Label localDataLabel;
        private System.Windows.Forms.Label addConnectLabel;
    }
}
