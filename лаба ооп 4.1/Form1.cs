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
        }
        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {

        }
    }
}
