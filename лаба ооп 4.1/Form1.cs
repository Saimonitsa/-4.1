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
            private int R = 40;
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
                Pen pen = new Pen(Color.PaleVioletRed, 5);
                Rectangle rec = new Rectangle(X - 20, Y - 20, R, R);
                g.DrawEllipse(pen, rec);
            }
            public void SelectedColor(Graphics g)
            {
                Pen pen = new Pen(Color.Red, 5);
                Rectangle rec = new Rectangle(X - 20, Y - 20, R, R);
                g.DrawEllipse(pen, rec);
            }
            public void NewColor(Graphics g)
            {
                Pen pen = new Pen(Color.Orange, 5);
                Rectangle rec = new Rectangle(X - 20, Y - 20, R, R);
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
        public class MyStorage 
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
            public void clickSel(int x, int y, Graphics g)
            {
                bool flag = true;
                for (int i = 0; i < a; i++)
                {
                    if (GetObject(i).check(x, y) == true)
                    {
                        for (int j = 0; j < selsize; j++)
                        {
                            if (i == sel[j])
                            {
                                flag = false;
                            }
                        }
                        if (flag == true)
                        {
                            sel[selsize] = i;
                            selsize++;
                            GetObject(i).SelectedColor(g);
                        }
                    }
                }
            }
            public void iSel(int i, Graphics g)
            {

                sel[selsize] = i;
                selsize++;
                GetObject(i).NewColor(g);
            }
            public void removeSel()
            {
                for (int i = 0; i < a; i++)
                {
                    sel[i] = -1;
                }
                selsize = 0;
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

            public void remove(int index)
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
                storage.removeSel();
                for (int i = 0; i < storage.getCount() - 1; i++)
                {
                    storage.GetObject(i).SimpleColor(g);
                }
                storage.GetObject(storage.getCount() - 1).SelectedColor(g);
            }

        };
        MyStorage storage = new MyStorage(0);
        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            Graphics g = CreateGraphics();
            if (e.Button == MouseButtons.Right)
            {
                storage.removeSel();
                Refresh();
                CCircle c = new CCircle(e.X, e.Y);
                storage.l++;
                storage.add(storage.l, c);
                storage.paint(storage, g); 
                storage.iSel(storage.l, g);
            }
            if (e.Button == MouseButtons.Left)
            {
                if (Control.ModifierKeys == Keys.Control)
                {
                    storage.clickSel(e.X, e.Y, g);
                }
                else
                {
                    storage.removeSel();
                    for (int i = 0; i < storage.getCount(); i++)
                    {
                        storage.GetObject(i).SimpleColor(g);
                    }
                    storage.clickSel(e.X, e.Y, g);
                }
            }


        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                for (int i = 0; i < storage.getSelsize(); i++)
                {
                    storage.remove(storage.getSel(i));
                    for (int j = i + 1; j < storage.getSelsize(); j++)
                    {
                        if (storage.getSel(j) > storage.getSel(i))
                        {
                            storage.decSel(j);
                        }
                    }
                }
                storage.removeSel();
                this.Refresh();
                Graphics g = CreateGraphics();
                for (int i = 0; i < storage.getCount(); i++)
                {
                    storage.GetObject(i).SimpleColor(g);
                }
            }

        }
    }
}
