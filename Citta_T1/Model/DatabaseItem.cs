﻿using C2.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C2.Model
{
    public enum DatabaseType
    {
        Null,
        Oracle,
        Hive
    }
    public class DatabaseItem
    {
        public DatabaseType Type { set; get; }//数据库类型
        public string Server { set; get; }//服务器
        public string Service { set; get; }//服务名
        public string SID { set; get; }//服务sid
        public string Port { set; get; }//端口
        public string User { set; get; }//用户
        public string Password { set; get; }//密码
        public string Group { set; get; }//架构
        public Table DataTable { set; get; }//表名
        //所有信息合并字符串，便于比较是否一致
        public string AllDatabaeInfo 
        {
            get
            { 
                return string.Join("", Type.ToString(), Server, SID, Service, Port, User, Password); 
            }
        }
        public DatabaseItem() { }
        public DatabaseItem(DatabaseType type, string server, string sid, string service, string port, string user, string password, string group = "", Table table =null)
        {
            Type = type;
            Server = server;
            SID = sid;
            Service = service;
            Port = port;
            User = user;
            Password = password;
            Group = group;
            DataTable = table;
        }
    }
}