﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Citta_T1.Business
{

    /*
     * 一个文档对应一个模型
     */
    class ModelDocument
    {
        private string userName;//用户名
        private string modelTitle;
        private List<ModelElement> modelElements;
        private string savePath;
        //private bool selected;
        private bool dirty;//字段表示模型是否被修改
        /*
         * 传入参数为模型文档名称，当前用户名
         */
        public ModelDocument(string modelTitle, string userName)
        {
            this.modelTitle = modelTitle;
            this.userName = userName;
            this.savePath = Directory.GetCurrentDirectory() + "\\cittaModelDocument\\" + userName + "\\" + modelTitle + "\\";
        }
        /*
         * 保存功能
         */
        public void Save()
        {
            DocumentSaveLoad dSaveLoad = new DocumentSaveLoad(savePath, modelTitle);
            dSaveLoad.WriteXml(modelElements);
        }
        public void AddModelElement(ModelElement modelElement)
        {
            modelElements = new List<ModelElement>();
            modelElements.Add(modelElement);
            dirty = true;
        }
        public void Load()
        {
            DocumentSaveLoad dSaveLoad = new DocumentSaveLoad(savePath, modelTitle);
            modelElements = dSaveLoad.ReadXml();
        }
        public void Show()
        {
            foreach (ModelElement el1 in modelElements)
            {
                el1.Show();
            }
        }
        public void Hide()
        {
            foreach (ModelElement el1 in modelElements)
            {
                el1.Hide();
            }
        }

    }
}
