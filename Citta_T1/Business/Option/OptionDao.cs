﻿using Citta_T1.Business.Model;
using Citta_T1.Controls.Move.Dt;
using Citta_T1.Controls.Move.Op;
using Citta_T1.Core;
using Citta_T1.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Citta_T1.Business.Option
{
    class OptionDao
    {
        private static LogUtil log = LogUtil.GetInstance("OptionDao");
        //添加relation
        private static bool IsSingleElement(ModelElement me)
        {
            ElementSubType[] doubleInputs = new ElementSubType[] {
                                                ElementSubType.CollideOperator,
                                                ElementSubType.UnionOperator,
                                                ElementSubType.RelateOperator,
                                                ElementSubType.DifferOperator,
                                                ElementSubType.KeyWordOperator,
                                                ElementSubType.CustomOperator2};
            return !doubleInputs.Contains(me.SubType);
        }



        // 情况1
        // LEFT_ME ----- StartID.MR.EndID ----- RIGHT_ME
        // RIGHT_ME.EnableOption开启设置菜单
        // 更新子图的配置状态
        //
        // 情况2
        // LEFT_ME0 ----- StartID.Brother1.EndID ----- RIGHT_ME(双元素算子)
        //                StartID.Brother2.EndID -----|
        // RIGHT_ME.EnableOption开启设置菜单
        // 更新子图的配置状态
        public void EnableOpOptionView(ModelRelation mr)
        {      
            ModelElement rightMe = Global.GetCurrentDocument().SearchElementByID(mr.EndID);
            // 手工划线时关系的ENDID必须为OPControl
            if (rightMe == ModelElement.Empty || rightMe.Type != ElementType.Operator)
                return;

            MoveOpControl moveOpControl = rightMe.GetControl as MoveOpControl;
            // 情况1   
            if (IsSingleElement(rightMe)) 
            {
                moveOpControl.EnableOption = true;
                DoInputComare(rightMe, mr, null);   
            }
            // 情况2
            else
            {
                List<ModelRelation> brothers = Global.GetCurrentDocument().SearchBrotherRelations(mr);
                if (brothers.Count != 2) return;
                moveOpControl.EnableOption = true;
                DoInputComare(rightMe, brothers[0], brothers[1]);
            }                
        }

        //  情况1: 旧表是新表的子集，且顺序一致
        //         算子恢复配置
        //  情况2: 否则
        //         后续子节点配置状态全为Null

        private void DoInputComare(ModelElement me, ModelRelation mr0, ModelRelation mr1)
        {
            /*
             * 获取单，双输入新旧数据源旧表头
             */
            MoveOpControl moveOpControl = me.GetControl as MoveOpControl;
            List<string> oldColumns0; 
            List<string> oldColumns1 = new List<string>();
            List<string> columns0 = new List<string>() { };
            List<string> columns1 = new List<string>() { };
            //mr1不为null,则me双输入算子
            if (mr1 == null)
            {
                oldColumns0 = moveOpControl.SingleDataSourceColumns;
                if (oldColumns0 == null || oldColumns0.Count() == 0)

                    return;
                GetNewColumns(mr0, columns0, mr1, columns1);
            }  
            else
            {
                Dictionary<string, List<string>> doubleDataSource = moveOpControl.DoubleDataSourceColumns;
                if (!doubleDataSource.ContainsKey("0") || !doubleDataSource.ContainsKey("1"))

                    return;
                oldColumns0 = doubleDataSource["0"];
                oldColumns1 = doubleDataSource["1"];
                if (oldColumns0 == null || oldColumns0.Count() == 0)
                    return;
                if (oldColumns1 == null || oldColumns1.Count() == 0)
                    return;
                if (mr1.EndPin != 1)
                    GetNewColumns(mr1, columns0, mr0, columns1);
                else
                    GetNewColumns(mr0, columns0, mr1, columns1);
            }
           
            if (mr1 == null)
            {
                //单输入情况1
                if (columns0.Count() >= oldColumns0.Count() && oldColumns0.SequenceEqual(columns0.Take(oldColumns0.Count())))
                    me.Status = LastOptionStatus(me);
                //单输入情况2
                else
                    Global.GetCurrentDocument().SetChildrenStatusNull(me.ID);
            }
            else
            {
                //双输入情况1
                bool factor0 = columns0.Count() >= oldColumns0.Count() && oldColumns0.SequenceEqual(columns0.Take(oldColumns0.Count()));
                bool factor1 = columns1.Count() >= oldColumns1.Count() && oldColumns1.SequenceEqual(columns1.Take(oldColumns1.Count()));
                if (factor0 && factor1)
                    me.Status = LastOptionStatus(me);
                //双输入情况2
                else
                    Global.GetCurrentDocument().SetChildrenStatusNull(me.ID);
            }
           


        }
        private void GetNewColumns(ModelRelation mr0, List<string> columns0, ModelRelation mr1, List<string> columns1)
        {
            ModelElement startElement0 = Global.GetCurrentDocument().SearchElementByID(mr0.StartID);
            BcpInfo bcpInfo0 = new BcpInfo(startElement0);
            foreach(string name in bcpInfo0.columnArray)
                columns0.Add(name);
            if (mr1 == null) return;
            ModelElement startElement1 = Global.GetCurrentDocument().SearchElementByID(mr1.StartID);
            BcpInfo bcpInfo1 = new BcpInfo(startElement1);
            foreach (string name in bcpInfo1.columnArray)
                columns1.Add(name);

        }


      
       
        //获取算子上次配置状态
        private ElementStatus LastOptionStatus(ModelElement me)
        { 
            Dictionary<string, string> optionDict = (me.GetControl as MoveOpControl).Option.OptionDict;
            if (optionDict == null) return ElementStatus.Null;
            foreach (KeyValuePair<string, string> kvp in optionDict)
            {
                //python算子、IA多源算子中的其他分隔符字段允许为空,输入其他参数\指定结果文件也可能为空，sort的结束行数也能为空。。。，直接判断为空会出问题
                List<string> keys = new List<string>() { "otherSeparator", "browseChosen", "endRow" };
                if (keys.Contains(kvp.Key)) continue;
                if (optionDict[kvp.Key] == "")
                   return ElementStatus.Null;
            }
            return ElementStatus.Ready;
        }
       

        //新数据源修改输出

        public bool IsDataSourceEqual(string[] oldColumnList, string[] columnName, int[] outIndex) 
        {
            int maxIndex = outIndex.Max();
            if (maxIndex > columnName.Length - 1)
                return true;
            return (!Enumerable.SequenceEqual(oldColumnList, columnName));
  
        }
        public bool IsSingleDataSourceChange(MoveOpControl opControl, string[] columnName,string field, List<int> fieldList = null)
        {
            //新数据源与旧数据源表头不匹配，对应配置内容是否清空进行判断

            if (opControl.Option.GetOption("columnname") == "") return true;
            string[] oldColumnList = opControl.Option.GetOption("columnname").Split('\t');
            try
            {
                if (field.Contains("factor") && opControl.Option.GetOption(field) != "")
                {
                    foreach (int fl in fieldList)
                    {
                        if (fl > columnName.Length - 1 || oldColumnList[fl] != columnName[fl])
                        {
                            opControl.Option.OptionDict[field] = "";
                            return false;
                        }
                    }
                }
                else if (field.Contains("outfield") && opControl.Option.GetOption(field) != "")
                {

                    string[] checkIndexs = opControl.Option.GetOption("outfield").Split(',');
                    int[] outIndex = Array.ConvertAll<string, int>(checkIndexs, int.Parse);
                    if (IsDataSourceEqual(oldColumnList, columnName, outIndex))
                    {
                        opControl.Option.OptionDict["outfield"] = "";
                        return false;
                    }

                }
                else if(opControl.Option.GetOption(field) != "")
                {
                    //单选框配置的判断
                    int index = Convert.ToInt32(opControl.Option.GetOption(field));
                    if (index > columnName.Length - 1 || oldColumnList[index] != columnName[index])
                        opControl.Option.OptionDict[field] = "";
                }
            }
            catch (Exception ex) { log.Error(ex.Message); }
            return true;
        }
        public bool IsDoubleDataSourceChange(MoveOpControl opControl, string[] columnName0, string[] columnName1, string field, List<int> fieldList = null)
        {
            //新数据源与旧数据源表头不匹配，对应配置内容是否情况进行判断
            if (opControl.Option.GetOption("columnname0") == "" || opControl.Option.GetOption("columnname1") == "") return true;
            string[] oldColumnList0 = opControl.Option.GetOption("columnname0").Split('\t');
            string[] oldColumnList1 = opControl.Option.GetOption("columnname1").Split('\t');

            try
            { 
                if (field.Contains("factor") && opControl.Option.GetOption(field) != "")
                {
                    bool IsEqual0 = fieldList[0] > columnName0.Length - 1 || oldColumnList0[fieldList[0]] != columnName0[fieldList[0]];
                    bool IsEqual1 = fieldList[1] > columnName1.Length - 1 || oldColumnList1[fieldList[1]] != columnName1[fieldList[1]];
                    if (IsEqual0 || IsEqual1)
                    {
                        opControl.Option.OptionDict[field] = "";
                        return false;
                    }
                }
                else if (field.Contains("outfield"))
                {

                    string[] checkIndexs = opControl.Option.GetOption(field).Split(',');
                    int[] outIndex = Array.ConvertAll<string, int>(checkIndexs, int.Parse);
                    if (field == "outfield1" && IsDataSourceEqual(oldColumnList1, columnName0, outIndex))
                    {
                        opControl.Option.OptionDict[field] = "";
                        return false;
                    }
                    if (field != "outfield1" && IsDataSourceEqual(oldColumnList0, columnName0, outIndex))
                    {
                        opControl.Option.OptionDict[field] = "";
                        return false;
                    }
                }
            }
            catch (Exception ex) { log.Error(ex.Message); };
            return true;
        }
        //修改配置输出
        public void IsModifyOut(List<string> oldColumns, List<string> currentcolumns, int ID)  
        {
           
            string path = Global.GetCurrentDocument().SearchResultElementByOpID(ID).GetFullFilePath();
            List<string> columns = new List<string>();
           
            //新输出字段中不包含旧字段
            foreach (string cn in oldColumns)
            {
                if (!currentcolumns.Contains(cn))
                {
                    IsNewOut(currentcolumns, ID);
                    return;
                }    
            }
            //新输出字段包含就字段，但是新输出字段数目少于旧字段数目，如并集的重复选择
            if (oldColumns.Count > currentcolumns.Count)
            {
                IsNewOut(currentcolumns, ID);
                return;
            }
            //判断输出顺序是否一致，如排序算子

            if (oldColumns.Count > 0)
            {
                for (int i = 0; i < oldColumns.Count(); i++)
                {
                    if (oldColumns[i] != currentcolumns[i])
                    {
                        IsNewOut(currentcolumns, ID);
                        return;
                    }
                }
                if (currentcolumns.Skip(oldColumns.Count()).Count() != 0)
                {
                    List<string> outColumns = oldColumns.Concat(currentcolumns.Skip(oldColumns.Count())).ToList<string>();
                    BCPBuffer.GetInstance().ReWriteBCPFile(path, outColumns);
                }

            }
            else if (oldColumns.Count == 0)
            { IsNewOut(currentcolumns, ID); }
                   
        }
        public void IsModifyDoubleOut(List<string> oldColumns0, List<string> currentcolumns0, List<string> oldColumns1, List<string> currentcolumns1, int ID)
        {
            List<string> columns = new List<string>();
            string path = Global.GetCurrentDocument().SearchResultElementByOpID(ID).GetFullFilePath();
            //左侧数据源判断
            if (oldColumns0.Count() != currentcolumns0.Count()|| !oldColumns0.SequenceEqual(currentcolumns0))
            {
                IsNewOut(currentcolumns0.Concat(currentcolumns1).ToList(), ID);
                return;
            }
            //右侧数据源判断,新输出字段中不包含旧字段
            foreach (string cn in oldColumns1)
            {
                if (!currentcolumns1.Contains(cn))
                {
                    IsNewOut(currentcolumns0.Concat(currentcolumns1).ToList(), ID);
                    return;
                }
            }

            //判断输出顺序是否一致，如排序算子

            if (oldColumns1.Count > 0)
            {
                for (int i = 0; i < oldColumns1.Count(); i++)
                {
                    if (oldColumns1[i] != currentcolumns1[i])
                    {
                        IsNewOut(currentcolumns0.Concat(currentcolumns1).ToList(), ID);
                        return;
                    }
                }
                if (currentcolumns1.Skip(oldColumns1.Count()).Count() != 0)
                {
                    List<string> outColumns = oldColumns1.Concat(currentcolumns1.Skip(oldColumns1.Count())).ToList<string>();
                    BCPBuffer.GetInstance().ReWriteBCPFile(path, currentcolumns0.Concat(outColumns).ToList());
                }
            }
            else if(oldColumns1.Count == 0)
            { IsNewOut(currentcolumns0.Concat(currentcolumns1).ToList(), ID); }
        }

        public void IsNewOut( List<string> currentColumns, int ID)
        {
            string fullFilePath = Global.GetCurrentDocument().SearchResultElementByOpID(ID).GetFullFilePath();
            BCPBuffer.GetInstance().ReWriteBCPFile(fullFilePath, currentColumns);
            Global.GetCurrentDocument().SetChildrenStatusNull(ID);
        }
        //更新输出列表选定项的索引
        public void UpdateOutputCheckIndexs(List<int> checkIndexs, List<int> outIndexs)
        {
            foreach (int index in checkIndexs)
            {
                if (!outIndexs.Contains(index))
                    outIndexs.Add(index);
            }
            foreach (int index in outIndexs)
            {
                if (!checkIndexs.Contains(index))
                {
                    outIndexs.Clear();
                    outIndexs.AddRange(checkIndexs);
                    break;
                }
            }
 
        }

        //配置初始化
        public Dictionary<string, string> GetDataSourceInfo(int ID, bool singelOperation = true)
        {
           
            Dictionary<string, string> dataInfo=new Dictionary<string, string>();
            Dictionary<int, int> startControls = new Dictionary<int,int>();
            foreach (ModelRelation mr in Global.GetCurrentDocument().ModelRelations)
            {
                if (mr.EndID == ID && singelOperation)
                {
                    startControls[mr.EndPin] = mr.StartID;
                    break;
                }
                else if (mr.EndID == ID && !singelOperation)
                    startControls[mr.EndPin] = mr.StartID;

            }
            if(startControls.Count == 0)
                return dataInfo;
            foreach (KeyValuePair<int,int> kvp in startControls)
            {
                char separator = '\t';
                ModelElement me = Global.GetCurrentDocument().SearchElementByID(kvp.Value);
                dataInfo["dataPath" + kvp.Key.ToString()] = me.GetFullFilePath();
                dataInfo["encoding" + kvp.Key.ToString()] = me.Encoding.ToString();
                if (me.GetControl is MoveDtControl)
                    separator = (me.GetControl as MoveDtControl).Separator;
                dataInfo["separator" + kvp.Key.ToString()] = separator.ToString();
            }
            return dataInfo;
        }
     
        public void GetSelectedItemIndex(object sender, EventArgs e)
        {
            (sender as ComboBox).Tag = (sender as ComboBox).SelectedIndex.ToString();
        }
   
    }
}
