﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Citta_T1.Controls
{
    public partial class MoveOpControl : UserControl
    {
        private string opControlName;
        private Citta_T1.Dialogs.RenameModel renameModel;
        private bool isMouseDown = false;
        private Point mouseOffset;
        public string doublePin = "连接算子 取差集 取交集 取并集 ";
        public bool doublelPinFlag = false;
        public MoveOpControl()
        {
            this.renameModel = new Citta_T1.Dialogs.RenameModel();
            InitializeComponent();
        }

        public void InitializeOpPinPicture()
        {
            SetOpControlName(this.textButton.Text);
            System.Console.WriteLine(doublelPinFlag);
            if (doublelPinFlag)
            {
                int x = this.leftPinPictureBox.Location.X;
                int y = this.leftPinPictureBox.Location.Y;
                this.leftPinPictureBox.Location = new System.Drawing.Point(x, y-7);
                PictureBox leftPinPictureBox1 = new PictureBox();
                leftPinPictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                leftPinPictureBox1.Location = new System.Drawing.Point(x, y+7);
                leftPinPictureBox1.Name = "leftPinPictureBox1";
                leftPinPictureBox1.Size = this.leftPinPictureBox.Size;
                leftPinPictureBox1.TabIndex = 3;
                leftPinPictureBox1.TabStop = false;
                leftPinPictureBox1.MouseEnter += new System.EventHandler(this.PinOpPictureBox_MouseEnter);
                leftPinPictureBox1.MouseLeave += new System.EventHandler(this.PinOpPictureBox_MouseLeave);
                this.leftPinPictureBox.Parent.Controls.Add(leftPinPictureBox1);
            }
            /*
            System.Windows.Forms.PictureBox leftPicture1 = this.leftPinPictureBox;
            leftPicture1.Location = new System.Drawing.Point(16, 24);
            this.Controls.Add(leftPicture1);
            */
        }

        private void MoveOpControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseDown)
            {
                if (sender is Button)
                {
                    sender = (sender as Button).Parent;
                }
                if (sender is PictureBox)
                {
                    sender = (sender as PictureBox).Parent;
                }
                int left = (sender as MoveOpControl).Left + e.X - mouseOffset.X;
                int top = (sender as MoveOpControl).Top + e.Y - mouseOffset.Y;
                (sender as MoveOpControl).Location = new Point(left, top);
            }
        }

        private void MoveOpControl_MouseDown(object sender, MouseEventArgs e)
        {
            Console.Write("Control");
            if (e.Button == MouseButtons.Left)
            {
                mouseOffset.X = e.X;
                mouseOffset.Y = e.Y;
                isMouseDown = true;
            }
        }

        private void MoveOpControl_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isMouseDown = false;
                if (sender is Button)
                {
                    sender = (sender as Button).Parent;
                }
                if (sender is PictureBox)
                {
                    sender = (sender as PictureBox).Parent;
                }
                Control parent = (sender as MoveOpControl).Parent;
                foreach (Control ct in parent.Controls)
                {
                    if (ct.Name == "naviViewControl")
                    {
                        (ct as NaviViewControl).UpdateNaviView();
                        break;
                    }
                }

            }

        }

        private void PinOpPictureBox_MouseEnter(object sender, EventArgs e)
        {
            (sender as PictureBox).Size = new System.Drawing.Size(15, 15);
        }

        private void PinOpPictureBox_MouseLeave(object sender, EventArgs e)
        {
            (sender as PictureBox).Size = new System.Drawing.Size(10, 10);
        }

        private void MoveOpControl_Load(object sender, EventArgs e)
        {

        }

        private void 菜单2ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        public void SetOpControlName(string opControlName)
        {

            this.opControlName = opControlName;
            int maxLength = 20;
            int sumcount = 0;

            sumcount = Regex.Matches(opControlName.Substring(0, Math.Min(maxLength, opControlName.Length)), "[a-zA-Z0-9]").Count;

            if (opControlName.Length > maxLength && sumcount >= 4)
            {
                if (opControlName.Length <= 8 || sumcount == 6 && opControlName.Length <= 9)
                    this.textButton.Text = opControlName.Substring(0, opControlName.Length);
                else if (sumcount == 6 && opControlName.Length > 9)
                    this.textButton.Text = opControlName.Substring(0, Math.Min(9, opControlName.Length)) + "...";
                else
                    this.textButton.Text = opControlName.Substring(0, Math.Min(7, opControlName.Length)) + "...";
            }
            else if (opControlName.Length > maxLength && sumcount < 4)
            {
                this.textButton.Text = opControlName.Substring(0, maxLength) + "...";
            }
            else
            {
                this.textButton.Text = opControlName.Substring(0, opControlName.Length);
            }
            this.toolTip1.SetToolTip(this.textButton, opControlName);
        }

        private void 重命名ToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            this.renameModel.StartPosition = FormStartPosition.CenterScreen;
            DialogResult dialogResult = this.renameModel.ShowDialog();
            if (dialogResult == DialogResult.OK)
                this.textButton.Text = this.renameModel.opControlName;
                SetOpControlName(this.textButton.Text);
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Panel parentPanel = (Panel)this.Parent;
            parentPanel.Controls.Remove(this);
            foreach (Control ct in parentPanel.Controls)
            {
                if (ct.Name == "naviViewControl")
                {
                    (ct as NaviViewControl).RemoveControl(this);
                    (ct as NaviViewControl).UpdateNaviView();
                    break;
                }
            }
        }
    }
}

