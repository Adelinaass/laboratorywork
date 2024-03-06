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
    public partial class Form4Primitives : Form
    {
        public Form4Primitives()
        {
            InitializeComponent();
            dataGridView9.RowCount = 4;
            dataGridView9.Rows[0].HeaderCell.Value = "1";
            dataGridView9.Rows[1].HeaderCell.Value = "2";
            dataGridView9.Rows[2].HeaderCell.Value = "3";
            dataGridView9.Rows[3].HeaderCell.Value = "4";
        }

        double kp;
        double dp;
        double eu;
        
        public int commonAmountPrimitives = 0;
        private void button1_Click(object sender, EventArgs e)
        {
            Form1.changePicture(pictureBox1);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form1.changePicture(pictureBox2);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Form1.changePicture(pictureBox3);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Form1.changePicture(pictureBox4);
        }

        public void writeRGB_Primitives(PictureBox pictureBox, DataGridView dataGridView1, DataGridView dataGridView2, Label label)
        {
            int[,] rgb = Form1.fromPicToMas(pictureBox);
            Form1.writeMatrixToGrid(dataGridView1, rgb);

            List<int> rgbList = new List<int>();
            foreach (int i in rgb) rgbList.Add(i);
            rgbList.Sort();
            List<int> rgbUniq = rgbList.Distinct().ToList();
            int[,] primitives = new int[50, rgbUniq.Count];

            for (int i = 0; i < primitives.GetLength(0); i++)
            {
                for (int j = 0; j < primitives.GetLength(1); j++)
                {
                    primitives[i, j] = 0;
                }
            }

            Form1.writeMatrixToGrid(dataGridView2, primitives, rgbUniq, true);
            Form1.writeMatrixToGrid(dataGridView1, rgb);
            commonAmountPrimitives = 0;
            for (int i = rgb.GetLength(0) - 1; i >= 1; i--)
            {
                for (int j = rgb.GetLength(1) - 1; j >= 0; j--)
                {
                    List<int> rasstoyanie = findPrimitive(i, j, rgb, dataGridView1);
                    foreach (int ras in rasstoyanie)
                    {
                        changeGrid(dataGridView2, rgbUniq.IndexOf(rgb[i, j]), ras - 1);
                    }
                }
            }
            label.Text = "Общее количество примитивов = " + commonAmountPrimitives;

            
        }

       

        public void changeGrid(DataGridView dataGridView, int primitive,int rasstoyanie)
        {
            
            dataGridView[primitive, rasstoyanie].Value = int.Parse(dataGridView[primitive, rasstoyanie].Value.ToString())+1;
            commonAmountPrimitives += 1;
        }
        public List<int> findPrimitive(int row,int column, int[,] rgb, DataGridView dataGridView)
        {
            int primitive=rgb[row, column];
            List<int> rasstoyanie = new List<int>();
            for(int i = 0;i<row; i++)
            {
                    if(rgb[i, column] == primitive)
                    {
                    bool isprimitive = true;
                    for(int k = i; k < row; k++)
                    {
                        if (rgb[k, column] != primitive)
                        {
                            isprimitive = false;
                        }
                        if(dataGridView[column, k].Style.BackColor == Color.FromArgb(primitive, primitive, 90))
                        {
                            isprimitive = false;
                        }
                        
                    }
                    if (isprimitive)
                    {
                        int s = row - i;
                        rasstoyanie.Add(s);

                        for(int m = row; m >= i; m--)
                        {

                            dataGridView[column, m].Style.BackColor = Color.FromArgb(primitive,primitive, 90);
                            
                        }
                        
                    }

                    }
            }
            return rasstoyanie;
        }


        //короткое примитивное подчеркивание
        public void findKp(DataGridView dataGridView)
        {
            kp = 0;
            dp = 0;
            eu = 0;
            for(int i = 0; i < dataGridView.ColumnCount; i++)
            {
                int sumstolbets = 0;
                for(int j = 0; j < dataGridView.RowCount; j++)
                {
                    kp += int.Parse(dataGridView[i,j].Value.ToString())/ 
                        Math.Pow(int.Parse(dataGridView.Rows[i].HeaderCell.Value.ToString()),2);
                    
                    dp += int.Parse(dataGridView[i, j].Value.ToString()) *
                        Math.Pow(int.Parse(dataGridView.Rows[i].HeaderCell.Value.ToString()),2);

                    sumstolbets += int.Parse(dataGridView[i, j].Value.ToString());
                }
                eu += Math.Pow(sumstolbets, 2);

            }
            kp /= commonAmountPrimitives;
            dp /= commonAmountPrimitives;
            eu /= commonAmountPrimitives;
        }

        //1
        private void button2_Click(object sender, EventArgs e)
        {
            writeRGB_Primitives(pictureBox1, dataGridView1, dataGridView2, label1);
            findKp(dataGridView2);
            dataGridView9[0, 0].Value = kp;
            dataGridView9[1, 0].Value = dp;
            dataGridView9[2, 0].Value = eu;
            findEvkl(0);


        }
        //2
        private void button3_Click(object sender, EventArgs e)
        {
            writeRGB_Primitives(pictureBox2, dataGridView3, dataGridView4, label2);
            findKp(dataGridView4);
            dataGridView9[0, 1].Value = kp;
            dataGridView9[1, 1].Value = dp;
            dataGridView9[2, 1].Value = eu;
            findEvkl(1);
        }

        //3
        private void button4_Click(object sender, EventArgs e)
        {
            writeRGB_Primitives(pictureBox3, dataGridView5, dataGridView6, label3);
            findKp(dataGridView6);
            dataGridView9[0, 2].Value = kp;
            dataGridView9[1, 2].Value = dp;
            dataGridView9[2, 2].Value = eu;
            findEvkl(2);
        }

        //4
        private void button5_Click(object sender, EventArgs e)
        {
            writeRGB_Primitives(pictureBox4, dataGridView7, dataGridView8, label4);
            findKp(dataGridView8);
            dataGridView9[0, 3].Value = kp;
            dataGridView9[1, 3].Value = dp;
            dataGridView9[2, 3].Value = eu;
            findEvkl(3);
        }

        public void findEvkl(int i)
        {
            double kp1 = Double.Parse(dataGridView9[0, 0].Value.ToString());
            double dp1 = Double.Parse(dataGridView9[1, 0].Value.ToString());
            double eu1 = Double.Parse(dataGridView9[2, 0].Value.ToString());

            double kp2 = Double.Parse(dataGridView9[0, i].Value.ToString());
            double dp2 = Double.Parse(dataGridView9[1, i].Value.ToString());
            double eu2 = Double.Parse(dataGridView9[2, i].Value.ToString());


            double evkl = Math.Sqrt(Math.Pow(kp1 - kp2, 2) + Math.Pow(dp1 - dp2, 2) + Math.Pow(eu1 - eu2, 2));
            dataGridView9[3, i].Value = evkl;

        }
    }
}
