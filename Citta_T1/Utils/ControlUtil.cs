﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Citta_T1.Controls.Title;

namespace Citta_T1.Utils
{
    class ControlUtil
    {
        //
        // 沿着控件向上的返回控件的根节点
        // 如果控件自己就是根节点，返回ct
        //
        public static Control FindRootConrtol(Control ct)
        {
            Control ret = ct;
            while (ret.Parent != null)
                ret = ret.Parent;
            return ret;
        }

        //
        // 递归遍历子控件，根据name寻找子控件
        // 找不到返回null
        //
        public static Control FindControlByName(Control root, string name)
        {
            foreach (Control ct in root.Controls)
            {
                if (ct.Name == name)
                    return ct;
                if (ct.Controls.Count > 0)
                {
                    Control ret = FindControlByName(ct, name);
                    if (ret != null)
                        return ret;
                }          
            }
            return null;
        }

        public static void DisableOrder(DataGridView dgv)
        {
            for (int i = 0; i < dgv.Columns.Count; i++)
            {
                dgv.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }
    }
}
