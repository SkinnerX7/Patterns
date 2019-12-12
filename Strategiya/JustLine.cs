using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Strategiya
{
    public class NonLine : LineEnd
    {
        /// <summary>
        /// Простая линия
        /// </summary>
        public NonLine(): base()
        {
        }
        public override string outpat()
        {
            return base.outpat() + ",\tJust\n";
        }
    }
}
