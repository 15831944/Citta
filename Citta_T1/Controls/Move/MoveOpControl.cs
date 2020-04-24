﻿using Citta_T1.Controls.Interface;
using Citta_T1.OperatorViews;
using Citta_T1.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Citta_T1.Business.Option;
using Citta_T1.Business.Model;
using System.Linq;
using static Citta_T1.Controls.CanvasPanel;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Diagnostics;
using Citta_T1.Business.Schedule;

namespace Citta_T1.Controls.Move
{ 
    public delegate void DeleteOperatorEventHandler(Control control); 
    public delegate void ModelDocumentDirtyEventHandler();
  

    public partial class MoveOpControl : UserControl, IScalable, IDragable, IMoveControl
    {
        private LogUtil log = LogUtil.GetInstance("MoveOpControl");
        public event ModelDocumentDirtyEventHandler ModelDocumentDirtyEvent;

        private ControlMoveWrapper controlMoveWrapper;
        private static System.Text.Encoding EncodingOfGB2312 = System.Text.Encoding.GetEncoding("GB2312");
        private static string doublePin = "关联算子 取差集 碰撞算子 取并集";

        private string opControlName;
        private Point mouseOffset;

        private bool doublelPinFlag = false;
        private PictureBox leftPinPictureBox1 = new PictureBox();

        private string subTypeName;
        private string oldTextString;
        private OperatorOption option = new OperatorOption();
        private int id;
        private string dataSourceColumns;

        // 一些倍率
        public string ReName { get => textBox.Text; }
        public string SubTypeName { get => subTypeName; }
        public OperatorOption Option { get => this.option; set => this.option = value; }
        private ElementStatus status;
        public ElementStatus Status { 
            get => this.status;
            set
            {                
                this.status = value;
                OptionDirty();
            }  
        }
        public int ID { get => this.id; set => this.id = value; }
        public bool EnableOpenOption { get => this.OptionMenuItem.Enabled; set => this.OptionMenuItem.Enabled = value; }
        public Rectangle RectOut { get => rectOut; set => rectOut = value; }

        public string DataSourceColumns { get => this.dataSourceColumns; set => this.dataSourceColumns = value; }
        public int RevisedPinIndex { get => revisedPinIndex; set => revisedPinIndex = value; }



        // 一些倍率
        // 鼠标放在Pin上，Size的缩放倍率
        int multiFactor = 2;
        // 画布上的缩放倍率
        float factor = 1.3F;
        // 缩放等级
        private int sizeLevel = 0;

        // 绘制贝塞尔曲线的起点
        private int startX;
        private int startY;
        private Point oldcontrolPosition;
        Bezier line;
        public List<Rectangle> leftPinArray = new List<Rectangle> {};
        private int revisedPinIndex;
        // 以该控件为起点的所有点
        private List<int> startLineIndexs = new List<int>() { };
        // 以该控件为终点的所有点
        private List<int> endLineIndexs = new List<int>() { };
        List<Bezier> affectedStartLines = new List<Bezier>() { };
        List<Bezier> affectedEndLines = new List<Bezier>() { };
        public ECommandType cmd = ECommandType.Null;

        // 绘制引脚

        private Point leftPin = new Point(2, 10);
        private Point rightPin = new Point(140, 10);


        private int pinWidth = 6;
        private int pinHeight = 6;
        private Pen pen = new Pen(Color.DarkGray, 1f);
        private SolidBrush trnsRedBrush = new SolidBrush(Color.WhiteSmoke);
        private Rectangle rectIn_down;
        private Rectangle rectIn_up;
        private Rectangle rectOut;
        private String pinStatus = "noEnter";
        private String rectArea = "rectIn_down rectIn_up rectOut";
        private String lineStatus = "";
        private Bitmap staticImage;
        private List<int> linePinArray = new List<int> { };
        // public MoveOpControl() 这个空函数我已经删了好几次了,但每次都又被你们合并进来了
        // 下次谁再给合并进来,我就开始一一排查了, 卢琪 2020.04.12
        public MoveOpControl(int sizeL, string description, string subTypeName, Point loc)
        {
            this.status = ElementStatus.Null;
            InitializeComponent();
            textBox.Text = description;
            this.subTypeName = subTypeName;
            Location = loc;
            doublelPinFlag = doublePin.Contains(SubTypeName);
            this.controlMoveWrapper = new ControlMoveWrapper(this);
            InitializeOpPinPicture();
            InitializeHelpToolTip();
            ChangeSize(sizeL);
            log.Info("Create a MoveOpControl, sizeLevel = " + sizeLevel);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true); // 禁止擦除背景.
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true); // 双缓冲DoubleBuffer
        }
        public void ChangeSize(int sizeL)
        {
            bool originVisible = this.Visible;
            if (originVisible)
                this.Hide();  // 解决控件放大缩小闪烁的问题，非当前文档的元素，不需要hide,show
            if (sizeL > sizeLevel)
            {
                while (sizeL > sizeLevel)
                {
                    ChangeSize(true);
                    sizeLevel += 1;
                }
            }
            else
            {
                while (sizeL < sizeLevel)
                {
                    ChangeSize(false);
                    sizeLevel -= 1;
                }
            }
            if (originVisible)
                this.Show();
        }

        private void InitializeOpPinPicture()
        {          

            int dy = 0;
            if (doublelPinFlag)
            {
                dy = 5;
            }
            rectIn_up = new Rectangle(this.leftPin.X, this.leftPin.Y - dy, this.pinWidth, this.pinHeight);
            this.leftPinArray.Add(rectIn_up);
            this.endLineIndexs.Add(-1);

            rectIn_down = new Rectangle(this.leftPin.X, this.leftPin.Y + dy, this.pinWidth, this.pinHeight);
            this.leftPinArray.Add(rectIn_down);
            this.endLineIndexs.Add(-1);

            rectOut = new Rectangle(this.rightPin.X, this.rightPin.Y, this.pinWidth, this.pinHeight);
            SetOpControlName(this.textBox.Text);
        }

        private void InitializeHelpToolTip()
        {
            switch (subTypeName)
            {
                case "关联算子":
                    this.helpToolTip.SetToolTip(this.rightPictureBox, HelpUtil.RelateOperatorHelpInfo);
                    break;
                case "碰撞算子":
                    this.helpToolTip.SetToolTip(this.rightPictureBox, HelpUtil.CollideOperatorHelpInfo);
                    break;
                case "取并集":
                    this.helpToolTip.SetToolTip(this.rightPictureBox, HelpUtil.UnionOperatorHelpInfo);
                    break;
                case "取差集":
                    this.helpToolTip.SetToolTip(this.rightPictureBox, HelpUtil.DifferOperatorHelpInfo);
                    break;
                case "随机采样":
                    this.helpToolTip.SetToolTip(this.rightPictureBox, HelpUtil.RandomOperatorHelpInfo);
                    break;
                case "过滤算子":
                    this.helpToolTip.SetToolTip(this.rightPictureBox, HelpUtil.FilterOperatorHelpInfo);
                    break;
                case "取最大值":
                    this.helpToolTip.SetToolTip(this.rightPictureBox, HelpUtil.MaxOperatorHelpInfo);
                    break;
                case "取最小值":
                    this.helpToolTip.SetToolTip(this.rightPictureBox, HelpUtil.MinOperatorHelpInfo);
                    break;
                case "取平均值":
                    this.helpToolTip.SetToolTip(this.rightPictureBox, HelpUtil.AvgOperatorHelpInfo);
                    break;
                case "频率算子":
                    this.helpToolTip.SetToolTip(this.rightPictureBox, HelpUtil.FreqOperatorHelpInfo);
                    break;
                case "排序算子":
                    this.helpToolTip.SetToolTip(this.rightPictureBox, HelpUtil.SortOperatorHelpInfo);
                    break;
                case "分组算子":
                    this.helpToolTip.SetToolTip(this.rightPictureBox, HelpUtil.GroupOperatorHelpInfo);
                    break;
                default:
                    break;
            }
    
        }
        #region MOC的事件
        private void MoveOpControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (Global.GetFlowControl().SelectDrag || Global.GetFlowControl().SelectFrame)
                return;
            PinOpLeaveAndEnter(this.PointToClient(MousePosition));

            if (cmd == ECommandType.Null)
                return;
            else if(cmd == ECommandType.Hold)
            {
                #region 控件移动
                int left = this.Left + e.X - mouseOffset.X;
                int top = this.Top + e.Y - mouseOffset.Y;
                this.Location = WorldBoundControl(new Point(left, top));
                #endregion
                bool isUpdateLine = false;
                CanvasPanel canvas = Global.GetCanvasPanel();
                foreach (ModelRelation mr in Global.GetCurrentDocument().ModelRelations)
                {
                    if (mr.StartID == this.id)
                    {
                        mr.StartP = this.GetStartPinLoc(0);
                        mr.UpdatePoints();
                        isUpdateLine = true;
                    }
                    if (mr.EndID == this.id)
                    {
                        mr.EndP = this.GetEndPinLoc(mr.EndPin);
                        mr.UpdatePoints();
                        isUpdateLine = true;
                    }
                    Bezier newLine = new Bezier(mr.StartP, mr.EndP);
                }
                if (isUpdateLine)
                    this.controlMoveWrapper.DragMove(this.Size, Global.GetCanvasPanel().ScreenFactor, e);
            }



        }
        public Point WorldBoundControl(Point Pm)
        {
           // float screenFactor = (this.Parent as CanvasPanel).ScreenFactor;
             float screenFactor = Global.GetCurrentDocument().ScreenFactor;
             Point mapOrigin = Global.GetCurrentDocument().MapOrigin;
            
            int orgX = Convert.ToInt32(Pm.X / screenFactor);
            int orgY = Convert.ToInt32(Pm.Y / screenFactor);
            Point Pw = Global.GetCurrentDocument().ScreenToWorld(new Point(orgX, orgY), mapOrigin);
            

            if (Pw.X < 20)
            {
                Pm.X = 20;
            }
            if (Pw.Y < 70)
            {
                Pm.Y = 70;
            }
            if (Pw.X > 2000 - this.Width)
            {
                Pm.X = this.Parent.Width - this.Width;
            }
            if (Pw.Y > 980  - this.Height)
            {
                Pm.Y = this.Parent.Height - this.Height;
            }
            return Pm;
        }

        private void MoveOpControl_MouseDown(object sender, MouseEventArgs e)
        {

            
            if (Global.GetFlowControl().SelectDrag || Global.GetFlowControl().SelectFrame)
                return;

            if (e.Button == MouseButtons.Left)
            {
                if (rectOut.Contains(e.Location))
                {
                    startX = this.Location.X + e.X;
                    startY = this.Location.Y + e.Y;
                    MouseEventArgs e1 = new MouseEventArgs(e.Button, e.Clicks, startX, startY, 0);
                    cmd = ECommandType.PinDraw;
                    CanvasPanel canvas = (this.Parent as CanvasPanel);
                    canvas.CanvasPanel_MouseDown(this, e1);
                    return;
                }
                mouseOffset.X = e.X;
                mouseOffset.Y = e.Y;
                cmd = ECommandType.Hold;
            }
            this.controlMoveWrapper.DragDown(this.Size, Global.GetCanvasPanel().ScreenFactor, e);
            oldcontrolPosition =this.Location;
         }

        private void TxtButton_MouseDown(object sender, MouseEventArgs e)
        {
            if (Global.GetFlowControl().SelectDrag || Global.GetFlowControl().SelectFrame)
                return;
            // 单击鼠标, 移动控件
            if (e.Clicks == 1)
                MoveOpControl_MouseDown(sender, e);
            // 双击鼠标, 改名字
            if (e.Clicks == 2)
            {
                RenameMenuItem_Click(this, e);
            }
        }

        private void StatusBox_MouseDown(object sender, MouseEventArgs e)
        {   // 只处理左键点击
            if (e.Button != MouseButtons.Left)
                return;
            // 单击视为移动,按父控件鼠标点击处理
            if (e.Clicks == 1)
            {
                base.OnMouseDown(e);
            }// 双击,弹出配置窗口
            else if (e.Clicks == 2)
            {
                // 清空焦点
                Global.GetMainForm().blankButton.Focus();
                // 显示配置
                ShowOptionDialog();
            }
        }

        private void MoveOpControl_MouseUp(object sender, MouseEventArgs e)
        {
            if (Global.GetFlowControl().SelectDrag || Global.GetFlowControl().SelectFrame)
                return;

            if (e.Button == MouseButtons.Left)
            {
                if (cmd == ECommandType.PinDraw)
                {
                    cmd = ECommandType.Null;
                    startX = this.Location.X + e.X;
                    startY = this.Location.Y + e.Y;
                    MouseEventArgs e1 = new MouseEventArgs(e.Button, e.Clicks, startX, startY, 0);
                    CanvasPanel canvas = Global.GetCanvasPanel();
                    canvas.CanvasPanel_MouseUp(this, e1);
                }
                cmd = ECommandType.Null;
                this.controlMoveWrapper.DragUp(this.Size, Global.GetCanvasPanel().ScreenFactor, e);
                Global.GetNaviViewControl().UpdateNaviView();

            }
            if (oldcontrolPosition != this.Location)
                Global.GetMainForm().SetDocumentDirty();


        }

 

        #endregion

        #region 控件名称长短改变时改变控件大小
        private string SubstringByte(string text, int startIndex, int length)
        {
            byte[] bytes = EncodingOfGB2312.GetBytes(text);
            return EncodingOfGB2312.GetString(bytes, startIndex, length);
        }
        public void SetOpControlName(string name)
        {
            this.opControlName = name;
            int maxLength = 8;

            int sumCount = Regex.Matches(name, "[\u4E00-\u9FA5]").Count * 2;
            int sumCountDigit = Regex.Matches(name, "[a-zA-Z0-9]").Count;

            if (sumCount + sumCountDigit > maxLength)
            {
                ResizeToBig();
                this.txtButton.Text = SubstringByte(name, 0, maxLength) + "...";
            }
            else
            {
                this.txtButton.Text = name;
                
                if (sumCount + sumCountDigit < 6) 
                    ResizeToSmall();
                else
                    ResizeToNormal();
            }
            this.nameToolTip.SetToolTip(this.txtButton, name);
        }

        private void ResizeToBig()
        {
            double f = Math.Pow(factor, sizeLevel);
            this.Size = new Size((int)(155 * f), (int)(30 * f));//194，25
            this.rightPictureBox.Location = new Point((int)(130 * f), (int)(7 * f));//159,2
            this.statusBox.Location = new Point((int)(114 * f), (int)(7 * f));//新增
            this.rectOut.Location = new Point((int)(146 * f), (int)(10 * f));

            this.txtButton.Size = new Size((int)(82 * f), (int)(26 * f));
            this.textBox.Size = new Size((int)(82 * f), (int)(26 * f));

            DrawRoundedRect((int)(4 * f), 0, this.Width - (int)(11 * f), this.Height - (int)(2 * f), (int)(3 * f));
        }
        private void ResizeToSmall()
        {
            double f = Math.Pow(factor, sizeLevel);
            this.Size = new Size((int)(130 * f), (int)(28 * f));//142，25
            this.rightPictureBox.Location = new Point((int)(105 * f), (int)(7 * f));//107,2
            this.statusBox.Location = new Point((int)(89 * f), (int)(7 * f));//新增
            this.txtButton.Size = new Size((int)(57 * f), (int)(25 * f));
            this.textBox.Size = new Size((int)(57 * f), (int)(24 * f));
            this.rectOut.Location = new Point((int)(121 * f), (int)(10 * f));
            DrawRoundedRect((int)(4 * f), 0, this.Width - (int)(11 * f), this.Height - (int)(2 * f), (int)(3 * f));
        }
        private void ResizeToNormal()
        {
            double f = Math.Pow(factor, sizeLevel);
            this.Size = new Size((int)(147 * f), (int)(29 * f));//184，25
            this.rightPictureBox.Location = new Point((int)(122 * f), (int)(7 * f));//151,2
            this.statusBox.Location = new Point((int)(104 * f), (int)(7 * f));//新增
            this.txtButton.Size = new Size((int)(72 * f), (int)(25 * f));
            this.textBox.Size = new Size((int)(72 * f), (int)(25 * f));
            this.rectOut.Location = new Point((int)(137 * f), (int)(10 * f));
            DrawRoundedRect((int)(4 * f), 0, this.Width - (int)(11 * f), this.Height - (int)(2 * f), (int)(3 * f));
        }
        #endregion

        #region 右键菜单
        public void OptionMenuItem_Click(object sender, EventArgs e)
        {
            ShowOptionDialog();
        }

        private void ShowOptionDialog()
        {
            if (!this.OptionMenuItem.Enabled)
            {
                MessageBox.Show("该算子没有对应的数据源，暂时还无法配置，请先连接数据，再进行算子设置。");
                return;
            }
            switch (this.subTypeName)
            {
                case "关联算子":
                    new RelateOperatorView(this).ShowDialog();
                    break;
                case "碰撞算子":
                    new CollideOperatorView(this).ShowDialog();
                    break;
                case "取并集":
                    new UnionOperatorView(this).ShowDialog();
                    break;
                case "取差集":
                    new DifferOperatorView(this).ShowDialog();
                    break;
                case "随机采样":
                    new RandomOperatorView(this).ShowDialog();
                    break;
                case "过滤算子":
                    new FilterOperatorView(this).ShowDialog();
                    break;
                case "取最大值":
                    new MaxOperatorView(this).ShowDialog();
                    break;
                case "取最小值":
                    new MinOperatorView(this).ShowDialog();
                    break;
                case "取平均值":
                    new AvgOperatorView(this).ShowDialog();
                    break;
                case "频率算子":
                    new FreqOperatorView(this).ShowDialog();
                    break;
                case "排序算子":
                    new SortOperatorView(this).ShowDialog();
                    break;
                case "分组算子":
                    new GroupOperatorView(this).ShowDialog();
                    break;
                default:
                    break;
            }
        }

        public void RenameMenuItem_Click(object sender, EventArgs e)
        {

            if (Global.GetFlowControl().SelectDrag || Global.GetFlowControl().SelectFrame)
                return;
            this.textBox.ReadOnly = false;
            this.oldTextString = this.textBox.Text;
            this.txtButton.Visible = false;
            this.textBox.Visible = true;
            this.textBox.Focus();//获取焦点
            this.textBox.Select(this.textBox.TextLength, 0);
            ModelDocumentDirtyEvent?.Invoke();
        }

        public void RunMenuItem_Click(object sender, EventArgs e)
        {
            //运行到此
            Manager currentManager = Global.GetCurrentDocument().Manager;
            currentManager.GetCurrentModelRunhereTripleList(Global.GetCurrentDocument(), Global.GetCurrentDocument().SearchElementByID(this.ID));
            int notReadyNum = currentManager.TripleList.AllOperatorNotReadyNum();
            if (notReadyNum > 0)
            {
                MessageBox.Show("有" + notReadyNum + "个未配置的算子，请配置后再运行模型", "未配置", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }


        public void DeleteMenuItem_Click(object sender, EventArgs e)
        {
            if (Global.GetFlowControl().SelectDrag || Global.GetFlowControl().SelectFrame)
                return;
            //删除连接的结果控件
            foreach (ModelRelation mr in Global.GetCurrentDocument().ModelRelations)
            {
                if (mr.StartID == this.id)
                {
                    DeleteResultControl(mr.EndID);
                    break;
                }
            }
            //删除自身
            Global.GetCanvasPanel().DeleteElement(this);
            Global.GetNaviViewControl().UpdateNaviView();
            Global.GetMainForm().DeleteDocumentElement(this);
            Global.GetMainForm().SetDocumentDirty();
           
        }
        private void DeleteResultControl(int endID)
        {
            foreach (ModelElement mrc in Global.GetCurrentDocument().ModelElements)
            {
                if (mrc.ID == endID)
                {
                    Global.GetCanvasPanel().DeleteElement(mrc.GetControl);
                    Global.GetNaviViewControl().UpdateNaviView();
                    Global.GetCurrentDocument().DeleteModelElement(mrc.GetControl);
                    return;
                }
            }
        }
        private void OptionDirty()
        {
            if (this.status == ElementStatus.Null)
                this.statusBox.Image = Properties.Resources.set;
            else if (this.status == ElementStatus.Done)
                this.statusBox.Image = Properties.Resources.done;
            else if (this.status == ElementStatus.Ready)
                this.statusBox.Image = Properties.Resources.setSuccess;
        }
        #endregion

        #region textBox
        public void textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Global.GetFlowControl().SelectDrag || Global.GetFlowControl().SelectFrame)
                return;
            // 按下回车键
            if (e.KeyChar == 13)
            {
                FinishTextChange();
            }
                
        }

        public void textBox1_Leave(object sender, EventArgs e)
        {
            if (Global.GetFlowControl().SelectDrag || Global.GetFlowControl().SelectFrame)
                return;
            FinishTextChange();
        }

        private void FinishTextChange()
        {
            if (this.textBox.Text.Length == 0)
                return;
            this.textBox.ReadOnly = true;
            SetOpControlName(this.textBox.Text);
            this.textBox.Visible = false;
            this.txtButton.Visible = true;
            if (this.oldTextString != this.textBox.Text)
            {
                this.oldTextString = this.textBox.Text;
                Global.GetMainForm().SetDocumentDirty();
            }
            Global.GetCurrentDocument().UpdateAllLines();
            Global.GetCanvasPanel().Invalidate(false);
        }
        #endregion

        #region 针脚事件
        private void PinOpLeaveAndEnter(Point mousePosition)
        {


            if(rectIn_up.Contains(mousePosition))
            {
                if (rectArea.Contains(pinStatus) || linePinArray.Contains(0)) return;
                rectIn_up = rectEnter(rectIn_up);
                this.Invalidate();
                pinStatus = "rectIn_up";
            }

            else if (rectIn_down.Contains(mousePosition))
            {
                if (rectArea.Contains(pinStatus) || linePinArray.Contains(1)) return;
                rectIn_down = rectEnter(rectIn_down);
                this.Invalidate();
                pinStatus = "rectIn_down";

            }
            else if(rectOut.Contains(mousePosition))
            {
                if (rectArea.Contains(pinStatus) || linePinArray.Contains(-1)) return;
                rectOut = rectEnter(rectOut);
                this.Invalidate();
                pinStatus = "rectOut";
            }

            else if (pinStatus != "noEnter")
            {             
                switch (pinStatus)
                {
                    case "rectIn_down":
                        if(!linePinArray.Contains(1))
                            rectIn_down = rectLeave(rectIn_down);
                        break;
                    case "rectIn_up":
                        if (!linePinArray.Contains(0))
                            rectIn_up = rectLeave(rectIn_up);
                        break;
                    case "rectOut":
                        if (!linePinArray.Contains(-1))
                            rectOut = rectLeave(rectOut);
                        break;
                }
                pinStatus = "noEnter";
                this.Invalidate();
            }
        }
        
        public void OutPinInit(String status)
        {
            if (status == "lineExit")
                linePinArray.Add(-1);

            if (pinStatus != "rectOut")
            {
               
                rectOut = rectEnter(rectOut);
                this.Invalidate();
            }

            PinOpLeaveAndEnter(new Point(0, 0));
        }

        public Rectangle rectEnter(Rectangle rect)
        {
            Point oriLtCorner = rect.Location;
            Size oriSize = rect.Size;
            Point oriCenter = new Point(oriLtCorner.X + oriSize.Width / 2, oriLtCorner.Y + oriSize.Height / 2);
            Point dstLtCorner = new Point(oriCenter.X - 4, oriCenter.Y - 4);
            Size dstSize = new Size(8, 8);
            return new Rectangle(dstLtCorner, dstSize);
        }
        public Rectangle rectLeave(Rectangle rect)
        {
            Point oriLtCorner = rect.Location;
            Size oriSize = rect.Size;
            Point oriCenter = new Point(oriLtCorner.X + oriSize.Width / 2, oriLtCorner.Y + oriSize.Height / 2);
            Point dstLtCorner = new Point(oriCenter.X - 3, oriCenter.Y - 3);
            Size dstSize = new Size(6, 6);
            return new Rectangle(dstLtCorner, dstSize);
        }
        #endregion

        #region 托块的放大与缩小
        private void ChangeSize(bool zoomUp, float factor = 1.3F)
        {
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true); // 禁止擦除背景.
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true); // 双缓冲DoubleBuffer
            
            SetDouble(this);
            double f = Math.Pow(factor, sizeLevel);
            DrawRoundedRect((int)(4 * f), 0, this.Width - (int)(11 * f), this.Height - (int)(2 * f), (int)(3 * f));
            if (zoomUp)
            {
                SetControlsBySize(factor, this);
                this.rectOut = SetRectBySize(factor, this.rectOut);
                this.rectIn_down = SetRectBySize(factor, this.rectIn_down);
                this.rectIn_up = SetRectBySize(factor, this.rectIn_up);
                this.Invalidate(); // TODO 干嘛用的？，为什么下面不写一个重绘？
            }
            else
            {
                SetControlsBySize(1 / factor, this);
                this.rectOut = SetRectBySize(1 / factor, this.rectOut);
                this.rectIn_down = SetRectBySize(1 / factor, this.rectIn_down);
                this.rectIn_up = SetRectBySize(1 / factor, this.rectIn_up);
            }


        }

        public static void SetDouble(Control cc)
        {

            cc.GetType().GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance |
                         System.Reflection.BindingFlags.NonPublic).SetValue(cc, true, null);

        }
        public void SetControlsBySize(float f, Control control)
        {      
            control.Width = Convert.ToInt32(control.Width * f);
            control.Height = Convert.ToInt32(control.Height * f);
            control.Left = Convert.ToInt32(control.Left * f);
            control.Top = Convert.ToInt32(control.Top * f);
            control.Font = new Font(control.Font.Name, control.Font.Size * f, control.Font.Style, control.Font.Unit);

            //遍历窗体中的控件，重新设置控件的值
            foreach (Control con in control.Controls)
                SetControlsBySize(f, con);
        }
        public Rectangle SetRectBySize(float f, Rectangle rect)
        {
            rect.Width = Convert.ToInt32(rect.Width * f);
            rect.Height = Convert.ToInt32(rect.Height * f);
            rect.X = Convert.ToInt32(rect.Left * f);
            rect.Y = Convert.ToInt32(rect.Top * f);
            return rect;
        }
        #endregion

        #region 拖动实现

        public void ChangeLoc(float dx, float dy)
        {

            int left = this.Left + Convert.ToInt32(dx);
            int top = this.Top + Convert.ToInt32(dy);
            this.Location = new Point(left, top);
        }


        #endregion

        #region IMoveControl 接口实现方法
        public void UpdateLineWhenMoving()
        {

        }
        public void SaveStartLines(int line_index)
        {
            //this.startLineIndexs.Add(line_index);
        }

        public void SaveEndLines(int line_index)
        {
            /*
             * 绘制动作结束后，将线索引存起来，存哪个针脚看线坐标修正结果
             */
            try
            {
                //this.endLineIndexs[revisedPinIndex] = line_index;
            }
            catch (IndexOutOfRangeException)
            {
                log.Error("索引越界");
            }
            catch (Exception ex)
            {
                log.Error("MoveOpControl SaveEndLines 出错: " + ex.ToString());
            }
        }
        public PointF RevisePointLoc(PointF p)
        {
            /*
             * 1. 遍历当前Document上所有LeftPin，检查该点是否在LeftPin的附近
             * 2. 如果在，对该点就行修正
             */
             // 鼠标判定矩形大小
            int mouseR = 15;
            int pinR = 6;
            bool isRevised = false;
            float maxIntersectPerct = 0.0F;
            PointF revisedP = new PointF(p.X, p.Y);
            Rectangle rect = new Rectangle(
                   new Point((int)p.X - mouseR / 2, (int)p.Y - mouseR / 2),
                   new Size(mouseR, mouseR));
            CanvasPanel canvas = Global.GetCanvasPanel();
            
            foreach (Rectangle _leftPinRect in leftPinArray)
            {
                Rectangle leftPinRect = new Rectangle(
                    new Point(_leftPinRect.Location.X + this.Location.X, _leftPinRect.Location.Y + this.Location.Y),
                    new Size(_leftPinRect.Width, _leftPinRect.Height));
                int pinLeftX = leftPinRect.X;
                int pinTopY = leftPinRect.Y;
                Console.WriteLine(leftPinRect);
                Console.WriteLine(rect);
                if (leftPinRect.IntersectsWith(rect))
                {
                    // 计算相交面积比
                    float iou = OpUtil.IOU(rect, leftPinRect);
                    if (iou > maxIntersectPerct)
                    {
                        maxIntersectPerct = iou;
                        //log.Info("修正鼠标坐标，修正前：" + p.ToString());
                        revisedP = new PointF(
                            pinLeftX + leftPinRect.Width / 2,
                            pinTopY + leftPinRect.Height / 2);
                        // 绑定控件
                        canvas.EndC = this;
                        log.Info("---------------------");
                        log.Info("绑定控件，canvas.EndC = " + canvas.EndC);
                        isRevised = true;
                        this.revisedPinIndex = leftPinArray.IndexOf(_leftPinRect);
                        log.Info("矫正前的point = " + p);
                        log.Info("矫正前的point = " + revisedP + "索引 = " + revisedPinIndex);
                        log.Info("---------------------");
                        //log.Info("revisedPinIndex: " + revisedPinIndex);
                        //log.Info("修正鼠标坐标，修正后：" + revisedP.ToString());
                    }
                }
            }
            if (!isRevised)
                canvas.EndC = null;
            return revisedP;
        }

        public int GetID()
        {
            return this.ID;
        }
        public PointF GetEndPinLoc(int pinIndex)
        {
            switch (pinIndex)
            {
                case 0:
                    return new PointF(
                        this.Location.X + this.rectIn_up.Location.X + this.rectIn_up.Width / 2, 
                        this.Location.Y + this.rectIn_up.Location.Y + this.rectIn_up.Height / 2);
                case 1:
                    return new PointF(
                        this.Location.X + this.rectIn_down.Location.X + this.rectIn_down.Width / 2, 
                        this.Location.Y + this.rectIn_down.Location.Y + this.rectIn_down.Height / 2);
                default:
                    // TODO [DK] 需要定义一个异常
                    return new PointF(0, 0);
            }
        }
        public PointF GetStartPinLoc(int pinIndex)
        {
            return new PointF(
                this.Location.X + this.rectOut.Location.X + this.rectOut.Width / 2, 
                this.Location.Y + this.rectOut.Location.Y + this.rectOut.Height / 2);
        }

        public void BindEndLine(int pinIndex, int relationIndex)
        { 
            try
            {
                this.endLineIndexs[pinIndex] = relationIndex;
            }
            catch (IndexOutOfRangeException)
            {
                log.Error("索引越界");
            }
            catch (Exception ex)
            {
                log.Error("MoveOpControl BindEndLine 出错: " + ex.ToString());
            }
        }


        public void rectInAdd(int pinIndex)
        {
            linePinArray.Add(pinIndex);
            
            
            switch (pinIndex)
            {
                case 1:
                    if (pinStatus!= "rectIn_down")
                    {
                        rectIn_down = rectEnter(rectIn_down);
                        this.Invalidate();
                    }

                    break;
                case 0:
                    if (pinStatus != "rectIn_up")
                    {
                        rectIn_up = rectEnter(rectIn_up);
                        this.Invalidate();
                    }
                    break;

            }
            

            PinOpLeaveAndEnter(new Point(0, 0));
        }
        #endregion

        private void MoveOpControl_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;//去掉锯齿
            e.Graphics.CompositingQuality = CompositingQuality.HighQuality;//合成图像的质量
            e.Graphics.FillEllipse(trnsRedBrush, rectIn_down);
            e.Graphics.DrawEllipse(pen, rectIn_down);
            e.Graphics.FillEllipse(trnsRedBrush, rectIn_up);
            e.Graphics.DrawEllipse(pen, rectIn_up);
            e.Graphics.FillEllipse(trnsRedBrush, rectOut);
            e.Graphics.DrawEllipse(pen, rectOut);
        }

        private void UpdateBackground()
        {

        }
        private void DrawRoundedRect(int x, int y, int width, int height, int radius)
        {
            if (this.staticImage != null)
            {   // bitmap是重型资源,需要强制释放
                this.staticImage.Dispose();
                this.staticImage = null;
            }
            this.staticImage = new Bitmap(this.Width, this.Height);
            Graphics g = Graphics.FromImage(staticImage);
            g.Clear(Color.White);
            //去掉圆角的锯齿
            System.Drawing.Pen p = new System.Drawing.Pen(Color.DarkGray, 1);

            g.SmoothingMode = SmoothingMode.HighQuality;//去掉锯齿
            g.CompositingQuality = CompositingQuality.HighQuality;//合成图像的质量
            g.TextRenderingHint = TextRenderingHint.SingleBitPerPixelGridFit;//去掉文字的锯齿

            //上
            g.DrawLine(pen, new PointF(x + radius, y), new PointF(x + width - radius, y));
            //下
            g.DrawLine(pen, new PointF(x + radius, y + height), new PointF(x + width - radius, y + height));
            //左
            g.DrawLine(pen, new PointF(x, y + radius), new PointF(x, y + height - radius));
            //右
            g.DrawLine(pen, new PointF(x + width, y + radius), new PointF(x + width, y + height - radius));

            //左上角
            g.DrawArc(pen, new Rectangle(x, y, radius * 2, radius * 2), 180, 90);
            //右上角
            g.DrawArc(pen, new Rectangle(x + width - radius * 2, y, radius * 2, radius * 2), 270, 90);
            //左下角
            g.DrawArc(pen, new Rectangle(x, y + height - radius * 2, radius * 2, radius * 2), 90, 90);
            //右下角
            g.DrawArc(pen, new Rectangle(x + width - radius * 2, y + height - radius * 2, radius * 2, radius * 2), 0, 90);
            g.Dispose();

            this.BackgroundImage = this.staticImage;
        }

        private void LeftPicture_MouseEnter(object sender, EventArgs e)
        {
            this.idToolTip.SetToolTip(this.leftPicture, String.Format("元素ID: {0}", this.ID.ToString()));
        }
    }
}