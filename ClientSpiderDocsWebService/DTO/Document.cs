using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ClientSpiderDocsWebService.DTO;

namespace ClientSpiderDocsWebService
{
    public class Document : SpiderDocsModule.Document
    {
        public DocumentPermission documentPermission {get ; set;}
        public string base64data { get; set; }
    }
}