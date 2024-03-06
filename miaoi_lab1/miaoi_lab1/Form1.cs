using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace miaoi_lab1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        public static int[,] fromPicToMas(PictureBox pic)
        {
            Bitmap picture =  new Bitmap( pic.Image);
            int[,] mas = new int[picture.Height,picture.Width];

            for (int i = 0; i < mas.GetLength(0); i++)
            {
                for (int j = 0; j < mas.GetLength(1); j++)
                {
                    mas[i, j] = picture.GetPixel(j,i).R;
                }
            }

            return mas;

        }

        //расширение нулями
        public int [,] extend(int [,] mas)
        {
            int[,] masNew = new int[mas.GetLength(0) + 2, mas.GetLength(1) + 2];
            for (int i = 0; i < masNew.GetLength(0); i++)
            {
                for(int j = 0; j < masNew.GetLength(1); j++)
                {
                    masNew[i, j] = 0;
                }
            } 

            for(int i = 0; i < mas.GetLength(0); i++)
            {
                for(int j = 0; j < mas.GetLength(1); j++)
                {
                    masNew[i + 1, j + 1] = mas[i, j];
                }
            }

            return masNew;
        }

        //передавать расширенный нулями массив, получим матрицу к количесвом более серых значений
        public int [,] makeLokal2Mas(int [,] mas)
        {
            int[,] masNew = new int[mas.GetLength(0)-2,mas.GetLength(1)-2];
            for(int i = 1; i < mas.GetLength(0) - 1; i++)
            {
                for(int j = 1; j < mas.GetLength(1) - 1; j++)
                {
                    int isxod = mas[i, j];
                    int moreGrey = 0;

                    if (mas[i, j+1] > isxod)
                    {
                        moreGrey+=1;
                    }
                    if (mas[i-1, j+1] > isxod)
                    {
                        moreGrey += 1;
                    }
                    if (mas[i-1, j] > isxod)
                    {
                        moreGrey += 1;
                    }
                    if (mas[i-1, j-1] > isxod)
                    {
                        moreGrey += 1;
                    }
                    if (mas[i, j-1] > isxod)
                    {
                        moreGrey += 1;
                    }
                    if (mas[i+1, j-1] > isxod)
                    {
                        moreGrey += 1;
                    }
                    if (mas[i+1, j] > isxod)
                    {
                        moreGrey += 1;
                    }
                    if (mas[i+1, j+1] > isxod)
                    {
                        moreGrey += 1;
                    }

                    masNew[i - 1, j - 1] = moreGrey;
                }
            }

            return masNew;
        }
        //гистограмма двоичного разбиения
        public Dictionary<int,int> makeGistogramLocal2(int [,]mas)
        {
            Dictionary<int, int> dict = new Dictionary<int, int>();
            dict.Add(0, 0);
            dict.Add(1, 0);
            dict.Add(2, 0);
            dict.Add(3, 0);
            dict.Add(4, 0);
            dict.Add(5, 0);
            dict.Add(6, 0);
            dict.Add(7, 0);
            dict.Add(8, 0);

            for(int i = 0; i < mas.GetLength(0); i++)
            {
                for(int j = 0; j < mas.GetLength(1); j++)
                {
                    dict[mas[i, j]] += 1;
                }
            }

            return dict;
        }
        //сравнение гистограм
        public int findL(Dictionary<int,int> dict1, Dictionary<int,int> dict2)
        {
            int l = Math.Abs(dict1[0] - dict2[0]) + Math.Abs(dict1[1] - dict2[1]) + Math.Abs(dict1[2] - dict2[2]) +
                Math.Abs(dict1[3] - dict2[3]) + Math.Abs(dict1[4] - dict2[4]) + Math.Abs(dict1[5] - dict2[5]) +
                Math.Abs(dict1[6] - dict2[6]) + Math.Abs(dict1[7] - dict2[7]) + Math.Abs(dict1[8] - dict2[8]);
            return l;
        }

        public int findMin(int[] mas)
        {
            int min = mas[0];
            int answer = 2;
            for (int i = 0; i < mas.Length; i++)
            {
                if (mas[i] < min)
                {
                    min = mas[i];
                    answer = i + 2;
                }
            }
            return answer;

        }


        public static  void changePicture(PictureBox pictureBox)
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

        //вывод значений матрицы RGB
        public static void writeMatrixToGrid(DataGridView dataGrid, int[,] mas, List<int> l=null, bool f = false)
        {
            dataGrid.Rows.Clear();
            dataGrid.Columns.Clear();
            if (f)
            {  dataGrid.RowCount = mas.GetLength(0) - 1;
            }
            else { dataGrid.RowCount = mas.GetLength(0); }
            dataGrid.ColumnCount = mas.GetLength(1);

            for (int i = 0; i < dataGrid.RowCount; i++)
            {
                for (int j = 0; j < mas.GetLength(1); j++)
                {
                    dataGrid.Rows[i].Cells[j].Value = mas[i, j];
                    
                    if (l == null)
                    {
                        dataGrid.Rows[i].HeaderCell.Value = (i + 1).ToString();
                        dataGrid.Columns[j].HeaderCell.Value = (j + 1).ToString();
                    }
                    else if (f==false)
                    {
                        dataGrid.Rows[i].HeaderCell.Value = l[i].ToString();
                        dataGrid.Columns[j].HeaderCell.Value = l[j].ToString();
                    }
                    
                    else if (f)
                    {
                        dataGrid.Rows[i].HeaderCell.Value = (i + 2).ToString();
                        dataGrid.Columns[j].HeaderCell.Value = l[j].ToString();
                        
                    }
                }

            }
        }
        public static void writeMatrixToGrid(DataGridView dataGrid, double[,] mas, List<int> l)
        {
            dataGrid.Rows.Clear();
            dataGrid.Columns.Clear();
            dataGrid.RowCount = mas.GetLength(0);
            dataGrid.ColumnCount = mas.GetLength(1);

            for (int i = 0; i < mas.GetLength(0); i++)
            {
                for (int j = 0; j < mas.GetLength(1); j++)
                {
                    dataGrid.Rows[i].Cells[j].Value = mas[i, j];
                    dataGrid.Rows[i].HeaderCell.Value = l[i].ToString();
                    dataGrid.Columns[j].HeaderCell.Value = l[j].ToString();
                }

            }
        }

        //рисование гистограммы
        public void DrawGistogram(Chart chart,Dictionary<int, int> d)
        {
            chart.Series[0].Points.DataBindXY(d.Keys, d.Values);
        }

        //-----------------------------------=кнопки=------------------
        //смена картинок
        private void button1_Click(object sender, EventArgs e)
        {
            changePicture(pictureBox1);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            changePicture(pictureBox2);
        }
        private void button3_Click(object sender, EventArgs e)
        {
            changePicture(pictureBox3);
        }
        private void button4_Click(object sender, EventArgs e)
        {
            changePicture(pictureBox4);
        }



        //сравнить
        private void button5_Click(object sender, EventArgs e)
        {
            int[,] mas1 = fromPicToMas(pictureBox1);
            int[,] mas2 = fromPicToMas(pictureBox2);
            int[,] mas3 = fromPicToMas(pictureBox3);
            int[,] mas4 = fromPicToMas(pictureBox4);

            //writeMatrixToGrid(dataGridView1, mas1);
            //writeMatrixToGrid(dataGridView2, mas2);
            //writeMatrixToGrid(dataGridView3, mas3);
            //writeMatrixToGrid(dataGridView4, mas4);

            int[,] extendedMas1 = extend(mas1);
            int[,] extendedMas2 = extend(mas2);
            int[,] extendedMas3 = extend(mas3);
            int[,] extendedMas4 = extend(mas4);

            int [,] lokalMas1=makeLokal2Mas(extendedMas1);
            int [,] lokalMas2=makeLokal2Mas(extendedMas2);
            int [,] lokalMas3=makeLokal2Mas(extendedMas3);
            int [,] lokalMas4=makeLokal2Mas(extendedMas4);

            Dictionary<int,int> dict1 = makeGistogramLocal2(lokalMas1);
            Dictionary<int,int> dict2 = makeGistogramLocal2(lokalMas2);
            Dictionary<int,int> dict3 = makeGistogramLocal2(lokalMas3);
            Dictionary<int,int> dict4 = makeGistogramLocal2(lokalMas4);

            int l12 = findL(dict1, dict2);
            //label8.Text = "L12= "+l12.ToString();
            int l13 = findL(dict1, dict3); 
            //label9.Text = "L13 = "+l13.ToString();
            int l14 = findL(dict1, dict4);
            //label10.Text = "L14 = "+l14.ToString();

            int[] mas = new int[3];
            mas[0] = l12;
            mas[1] = l13;
            mas[2] = l14;

            int min = findMin(mas);
            //richTextBox1.Text = "Изображение 1 больше всего похоже на изображение " + min;

            Form2Gistograms f2 = new Form2Gistograms();
            f2.Show();
            DrawGistogram(f2.chart1, dict1);
            DrawGistogram(f2.chart2, dict2);
            DrawGistogram(f2.chart3, dict3);
            DrawGistogram(f2.chart4, dict4);
        }

        // заполнение rgb матриц
        private void button6_Click(object sender, EventArgs e)
        {
            int[,] mas1 = fromPicToMas(pictureBox1);
            writeMatrixToGrid(dataGridView1, mas1);
        }
        private void button9_Click(object sender, EventArgs e)
        {
            int[,] mas2 = fromPicToMas(pictureBox2);
            writeMatrixToGrid(dataGridView2, mas2);
        }
        private void button10_Click(object sender, EventArgs e)
        {
            int[,] mas3 = fromPicToMas(pictureBox3);
            writeMatrixToGrid(dataGridView3, mas3);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            int[,] mas4 = fromPicToMas(pictureBox4);
            writeMatrixToGrid(dataGridView4, mas4);
        }

       
        //вывод матриц более серого
     /*   private void button12_Click(object sender, EventArgs e)
        {
            int[,] mas1 = fromPicToMas(pictureBox1);
            int[,] extendedMas1 = extend(mas1);
            int[,] lokalMas1 = makeLokal2Mas(extendedMas1);
            writeMatrixToGrid(dataGridView8, lokalMas1);
        }

        private void button13_Click(object sender, EventArgs e)
        {

            int[,] mas2 = fromPicToMas(pictureBox2);
            int[,] extendedMas2 = extend(mas2);
            int[,] lokalMas2 = makeLokal2Mas(extendedMas2);
            writeMatrixToGrid(dataGridView7, lokalMas2);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            int[,] mas3 = fromPicToMas(pictureBox3);
            int[,] extendedMas3 = extend(mas3);
            int[,] lokalMas3 = makeLokal2Mas(extendedMas3);
            writeMatrixToGrid(dataGridView6, lokalMas3);
        }

        private void button14_Click(object sender, EventArgs e)
        {

            int[,] mas4 = fromPicToMas(pictureBox4);
            int[,] extendedMas4 = extend(mas4);
            int[,] lokalMas4 = makeLokal2Mas(extendedMas4);
            writeMatrixToGrid(dataGridView5, lokalMas4);

        }*/

        //матрица смежности
        private void button8_Click(object sender, EventArgs e)
        {
            Form3MatrixSmeznosti f3 = new Form3MatrixSmeznosti();
            f3.Show();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            Form4Primitives f4 = new Form4Primitives();
            f4.Show();
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            Form3MatrixSmeznosti f3= new Form3MatrixSmeznosti();
            f3.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
    }
}
