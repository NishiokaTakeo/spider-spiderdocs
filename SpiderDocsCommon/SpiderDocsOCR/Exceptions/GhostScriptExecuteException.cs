using System;

namespace SpiderDocsOCR.Exceptions
{
   public  class GhostScriptExecuteException:Exception
    {
        public GhostScriptExecuteException(string msg, Exception innerException) : base(msg, innerException)
        {

        }
    }
}
