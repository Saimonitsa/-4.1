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

        }
        public class MySrorage {
            CCircle[] circle;
            int a;
            int max_a;
            int l;

            void checkSize()
            {
                if (a >= max_a)
                    increase();
            }
            void increase()
            {
                CCircle[] temp = new CCircle[max_a + 10];
                for (int i = 0; i < max_a; i++)
                {
                    temp[i] = this.circle[i];
                }
                max_a += 10;
                this.circle = new CCircle[max_a];
                for (int i = 0; i < max_a; i++)
                {
                    this.circle[i] = temp[i];
                }
            }
            public MyStorage()
            {
                max_a = 0;
                a = 0;
                l = -1;
                circle = new CCircle[max_a];

            }
            public MyStorage(int max_a)
            {
                l = -1;
                a = 0;
                this.max_a = max_a;
                circle = new CCircle[max_a];
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


        }


        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {

        }
    }
}
