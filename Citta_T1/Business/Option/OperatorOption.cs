﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Citta_T1.Business.Model;

namespace Citta_T1.Business.Option
{
    
    public class OperatorOption
    {
        private Dictionary<string, string> optionDict = new Dictionary<string, string>();

        public Dictionary<string, string> OptionDict { get => optionDict; }

        public string GetOption(string OpKey)
        {
            if (OptionDict.ContainsKey(OpKey))
                return OptionDict[OpKey];
            return "";
        }
        public void SetOption(string OpKey, string OpVaule)
        {
            OptionDict[OpKey] = OpVaule;
        }

        public int KeysCount(string name)
        {
            int count = 0;
            List<string> keys = this.OptionDict.Keys.ToList();
            foreach (string key in keys)
            { 
                if(key.Contains(name))
                    count += 1;
            }
            return count;
        }
    }
   
}