using System.Collections.Generic;

namespace SpiderDocsOCR.HocrElements
{
    internal class HParagraph : HOcrClass
    {
        public HParagraph() { Lines = new List<HLine>(); }

        public IList<HLine> Lines { get; set; }
    }
}