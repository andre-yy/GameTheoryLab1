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

namespace GameTheoryLab1
{
    public partial class Form1 : Form
    {
        int X = 2, Y = 2;
        double[,] A;
        Form1 my;       
        private const string _TABLE_PANEL_NAME = "tableLayoutPanel";
        public Form1()
        {
            InitializeComponent();
            A = new double[X, Y];
            my = this;
            ShowMatrix(X, Y);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private bool softDom(double[,] A, int i1, int i2)
        {
            
            bool fl = true;
            for (int j = 0; j < Y; j++)
            {
                if (A[i1, j] > A[i2, j]) fl = false;
            }
            return fl;
        }

        private bool softDom2(double[,] A, int j1, int j2)
        {

            bool fl = true;
            for (int i = 0; i < X; i++)
            {
                if (A[i, j1] > A[i, j2]) fl = false;
            }
            return fl;
        }

        private bool hardDom(double[,] A, int i1, int i2)
        {
            bool fl = true;
            for (int j = 0; j < Y; j++)
            {
                if (A[i1, j] >= A[i2, j]) fl = false;
            }
            return fl;
        }
        private bool hardDom2(double[,] A, int j1, int j2)
        {
            bool fl = true;
            for (int i = 0; i < X; i++)
            {
                if (A[i, j1] >= A[i, j2]) fl = false;
            }
            return fl;
        }

        private bool getMatrix(double[,] A)
        {
            for (int i = 0; i < X; i++)
            {
                for (int j = 0; j < Y; j++)
                {
                    try
                    {
                        A[i, j] = Convert.ToDouble(((my.Controls[_TABLE_PANEL_NAME] as TableLayoutPanel).Controls[$"textBox{i},{j}"] as TextBox).Text);
                        
                    } catch (FormatException e)
                    {
                        MessageBox.Show("Не были введены значения в поля матрицы", "Пустые элементы матрицы");
                        return false;
                    }
                }
            }
            return true;
        }

        private bool getMatrix2(double[,] A)
        {
            for (int i = 0; i < X; i++)
            {
                for (int j = 0; j < Y; j++)
                {
                    try
                    {
                        A[i, j] = -Convert.ToDouble(((my.Controls[_TABLE_PANEL_NAME] as TableLayoutPanel).Controls[$"textBox{i},{j}"] as TextBox).Text);

                    }
                    catch (FormatException e)
                    {
                        MessageBox.Show("Не были введены значения в поля матрицы", "Пустые элементы матрицы");
                        return false;
                    }
                }
            }
            return true;
        }


        private void ShowMatrix(int columnCount, int rowCount)
        {
            //создание TableLayoutPanel
            CreateTablePanel(columnCount, rowCount);

            //находим по имени только что созданную TableLayoutPanel
            var tablePanel = this.Controls
                                    .Find(_TABLE_PANEL_NAME, true)
                                    .First() as TableLayoutPanel;

            //вставляем текстбоксы
            for (int i = 0; i < columnCount; i++)
            {
                for (int j = 0; j < rowCount; j++)
                {
                    var txt = $"textBox{i},{j}";
                    //новый текстбокс с текстовкой, именем, шириной
                    var tb = new TextBox { Text = "", Name = txt, Width = 30 };
                    //добавляем
                    tablePanel.Controls.Add(tb);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            X = Convert.ToInt32(textBox1.Text);
            Y = Convert.ToInt32(textBox2.Text);
            
            A = new double[X, Y];
            ShowMatrix(X, Y);
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            //FileStream file = new FileStream(Convert.ToString(textBox3), FileMode.Open);
            try
            {
                var fs = new StreamReader(textBox3.Text);
                
                var line = fs.ReadLine();
                string[] str = line.Split(' ');
                textBox1.Text = str[0];
                textBox2.Text = str[1];
                
                button1_Click(sender, e);
                for (int i = 0; i < X; i++)
                {
                    line = fs.ReadLine();
                    str = line.Split(' ');
                    for (int j = 0; j < Y; j++)
                    {
                        ((my.Controls[_TABLE_PANEL_NAME] as TableLayoutPanel).Controls[$"textBox{i},{j}"] as TextBox).Text = str[j];
                    }
                    
                }
                fs.Close();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show("Есть пустые поля, которые необходимо заполнить", "Ошибка");
            }
            catch(FileNotFoundException ex)
            {
                MessageBox.Show("Файл не найден", "Ошибка");
            }

        }

        private void maxmin(double[,] A, out double maxv, out int maxi, out int maxj)
        {
            double[] minimums = new double[X];
            double minim;
            int[] minInd = new int[X];
            for (int i = 0; i < X; i++)
            {
                minim = A[i, 0];
                for (int j = 0; j < Y; j++)
                {
                    if (A[i, j] <= minim)
                    {
                        minim = A[i, j];
                        minInd[i] = j;
                    }
                }
                minimums[i] = minim;

            }
            maxv = minimums.Max();
            maxj = minInd[Array.IndexOf(minimums, maxv)];
            maxi = Array.IndexOf(minimums, maxv);            
        }

        private void minmax(double[,] A, out double minv, out int mini, out int minj)
        {
            double[] maximus = new double[Y];
            double maxim;
            int[] maxInd = new int[Y];
            for (int j = 0; j < Y; j++)
            {
                maxim = A[0, j];
                for (int i = 0; i < X; i++)
                {
                    if (A[i, j] >= maxim)
                    {
                        maxim = A[i, j];
                        maxInd[j] = i;
                    }
                }
                maximus[j] = maxim;

            }
            minv = maximus.Min();
            minj = maxInd[Array.IndexOf(maximus, minv)];
            mini = Array.IndexOf(maximus, minv);

        }

        private void button4_Click(object sender, EventArgs e)
        {
            bool fl = getMatrix(A);
            if (!fl) return;
            double maxv;
            int maxi;
            int maxj;
            maxmin(A, out maxv, out maxi, out maxj);
            ((my.Controls[_TABLE_PANEL_NAME] as TableLayoutPanel).Controls[$"textBox{maxi},{maxj}"] as TextBox).BackColor = Color.BlueViolet;
            textBox5.Text = Convert.ToString(maxv);
            textBox4.Text = Convert.ToString(++maxi);
            textBox4.AppendText("; " + ++maxj);

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            Random r = new Random();
            for (int i = 0; i < X; i++)
            {
                for (int j = 0; j < Y; j++)
                {
                    ((my.Controls[_TABLE_PANEL_NAME] as TableLayoutPanel).Controls[$"textBox{i},{j}"] as TextBox).Text = Convert.ToString(r.Next(-10, 10));
                    ((my.Controls[_TABLE_PANEL_NAME] as TableLayoutPanel).Controls[$"textBox{i},{j}"] as TextBox).BackColor = SystemColors.Window;
                }
            }
        }

        

        private void button6_Click(object sender, EventArgs e)
        {
            textBox8.Text = "";
            int[] delIndex = new int[X];
            for (int i = 0; i < X; i++) delIndex[i] = i;
            bool fl = getMatrix(A);
            if (!fl) return;
            int ii = 0, ij = 0;
            bool isDeleted = false;
            var matr = ((my.Controls[_TABLE_PANEL_NAME] as TableLayoutPanel).Controls[$"textBox{ii},{ij}"] as TextBox);
            while ((ii < X))
            {
                isDeleted = false;
                ij = 0;
                while ((ij < X) && (!isDeleted))
                {
                    if ((ij != ii) && (delIndex[ij] != -1) && (delIndex[ii] != -1)) isDeleted = softDom(A, ii, ij);                    
                    ij++;
                }
                if (isDeleted)
                {
                    delIndex[ii] = -1;
                    for (int j = 0; j < Y; j++)
                    {
                        ((my.Controls[_TABLE_PANEL_NAME] as TableLayoutPanel).Controls[$"textBox{ii},{j}"] as TextBox).Text = "";
                        
                    }

                }
                ii++;
            }
            for (int i = 0; i < X; i++)
            {
                if (delIndex[i] != -1) textBox8.AppendText(Convert.ToString(++delIndex[i]) + "; ");
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            bool fl = getMatrix(A);
            if (!fl) return;
            double minv;
            int mini;
            int minj;
            minmax(A, out minv, out mini, out minj);
            ((my.Controls[_TABLE_PANEL_NAME] as TableLayoutPanel).Controls[$"textBox{minj},{mini}"] as TextBox).BackColor = Color.Green;
            textBox7.Text = Convert.ToString(minv);
            textBox6.Text = Convert.ToString(++minj);
            textBox6.AppendText("; " + ++mini);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            
            for (int i = 0; i < X; i++)
            {
                for (int j = 0; j < Y; j++)
                {
                    ((my.Controls[_TABLE_PANEL_NAME] as TableLayoutPanel).Controls[$"textBox{i},{j}"] as TextBox).Text = "";
                    ((my.Controls[_TABLE_PANEL_NAME] as TableLayoutPanel).Controls[$"textBox{i},{j}"] as TextBox).BackColor = SystemColors.Window;
                }
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            textBox8.Text = "";
            int[] delIndex = new int[X];
            for (int i = 0; i < X; i++) delIndex[i] = i;
            bool fl = getMatrix(A);
            if (!fl) return;
            int ii = 0, ij = 0;
            bool isDeleted = false;
            var matr = ((my.Controls[_TABLE_PANEL_NAME] as TableLayoutPanel).Controls[$"textBox{ii},{ij}"] as TextBox);
            while ((ii < X))
            {
                isDeleted = false;
                ij = 0;
                while ((ij < X) && (!isDeleted))
                {
                    if ((ij != ii) && (delIndex[ij] != -1) && (delIndex[ii] != -1)) isDeleted = hardDom(A, ii, ij);
                    ij++;
                }
                if (isDeleted)
                {
                    delIndex[ii] = -1;
                    for (int j = 0; j < Y; j++)
                    {
                        ((my.Controls[_TABLE_PANEL_NAME] as TableLayoutPanel).Controls[$"textBox{ii},{j}"] as TextBox).Text = "";

                    }

                }
                ii++;
            }
            for (int i = 0; i < X; i++)
            {
                if (delIndex[i]!=-1) textBox8.AppendText(Convert.ToString(++delIndex[i]) + "; ");
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            getMatrix(A);
            double[,] B = new double[X, Y];
            textBox8.Text = "";
            int[] delIndex = new int[Y];
            for (int i = 0; i < Y; i++) delIndex[i] = i;
            bool fl = getMatrix2(B);
            if (!fl) return;
            int ji = 0, jj = 0;
            bool isDeleted = false;
            var matr = ((my.Controls[_TABLE_PANEL_NAME] as TableLayoutPanel).Controls[$"textBox{ji},{jj}"] as TextBox);
            while ((jj < Y))
            {
                isDeleted = false;
                ji = 0;
                while ((ji < Y) && (!isDeleted))
                {
                    if ((ji != jj) && (delIndex[ji] != -1) && (delIndex[jj] != -1)) isDeleted = softDom2(B, jj, ji);
                    ji++;
                }
                if (isDeleted)
                {
                    delIndex[jj] = -1;
                    for (int i = 0; i < X; i++)
                    {
                        ((my.Controls[_TABLE_PANEL_NAME] as TableLayoutPanel).Controls[$"textBox{i},{jj}"] as TextBox).Text = "";

                    }

                }
                jj++;
            }
            for (int j = 0; j < Y; j++)
            {
                if (delIndex[j] != -1) textBox8.AppendText(Convert.ToString(++delIndex[j]) + "; ");
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            double[,] B = new double[X, Y];
            getMatrix(A);
            textBox8.Text = "";
            int[] delIndex = new int[Y];
            for (int i = 0; i < Y; i++) delIndex[i] = i;
            bool fl = getMatrix2(B);
            if (!fl) return;
            int ji = 0, jj = 0;
            bool isDeleted = false;
            var matr = ((my.Controls[_TABLE_PANEL_NAME] as TableLayoutPanel).Controls[$"textBox{ji},{jj}"] as TextBox);
            while ((jj < Y))
            {
                isDeleted = false;
                ji = 0;
                while ((ji < Y) && (!isDeleted))
                {
                    if ((ji != jj) && (delIndex[ji] != -1) && (delIndex[jj] != -1)) isDeleted = hardDom2(B, jj, ji);
                    ji++;
                }
                if (isDeleted)
                {
                    delIndex[jj] = -1;
                    for (int i = 0; i < X; i++)
                    {
                        ((my.Controls[_TABLE_PANEL_NAME] as TableLayoutPanel).Controls[$"textBox{i},{jj}"] as TextBox).Text = "";

                    }

                }
                jj++;
            }
            for (int j = 0; j < Y; j++)
            {
                if (delIndex[j] != -1) textBox8.AppendText(Convert.ToString(++delIndex[j]) + "; ");
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {

            for (int i = 0; i < X; i++)
            {
                for (int j = 0; j < Y; j++)
                {
                    ((my.Controls[_TABLE_PANEL_NAME] as TableLayoutPanel).Controls[$"textBox{i},{j}"] as TextBox).Text = Convert.ToString(A[i, j]); ;
                    ((my.Controls[_TABLE_PANEL_NAME] as TableLayoutPanel).Controls[$"textBox{i},{j}"] as TextBox).BackColor = SystemColors.Window;
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            /*using (FileStream fs = new FileStream(textBox3.Text, FileMode.Create, FileAccess.Write))
            {
                
            }*/
            try
            {
                var fs = new StreamWriter(textBox3.Text);
                fs.WriteLine(textBox1.Text + " " + textBox2.Text);
                for (int i = 0; i < X; i++)
                {
                    for (int j = 0; j < Y; j++)
                    {
                        fs.Write(((my.Controls[_TABLE_PANEL_NAME] as TableLayoutPanel).Controls[$"textBox{i},{j}"] as TextBox).Text + " ");
                    }
                    fs.Write("\n");
                }
                fs.Flush();
                fs.Close();
            } catch(ArgumentException ex)
            {
                MessageBox.Show("Есть пустые поля, которые необходимо заполнить", "Ошибка");
            }
           
        }

        private void CreateTablePanel(int columnCount, int rowCount)
        {
            //если были ранее созданы с тем же именем TableLayoutPanel
            //то удаляем их
            var oldPanels = this.Controls.Find(_TABLE_PANEL_NAME, true);
            Array.ForEach(oldPanels, new Action<Control>(c => this.Controls.Remove(c)));

            //позиция для новой панели
            Point pos = new Point { X = 10, Y = 65 };

            TableLayoutPanel tablePanel = new TableLayoutPanel
            {

                Location = pos,
                Name = _TABLE_PANEL_NAME,
                RowCount = columnCount,
                ColumnCount = rowCount,
                AutoScroll = true,
                Width = 500,
                Height = 300,
               
                
                //Dock = DockStyle.Left
                //AutoSize = true,
                //MaximumSize = size

                
            };

            //назначаем размеры для строк и столбцов
            for (int i = 0; i < tablePanel.RowStyles.Count; i++)
            {
                tablePanel.RowStyles[i].SizeType = SizeType.Absolute;
                tablePanel.RowStyles[i].Height = 30;
            }
            for (int i = 0; i < tablePanel.ColumnStyles.Count; i++)
            {
                tablePanel.ColumnStyles[i].SizeType = SizeType.Absolute;
                tablePanel.ColumnStyles[i].Width = 40;
            }

            //добавляем панель
            this.Controls.Add(tablePanel);
        }

    }
}
