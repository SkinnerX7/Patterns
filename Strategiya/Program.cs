using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Strategiya
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Line> MassLines = new List<Line>();
            Random r = new Random();
            for (int i=0; i<10;i++)
            {
                LineEnd start, end;
                switch (r.Next(0, 3))
                {
                    case 0: start = new NonLine();
                        break;
                    case 1: start = new ArrowLine();
                        break;
                    case 2: start = new LineRomb();
                        break;
                    default: start = new NonLine();break;
                }
                switch (r.Next(0, 3))
                {
                    case 0: end = new NonLine();
                        break;
                    case 1: end = new ArrowLine();
                        break;
                    case 2: end = new LineRomb();
                        break;
                    default: end = new NonLine(); break;
                }
                MassLines.Add(new Line(start,end));
            }
            for(int i=0; i < MassLines.Count; i++)
            {
                Console.Write("Линия {0}\n",i+1);
                Console.Write(MassLines[i].OutputLine());
            }
            Console.Read();
        }
    }
}
