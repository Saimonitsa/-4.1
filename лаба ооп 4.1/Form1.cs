using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace лаба_ооп_4._1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public class CCircle
        {
            private int X, Y;
            private int R = 100;
            public CCircle()
            {
                X = 0;
                Y = 0;
            }
            public CCircle(int x, int y)
            {
                this.X = x;
                this.Y = y;
            }
            public int getRadius()
            {
                return R;
            }
            public void setXY(int x, int y)
            {
                this.X = x;
                this.Y = y;
            }
            public void SimpleColor(Graphics g)
            {
                Pen pen = new Pen(Color.Gray, 5);
                Rectangle rec = new Rectangle(X - 50, Y - 50, R, R);
                g.DrawEllipse(pen, rec);
            }
            public void SelectedColor(Graphics g)
            {
                Pen pen = new Pen(Color.Red, 5);
                Rectangle rec = new Rectangle(X - 50, Y - 50, R, R);
                g.DrawEllipse(pen, rec);
            }
            public void NewColor(Graphics g)
            {
                Pen pen = new Pen(Color.GreenYellow, 5);
                Rectangle rec = new Rectangle(X - 50, Y - 50, R, R);
                g.DrawEllipse(pen, rec);
            }

            public bool check(int x, int y)
            {
                if (x < this.X + 30 && x > this.X - 20 && y < this.Y + 20 && y > this.Y - 20)
                    return true;
                else
                    return false;

            }

        };
        public class MySrorage 
        {
            CCircle[] circle;
            int a;
            int max_a;
            public int l;
            int[] sel;
            int selsize;

            void checkSize()
            {
                if (a >= max_a)
                    increase();
            }
            void increase()
            {
                CCircle[] temp = new CCircle[max_a + 10];
                int[] tmp = new int[max_a + 10];
                for (int i = 0; i < max_a; i++)
                {
                    tmp[i] = sel[i];
                    temp[i] = this.circle[i];
                }
                max_a += 10;
                sel = new int[max_a];
                this.circle = new CCircle[max_a];
                for (int i = 0; i < max_a; i++)
                {
                    sel[i] = tmp[i];
                    this.circle[i] = temp[i];
                }
            }
            public int getSelsize()
            {
                return selsize;
            }
            public void decSel(int index)
            {
                sel[index]--;
            }
            public int getSel(int index)
            {
                return sel[index];
            }

            public MyStorage()
            {
                max_a = 0;
                a = 0;
                selsize = 0;
                l = -1;
                sel = new int[max_a];
                circle = new CCircle[max_a];

            }
            public MyStorage(int max_a)
            {
                a = 0;
                selsize = 0;
                l = -1;
                this.max_a = max_a;
                sel = new int[max_a];
                circle = new CCircle[max_a];
            }
            void SetObject(int index, CCircle objects)
            {
                if (index < max_a)
                    circle[index] = objects;
            }
            public CCircle GetObject(int index)
            {
                return circle[index];
            }

            public int getCount()
            {
                return a;
            }
            void move(int l)
            {
                a++;
                checkSize();
                for (int i = a - 1; i > l; i--)
                {
                    circle[i] = circle[i - 1];
                }
            }
            public void add(int i, CCircle objects)
            {
                move(i);
                SetObject(i, objects);
            }

            void remove(int index)
            {
                if (a > 0)
                {
                    l++;
                    for (int i = index; i < a; i++)
                    {
                        circle[i] = circle[i + 1];
                        circle[i + 1] = null;
                    }
                    a++;
                }
            }
            public void paint(MyStorage storage, Graphics g)
            {
                storage.removeSelected();
                for (int i = 0; i < storage.getCount() - 1; i++)
                {
                    storage.GetObject(i).SimplyColor(g);
                }
                storage.getObject(storage.getCount() - 1).paintAsSelected(g);
            }

        };
        MyStorage storage = new MyStorage(0);
        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            Graphics g = CreateGraphics();
            if (e.Button == MouseButtons.Right)
            {
                storage.removeSelected();
                Refresh();
                CCircle c = new CCircle(e.X, e.Y);
                storage.choice++;
                storage.add(storage.choice, c);
                storage.paint(storage, g);
                storage.pushToSelected(storage.choice, g);
            }
            if (e.Button == MouseButtons.Left)
            {
                if (Control.ModifierKeys == Keys.Control)
                {
                    storage.pushToSelected(e.X, e.Y, g);
                }
                else
                {
                    storage.removeSelected();
                    for (int i = 0; i < storage.getCount(); i++)
                    {
                        storage.getObject(i).paintAsUsual(g);
                    }
                    storage.pushToSelected(e.X, e.Y, g);
                }
            }


        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                for (int i = 0; i < storage.getSizeOfSelected(); i++)
                {
                    storage.remove(storage.getSelected(i));
                    for (int j = i + 1; j < storage.getSizeOfSelected(); j++)
                    {
                        if (storage.getSelected(j) > storage.getSelected(i))
                        {
                            storage.decSelected(j);
                        }
                    }
                }
                storage.removeSelected();
                this.Refresh();
                Graphics g = CreateGraphics();
                for (int i = 0; i < storage.getCount(); i++)
                {
                    storage.getObject(i).paintAsUsual(g);
                }
            }

        }
    }
}
