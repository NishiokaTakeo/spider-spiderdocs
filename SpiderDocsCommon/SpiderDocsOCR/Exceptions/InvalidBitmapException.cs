using System;

namespace SpiderDocsOCR.Exceptions
{
   public class InvalidBitmapException:Exception
    {
        public InvalidBitmapException(string msg, Exception inner) : base(msg, inner)
        {

        }
    }
}
