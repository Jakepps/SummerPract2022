using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZedGraph;

namespace SummerPr2022
{
    public partial class Form1 : Form
    {
        public string eyler = "Метод Эйлера";
        public string exsol = "Точное решение";
        ZedGraphControl zedGrapgControl1 = new ZedGraphControl();
        public Form1()
        {
            InitializeComponent();
        }
        protected override void OnLoad(EventArgs e)
        {
            zedGrapgControl1.Location = new Point(10, 30);
            zedGrapgControl1.Name = "text";
            zedGrapgControl1.Size = new Size(500, 500);
            Controls.Add(zedGrapgControl1);
            GraphPane my_Pane = zedGrapgControl1.GraphPane;
            my_Pane.Title.Text = "График функции";
            my_Pane.XAxis.Title.Text = " X";
            my_Pane.YAxis.Title.Text = " Y";
        }
        private void GetSize()
        {
            zedGrapgControl1.Location = new Point(10, 10);
            zedGrapgControl1.Size = new Size(ClientRectangle.Width - 20, ClientRectangle.Height - 20);
        }
        static double f(double x)
        {
            //return ()/Math.Exp(y)
            return Math.Sqrt(Math.Sin(x) + Math.Exp(-1 * Math.Sin(x))); //решение вручную
        }
        static double f1(double x, double y)
        {
            //return ()
            return ((((Math.Cos(x) * (1 + Math.Sin(x))) / y) - (y * Math.Cos(x))) / 2);
        }
        private void Eiler(ZedGraphControl Zed_GraphControl)
        {
                GraphPane my_Pane = Zed_GraphControl.GraphPane;
                PointPairList list = new PointPairList();
                PointPairList list_1 = new PointPairList();

                double m = -1;

                double y = 1;
                double x = 0;

                double a = 0;

                double n = Convert.ToDouble(textBox1.Text);

                if (textBox1.Text==null)
                {
                    throw new Exception("Вы не ввели колличество итераций!");
                }

                double h = 1 / n;

                for (double i = 0; i <= n; i++)
                {
                    list_1.Add(a + h * i, f(a + h * i));

                    list.Add(x, y);
                    y = y + h * f1(x, y);
                    x += h;

                    if (Math.Abs(y - f(a + h * i)) > m) m = Math.Abs(y - f(a + h * i));
                }

                LineItem myCircle_1 = my_Pane.AddCurve(exsol, list_1, Color.Red, SymbolType.Circle);
                LineItem myCircle = my_Pane.AddCurve(eyler, list, Color.Maroon, SymbolType.Circle);
                zedGrapgControl1.AxisChange();
                zedGrapgControl1.Invalidate();
                label1.BackColor = Color.White;
                label1.Text = Convert.ToString("Максимальная невязка:\n " + m);
           
        }
        private void GriddenOn(GraphPane my_Pane)
        {
            my_Pane.XAxis.MajorGrid.IsVisible = true;
            my_Pane.YAxis.MajorGrid.IsVisible = true;
            my_Pane.YAxis.MinorGrid.IsVisible = true;
            my_Pane.XAxis.MinorGrid.IsVisible = true;
        }
        private void Clear(ZedGraphControl Zed_GraphControl)
        {
            zedGrapgControl1.GraphPane.CurveList.Clear();
            zedGrapgControl1.GraphPane.GraphObjList.Clear();
            zedGrapgControl1.GraphPane.XAxis.Type = AxisType.Linear;
            zedGrapgControl1.GraphPane.XAxis.Scale.TextLabels = null;
            zedGrapgControl1.GraphPane.XAxis.MajorGrid.IsVisible = false;
            zedGrapgControl1.GraphPane.YAxis.MajorGrid.IsVisible = false;
            zedGrapgControl1.GraphPane.YAxis.MinorGrid.IsVisible = false;
            zedGrapgControl1.GraphPane.XAxis.MinorGrid.IsVisible = false;
            zedGrapgControl1.RestoreScale(zedGrapgControl1.GraphPane);
            zedGrapgControl1.AxisChange();
            zedGrapgControl1.Invalidate();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            GriddenOn(zedGrapgControl1.GraphPane);
            Eiler(zedGrapgControl1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Clear(zedGrapgControl1);
            label1.BackColor = Color.Maroon;
        }
    }
}
