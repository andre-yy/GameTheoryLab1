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
        
        //public static List<TextBox> TextBoxes = new List<TextBox>();
        private const string _TABLE_PANEL_NAME = "tableLayoutPanel";
        public Form1()
        {
            InitializeComponent();
            float[,] A = new float[X, Y];
            ShowMatrix(X, Y);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        

        private void ShowMatrix(int rowCount, int columnCount)
        {
            //создание TableLayoutPanel
            CreateTablePanel(rowCount, columnCount);

            //находим по имени только что созданную TableLayoutPanel
            var tablePanel = this.Controls
                                    .Find(_TABLE_PANEL_NAME, true)
                                    .First() as TableLayoutPanel;

            //вставляем текстбоксы
            for (int i = 0; i < columnCount; i++)
            {
                for (int j = 0; j < rowCount; j++)
                {
                    var txt = $"textBox{i}{j}";
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
            ShowMatrix(X, Y);
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            FileStream file = new FileStream(Convert.ToString(textBox3), FileMode.Open);
            
        }

        private void CreateTablePanel(int rowCount, int columnCount)
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
                RowCount = rowCount,
                ColumnCount = columnCount,
                AutoScroll = true,
                Width = 500,
                Height = 300
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
