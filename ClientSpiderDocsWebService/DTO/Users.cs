using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClientSpiderDocsWebService
{
    public class Users
    {
        public int id { get; set; }
        public string login { get; set; }
        public string name { get; set; }
        public string password { get; set; }
        public int active { get; set; }
        public int id_permission { get; set; }
    }
}