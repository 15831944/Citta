﻿namespace Citta_T1
{
    partial class LoginForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            this.label1 = new System.Windows.Forms.Label();
            this.userNameComboBox = new System.Windows.Forms.ComboBox();
            this.loginCheckBox = new System.Windows.Forms.CheckBox();
            this.loginbutton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(221, 72);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 31);
            this.label1.TabIndex = 0;
            this.label1.Text = "用户名";
            // 
            // userNameComboBox
            // 
            this.userNameComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.userNameComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.userNameComboBox.Font = new System.Drawing.Font("微软雅黑", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.userNameComboBox.FormattingEnabled = true;
            this.userNameComboBox.Location = new System.Drawing.Point(139, 129);
            this.userNameComboBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.userNameComboBox.Name = "userNameComboBox";
            this.userNameComboBox.Size = new System.Drawing.Size(253, 35);
            this.userNameComboBox.TabIndex = 2;
            this.userNameComboBox.Text = "新手上路";
            this.userNameComboBox.SelectedIndexChanged += new System.EventHandler(this.userNameComboBox_SelectedIndexChanged);
            // 
            // loginCheckBox
            // 
            this.loginCheckBox.AutoSize = true;
            this.loginCheckBox.Checked = true;
            this.loginCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.loginCheckBox.Location = new System.Drawing.Point(139, 236);
            this.loginCheckBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.loginCheckBox.Name = "loginCheckBox";
            this.loginCheckBox.Size = new System.Drawing.Size(110, 20);
            this.loginCheckBox.TabIndex = 4;
            this.loginCheckBox.Text = "记住用户名";
            this.loginCheckBox.UseVisualStyleBackColor = true;
            // 
            // loginbutton
            // 
            this.loginbutton.Location = new System.Drawing.Point(139, 184);
            this.loginbutton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.loginbutton.Name = "loginbutton";
            this.loginbutton.Size = new System.Drawing.Size(255, 31);
            this.loginbutton.TabIndex = 6;
            this.loginbutton.Text = "登录";
            this.loginbutton.UseVisualStyleBackColor = true;
            this.loginbutton.Click += new System.EventHandler(this.LoginButton_Click);
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(535, 336);
            this.Controls.Add(this.loginbutton);
            this.Controls.Add(this.loginCheckBox);
            this.Controls.Add(this.userNameComboBox);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "用户登录";
            this.Load += new System.EventHandler(this.LoginForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox userNameComboBox;
        private System.Windows.Forms.CheckBox loginCheckBox;
        private System.Windows.Forms.Button loginbutton;
    }
}