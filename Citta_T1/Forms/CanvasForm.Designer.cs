﻿namespace C2.Forms
{
    partial class CanvasForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CanvasForm));
            this.canvasPanel = new C2.Controls.CanvasPanel();
            this.topToolBarControl = new C2.Controls.Top.TopToolBarControl();
            this.currentModelRunBackLab = new System.Windows.Forms.Label();
            this.currentModelFinLab = new System.Windows.Forms.Label();
            this.currentModelRunLab = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.resetButton = new System.Windows.Forms.Button();
            this.stopButton = new System.Windows.Forms.Button();
            this.runButton = new System.Windows.Forms.Button();
            this.rightShowButton = new C2.Controls.Flow.RightShowButton();
            this.rightHideButton = new C2.Controls.Flow.RightHideButton();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.progressBarLabel = new System.Windows.Forms.Label();
            this.remarkControl = new C2.Controls.Flow.RemarkControl();
            this.flowControl = new C2.Controls.Flow.FlowControl();
            this.operatorControl = new C2.Controls.Left.OperatorControl();
            this.SuspendLayout();
            // 
            // canvasPanel
            // 
            this.canvasPanel.AllowDrop = true;
            this.canvasPanel.BackColor = System.Drawing.Color.White;
            this.canvasPanel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.canvasPanel.DelEnable = false;
            this.canvasPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.canvasPanel.Document = null;
            this.canvasPanel.EndC = null;
            this.canvasPanel.EndP = ((System.Drawing.PointF)(resources.GetObject("canvasPanel.EndP")));
            this.canvasPanel.LeftButtonDown = false;
            this.canvasPanel.Location = new System.Drawing.Point(0, 33);
            this.canvasPanel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.canvasPanel.Name = "canvasPanel";
            this.canvasPanel.Size = new System.Drawing.Size(1106, 475);
            this.canvasPanel.StartC = null;
            this.canvasPanel.StartP = ((System.Drawing.PointF)(resources.GetObject("canvasPanel.StartP")));
            this.canvasPanel.TabIndex = 8;
            // 
            // topToolBarControl
            // 
            this.topToolBarControl.BackColor = System.Drawing.Color.GhostWhite;
            this.topToolBarControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.topToolBarControl.Location = new System.Drawing.Point(0, 0);
            this.topToolBarControl.Name = "topToolBarControl";
            this.topToolBarControl.Size = new System.Drawing.Size(1106, 33);
            this.topToolBarControl.TabIndex = 25;
            // 
            // currentModelRunBackLab
            // 
            this.currentModelRunBackLab.Image = global::C2.Properties.Resources.currentModelRunningBack;
            this.currentModelRunBackLab.Location = new System.Drawing.Point(300, 68);
            this.currentModelRunBackLab.Name = "currentModelRunBackLab";
            this.currentModelRunBackLab.Size = new System.Drawing.Size(150, 100);
            this.currentModelRunBackLab.TabIndex = 26;
            this.currentModelRunBackLab.Visible = false;
            // 
            // currentModelFinLab
            // 
            this.currentModelFinLab.Image = global::C2.Properties.Resources.currentModelFin;
            this.currentModelFinLab.Location = new System.Drawing.Point(612, 68);
            this.currentModelFinLab.Name = "currentModelFinLab";
            this.currentModelFinLab.Size = new System.Drawing.Size(150, 100);
            this.currentModelFinLab.TabIndex = 27;
            this.currentModelFinLab.Visible = false;
            // 
            // currentModelRunLab
            // 
            this.currentModelRunLab.Image = global::C2.Properties.Resources.currentModelRunning;
            this.currentModelRunLab.Location = new System.Drawing.Point(456, 68);
            this.currentModelRunLab.Name = "currentModelRunLab";
            this.currentModelRunLab.Size = new System.Drawing.Size(150, 100);
            this.currentModelRunLab.TabIndex = 28;
            this.currentModelRunLab.Visible = false;
            // 
            // resetButton
            // 
            this.resetButton.BackColor = System.Drawing.Color.White;
            this.resetButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.resetButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.resetButton.FlatAppearance.BorderSize = 0;
            this.resetButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.resetButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.resetButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.resetButton.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.resetButton.Image = global::C2.Properties.Resources.reset;
            this.resetButton.Location = new System.Drawing.Point(223, 133);
            this.resetButton.Name = "resetButton";
            this.resetButton.Size = new System.Drawing.Size(52, 53);
            this.resetButton.TabIndex = 32;
            this.resetButton.UseVisualStyleBackColor = false;
            this.resetButton.Click += new System.EventHandler(this.ResetButton_Click);
            // 
            // stopButton
            // 
            this.stopButton.BackColor = System.Drawing.Color.White;
            this.stopButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.stopButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.stopButton.FlatAppearance.BorderSize = 0;
            this.stopButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.stopButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.stopButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.stopButton.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.stopButton.Image = ((System.Drawing.Image)(resources.GetObject("stopButton.Image")));
            this.stopButton.Location = new System.Drawing.Point(138, 133);
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(52, 53);
            this.stopButton.TabIndex = 31;
            this.stopButton.UseVisualStyleBackColor = false;
            this.stopButton.Click += new System.EventHandler(this.StopButton_Click);
            // 
            // runButton
            // 
            this.runButton.BackColor = System.Drawing.Color.White;
            this.runButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.runButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.runButton.FlatAppearance.BorderSize = 0;
            this.runButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.runButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.runButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.runButton.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.runButton.Image = ((System.Drawing.Image)(resources.GetObject("runButton.Image")));
            this.runButton.Location = new System.Drawing.Point(56, 133);
            this.runButton.Name = "runButton";
            this.runButton.Size = new System.Drawing.Size(52, 53);
            this.runButton.TabIndex = 30;
            this.runButton.UseVisualStyleBackColor = false;
            this.runButton.Click += new System.EventHandler(this.RunButton_Click);
            // 
            // rightShowButton
            // 
            this.rightShowButton.BackColor = System.Drawing.Color.Transparent;
            this.rightShowButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("rightShowButton.BackgroundImage")));
            this.rightShowButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.rightShowButton.Location = new System.Drawing.Point(122, 39);
            this.rightShowButton.Name = "rightShowButton";
            this.rightShowButton.Size = new System.Drawing.Size(55, 55);
            this.rightShowButton.TabIndex = 34;
            // 
            // rightHideButton
            // 
            this.rightHideButton.BackColor = System.Drawing.Color.Transparent;
            this.rightHideButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("rightHideButton.BackgroundImage")));
            this.rightHideButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.rightHideButton.Location = new System.Drawing.Point(183, 39);
            this.rightHideButton.Name = "rightHideButton";
            this.rightHideButton.Size = new System.Drawing.Size(55, 55);
            this.rightHideButton.TabIndex = 33;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(806, 107);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(125, 10);
            this.progressBar1.TabIndex = 35;
            this.progressBar1.Visible = false;
            // 
            // progressBarLabel
            // 
            this.progressBarLabel.AutoSize = true;
            this.progressBarLabel.BackColor = System.Drawing.Color.Transparent;
            this.progressBarLabel.Font = new System.Drawing.Font("微软雅黑", 8.25F);
            this.progressBarLabel.ForeColor = System.Drawing.Color.Black;
            this.progressBarLabel.Location = new System.Drawing.Point(873, 133);
            this.progressBarLabel.Name = "progressBarLabel";
            this.progressBarLabel.Size = new System.Drawing.Size(0, 16);
            this.progressBarLabel.TabIndex = 36;
            this.progressBarLabel.Visible = false;
            // 
            // remarkControl
            // 
            this.remarkControl.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.remarkControl.BackColor = System.Drawing.Color.Transparent;
            this.remarkControl.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("remarkControl.BackgroundImage")));
            this.remarkControl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.remarkControl.Location = new System.Drawing.Point(518, 50);
            this.remarkControl.Margin = new System.Windows.Forms.Padding(4);
            this.remarkControl.Name = "remarkControl";
            this.remarkControl.RemarkDescription = "";
            this.remarkControl.Size = new System.Drawing.Size(135, 380);
            this.remarkControl.TabIndex = 26;
            this.remarkControl.Visible = false;
            // 
            // flowControl
            // 
            this.flowControl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.flowControl.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("flowControl.BackgroundImage")));
            this.flowControl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.flowControl.Cursor = System.Windows.Forms.Cursors.Default;
            this.flowControl.Location = new System.Drawing.Point(687, 50);
            this.flowControl.Margin = new System.Windows.Forms.Padding(4);
            this.flowControl.Name = "flowControl";
            this.flowControl.SelectDrag = false;
            this.flowControl.SelectFrame = false;
            this.flowControl.SelectRemark = false;
            this.flowControl.Size = new System.Drawing.Size(209, 51);
            this.flowControl.TabIndex = 25;
            // 
            // operatorControl
            // 
            this.operatorControl.AllowDrop = true;
            this.operatorControl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.operatorControl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.operatorControl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.operatorControl.Location = new System.Drawing.Point(435, -61);
            this.operatorControl.Name = "operatorControl";
            this.operatorControl.Size = new System.Drawing.Size(215, 320);
            this.operatorControl.TabIndex = 40;
            // 
            // CanvasForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1106, 508);
            this.Controls.Add(this.operatorControl);
            this.Controls.Add(this.remarkControl);
            this.Controls.Add(this.flowControl);
            this.Controls.Add(this.progressBarLabel);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.rightShowButton);
            this.Controls.Add(this.rightHideButton);
            this.Controls.Add(this.resetButton);
            this.Controls.Add(this.stopButton);
            this.Controls.Add(this.runButton);
            this.Controls.Add(this.currentModelRunLab);
            this.Controls.Add(this.currentModelFinLab);
            this.Controls.Add(this.currentModelRunBackLab);
            this.Controls.Add(this.canvasPanel);
            this.Controls.Add(this.topToolBarControl);
            this.Name = "CanvasForm";
            this.Text = "CanvasForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CanvasForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.CanvasForm_FormClosed);
            this.SizeChanged += new System.EventHandler(this.CanvasForm_SizeChanged);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.CanvasPanel canvasPanel;
        private Controls.Top.TopToolBarControl topToolBarControl;
        private System.Windows.Forms.Label currentModelRunBackLab;
        private System.Windows.Forms.Label currentModelFinLab;
        private System.Windows.Forms.Label currentModelRunLab;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button resetButton;
        private System.Windows.Forms.Button stopButton;
        private System.Windows.Forms.Button runButton;
        private Controls.Flow.RightShowButton rightShowButton;
        private Controls.Flow.RightHideButton rightHideButton;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label progressBarLabel;
        private Controls.Flow.RemarkControl remarkControl;
        private Controls.Flow.FlowControl flowControl;
        private Controls.Left.OperatorControl operatorControl;
    }
}