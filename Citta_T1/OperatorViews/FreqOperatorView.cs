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
    public partial class FreqOperatorView : Form
    {
        private MoveOpControl opControl;
        private string dataPath;
        private string[] columnName;
        private string oldOptionDict;
        private List<int> oldOutList;
        private List<string> selectColumn;
        private List<bool> oldCheckedItems=new List<bool>();
        private List<string> oldColumnName;
        private LogUtil log = LogUtil.GetInstance("FreqOperatorView");

        public FreqOperatorView(MoveOpControl opControl)
        {
            InitializeComponent();
            this.opControl = opControl;
            dataPath = "";
            oldColumnName = new List<string>();
            InitOptionInfo();
            LoadOption();
            this.oldOutList = this.outList.GetItemCheckIndex();
            this.oldCheckedItems.Add(this.noRepetition.Checked);
            this.oldCheckedItems.Add(this.repetition.Checked);
            this.oldCheckedItems.Add(this.ascendingOrder.Checked);
            this.oldCheckedItems.Add(this.descendingOrder.Checked);
            this.oldOptionDict = string.Join(",", this.opControl.Option.OptionDict.ToList());



        }
        #region 初始化配置
        private void InitOptionInfo()
        {
            Dictionary<string, string> dataInfo = Global.GetOptionDao().GetDataSourceInfo(this.opControl.ID);
            if (dataInfo.ContainsKey("dataPath0") && dataInfo.ContainsKey("encoding0"))
            {
                this.dataPath = dataInfo["dataPath0"];
                this.dataInfo.Text = Path.GetFileNameWithoutExtension(this.dataPath);
                SetOption(this.dataPath, this.dataInfo.Text, dataInfo["encoding0"]);
            }
        }

        private void SetOption(string path, string dataName, string encoding)
        {

            BcpInfo bcpInfo = new BcpInfo(path, dataName, ElementType.Null, EnType(encoding));
            string column = bcpInfo.columnLine;
            this.columnName = column.Split('\t');
            foreach (string name in this.columnName)
                this.outList.AddItems(name);
            CompareDataSource();
            this.opControl.SingleDataSourceColumns = column;
            this.opControl.Option.SetOption("columnname", this.opControl.SingleDataSourceColumns);
        }
        private void CompareDataSource()
        {
            //新数据源与旧数据源表头不匹配，对应配置内容是否情况进行判断
            if (this.opControl.Option.GetOption("columnname") == "") return;
            string[] oldColumnList = this.opControl.Option.GetOption("columnname").Split('\t');
            try
            {
                if (this.opControl.Option.GetOption("outfield") != "")
                {
                    string[] checkIndexs = this.opControl.Option.GetOption("outfield").Split(',');
                    int[] outIndex = Array.ConvertAll<string, int>(checkIndexs, int.Parse);
                    if (Global.GetOptionDao().IsDataSourceEqual(oldColumnList, this.columnName, outIndex))
                        this.opControl.Option.OptionDict.Remove("outfield");
                }
            }
            catch (Exception ex) { log.Error(ex.Message); };
        }
        #endregion
        #region 添加取消
        private void confirmButton_Click(object sender, EventArgs e)
        {
            //未设置字段警告           
            if (this.outList.GetItemCheckIndex().Count == 0)
            {
                MessageBox.Show("请选择输出字段!");
                return;
            }
            if (!this.repetition.Checked && !this.noRepetition.Checked)
            {
                MessageBox.Show("请选择数据是否进行去重");
                return;
            }
            if (!this.ascendingOrder.Checked && !this.descendingOrder.Checked)
            {
                MessageBox.Show("请选择数据排序");
                return;
            }
            this.DialogResult = DialogResult.OK;
            if (this.dataInfo.Text == "") return;
            SaveOption();
            //内容修改，引起文档dirty

            if (this.oldCheckedItems[0] != this.noRepetition.Checked)
                Global.GetMainForm().SetDocumentDirty();
            else if(this.oldCheckedItems[1] != this.repetition.Checked)
                Global.GetMainForm().SetDocumentDirty();
            else if(this.oldCheckedItems[2] != this.ascendingOrder.Checked)
                Global.GetMainForm().SetDocumentDirty();
            else if(this.oldCheckedItems[3] != this.descendingOrder .Checked)
                Global.GetMainForm().SetDocumentDirty();
            else if (!this.oldOutList.SequenceEqual(this.outList.GetItemCheckIndex()))
                Global.GetMainForm().SetDocumentDirty();
            //生成结果控件,创建relation,bcp结果文件
            this.selectColumn = this.outList.GetItemCheckText();
            this.selectColumn.Add("频率统计结果");
            ModelElement hasResutl = Global.GetCurrentDocument().SearchResultOperator(this.opControl.ID);
            if (hasResutl == null)
            {
                Global.GetOptionDao().CreateResultControl(this.opControl, this.selectColumn);
                return;
            }
            //输出变化，重写BCP文件
            if (hasResutl != null && !this.oldOutList.SequenceEqual(this.outList.GetItemCheckIndex()))
                Global.GetOptionDao().IsModifyOut(this.oldColumnName, this.outList.GetItemCheckText(), this.opControl.ID);

        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            Close();
        }
        #endregion
        #region 配置信息的保存与加载
        private void SaveOption()
        {
            List<int> checkIndexs = this.outList.GetItemCheckIndex();
            string outField = string.Join(",", checkIndexs);

            this.opControl.Option.SetOption("outfield", outField);
            this.opControl.Option.SetOption("noRepetition", this.noRepetition.Checked.ToString());
            this.opControl.Option.SetOption("repetition", this.repetition.Checked.ToString());
            this.opControl.Option.SetOption("ascendingOrder", this.ascendingOrder.Checked.ToString());
            this.opControl.Option.SetOption("descendingOrder", this.descendingOrder.Checked.ToString());

            this.opControl.Status = ElementStatus.Ready;

        }

        private void LoadOption()
        {
            if (this.opControl.Option.GetOption("noRepetition") != "")
                this.noRepetition.Checked = Convert.ToBoolean(this.opControl.Option.GetOption("noRepetition"));
            if (this.opControl.Option.GetOption("repetition") != "")
                this.repetition.Checked = Convert.ToBoolean(this.opControl.Option.GetOption("repetition"));
            if (this.opControl.Option.GetOption("ascendingOrder") != "")
                this.ascendingOrder.Checked = Convert.ToBoolean(this.opControl.Option.GetOption("ascendingOrder"));
            if (this.opControl.Option.GetOption("descendingOrder") != "")
                this.descendingOrder.Checked = Convert.ToBoolean(this.opControl.Option.GetOption("descendingOrder"));            
            if (this.opControl.Option.GetOption("outfield") != "")
            {
                string[] checkIndexs = this.opControl.Option.GetOption("outfield").Split(',');
                int[] indexs = Array.ConvertAll<string, int>(checkIndexs, int.Parse);
                this.oldOutList = indexs.ToList();
                this.outList.LoadItemCheckIndex(indexs);
                foreach (int index in indexs)
                    this.oldColumnName.Add(this.outList.Items[index].ToString());
            }
           
        }
        #endregion
        private void groupBox1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(this.BackColor);
        }

        private void groupBox2_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(this.BackColor);
        }
        private DSUtil.Encoding EnType(string type)
        { return (DSUtil.Encoding)Enum.Parse(typeof(DSUtil.Encoding), type); }
    }
}
