﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Citta_T1.Controls.Left;


namespace Citta_T1.Business.DataSource
{
    class DataSourceInfo
    {
        private string userPath;
        private string DataSourcePath;
        public DataSourceInfo(string userName)
        {
           this.userPath = Directory.GetCurrentDirectory().ToString() + "\\cittaModelDocument\\" + userName;
           this.DataSourcePath = this.userPath + "\\DataSourceInformation.xml";
        
         }
       
        public void WriteDataSourceInfo(DataButton db)
        {
            Directory.CreateDirectory(userPath);
            XmlDocument xDoc = new XmlDocument();
            if (!File.Exists(DataSourcePath))
            {              
                XmlElement rootElement = xDoc.CreateElement("DataSourceDocument");
                xDoc.AppendChild(rootElement);
                xDoc.Save(DataSourcePath);
            }
            xDoc.Load(DataSourcePath);
            var node = xDoc.SelectSingleNode("DataSourceDocument");

            XmlElement dataSourceNode = xDoc.CreateElement("DataSource");
            node.AppendChild(dataSourceNode);
            XmlElement nameNode = xDoc.CreateElement("name");
            nameNode.InnerText = db.DataName;
            dataSourceNode.AppendChild(nameNode);

            XmlElement codeNode = xDoc.CreateElement("encoding");
            codeNode.InnerText = db.Encoding.ToString();
            dataSourceNode.AppendChild(codeNode);

            XmlElement pathNode = xDoc.CreateElement("path");
            pathNode.InnerText = db.FilePath;
            dataSourceNode.AppendChild(pathNode);

            XmlElement countNode = xDoc.CreateElement("count");
            countNode.InnerText = "0";//默认为0
            dataSourceNode.AppendChild(countNode);
            xDoc.Save(DataSourcePath);
        }
        public List<DataButton> LoadDataSourceInfo() 
        {
            XmlDocument xDoc = new XmlDocument();
            List<DataButton> dataSourceList = new List<DataButton>();
            if (!File.Exists(DataSourcePath))
                return dataSourceList;
            xDoc.Load(DataSourcePath);
            XmlNode rootNode = xDoc.SelectSingleNode("DataSourceDocument");
            XmlNodeList nodeList = rootNode.SelectNodes("DataSource");
            foreach (XmlNode xn in nodeList)
            {
                try
                {
                    string filePath = xn.SelectSingleNode("path").InnerText;
                    string dataName = xn.SelectSingleNode("name").InnerText;
                    bool isutf8 = Convert.ToBoolean(xn.SelectSingleNode("encoding").InnerText);
                    DataButton dataButton = new DataButton(filePath, dataName, isutf8);
                    dataButton.Count = Convert.ToInt32(xn.SelectSingleNode("count").InnerText);
                    dataSourceList.Add(dataButton);
                }
                catch (Exception e) { System.Console.WriteLine(e.Message); }
            }
            return dataSourceList;
        }
    }
}
