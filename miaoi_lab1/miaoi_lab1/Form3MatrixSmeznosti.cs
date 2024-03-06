using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace miaoi_lab1
{
    public partial class Form3MatrixSmeznosti : Form
    {
        public Form3MatrixSmeznosti()
        {
            InitializeComponent();
        }

        public int sum = 0;
        public double[,] normalizedMas;
        //public List<int> rgb = new List<int>();
        //смена картинки
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                Bitmap b = new Bitmap(openFileDialog.FileName);
                pictureBox1.Width = b.Width;
                pictureBox1.Height = b.Height;
                pictureBox1.Image = b;

            }
        }


        public void changePicture(PictureBox pictureBox)
        {

            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                Bitmap b = new Bitmap(openFileDialog.FileName);
                pictureBox.Width = b.Width;
                pictureBox.Height = b.Height;
                pictureBox.Image = b;


            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            changePicture(pictureBox2);

        }
        private void button6_Click(object sender, EventArgs e)
        {
            changePicture(pictureBox4);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            changePicture(pictureBox3);
        }


        public List<int> fillListrgb(int [,] mas)
        {
            List<int> rgb = new List<int>();
            for (int i = 0; i < mas.GetLength(0); i++)
            {
                for(int j = 0; j < mas.GetLength(1); j++)
                {
                    rgb.Add(mas[i, j]);
                    //Console.WriteLine(mas[i, j]);
                }
            }

            List<int> rgbUniq=rgb.Distinct().ToList();
            rgbUniq.Sort();
            return rgbUniq ;
        }


        public int[,] makeMatrixSmeznosti(List<int> rgb, int [,] massive)
        {
            int[,] mas = new int[rgb.Count, rgb.Count];
            for (int i = 0; i < rgb.Count; i++)
            {
                for (int j = 0; j < rgb.Count; j++)
                {
                    mas[i, j] = findNumberSmeznosti(rgb[i], rgb[j], massive);
                    //Console.WriteLine(mas[i, j]);
                    sum += mas[i, j];
                }
            }

            return mas;

        }

        //ищет в массиве количество пар чисел по углу и расстоянию
        public int findNumberSmeznosti(int n,int m, int[,] mas)
        {
            int number = 0;
            for (int i = 0; i < mas.GetLength(0)-2; i++)
            {
                for (int j = 2; j < mas.GetLength(0); j++)
                {
                    //if (mas[i, j] == n && mas[i-1,j+1]==m)
                       if (mas[i, j] == n && mas[i + 2, j - 2] == m)
                        {
                        //Console.WriteLine(mas[i, j]);
                        number++;
                    }
                }
            }
            return number;
        }

        public double [,] makeNormalizeMas(int[,] mas)
        {
            double[,] masNew = new double[mas.GetLength(0),mas.GetLength(1)];
            for (int i = 0; i < mas.GetLength(0); i++)
            {
                for (int j = 0; j < mas.GetLength(1); j++)
                {
                     masNew[i,j] =Math.Round(((double)mas[i, j] / sum),3);
                }
            }
            return masNew;
        }



        private void button7_Click(object sender, EventArgs e)
        {

            sum = 0;
            int[,] mas2 = Form1.fromPicToMas(pictureBox2);
            List<int> rgb2 = fillListrgb(mas2);
            
            int[,] matrixSmeznosti2 = makeMatrixSmeznosti(rgb2, mas2);
            Form1.writeMatrixToGrid(dataGridView4, matrixSmeznosti2, rgb2);

            normalizedMas = makeNormalizeMas(matrixSmeznosti2);
            Form1.writeMatrixToGrid(dataGridView3, normalizedMas, rgb2);
            write_characteristics(label8, label7, label6, label5);
            label34.Text = findEvklidovo(Double.Parse(label8.Text), Double.Parse(label1.Text),
                Double.Parse(label7.Text), Double.Parse(label2.Text), Double.Parse(label5.Text), Double.Parse(label4.Text)).ToString();


        }

        public double findEvklidovo(double e1, double e2, double en1, double en2, double g1, double g2)
        {
            return Math.Sqrt(Math.Pow(e1 - e2, 2) + Math.Pow(en1 - en2, 2) + Math.Pow(g1 - g2, 2));
        }

        private void button8_Click(object sender, EventArgs e)
        {

            sum = 0;
            int[,] mas3 = Form1.fromPicToMas(pictureBox3);
            List<int> rgb3 = fillListrgb(mas3);
            
            int[,] matrixSmeznosti3 = makeMatrixSmeznosti(rgb3, mas3);
            Form1.writeMatrixToGrid(dataGridView6, matrixSmeznosti3, rgb3);

            normalizedMas = makeNormalizeMas(matrixSmeznosti3);
            Form1.writeMatrixToGrid(dataGridView5, normalizedMas, rgb3);
            write_characteristics(label12, label11, label10, label9);
            label35.Text = findEvklidovo(Double.Parse(label12.Text), Double.Parse(label1.Text),
                Double.Parse(label11.Text), Double.Parse(label2.Text), Double.Parse(label9.Text), Double.Parse(label4.Text)).ToString();

        }
        private void button2_Click(object sender, EventArgs e)
        {
            sum = 0;
            int[,] mas = Form1.fromPicToMas(pictureBox1);
            List<int> rgb =fillListrgb( mas);
            
            int[,] matrixSmeznosti= makeMatrixSmeznosti(rgb, mas);
            Form1.writeMatrixToGrid(dataGridView1, matrixSmeznosti, rgb);

            normalizedMas = makeNormalizeMas(matrixSmeznosti);
            Form1.writeMatrixToGrid(dataGridView2, normalizedMas, rgb);
            write_characteristics(label1,label2,label3,label4);

            
        }

        private void button9_Click(object sender, EventArgs e)
        {
            sum = 0;
            int[,] mas4 = Form1.fromPicToMas(pictureBox4);
            List<int> rgb4 = fillListrgb(mas4);
            
            int[,] matrixSmeznosti4 = makeMatrixSmeznosti(rgb4, mas4);
            Form1.writeMatrixToGrid(dataGridView8, matrixSmeznosti4, rgb4);

            normalizedMas = makeNormalizeMas(matrixSmeznosti4);
            Form1.writeMatrixToGrid(dataGridView7, normalizedMas, rgb4);
            write_characteristics(label16, label15, label14, label13);
            label36.Text = findEvklidovo(Double.Parse(label16.Text), Double.Parse(label1.Text),
                Double.Parse(label15.Text), Double.Parse(label2.Text), Double.Parse(label13.Text), Double.Parse(label4.Text)).ToString();
        }

        public void write_characteristics(Label label1, Label label2, Label label3, Label label4)
        {
            double energy = 0;
            double entropiya = 0;
            double kontrast = 0;
            double gomogennost = 0;
            for(int i = 0; i < normalizedMas.GetLength(0); i++)
            {
                for (int j = 0; j < normalizedMas.GetLength(1); j++)
                {
                    energy += normalizedMas[i, j] * normalizedMas[i, j];
                    if(normalizedMas[i,j]!=0)
                    entropiya -= normalizedMas[i, j] * Math.Log(normalizedMas[i, j], 2);

                    kontrast += (i - j) * (i - j) * normalizedMas[i, j];
                    gomogennost += normalizedMas[i, j] / (1 + Math.Abs(i - j));
                }
            }
            label1.Text = "  " + Math.Round(energy,4);
            label2.Text = "  " + Math.Round(entropiya,4);
            label3.Text = "  " + Math.Round(kontrast,4);
            label4.Text = "  " + Math.Round(gomogennost,4);

        }

        private void label37_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dataGridView8_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView4_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView6_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
