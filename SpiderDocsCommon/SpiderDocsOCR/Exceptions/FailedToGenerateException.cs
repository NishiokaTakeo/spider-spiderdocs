﻿using System;

namespace SpiderDocsOCR.Exceptions
{
  public  class FailedToGenerateException:Exception
    {
        public FailedToGenerateException(string msg, Exception innerException) : base(msg, innerException)
        {

        }
    }
}
