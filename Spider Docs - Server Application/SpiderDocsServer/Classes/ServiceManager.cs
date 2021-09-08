using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace SpiderDocsServer
{
    public class ServiceManager
    {
        static Logger logger = LogManager.GetCurrentClassLogger();

        string _serviceName = string.Empty;
        public ServiceManager(string serviceName)
        {
            _serviceName = serviceName;
        }

        public void Stop()
        {
            ServiceController service = new ServiceController(_serviceName);

            if (service.Status == ServiceControllerStatus.Stopped)
            {
                logger.Info("SpiderDocs Server Service has already been stopped.");
                return; 
            };

            try
            {
                TimeSpan timeout = TimeSpan.FromMilliseconds(50000);

                service.Stop();
                service.WaitForStatus(ServiceControllerStatus.Stopped, timeout);

                logger.Info("SpiderDocs Server Service has been stopped.");
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        public void Start()
        {
            ServiceController service = new ServiceController(_serviceName);

            if (service.Status == ServiceControllerStatus.Running)
            {
                logger.Info("SpiderDocs Server Service has already been started.");
                return;
            };

            try
            {
                TimeSpan timeout = TimeSpan.FromMilliseconds(50000);

                service.Start(); 
                service.WaitForStatus(ServiceControllerStatus.Running, timeout);

                logger.Info("SpiderDocs Server Service has been started.");
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        public void Restart()
        {
            Stop();
            Start();
        }
    }


}
