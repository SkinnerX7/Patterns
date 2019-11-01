using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.IO;
using System.Drawing.Drawing2D;
using System.Threading;

namespace Paint
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            g = panel.CreateGraphics();
        }
        /// <summary>
        /// Текущий цвет
        /// </summary>
        Color Color = Color.Black;
        /// <summary>
        /// Переменная для опеределения когда можно рисовать на panel
        /// </summary>
        bool isPressed = false;
        /// <summary>
        /// Текущая точка ресунка
        /// </summary>
        Point CurrentPoint;
        /// <summary>
        /// Это начальная точка рисунка
        /// </summary>
        Point PrevPoint;  
        /// <summary>
        /// Графическая переменная
        /// </summary>
        Graphics g;
        /// <summary>
        /// Диалоговое окно для выбора цвета
        /// </summary>
        ColorDialog colorDialog = new ColorDialog();
        /// <summary>
        /// Выбор цвета
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Colors_Click(object sender, EventArgs e)
        {
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                Color = colorDialog.Color; //меняем цвет для пера
                label2.BackColor = colorDialog.Color; //меняем цвет для Фона label2
            }
        }
        /// <summary>
        /// Очистить холст
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Clear_Click(object sender, EventArgs e)
        {
            panel.Refresh();
            list.Clear();
            list2.Clear();
            list3.Clear();
        }
        /// <summary>
        /// Нажали на холст
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panel_MouseDown(object sender, MouseEventArgs e)
        {
            isPressed = true;
            if(flag==-1)
                CurrentPoint = e.Location;
            if (flag == 0)
                PrevPoint = e.Location;
            if (flag == 1)
                PrevPoint = e.Location;
        }
        /// <summary>
        /// Ведем курсор по холсту
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panel_MouseMove(object sender, MouseEventArgs e)
        {
            if (isPressed)//пока жмём рисуем
            {
                if (flag == -1) //перо
                {
                    PrevPoint = CurrentPoint;
                    CurrentPoint = e.Location;
                    paint();
                }
            }
        }
        /// <summary>
        /// Переменная для задавания цвета и толщины разным объектам
        /// </summary>
        Pen pen;
        /// <summary>
        /// Лист, хранящий линии карандаша
        /// </summary>
        Karandash list = new Karandash();
        /// <summary>
        /// Лист, хранящий прямоугольники
        /// </summary>
        Paint.Pryam list2 = new Paint.Pryam();
        /// <summary>
        /// Лист, хранящий эллипсы
        /// </summary>
        Paint.Pryam list3 = new Paint.Pryam();
        
        /// <summary>
        /// Метод, рисующий карандашом
        /// </summary>
        private void paint()
        {
            fl = false;
            pen = new Pen(Color, (float)numericUpDown1.Value); //Создаем перо, задаем ему цвет и толщину.
            pen.StartCap = LineCap.Round;
            pen.EndCap = LineCap.Round;
            pen.LineJoin = LineJoin.Round;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            list.Add(pen.Color, (int)pen.Width, CurrentPoint, PrevPoint);
            g.DrawLine(pen, CurrentPoint, PrevPoint);

        }

        /// <summary>
        /// Метод, рисующий прямоугольники и эллипсы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panel_MouseUp(object sender, MouseEventArgs e)
        {
            isPressed = false;
            fl = false;
            if (flag == -1) // если рисуем карандашом, уходим из метода
                return;
            CurrentPoint = e.Location;
            pen = new Pen(Color, (float)numericUpDown1.Value);
            int width = CurrentPoint.X - PrevPoint.X; // запоминание координат
            int heidht = CurrentPoint.Y - PrevPoint.Y; // запоминание координат
            if (width < 0 && heidht < 0) // Перенос ключевых точек для рисования
            {
                Point t = CurrentPoint;
                CurrentPoint = PrevPoint;
                PrevPoint = t;
            }
            else if (width > 0 && heidht < 0) // Перенос ключевых точек для рисования
            {
                Point def1 = PrevPoint;
                Point def2 = CurrentPoint;
                PrevPoint.X = def1.X;
                PrevPoint.Y = def2.Y;
                CurrentPoint.X = def2.X;
                CurrentPoint.Y = def1.Y;

            }
            else if (width < 0 && heidht > 0) // Перенос ключевых точек для рисования
            {
                Point def1 = PrevPoint;
                Point def2 = CurrentPoint;
                PrevPoint.X = def2.X;
                PrevPoint.Y = def1.Y;
                CurrentPoint.X = def1.X;
                CurrentPoint.Y = def2.Y;
            }
            if (flag == 0) // рисуем прямоугольник
            {
                g.DrawRectangle(pen, PrevPoint.X, PrevPoint.Y, CurrentPoint.X - PrevPoint.X, CurrentPoint.Y - PrevPoint.Y);
                list2.Add(pen.Color, (int)pen.Width, PrevPoint,CurrentPoint);
            }
            if(flag == 1) // рисуем эллипс
            {
                g.DrawEllipse(pen, PrevPoint.X, PrevPoint.Y, CurrentPoint.X - PrevPoint.X, CurrentPoint.Y - PrevPoint.Y);
                list3.Add(pen.Color,(int)pen.Width, PrevPoint, CurrentPoint);
            }
        }

        private void выходToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Выйти ?", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Версия 10.1705.21204.0 \n © Корпорация Весёлый Чув(Veselyi Tchuv Corporation), 2019.\n Все права защищены.\n", "Справка", MessageBoxButtons.OK);

        }

        /// <summary>
        /// Метод, запускающийся при изменении размеров формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            Ref();
        }
        /// <summary>
        /// Флаг для определения когда отрисовывать листы, а когда файл из загрузки
        /// </summary>
        bool fl = false;
        /// <summary>
        /// Перерисовка объектов
        /// </summary>
        private void Ref()
        {
            Pen p;
            if (fl)
            {
                list = inst.Add1;
                list2 = (Paint.Pryam)inst.Add2;
                list3 = (Paint.Pryam)inst.Add3;
            }
            if (list.Count!=0)
            {
                Element1 t = list.Head;
                for (int i = 0; i < list.Count; i++)
                {
                    Color col = Color.FromArgb(t.Col);
                    p = new Pen(col, t.T);
                    p.StartCap = LineCap.Round;
                    p.EndCap = LineCap.Round;
                    p.LineJoin = LineJoin.Round;
                    g.SmoothingMode = SmoothingMode.AntiAlias;
                    g.DrawLine(p,t.X, t.Y);
                    t = t.Next;
                }
            }
            if (list2.Count != 0)
            {
                Element1 t = list2.Head;
                for (int i = 0; i < list2.Count; i++)
                {
                    Color col = Color.FromArgb(t.Col);
                    p = new Pen(col, t.T);
                    g.DrawRectangle(p, t.X.X,t.X.Y,t.Y.X- t.X.X, t.Y.Y- t.X.Y);
                    t = t.Next;
                }
            }
            if (list3.Count != 0)
            {
                Element1 t = list3.Head;
                for (int i = 0; i < list3.Count; i++)
                {
                    Color col = Color.FromArgb(t.Col);
                    p = new Pen(col, t.T);
                    g.DrawEllipse(p, t.X.X, t.X.Y, t.Y.X - t.X.X, t.Y.Y - t.X.Y);
                    t = t.Next;
                }
            }

        }
        /// <summary>
        /// Флаг для определения, чем сейчас рисуют (-1 - "Карандаш",0 - "Прямоугольник", 1 - "Эллипс")
        /// </summary>
        private int flag = -1;
        /// <summary>
        /// Выбрали карандаш
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pero_Click(object sender, EventArgs e)
        {
            flag = -1;
        }
        /// <summary>
        /// Выбрали прямоугольник
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void kvadrat_Click(object sender, EventArgs e)
        {
            flag = 0;
        }

        /// <summary>
        /// Выбрали эллипс
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void elips_Click(object sender, EventArgs e)
        {
            flag = 1;
        }
        XmlSerializer formatter = new XmlSerializer(typeof(Instrument));
        Instrument inst = new Instrument();
        private void сохранитьКакToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                inst.Add1= list;
                inst.Add2 = list2;
                inst.Add3 = list3;
                string name = saveFileDialog1.FileName;
                if (name.IndexOf('.') != -1)
                {
                    name = name.Remove(name.IndexOf('.'), 4);
                }
                using (FileStream fs = new FileStream(name + ".xml", FileMode.OpenOrCreate))
                {
                    statusStrip1.Show();
                    formatter.Serialize(fs, inst);
                }
            }
        }

        private void открытьФайлToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string name = openFileDialog1.FileName;
                    string xml = name.Remove(0, name.Length - 3);
                    if (xml != "xml")
                        throw new Exception("Неверный формат");
                    using (FileStream fs = new FileStream(name, FileMode.Open))
                    {
                        list.Clear();
                        list2.Clear();
                        list3.Clear();
                        panel.Refresh();
                        inst = (Instrument)formatter.Deserialize(fs);
                        fl = true;
                        Ref();
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
    }
}
