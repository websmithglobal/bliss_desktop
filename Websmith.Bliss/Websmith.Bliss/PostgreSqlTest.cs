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

namespace Websmith.Bliss
{
    public partial class PostgreSqlTest : Form
    {
        NpgsqlConnection conn;
        string connstring;
        private DataTable dt;
        string mode;

        private void fillgrid()
        {
            try
            {
                dt = new DataTable();
                dataGridView1.DataSource = null;
                conn.Open();
                string sql = "SELECT * FROM \"EmployeeMaster\" ORDER BY \"EmpID\"";
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, conn);
                da.Fill(dt);
                dataGridView1.DataSource = dt;
                conn.Close();
            }
            catch (Exception ex)
            {
                conn.Close();
                MessageBox.Show(ex.Message.ToString());
            }
        }

        public PostgreSqlTest()
        {
            InitializeComponent();
           connstring = "Server=localhost;Port=5432;User Id=postgres;Password=115599;Database=BLISS;";
            conn = new NpgsqlConnection(connstring);
        }

        private void PostgreSqlTest_Load(object sender, EventArgs e)
        {
            mode = "Add";
            this.fillgrid();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string sql = "";
                conn.Open();
                if (mode == "Add")
                {
                    sql = "INSERT INTO \"EmployeeMaster\" (\"EmpID\",\"EmpName\", \"JoinDate\") values('" + textBox1.Text + "','" + textBox2.Text + "','" + dateTimePicker1.Text + "')";
                }
                else
                {
                    sql = "UPDATE \"EmployeeMaster\" SET \"EmpName\"='" + textBox2.Text + "', \"JoinDate\"='" + dateTimePicker1.Text + "' WHERE \"EmpID\"='" + textBox1.Text + "'";
                }
                NpgsqlCommand cmd = new NpgsqlCommand(sql);
                cmd.Connection = conn;
                cmd.ExecuteNonQuery();
                conn.Close();
                this.fillgrid();
                mode = "Add";
                textBox1.Text = "";
                textBox2.Text = "";
                dateTimePicker1.Text = DateTime.Now.ToString("dd/MM/yyyy");
            }
            catch (Exception ex)
            {
                conn.Close();
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                textBox1.Text = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value.ToString();
                string sql = "DELETE FROM \"EmployeeMaster\" WHERE \"EmpID\"='" + textBox1.Text + "'";
                NpgsqlCommand cmd = new NpgsqlCommand(sql);
                cmd.Connection = conn;
                cmd.ExecuteNonQuery();
                conn.Close();
                this.fillgrid();
                textBox1.Text = "";
            }
            catch (Exception ex)
            {
                conn.Close();
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                dt = new DataTable();
                textBox1.Text = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value.ToString();
                string sql = "SELECT * FROM \"EmployeeMaster\" WHERE \"EmpID\"='" + textBox1.Text + "'";
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, conn);
                da.Fill(dt);
                conn.Close();
                if (dt.Rows.Count > 0)
                {
                    textBox1.Text = dt.Rows[0][0].ToString();
                    textBox2.Text = dt.Rows[0][1].ToString();
                    dateTimePicker1.Text = dt.Rows[0][2].ToString();
                    mode = "Update";
                }
                else
                {
                    MessageBox.Show("Record Not Found.");
                }
            }
            catch (Exception ex)
            {
                conn.Close();
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
