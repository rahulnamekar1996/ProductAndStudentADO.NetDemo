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
using System.Configuration;
using System.Xml.Linq;

namespace ProductAndStudentADO.NetDemo
{
    public partial class Form1 : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;

        public Form1()
        {
            InitializeComponent();
            string constr = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            con = new SqlConnection(constr);

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void ClearFileds()
        {
            txtpid.Clear();
           txtpname.Clear();
            txtAuthorName.Clear();
            txtprice.Clear();
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
               
                string qry = "insert into products values(@name,@author,@price)";
              
                cmd = new SqlCommand(qry, con);
              
                cmd.Parameters.AddWithValue("@name", txtpname.Text);
                cmd.Parameters.AddWithValue("@author", txtAuthorName.Text);
                cmd.Parameters.AddWithValue("@price", txtprice.Text);
               
                con.Open();
                int result = cmd.ExecuteNonQuery();
                if (result >= 1)
                {
                    MessageBox.Show("Record inserted");
                    ClearFileds();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
            
                string qry = "update products set name=@name,author=@author,price=@price where id=@id";
               
                cmd = new SqlCommand(qry, con);
              
                cmd.Parameters.AddWithValue("@name", txtpname.Text);
                cmd.Parameters.AddWithValue("@author", txtAuthorName.Text);
                cmd.Parameters.AddWithValue("@price", txtprice.Text);
                cmd.Parameters.AddWithValue("@id", txtpid.Text);
                // fire the query
                con.Open();
                int result = cmd.ExecuteNonQuery();
                if (result >= 1)
                {
                    MessageBox.Show("Record updated");
                    ClearFileds();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }


        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                // step 1
                string qry = "select * from products where id=@id";
              
                cmd = new SqlCommand(qry, con);
                cmd.Parameters.AddWithValue("@id", txtpid.Text);
              
                con.Open();
                dr = cmd.ExecuteReader();
                if (dr.HasRows) 
                {
                    while (dr.Read())
                    {
                      
                        txtpname.Text = dr["name"].ToString();
                       txtAuthorName.Text = dr["author"].ToString();
                       txtprice.Text = dr["price"].ToString();
                    }
                }
                else
                {
                    MessageBox.Show("Record not found");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {

              
                string qry = "delete from products where id=@id";
            
                cmd = new SqlCommand(qry, con);
            
                cmd.Parameters.AddWithValue("@id", txtpid.Text);
            
                con.Open();
                int result = cmd.ExecuteNonQuery();
                if (result >= 1)
                {
                    MessageBox.Show("Record deleted");
                    ClearFileds();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }


        }

        private void btnSearchAll_Click(object sender, EventArgs e)
        {
            try
            {
                string qry = "select *from products";
                cmd = new SqlCommand(qry, con);
                con.Open();
                dr = cmd.ExecuteReader();

                DataTable table = new DataTable();
                table.Load(dr);
                dataGridView1.DataSource = table;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }

        }
    }
}
