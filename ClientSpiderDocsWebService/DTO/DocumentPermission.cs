using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClientSpiderDocsWebService.DTO
{
    public class DocumentPermission
    {

        public bool delete { get; set; }
        public bool archive { get; set; }


        public DocumentPermission()
        {
        }

        public DocumentPermission(bool _delete, bool _archive)
        {
            this.delete = _delete;
            this.archive = _archive;

        }


    }
}