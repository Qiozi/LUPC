using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;

namespace AutoExecute
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            EventLog.WriteEntry("LUComputer Auto Execute start4.");          
            MainRun.OnStart();
            Logs.WriteLog("Start");
        }

        protected override void OnStop()
        {
            MainRun.OnStop();
            EventLog.WriteEntry("LUComputer Auto Execute stop.");
        }
    }
}
