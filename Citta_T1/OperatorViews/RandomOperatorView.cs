﻿using Citta_T1.Business.Model;
using Citta_T1.Business.Option;
using Citta_T1.Controls.Move;
using Citta_T1.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Citta_T1.OperatorViews
{

    public partial class RandomOperatorView : Form
    {
        private MoveOpControl opControl;
        private string dataPath;
        private string oldRandomNum;
        private List<int> oldOutList;
        public RandomOperatorView(MoveOpControl opControl)
        {
            InitializeComponent();
            this.dataPath = "";
            this.opControl = opControl;
            InitOptionInfo();
            LoadOption();
            this.oldRandomNum =this.RandomNumBox.Text;
            this.oldOutList = this.OutList.GetItemCheckIndex();

        }
        #region 初始化配置
        private void InitOptionInfo()
        {
            int startID = -1;
            string encoding = "";
            List<ModelRelation> modelRelations = Global.GetCurrentDocument().ModelRelations;
            List<ModelElement> modelElements = Global.GetCurrentDocument().ModelElements;
            foreach (ModelRelation mr in modelRelations)
            {
                if (mr.End == this.opControl.ID)
                {
                    startID = mr.Start;
                    break;
                }
            }
            foreach (ModelElement me in modelElements)
            {
                if (me.ID == startID)
                {
                    this.dataPath = me.GetPath();
                    //设置数据信息选项
                    this.DataInfoBox.Text = Path.GetFileNameWithoutExtension(this.dataPath);
                    encoding = me.Encoding.ToString();
                    break;
                }
            }
            if (this.dataPath != "")
                SetOption(this.dataPath, this.DataInfoBox.Text, encoding);

        }
        private void SetOption(string path, string dataName, string encoding)
        {

            BcpInfo bcpInfo = new BcpInfo(path, dataName, ElementType.Null, EnType(encoding));
            string column = bcpInfo.columnLine;
            string[] columnName = column.Split('\t');
            foreach (string name in columnName)
            {
                this.OutList.AddItems(name);
            }
        }

        #endregion
        #region 配置信息的保存与加载
        private void SaveOption()
        {
            this.opControl.Option.SetOption("randomnum", this.RandomNumBox.Text);
            List<int> checkIndexs = this.OutList.GetItemCheckIndex();
            string outField = string.Join(",", checkIndexs);
            this.opControl.Option.SetOption("outfield", outField);             
            if (this.RandomNumBox.Text != "" && outField != "")
                this.opControl.Status = ElementStatus.Ready;

        }

        private void LoadOption()
        {
            if (this.opControl.Option.GetOption("randomnum") != "")
                this.RandomNumBox.Text = this.opControl.Option.GetOption("randomnum");
            if (this.opControl.Option.GetOption("outfield") != "")
            {
                string[] checkIndexs = this.opControl.Option.GetOption("outfield").Split(',');
                this.OutList.LoadItemCheckIndex(Array.ConvertAll<string, int>(checkIndexs, int.Parse));
            }
        }
        #endregion
        #region 添加取消
        private void confirmButton_Click(object sender, EventArgs e)
        {
            //未设置字段警告
            if (this.RandomNumBox.Text == "")
            {
                MessageBox.Show("请选择随机条数字段!");
                return;
            }
            if (this.OutList.GetItemCheckIndex().Count == 0)
            {
                MessageBox.Show("请选择输出字段!");
                return;
            }
            this.DialogResult = DialogResult.OK;
            if (this.DataInfoBox.Text == "") return;
            SaveOption();
            //内容修改，引起文档dirty
            if (this.oldRandomNum!= this.RandomNumBox.Text)
                Global.GetMainForm().SetDocumentDirty();
            else if (!this.oldOutList.SequenceEqual(this.OutList.GetItemCheckIndex()))
                Global.GetMainForm().SetDocumentDirty();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            Close();
        }
        #endregion
        private DSUtil.Encoding EnType(string type)
        {
            return (DSUtil.Encoding)Enum.Parse(typeof(DSUtil.Encoding), type);
        }
    }
}
