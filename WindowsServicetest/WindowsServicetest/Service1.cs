using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace WindowsServicetest
{
    public partial class Service1 : ServiceBase
    {

        public Service1()
        {
            InitializeComponent();
            eventLog1 = new System.Diagnostics.EventLog();
            if (!System.Diagnostics.EventLog.SourceExists("ServiceSourceBenoit"))
            {
                System.Diagnostics.EventLog.CreateEventSource(
                    "ServiceSourceBenoit", "Ca c'est un event de fou");
            }
            eventLog1.Source = "MySource";
            eventLog1.Log = "MyNewLog";


        }

        protected override void OnStart(string[] args)
        {
            eventLog1.WriteEntry("On demarre le service");
        }

        protected override void OnStop()
        {
            eventLog1.WriteEntry("on arrete le service");
        }
    }
}
