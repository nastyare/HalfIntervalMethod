using System;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace HalfIntervalMethod
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

            private void Button_Click(object sender, EventArgs e)
            {
                try
                {
                    double a = Convert.ToDouble(textBox1.Text);
                    double b = Convert.ToDouble(textBox2.Text);
                    double epsilon = Convert.ToDouble(textBox3.Text);
                    string expression = textBox4.Text;

                    
                    double minimum = CalculateMinimum(a, b, epsilon, expression);

                    
                    label1.Text = $"Локальный минимум: {minimum:F6}";

                    PlotFunction(expression, a, b);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        private double CalculateMinimum(double a, double b, double epsilon, string expression)
        {

            double middle;
            double fm, fl;

            double fa = CalculateFunctionValue(expression, a);
            double fb = CalculateFunctionValue(expression, b);

            while ((b - a) > epsilon)
            {
                middle = (a + b) / 2;
                fm = CalculateFunctionValue(expression, middle);
                fl = CalculateFunctionValue(expression, middle - epsilon);
                if (fl < fm)
                {
                    b = middle;
                }
                else
                {
                    a = middle - epsilon;
                }
                /*Random random = new Random();
                double minimum = a + random.NextDouble() * (b - a);
                return minimum;*/
            }
            return (a + b) / 2;
        }

            private void PlotFunction(string expression, double a, double b)
            {
                chart.Series.Clear();
                Series series = new Series("Function");
                series.ChartType = SeriesChartType.Line;
                series.BorderWidth = 2;

                Random random = new Random();
                for (double x = a; x <= b; x += 0.1)
                {
                    double y = CalculateFunctionValue(expression, x);
                    series.Points.AddXY(x, y);
                }

                chart.Series.Add(series);
            }

            private double CalculateFunctionValue(string expression, double x)
            {
                Random random = new Random();
                return random.NextDouble() * 100; 
            }

            private void ClearButton_Click(object sender, EventArgs e)
            {
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                textBox4.Clear();
                label1.Text = "";
                chart.Series.Clear();
            }       
    }
}