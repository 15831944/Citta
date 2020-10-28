﻿namespace C2.OperatorViews
{
    partial class FilterOperatorView
    {       

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FilterOperatorView));
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.button1 = new System.Windows.Forms.Button();
            this.textBoxEx1 = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.bottomPanel.SuspendLayout();
            this.valuePanel.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataSourceTB0
            // 
            this.dataSourceTB0.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dataSourceTB0.Location = new System.Drawing.Point(149, 5);
            this.dataSourceTB0.Size = new System.Drawing.Size(137, 23);
            this.dataSourceTB0.TabIndex = 9;
            // 
            // cancelButton
            // 
            this.cancelButton.FlatAppearance.BorderSize = 0;
            this.cancelButton.Location = new System.Drawing.Point(494, 19);
            this.cancelButton.Size = new System.Drawing.Size(63, 27);
            // 
            // confirmButton
            // 
            this.confirmButton.FlatAppearance.BorderSize = 0;
            this.confirmButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.confirmButton.Location = new System.Drawing.Point(394, 19);
            this.confirmButton.Size = new System.Drawing.Size(60, 27);
            this.confirmButton.UseVisualStyleBackColor = true;
            // 
            // outListCCBL0
            // 
            this.outListCCBL0.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.outListCCBL0.Location = new System.Drawing.Point(148, 47);
            this.outListCCBL0.Size = new System.Drawing.Size(138, 21);
            this.outListCCBL0.TabIndex = 8;
            // 
            // comboBox0
            // 
            this.comboBox0.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.comboBox0.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBox0.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.comboBox0.Location = new System.Drawing.Point(2, 2);
            this.comboBox0.Size = new System.Drawing.Size(86, 21);
            // 
            // comboBox1
            // 
            this.comboBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.comboBox1.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.comboBox1.Items.AddRange(new object[] {
            "大于 >",
            "小于 <",
            "等于 =",
            "大于等于 ≥",
            "小于等于 ≦",
            "不等于 ≠"});
            this.comboBox1.Location = new System.Drawing.Point(92, 2);
            this.comboBox1.Size = new System.Drawing.Size(86, 21);
            this.comboBox1.Visible = true;
            // 
            // topPanel
            // 
            this.topPanel.BackColor = System.Drawing.SystemColors.Window;
            this.topPanel.Size = new System.Drawing.Size(589, 37);
            // 
            // bottomPanel
            // 
            this.bottomPanel.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.bottomPanel.Location = new System.Drawing.Point(0, 340);
            this.bottomPanel.Size = new System.Drawing.Size(589, 64);
            // 
            // valuePanel
            // 
            this.valuePanel.BackColor = System.Drawing.SystemColors.Window;
            this.valuePanel.Controls.Add(this.dataSourceTB0);
            this.valuePanel.Controls.Add(this.outListCCBL0);
            this.valuePanel.Controls.Add(this.tableLayoutPanel2);
            this.valuePanel.Controls.Add(this.label3);
            this.valuePanel.Controls.Add(this.label2);
            this.valuePanel.Controls.Add(this.label1);
            this.valuePanel.Controls.Add(this.panel1);
            this.valuePanel.Location = new System.Drawing.Point(0, 37);
            this.valuePanel.Size = new System.Drawing.Size(589, 303);
            this.valuePanel.Controls.SetChildIndex(this.panel1, 0);
            this.valuePanel.Controls.SetChildIndex(this.label1, 0);
            this.valuePanel.Controls.SetChildIndex(this.label2, 0);
            this.valuePanel.Controls.SetChildIndex(this.label3, 0);
            this.valuePanel.Controls.SetChildIndex(this.tableLayoutPanel2, 0);
            this.valuePanel.Controls.SetChildIndex(this.outListCCBL0, 0);
            this.valuePanel.Controls.SetChildIndex(this.dataSourceTB0, 0);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(60, 9);
            this.label1.Size = new System.Drawing.Size(63, 14);
            this.label1.Text = "数据信息";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(60, 50);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 14);
            this.label3.TabIndex = 0;
            this.label3.Text = "输出字段";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(60, 89);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 14);
            this.label2.TabIndex = 0;
            this.label2.Text = "过滤条件";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 4;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 120F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 120F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            this.tableLayoutPanel2.Controls.Add(this.button1, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.comboBox0, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.comboBox1, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.textBoxEx1, 2, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(147, 83);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(308, 32);
            this.tableLayoutPanel2.TabIndex = 6;
            this.tableLayoutPanel2.Controls.SetChildIndex(this.textBoxEx1, 0);
            this.tableLayoutPanel2.Controls.SetChildIndex(this.comboBox1, 0);
            this.tableLayoutPanel2.Controls.SetChildIndex(this.comboBox0, 0);
            this.tableLayoutPanel2.Controls.SetChildIndex(this.button1, 0);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.BackColor = System.Drawing.SystemColors.Window;
            this.button1.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.button1.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Image = global::C2.Properties.Resources.add;
            this.button1.Location = new System.Drawing.Point(272, 4);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(34, 24);
            this.button1.TabIndex = 5;
            this.button1.UseVisualStyleBackColor = false;
            // 
            // textBoxEx1
            // 
            this.textBoxEx1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBoxEx1.Location = new System.Drawing.Point(182, 2);
            this.textBoxEx1.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxEx1.Name = "textBoxEx1";
            this.textBoxEx1.Size = new System.Drawing.Size(86, 21);
            this.textBoxEx1.TabIndex = 6;
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Location = new System.Drawing.Point(57, 113);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(469, 160);
            this.panel1.TabIndex = 1;
            // 
            // FilterOperatorView
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(589, 404);
            this.ControlBox = true;
            this.Controls.Add(this.valuePanel);
            this.Controls.Add(this.bottomPanel);
            this.Controls.Add(this.topPanel);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(605, 442);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(605, 442);
            this.Name = "FilterOperatorView";
            this.ShowIcon = true;
            this.Text = "条件筛选算子设置";
            this.bottomPanel.ResumeLayout(false);
            this.valuePanel.ResumeLayout(false);
            this.valuePanel.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TextBox textBoxEx1;

    }
}