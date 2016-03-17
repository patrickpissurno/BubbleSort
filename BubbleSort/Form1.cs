using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BubbleSort
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            chart1.Series[0].LegendText = "Temp(ms) x Quantidade";
            GenerateGraph();
        }

        private int[] GenerateData(int amount)
        {
            int[] data = new int[amount];
            Random r = new Random();
            for (int i = 0; i < amount; i++)
                data[i] = (r.Next(0, 4000));
            return data;
        }

        private void GenerateGraph()
        {
            chart1.Series[0].Points.Clear();
            for(int i = 10; i < 2000; i += 5)
            {
                int[] data = GenerateData(i);
                var sw = new Stopwatch();
                sw.Start();
                data = BubbleSort(data);
                sw.Stop();
                chart1.Series[0].Points.AddXY(i, sw.Elapsed.TotalMilliseconds);
            }
        }

        private int[] BubbleSort(int[] data)
        {
            for(int i=0; i < data.Length - 1; i++)
            {
                for (int j = i + 1; j < data.Length; j++)
                {
                    if(data[i] > data[j])
                    {
                        int temp = data[i];
                        data[i] = data[j];
                        data[j] = temp;
                    }
                }
            }
            return data;
        }
    }
}
