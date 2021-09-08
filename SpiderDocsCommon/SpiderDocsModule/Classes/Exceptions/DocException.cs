using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NLog;

namespace SpiderDocsModule
{
    [Serializable]
    public class DocException : Exception
	{        
        public string MyTitle {set;get;}
        public string MyMessage {set;get;}

        public DocException(Exception ex) : base(ex.Message, ex.InnerException)
        {

        }

        public DocException(Exception ex, string title, string message): base(ex.Message, ex.InnerException)
        {
            MyTitle = title;
            MyMessage = message;
        }
        
        public DocException(string title, string message)
        {
            MyTitle = title;
            MyMessage = message;
        }

        public void Log(string id = "")
        {
            Logger logger ; 
            if( string.IsNullOrWhiteSpace(id))
            {
                logger = LogManager.GetCurrentClassLogger();
            }
            else
            {
                logger = LogManager.GetLogger(id);
            }
            
            logger.Error(this);
        }
    }
}
