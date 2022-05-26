using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace var13
{
    public partial class Form1 : Form
    {
        int N = 4;
        int M = 4;
        List<double> chisla = new List<double>();
        
        public Form1()
        {
            InitializeComponent();
            numericUpDown1.Maximum = N;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = dialog.FileName;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                using (StreamReader file = new StreamReader(textBox1.Text))
                {
                    //double[,] A = new double[N, M];
                    if(textBox1.Text == null)
                    {
                        throw new ArgumentNullException("");
                    }
                    string line;
                    int c = 0;
                    dataGridVivod.Rows.Clear();
                    dataGridVivod.Columns.Clear();
                    while ((line = file.ReadLine()) != null)
                    {
                        string[] dat = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        for (int j = 0; j < dat.Length; j++)
                        {
                            chisla.Add(double.Parse(dat[j]));
                        }
                    }
                    //file.Close();
                    //for(int i = 0; i < N; i++)
                    //{
                    //    for(int j = 0; j < M; j++)
                    //    {
                    //        A[i, j] = chisla[c];
                    //        c++;
                    //    }
                    //}
                    for (int i = 0; i < M; i++)
                    {
                        dataGridVivod.Columns.Add(i.ToString(), i.ToString());
                    }
                    for (int j = 1; j < N; j++)
                    {
                        dataGridVivod.Rows.Add("0");
                    }
                    for (int i = 0; i < N; i++)
                    {
                        for (int j = 0; j < M; j++)
                        {
                            dataGridVivod.Rows[i].Cells[j].Value = chisla[c];
                            c++;
                        }
                    }
                }
                using (StreamWriter fileVvod = new StreamWriter(textBox1.Text,true))
                {
                    fileVvod.WriteLine();
                    if (radioButton1.Checked)
                    {
                        double[] B = new double[M];
                        for (int i = 0; i < M; i++)
                        {
                            B[i] = Convert.ToDouble(dataGridVivod.Rows[Convert.ToInt32(numericUpDown1.Value) - 1].Cells[i].Value);
                            dataGridVivod.Rows[Convert.ToInt32(numericUpDown1.Value) - 1].Cells[i].Style.BackColor = System.Drawing.Color.Red;
                        }
                        for (int i = 0; i < B.Length; i++)
                        {
                            fileVvod.Write(string.Format("{0} ", B[i]));
                        }
                    }
                    else
                    {
                        double[] B = new double[N];
                        for (int i = 0; i < N; i++)
                        {
                            B[i] = Convert.ToDouble(dataGridVivod.Rows[i].Cells[Convert.ToInt32(numericUpDown1.Value) - 1].Value);
                            dataGridVivod.Rows[i].Cells[Convert.ToInt32(numericUpDown1.Value) - 1].Style.BackColor = System.Drawing.Color.Red;
                        }
                        for (int i = 0; i < B.Length; i++)
                        {
                            fileVvod.Write(string.Format(" {0} ", B[i]));
                        }
                    }
                }

            }
            catch(ArgumentNullException)
            {
                MessageBox.Show("Выберите путь к файлу");
            }
        }

        private void radioButton1_Click(object sender, EventArgs e)
        {
            numericUpDown1.Maximum = N;
        }

        private void radioButton2_Click(object sender, EventArgs e)
        {
            numericUpDown1.Maximum = M;
        }
    }
}
