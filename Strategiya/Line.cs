using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Strategiya
{
    public class Line
    {
        LineEnd startline, endline;
        /// <summary>
        /// Линия с началом в точке а и концом в точке b
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public Line(LineEnd a, LineEnd b)
        {
            startline = a;
            endline = b;
        }
        /// <summary>
        /// Вывод линии
        /// </summary>
        /// <returns></returns>
        public string OutputLine()
        {
            return startline.outpat()+ endline.outpat();
        }
    }
}
