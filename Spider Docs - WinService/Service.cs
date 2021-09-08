using System;
using System.ServiceProcess;

namespace SpiderDocsWinService
{
    public partial class Service : ServiceBase
    {
        Form1 Form1 = new Form1();

        protected override void OnStart(string[] args)
        {
            Form1.startService();
        }

        protected override void OnStop()
        {
            Form1.stopService();
        }
    }
}
