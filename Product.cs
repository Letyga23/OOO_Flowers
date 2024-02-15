using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OOO_Flowers
{
    public partial class Product : Form
    {
        public Product()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Authoriz authoriz = new Authoriz();
            authoriz.Show();
            Hide();
        }

        private void Product_Load(object sender, EventArgs e)
        {
            DataBaseWorker.LoadData("Product",dataGridView1);
        }

        private void Product_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].Name == "ProductPhoto" && e.Value != DBNull.Value)
            {
                if(e.Value is byte[]) 
                {
                    byte[] imageData = (byte[])e.Value;
                    if (imageData != null && imageData.Length > 0)
                    {
                        MemoryStream ms = new MemoryStream(imageData);
                        Image image = Image.FromStream(ms);
                        e.Value = image;
                    }
                }
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && dataGridView1.Columns[e.ColumnIndex].Name == "ProductPhoto")
            {
                byte[] imageData = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value as byte[];

                if (imageData != null && imageData.Length > 0)
                {
                    MemoryStream ms = new MemoryStream(imageData);
                    Image image = Image.FromStream(ms);

                    Form imageForm = new Form();
                    PictureBox pictureBox = new PictureBox
                    {
                        Image = image,
                        SizeMode = PictureBoxSizeMode.Zoom,
                        Dock = DockStyle.Fill
                    };

                    imageForm.Controls.Add(pictureBox);
                    imageForm.StartPosition = FormStartPosition.CenterScreen;
                    imageForm.ShowDialog();
                }
            }
        }
    }
}
