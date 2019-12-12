using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Strategiya
{
    public class LineRomb : LineEnd
    {
        /// <summary>
        /// Линия с ромбами
        /// </summary>
        public LineRomb(): base()
        {
        }
        public override string outpat()
        {
            return base.outpat() + ",\tRomb\n";
        }
    }
}
