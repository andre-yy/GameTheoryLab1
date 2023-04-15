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
        double[,] A, B;
        Form1 my;       
        private const string _TABLE_PANEL_NAME = "tableLayoutPanel";
        public Form1()
        {
            InitializeComponent();
            //my.FormBorderStyle = FormBorderStyle.FixedSingle;
           
            A = new double[X, Y];
            B = new double[X, Y];
            my = this;
            
            my.MaximizeBox = false;
            
            ShowMatrix(X, Y);
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

        private bool getMatrix1(double[,] A)
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
                        ((my.Controls[_TABLE_PANEL_NAME] as TableLayoutPanel).Controls[$"textBox{i},{j}"] as TextBox).BackColor = Color.Red;
                        return false;
                    }
                }
            }
            return true;
        }

        private bool getMatrix2(double[,] A, double[,] B)
        {

            for (int i = 0; i < X; i++)
            {
                for (int j = 0; j < Y; j++)
                {
                    try
                    {
                        string str = ((my.Controls[_TABLE_PANEL_NAME] as TableLayoutPanel).Controls[$"textBox{i},{j}"] as TextBox).Text;
                        string[] spl = str.Split(' ');
                        A[i, j] = Convert.ToDouble(spl[0]);
                        B[i, j] = Convert.ToDouble(spl[1]);
                    }
                    catch (FormatException e)
                    {
                        MessageBox.Show("Не были введены значения в поля матрицы", "Пустые элементы матрицы");
                        ((my.Controls[_TABLE_PANEL_NAME] as TableLayoutPanel).Controls[$"textBox{i},{j}"] as TextBox).BackColor = Color.Red;
                        return false;
                    }
                    catch (IndexOutOfRangeException)
                    {
                        MessageBox.Show("Введите через пробел элементы 2-х матриц в ячейки, в соответсвии с их индексам",
                            "Ошибка формата двойной матрицы");
                        return false;
                    }
                }
            }
            return true;
        }

        private bool getMatrix3(double[,] A)
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
                        ((my.Controls[_TABLE_PANEL_NAME] as TableLayoutPanel).Controls[$"textBox{i},{j}"] as TextBox).BackColor = Color.Red;
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
                    ((my.Controls[_TABLE_PANEL_NAME] as TableLayoutPanel).Controls[$"textBox{i},{j}"] as TextBox).MouseClick += new MouseEventHandler(tableLayoutPanel_MouseHover);
                }
            }
        }

        private void tableLayoutPanel_MouseHover(object sender, EventArgs e)
        {
            for (int i = 0; i < X; i++)
            {
                for (int j = 0; j < Y; j++)
                {   
                    

                        ((my.Controls[_TABLE_PANEL_NAME] as TableLayoutPanel).Controls[$"textBox{i},{j}"] as TextBox).BackColor = SystemColors.Window;


                    
                }
            }
        }

            private void button1_Click(object sender, EventArgs e)
        {
            X = Convert.ToInt32(textBox1.Text);
            Y = Convert.ToInt32(textBox2.Text);
            
            A = new double[X, Y];
            B = new double[X, Y];
            ShowMatrix(X, Y);
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            //FileStream file = new FileStream(Convert.ToString(textBox3), FileMode.Open);
            if (!checkBox2.Checked)
            {
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
                catch (FileNotFoundException ex)
                {
                    MessageBox.Show("Файл не найден", "Ошибка");
                }
            }else
            {
                try
                {
                    var fs = new StreamReader(textBox3.Text);

                    var line = fs.ReadLine();
                    string[] str = line.Split(' ');
                    if ((str[0] == "2") && (str[1] =="2") && (checkBox1.Checked) || (!checkBox1.Checked))
                    { 
                    textBox1.Text = str[0];
                    textBox2.Text = str[1];
                    }
                    else
                    {
                        MessageBox.Show("Для поиска равновесия по Нэшу в смешанный стратегиях необходимо, чтобы " +
                            "матрица имела размер 2х2!", "Неверный размер матрицы в файле");
                        return;
                    }

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
                    
                        for (int i = 0; i < X; i++)
                        {
                            line = fs.ReadLine();
                        try
                        {
                            str = line.Split(' ');
                        }catch(NullReferenceException err)
                            {
                            MessageBox.Show("Формат матрицы из файла для загрузки не совпадает с форматом выбранного типа матрицы", "Ошибка");
                            return;
                            }
                            
                            for (int j = 0; j < Y; j++)
                            {
                                ((my.Controls[_TABLE_PANEL_NAME] as TableLayoutPanel).Controls[$"textBox{i},{j}"] as TextBox).AppendText(" " + str[j]);
                            }

                        }
                    
                    
                    fs.Close();
                }
                catch (ArgumentException ex)
                {
                    MessageBox.Show("Есть пустые поля, которые необходимо заполнить", "Ошибка");
                }
                catch (FileNotFoundException ex)
                {
                    MessageBox.Show("Файл не найден", "Ошибка");
                }
               
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
            if (!checkBox2.Checked)
            {
                bool fl = getMatrix1(A);
                if (!fl) return;
            }else
            {
                bool fl = getMatrix2(A, B);
                if (!fl) return;
            }
                double maxv;
                int maxi;
                int maxj;
                maxmin(A, out maxv, out maxi, out maxj);
                ((my.Controls[_TABLE_PANEL_NAME] as TableLayoutPanel).Controls[$"textBox{maxi},{maxj}"] as TextBox).BackColor = Color.BlueViolet;
                textBox5.Text = Convert.ToString(maxv);
                textBox4.Text = Convert.ToString(++maxi);
                textBox4.AppendText("; " + ++maxj);
            

        }

        private void button5_Click(object sender, EventArgs e)
        {
            Random r = new Random();
            for (int i = 0; i < X; i++)
            {
                for (int j = 0; j < Y; j++)
                {
                    if (!checkBox2.Checked)
                    {
                        ((my.Controls[_TABLE_PANEL_NAME] as TableLayoutPanel).Controls[$"textBox{i},{j}"] as TextBox).Text = Convert.ToString(r.Next(-10, 10));
                        ((my.Controls[_TABLE_PANEL_NAME] as TableLayoutPanel).Controls[$"textBox{i},{j}"] as TextBox).BackColor = SystemColors.Window;
                    }
                    else
                    {
                        ((my.Controls[_TABLE_PANEL_NAME] as TableLayoutPanel).Controls[$"textBox{i},{j}"] as TextBox).Text = Convert.ToString(r.Next(-10, 10));
                        ((my.Controls[_TABLE_PANEL_NAME] as TableLayoutPanel).Controls[$"textBox{i},{j}"] as TextBox).AppendText(" " + Convert.ToString(r.Next(-10, 10)));
                        ((my.Controls[_TABLE_PANEL_NAME] as TableLayoutPanel).Controls[$"textBox{i},{j}"] as TextBox).BackColor = SystemColors.Window;
                    }
                }
            }
            if (!checkBox2.Checked) { getMatrix1(A); getMatrix3(B); }
            else getMatrix2(A, B);
        }

        

        private void button6_Click(object sender, EventArgs e)
        {
            textBox8.Text = "";
            int[] delIndex = new int[X];
            for (int i = 0; i < X; i++) delIndex[i] = i;
            bool fl;
            if (!checkBox2.Checked)
            {
                fl = getMatrix1(A);
            }
            else
            {
                fl = getMatrix2(A, B);
            }
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
            if (!checkBox2.Checked)
            {
                bool fl = getMatrix1(A);
                if (!fl) return;
                double minv;
                int mini;
                int minj;
                minmax(A, out minv, out mini, out minj);
                ((my.Controls[_TABLE_PANEL_NAME] as TableLayoutPanel).Controls[$"textBox{minj},{mini}"] as TextBox).BackColor = Color.Green;
                textBox7.Text = Convert.ToString(minv);
                textBox6.Text = Convert.ToString(++minj);
                textBox6.AppendText("; " + ++mini);
            }else
            {
                bool fl = getMatrix2(A, B);
                if (!fl) return;
                double minv;
                int mini;
                int minj;
                minmax(B, out minv, out mini, out minj);
                ((my.Controls[_TABLE_PANEL_NAME] as TableLayoutPanel).Controls[$"textBox{minj},{mini}"] as TextBox).BackColor = Color.Green;
                textBox7.Text = Convert.ToString(minv);
                textBox6.Text = Convert.ToString(++minj);
                textBox6.AppendText("; " + ++mini);
            }
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
            bool fl;
            if (!checkBox2.Checked)
            {
                fl = getMatrix1(A);
            }
            else
            {
                fl = getMatrix2(A, B);
            }
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
            //getMatrix1(A);
            //double[,] B = new double[X, Y];
            textBox12.Text = "";
            int[] delIndex = new int[Y];
            for (int i = 0; i < Y; i++) delIndex[i] = i;
            bool fl;
            if (!checkBox2.Checked)
            {
                fl = getMatrix3(B);
                getMatrix1(A);
            }
            else
            {
                fl = getMatrix2(A, B);
            }
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
                if (delIndex[j] != -1) textBox12.AppendText(Convert.ToString(++delIndex[j]) + "; ");
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            //double[,] B = new double[X, Y];
            //getMatrix1(A);
            textBox12.Text = "";
            int[] delIndex = new int[Y];
            for (int i = 0; i < Y; i++) delIndex[i] = i;
            bool fl;
            if (!checkBox2.Checked)
            {
                fl = getMatrix3(B);
                getMatrix1(A);
            }
            else
            {
                fl = getMatrix2(A, B);
            }
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
                if (delIndex[j] != -1) textBox12.AppendText(Convert.ToString(++delIndex[j]) + "; ");
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (!checkBox2.Checked)
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
            else
            {
                for (int i = 0; i < X; i++)
                {
                    for (int j = 0; j < Y; j++)
                    {
                        ((my.Controls[_TABLE_PANEL_NAME] as TableLayoutPanel).Controls[$"textBox{i},{j}"] as TextBox).Text = Convert.ToString(A[i, j]);
                        ((my.Controls[_TABLE_PANEL_NAME] as TableLayoutPanel).Controls[$"textBox{i},{j}"] as TextBox).AppendText(" " + Convert.ToString(B[i, j]));
                        ((my.Controls[_TABLE_PANEL_NAME] as TableLayoutPanel).Controls[$"textBox{i},{j}"] as TextBox).BackColor = SystemColors.Window;
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            /*using (FileStream fs = new FileStream(textBox3.Text, FileMode.Create, FileAccess.Write))
            {
                
            }*/
            if (!checkBox2.Checked)
            {
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
                }
                catch (ArgumentException ex)
                {
                    MessageBox.Show("Есть пустые поля, которые необходимо заполнить", "Ошибка");
                }
            }
            else
            {

                try
                {
                    var fs = new StreamWriter(textBox3.Text);
                    fs.WriteLine(textBox1.Text + " " + textBox2.Text);
                    for (int i = 0; i < X; i++)
                    {
                        for (int j = 0; j < Y; j++)
                        {
                            var str = ((my.Controls[_TABLE_PANEL_NAME] as TableLayoutPanel).Controls[$"textBox{i},{j}"] as TextBox).Text.Split(' ');
                            fs.Write(str[0] + " ");
                        }
                        fs.Write("\n");
                    }
                    for (int i = 0; i < X; i++)
                    {
                        for (int j = 0; j < Y; j++)
                        {
                            var str = ((my.Controls[_TABLE_PANEL_NAME] as TableLayoutPanel).Controls[$"textBox{i},{j}"] as TextBox).Text.Split(' ');
                            fs.Write(str[1] + " ");
                        }
                        fs.Write("\n");
                    }
                    fs.Flush();
                    fs.Close();
                }
                catch (ArgumentException ex)
                {
                    MessageBox.Show("Есть пустые поля, которые необходимо заполнить", "Ошибка");
                }
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            double p, q;
            bool fl = getMatrix2(A, B);
            if (!fl) return;
            q = (A[1, 1] - A[0, 1]) / (A[0, 0] - A[0, 1] - A[1, 0] + A[1, 1]);
            p = (B[1, 1] - B[1, 0]) / (B[0, 0] - B[0, 1] - B[1, 0] + B[1, 1]);
            if (p < 0) p = 0;
            if (q < 0) q = 0;
            if (p > 1) p = 1;
            if (q > 1) q = 1;
            textBox9.Text = Convert.ToString(p);
            textBox10.Text = Convert.ToString(q);
            p = System.Math.Round(p, 2);
            q = System.Math.Round(q, 2);
            textBox11.Text = "((" + p + ";" + (1-p) + ");(" + q + ";" + (1-q) + "))";
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                button14.Enabled = true;
                button16.Enabled = true;
                button17.Enabled = true;
               


            }
            else
            {
                button14.Enabled = false;
                button16.Enabled = false;
                button17.Enabled = false;
            }
         }

        private void Nash(double[,] A, double[,] B, int[] N1, int[] N2)
        {
            for (int j = 0; j < Y; j++)
            {
                double maxv = A[0, j];
                int maxi = 0;
                for (int i = 1; i < X; i++)
                {
                    if (A[i, j] > maxv)
                    {
                        maxv = A[i, j];
                        maxi = i;
                    }

                }
                N1[j] = maxi;
            }

            for (int i = 0; i < X; i++)
            {
                double maxv = B[i, 0];
                int maxj = 0;
                for (int j = 1; j < Y; j++)
                {
                    if (B[i, j] > maxv)
                    {
                        maxv = B[i, j];
                        maxj = j;
                    }

                }
                N2[i] = maxj;
            }
        }


            private void button14_Click(object sender, EventArgs e)
        {
            bool fl = getMatrix2(A, B);
            if (!fl) return;
            int[] N1 = new int[Y];
            int[] N2 = new int[X];

            Nash(A, B, N1, N2);

            for (int i = 0; i < X; i++)
            {
                for (int j = 0; j < Y; j++)
                {
                    if ((N1[j] == i) && (N2[i] == j)) ((my.Controls[_TABLE_PANEL_NAME] as TableLayoutPanel).Controls[$"textBox{i},{j}"] as TextBox).BackColor = Color.Yellow;
                }
            }

        }

        private void button16_Click(object sender, EventArgs e)
        {
            bool fl = getMatrix2(A, B);
            if (!fl) return;
            int[] N1 = new int[Y];
            int[] N2 = new int[X];
            Nash(A, B, N1, N2);
            bool fl1;

            for (int i = 0; i < X; i++)
            {
                fl1 = false;
                for (int j = 0; j < Y; j++)
                {
                    if (N1[j] == i) fl1 = true;
                }
                if (!fl1)
                {
                    for (int j = 0; j < Y; j++)
                    {
                        ((my.Controls[_TABLE_PANEL_NAME] as TableLayoutPanel).Controls[$"textBox{i},{j}"] as TextBox).Text = "";
                    }
                }
            }

        }

        private void button17_Click(object sender, EventArgs e)
        {
            bool fl = getMatrix2(A, B);
            if (!fl) return;
            int[] N1 = new int[Y];
            int[] N2 = new int[X];
            Nash(A, B, N1, N2);
            bool fl1;

            for (int j = 0; j < Y; j++)
            {
                fl1 = false;
                for (int i = 0; i < X; i++)
                {
                    if (N2[i] == j) fl1 = true;
                }
                if (!fl1)
                {
                    for (int i = 0; i < X; i++)
                    {
                        ((my.Controls[_TABLE_PANEL_NAME] as TableLayoutPanel).Controls[$"textBox{i},{j}"] as TextBox).Text = "";
                    }
                }
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                textBox1.Text = "2";
                textBox2.Text = "2";
                button1_Click(sender, e);
                textBox1.Enabled = false;
                textBox2.Enabled = false;
                button13.Enabled = true;
                checkBox2.Checked = true;
                checkBox2.Enabled = false;
            }
            else
            {
                textBox1.Enabled = true;
                textBox2.Enabled = true;
                button13.Enabled = false;
                checkBox2.Enabled = true;
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
            //(my.Controls[_TABLE_PANEL_NAME] as TableLayoutPanel).MouseHover += new EventHandler(tableLayoutPanel_MouseHover);
            
        }

    }
}
