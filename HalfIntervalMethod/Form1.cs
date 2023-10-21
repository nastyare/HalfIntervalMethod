using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Windows.Forms.DataVisualization.Charting;

namespace HalfIntervalMethod
{
    public partial class Form1 : Form
    {
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
          
            try
            {
                double a, b, exp;

                if (!double.TryParse(textBoxA.Text, out a))
                {
                    MessageBox.Show("a должна быть натуральным числом");

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

                if (!double.TryParse(textBoxE.Text, out exp) || !Regex.IsMatch(textBoxE.Text, @"(0|10+)|(0,(1|0+1))")
                  || textBoxE.Text[0] == '-')
                {
                    MessageBox.Show("в десятичной форме e пишется через запятую");

                    return;
                }
                this.chart.Series[0].Points.Clear();
                double x = a;
                double y;
                while (x <= b)
                {
                    y = Fun(x);
                    this.chart.Series[0].Points.AddXY(x, y);
                    x += 0.1;
                }
                exp = (int)-Math.Log10(exp);
                double res;
                if (Fun(a) * Fun(b) <= 0)
                {
                    res = (a + b) / 2;

                    while (Math.Abs(b - a) > Math.Pow(10, -exp))
                    {
                        double y1 = Fun(a), y2 = Fun(b), y3 = Fun(res);
                        if (y1 * y3 < 0)
                        {
                            b = res;
                        }
                        else if (y2 * y3 < 0)
                        {
                            a = res;
                        }
                        else
                        {
                            break;
                        }
                        res = (a + b) / 2;

                    }
                    if ((27 - 18 * res + 2 * Math.Pow(res, 2)) * Math.Exp(-res / 3) < 0 + res && (27 - 18 * res + 2 * Math.Pow(res, 2)) * Math.Exp(-res / 3) > 0 - res)
                    {
                        textBox1.Text = $"{res:F4}" ;
                    }
                }
                else
                {
                   MessageBox.Show("Нет корней на этом интервале или их больше одного");
                }
            }

            catch (Exception)
            {
                MessageBox.Show("Что-то пошло не так");

                return;
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

                    if (Fun(a) * Fun(x) <= 0)
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