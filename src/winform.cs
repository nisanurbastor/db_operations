using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        NpgsqlConnection connect = new NpgsqlConnection("server=localHost; port=5432; UserId=postgres; password=12345; database=mydb");

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e) //LIST OPERATION
        {
            string query = "select passportid,name,surname from passenger";
            NpgsqlDataAdapter adapt = new NpgsqlDataAdapter(query, connect);
            DataSet ds = new DataSet();
            adapt.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void button2_Click(object sender, EventArgs e) //INSERT OPERATION
        {
            connect.Open();
            NpgsqlCommand command1 = new NpgsqlCommand("insert into passenger values (@passportid,@name,@surname)", connect);
            command1.Parameters.AddWithValue("@passportid", int.Parse(textBox1.Text));
            command1.Parameters.AddWithValue("@name", textBox2.Text);
            command1.Parameters.AddWithValue("@surname", textBox3.Text);
            command1.ExecuteNonQuery();
            connect.Close();
            MessageBox.Show("Passenger insert operation has been done succesfully.");
        }

        private void button3_Click(object sender, EventArgs e) //DELETE OPERATION
        {
            connect.Open();
            NpgsqlCommand command2 = new NpgsqlCommand("Delete from passenger where \"passportid\"=@passportid", connect);
            command2.Parameters.AddWithValue("@passportid", int.Parse(textBox1.Text));
            command2.ExecuteNonQuery();
            connect.Close();
            MessageBox.Show("Passenger information has been deleted succesfully.");
        }

        private void button4_Click(object sender, EventArgs e) //UPDATE OPERATION
        {
            connect.Open();
            NpgsqlCommand command3 = new NpgsqlCommand("update passenger set \"name\"=@name, \"surname\"=@surname where \"passportid\"=@passportid", connect);
         
            command3.Parameters.AddWithValue("@passportid", int.Parse(textBox1.Text));
            command3.Parameters.AddWithValue("@name", textBox2.Text);
            command3.Parameters.AddWithValue("@surname", textBox3.Text);

            command3.ExecuteNonQuery();
            MessageBox.Show("Passenger information has been updated succesfully.");
            connect.Close();
        }
    }
}
