using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Strategiya
{
    public class LineEnd
    {
        int x, y;
        /// <summary>
        /// Конец линии(координаты)
        /// </summary>
        public LineEnd()
        {
            Random r = new Random();
            x = r.Next(-10, 11);
            y = r.Next(-10, 11);
            System.Threading.Thread.Sleep(20);
        }
        /// <summary>
        /// Вывод координат точки конца линии
        /// </summary>
        /// <returns></returns>
        public virtual string outpat()
        {
            return "x=" + x + ",\ty=" + y;
        }
    }
}
