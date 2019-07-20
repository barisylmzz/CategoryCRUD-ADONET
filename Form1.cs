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

namespace odev
{
    public partial class Form1 : Form
    {
        SqlConnection conn;

        public Form1()
        {
            InitializeComponent();
            conn = new SqlConnection(Properties.Settings.Default.Northwind);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            query = "Select CategoryID,CategoryName From Categories Order By CategoryID desc";
            cmd = new SqlCommand(query, conn);
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                listBox1.Items.Add(reader["CategoryID"] + "-" + reader["CategoryName"]);
                //MessageBox.Show(reader[0].ToString());
            }
            conn.Close();

        }
        SqlDataReader reader;
        string query;
        SqlCommand cmd;

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            query = "Select CategoryID,CategoryName,Description From Categories";
            cmd = new SqlCommand(query, conn);
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                //listBox1.Items.Add(reader["CategoryID"] + "-" + reader["CategoryName"]);
                if (listBox1.SelectedItem.ToString() == reader["CategoryID"] + "-" + reader["CategoryName"])
                {
                    txtCategoryName.Text = reader["CategoryName"].ToString();
                    txtDescription.Text = reader["Description"].ToString();
                }
            }
            conn.Close();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            int affectedRows = 0;
            string kayit = "Insert Into Categories(CategoryName,Description) Values ('" + txtCategoryName.Text + "','" + txtDescription.Text + "')";
            cmd = new SqlCommand(kayit, conn);
            query = "Select CategoryID,CategoryName,Description From Categories Order By CategoryID desc";
            SqlCommand cmd1 = new SqlCommand(query, conn);
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
                affectedRows = (int)cmd.ExecuteNonQuery();
            }
            reader = cmd1.ExecuteReader();
            listBox1.Items.Clear();
            while (reader.Read())
            {
                listBox1.Items.Add(reader["CategoryID"] + "-" + reader["CategoryName"]);
            }
            conn.Close();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            int affectedRows = 0;
            string guncelleme = "Update Categories Set CategoryName='" + txtCategoryName.Text + "',Description='" + txtDescription.Text + "' Where Convert(nvarchar,CategoryID)+'-'+CategoryName='" + listBox1.SelectedItem.ToString() + "'";
            cmd = new SqlCommand(guncelleme, conn);
            query = "Select CategoryID,CategoryName,Description From Categories Order By CategoryID desc";
            SqlCommand cmd1 = new SqlCommand(query, conn);

            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
                affectedRows = cmd.ExecuteNonQuery();
            }
            reader = cmd1.ExecuteReader();
            listBox1.Items.Clear();
            while (reader.Read())
            {
                listBox1.Items.Add(reader["CategoryID"] + "-" + reader["CategoryName"]);
               
            }
            conn.Close();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            int affectedRows = 0;
            string silme = "Delete From Categories Where Convert(nvarchar,CategoryID)+'-'+CategoryName='" + listBox1.SelectedItem.ToString() + "'";
            cmd = new SqlCommand(silme, conn);
            query = "Select CategoryID,CategoryName,Description From Categories Order By CategoryID desc";
            SqlCommand cmd1 = new SqlCommand(query, conn);

            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
                affectedRows = cmd.ExecuteNonQuery();
            }
            reader = cmd1.ExecuteReader();
            listBox1.Items.Clear();
            while (reader.Read())
            {
                listBox1.Items.Add(reader["CategoryID"] + "-" + reader["CategoryName"]);

            }
            conn.Close();
        }
    }
}
