﻿namespace Citta_T1.Controls.Small
{
    partial class ModelTitleControl
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.closePictureBox = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.closePictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.closePictureBox);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(113, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(27, 26);
            this.panel2.TabIndex = 1;
            // 
            // closePictureBox
            // 
            this.closePictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.closePictureBox.Image = global::Citta_T1.Properties.Resources.close;
            this.closePictureBox.Location = new System.Drawing.Point(0, 0);
            this.closePictureBox.Name = "closePictureBox";
            this.closePictureBox.Size = new System.Drawing.Size(27, 26);
            this.closePictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.closePictureBox.TabIndex = 0;
            this.closePictureBox.TabStop = false;
            this.closePictureBox.Click += new System.EventHandler(this.ClosePictureBox_Click);
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 11F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 26);
            this.label1.TabIndex = 2;
            this.label1.Text = "新建模型";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ModelTitleControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(216)))), ((int)(((byte)(216)))));
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel2);
            this.Name = "ModelTitleControl";
            this.Size = new System.Drawing.Size(140, 26);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.closePictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox closePictureBox;
        private System.Windows.Forms.Label label1;
    }
}