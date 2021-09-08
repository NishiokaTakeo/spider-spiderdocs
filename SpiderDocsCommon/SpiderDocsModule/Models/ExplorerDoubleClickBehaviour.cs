using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using Spider.Common;
using Spider.Types;

//---------------------------------------------------------------------------------
namespace SpiderDocsModule
{
//---------------------------------------------------------------------------------


    public class ExplorerDblClickBehaviour
    {
        public enum en_Behaviour
        {
            INVALID = 0,
            Search = 1,
            Max
        }

        public int id { get; set; } = 0;
        public int id_folder { get; set; } = 0;
        public en_Behaviour id_behaviour { get; set; } = en_Behaviour.INVALID;

        public ExplorerDblClickBehaviour()
        {

        }
    }
}
