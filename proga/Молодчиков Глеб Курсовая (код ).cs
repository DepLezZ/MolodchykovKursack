using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace kursach
{
    public partial class Form1 : Form
    {
        SqlConnection sql = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\steam\source\repos\kursach\kursach\DBOSBB.mdf;Integrated Security=True");

        public Form1()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int Electr_now = int.Parse(textBox1.Text);
            int ColdW_drainage_now = int.Parse(textBox2.Text);
            int HotW_now = int.Parse(textBox6.Text);
            int HotW_drainage_now = int.Parse(textBox3.Text);
            int heating_now = int.Parse(textBox4.Text);
            int Electr_pre = int.Parse(textBox14.Text);
            int ColdW_drainage_pre = int.Parse(textBox13.Text);
            int HotW_pre = int.Parse(textBox9.Text);
            int HotW_drainage_pre = int.Parse(textBox11.Text);
            int heating_pre = int.Parse(textBox8.Text);
            double rahunok = (((Electr_now - Electr_pre) - 100) * 1.68 + 90) + ((ColdW_drainage_now - ColdW_drainage_pre) * 21.75) + ((HotW_now - HotW_pre) * 97.89) + ((HotW_drainage_now - HotW_drainage_pre) * 10.21) + ((heating_now - heating_pre));
            


            textBox5.Text = rahunok.ToString();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            double rahunok = double.Parse(textBox5.Text);
            double kinec = double.Parse(textBox7.Text);
            if (rahunok == kinec)
            {
                MessageBox.Show("поздоровляю, ви оплатили комунальні платежі");
            }
            if (kinec < rahunok)
            {
                MessageBox.Show("ви недооплатили комунальні платежі! Борг складає " + (rahunok - kinec));
            }
            if (kinec > rahunok)
            {
                MessageBox.Show("поздоровляю, ви оплатили комунальні платежі. Переплата складає " + ((rahunok - kinec) * (-1)));
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox14_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox13_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Prosmotr_vvoda f1 = new Prosmotr_vvoda();
            f1.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (sql.State == ConnectionState.Closed)
                    sql.Open();
                SqlCommand cmd = new SqlCommand("Procedure_OSBBPRE", sql);
                SqlCommand cmd1 = new SqlCommand("Procedure_OSBBNOW", sql);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd1.CommandType = CommandType.StoredProcedure;

                cmd1.Parameters.AddWithValue("@mode", "Add");
                cmd.Parameters.AddWithValue("@mode", "Add");
                cmd1.Parameters.AddWithValue("@Id", 0);
                cmd.Parameters.AddWithValue("@Id", 0);
                cmd.Parameters.AddWithValue("@Electr_pre", int.Parse(textBox14.Text));
                cmd.Parameters.AddWithValue("@ColdW_drainage_pre", int.Parse(textBox13.Text));
                cmd.Parameters.AddWithValue("@HotW_pre", int.Parse(textBox9.Text));
                cmd.Parameters.AddWithValue("@HotW_drainage_pre", int.Parse(textBox11.Text));
                cmd.Parameters.AddWithValue("@heating_pre", int.Parse(textBox8.Text));
                cmd1.Parameters.AddWithValue("@Electr_now", int.Parse(textBox1.Text));
                cmd1.Parameters.AddWithValue("@ColdW_drainage_now", int.Parse(textBox2.Text));
                cmd1.Parameters.AddWithValue("@HotW_now", int.Parse(textBox6.Text));
                cmd1.Parameters.AddWithValue("@HotW_drainage_now", int.Parse(textBox3.Text));
                cmd1.Parameters.AddWithValue("@heating_now", int.Parse(textBox4.Text));

                cmd.ExecuteNonQuery();
                cmd1.ExecuteNonQuery();
                MessageBox.Show("Сохранение значений комунальных платежей произошло успешно!  🎉");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Message");
            }
            finally
            {
                sql.Close();
            }
        }
    }
}
