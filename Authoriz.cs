using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Captcha;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace OOO_Flowers
{
    public partial class Authoriz : Form
    {
        public Authoriz()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(DataBaseWorker.GetConnString());
            conn.Open();
            string Sql = "select UserRole from Users where UserLogin = '" + textBox1.Text + "'" + " and UserPassword = '" + textBox2.Text + "'";

            SqlCommand com = new SqlCommand(Sql, conn);
            SqlDataReader reader = com.ExecuteReader();

            String Roll = null;

            while (reader.Read())
                Roll = reader[0].ToString();
 
            conn.Close();

            if (Roll != null)
            {
                Product product = new Product();
                Hide();
                product.Show();
            }
            else
            {
                textBox1.Enabled = false;
                textBox1.BackColor = Color.Gray;
                textBox2.Enabled = false;
                textBox2.BackColor = Color.Gray;
                CaptchaForm captchaForm = new CaptchaForm();
                captchaForm.ShowDialog();
                if(captchaForm.DialogResult == DialogResult.OK)
                {
                    textBox1.Enabled = true;
                    textBox1.BackColor = Color.FromArgb(118, 227, 131);
                    textBox2.Enabled = true;
                    textBox2.BackColor = Color.FromArgb(118, 227, 131);
                }
                if (captchaForm.DialogResult == DialogResult.Cancel)
                {
                    MessageBox.Show("Капча не введена");
                }
            }
        }
    }
}
