using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace HalfIntervalMethod
{
    public partial class Form1 : Form
    {
        /*private double a, b, e, x;
        private const double step = 0.01;*/
        public Form1()
        {
            InitializeComponent();
        }       

        private void calculateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            double Function(double x)
            {
                return (27 - 18 * x + 2 * Math.Pow(x, 2)) * Math.Exp(-(x / 3));
            }
            try
            {
                double a, b, exp, x;

                if (!double.TryParse(textBoxA.Text, out a))
                {
                    MessageBox.Show("В а должно быть только натуральное число.");

                    return;
                }

                if (!double.TryParse(textBoxB.Text, out b))
                {
                    MessageBox.Show("В b должно быть только натуральное число.");

                    return;
                }

                if (a > b)
                {
                    MessageBox.Show("a не может быть больше b");

                    return;
                }

                if (!double.TryParse(textBoxE.Text, out exp) || !Regex.IsMatch(textBoxE.Text, @"(1|10+)|(0,(1|0+1))")
                    || textBoxE.Text[0] == '-')
                {
                    MessageBox.Show("неправильный формат. пример: 0.1");

                    return;
                }

                while (Math.Abs(b - a) > exp)
                {
                    /*double x1 = (a + b - exp) / 2;
                    double x2 = (a + b + exp) / 2;

                    if (Function(x1) < Function(x2))
                    {
                        b = x2;
                    }
                    else
                    {
                        a = x1;
                    }*/
                    x = (b - a) / 2 + a;

                    if (Function(a) * Function(x) < 0)
                    {
                        b = x;
                    }
                    else
                    {
                        a = x;
                    }

                }
                double minimum = (a + b) / 2;
                textBox1.Text = $"{minimum:F4}";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Что-то пошло не так");
            }
        }
            /*x = a;

            double pointXMin = Math.Round(Functions.Dychotomy(a, b, this.e), Math.Abs((int)Math.Log10(this.e)));
            double pointYMin = Functions.Dychotomy(pointXMin);

            labelCondition.Text += $"Минимум функции на отрезке [{a}; {b}]:\n\tx = {pointXMin}";*/       

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBoxA.Clear();
            textBoxB.Clear();
            textBoxE.Clear();
            textBox1.Clear();
        }
    }
}
