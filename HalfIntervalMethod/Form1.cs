using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Windows.Forms.DataVisualization.Charting;

namespace HalfIntervalMethod
{
    public partial class Form1 : Form
    {
        private double a, b, exp, y, x;
        private const double step = 0.01;

        public Form1()
        {
            InitializeComponent();
        }
        public static double Fun(double x)
        {
            return (27 - 18 * x + 2 * Math.Pow(x, 2)) * Math.Exp(-(x / 3));
        }

        private void calculateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!double.TryParse(textBoxA.Text, out a))
            {
                MessageBox.Show("a должна быть натуральным числомю");

                return;
            }

            if (!double.TryParse(textBoxB.Text, out b))
            {
                MessageBox.Show("b должна быть натуральным числом");

                return;
            }

            if (a > b)
            {
                MessageBox.Show("а должна быть меньше b");

                return;
            }

            if (!double.TryParse(textBoxE.Text, out this.exp) || !Regex.IsMatch(textBoxE.Text, @"(1|10+)|(0,(1|0+1))")
              || textBoxE.Text[0] == '-')
            {
                MessageBox.Show("e должна быть записана в десятичной форме через запятую");

                return;
            }
            x = a;



            try
            {
                Function.Dychotomy(a, b, this.exp);
            }
            catch (Exception)
            {
                MessageBox.Show("Что-то пошло не так");

                return;
            }

            double pointX;

            try
            {
                pointX = Math.Round((double)Function.Dychotomy(a, b, this.exp), Math.Abs((int)Math.Log10(this.exp)));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Что-то пошло не так");

                return;
            }
            textBox1.Text = $"{pointX}";
            while (x <= b)
            {
                y = Fun(x);
                this.chart.Series[0].Points.AddXY(x, y);
                x += 0.1;
            }
            {
                y = Fun(x);
                this.chart.Series[0].Points.AddXY(x, y);
                x += 0.1;
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBoxA.Clear();
            textBoxB.Clear();
            textBoxE.Clear();
            chart.Series[0].Points.Clear();
            textBox1.Text = string.Empty;
        }

        public class Function
        {
            public static double Dychotomy(double a, double b, double e, double x = default)
            {

                while ((b - a) > 2 * e)
                {
                    x = (b - a) / 2 + a;

                    if (Fun(a) * Fun(x) < 0)
                    {
                        b = x;
                    }
                    else
                    {
                        a = x;
                    }
                }

                return x;
            }
        }
    }
}