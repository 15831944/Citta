﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Citta_T1.Business.Schedule.Cmd
{
    class MinOperatorCmd : OperatorCmd
    {
        public MinOperatorCmd(Triple triple) : base(triple)
        {
        }

        public string GenCmd()
        {
            Thread.Sleep(5000);
            return "echo min";
        }

    }
}