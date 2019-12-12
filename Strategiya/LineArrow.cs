using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Strategiya
{
    public class ArrowLine : LineEnd
    {
        /// <summary>
        /// Линия со стрелками
        /// </summary>
        public ArrowLine() : base ()
        {
        }
        public override string outpat()
        {
            return base.outpat() + ",\tArrow\n";
        }
    }
}
