using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BubbleSort
{
    public partial class Form1 : Form
    {
        Thread backgroundWorker;
        bool isRunning = false;
        int currentSeries = 0;
        public Form1()
        {
            InitializeComponent();
            chart1.Series.Clear();
            Restart();
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
            this.Invoke(new MethodInvoker(() =>
            {
                chart1.Series.Add("Execution: " + currentSeries);
                chart1.Series[currentSeries].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
                chart1.Series[currentSeries].Points.Clear();
            }));
            Stopwatch sw = new Stopwatch();
            for(int i = 10; i <= 2000; i += 5)
            {
                int[] data = GenerateData(i);
                sw.Restart();
                data = BubbleSort(data);
                sw.Stop();
                this.Invoke(new MethodInvoker(() =>
                {
                    chart1.Series[currentSeries].Points.AddXY(i, sw.Elapsed.TotalMilliseconds);
                }));
            }
            currentSeries++;
            isRunning = false;
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

        private void button1_Click(object sender, EventArgs e)
        {
            Restart();
        }

        private void Restart()
        {
            if(!isRunning)
            {
                isRunning = true;
                backgroundWorker = new Thread(GenerateGraph);
                backgroundWorker.IsBackground = true;
                backgroundWorker.Start();
            }
        }
    }
}
