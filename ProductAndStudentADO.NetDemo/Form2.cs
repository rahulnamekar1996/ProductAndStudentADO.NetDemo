using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProductAndStudentADO.NetDemo
{
    public partial class Form2 : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;

        public Form2()
        {
            InitializeComponent();
            string constr = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            con = new SqlConnection(constr);

        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }
        private void ClearFileds()
        {
            txtsid.Clear();
            txtsname.Clear();
            txtcity.Clear();
           
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
               
                string qry = "insert into student values(@name,@city)";
               
                cmd = new SqlCommand(qry, con);
           
                cmd.Parameters.AddWithValue("@name", txtsname.Text);
                cmd.Parameters.AddWithValue("@city", txtcity.Text);
               
            
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

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                
                string qry = "select * from student where id=@id";
            
                cmd = new SqlCommand(qry, con);
                cmd.Parameters.AddWithValue("@id", txtsid.Text);
            
                con.Open();
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read()) 
                    {
                     
                        txtsname.Text = dr["name"].ToString();
                        txtcity.Text = dr["city"].ToString();
                        
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

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
            
                string qry = "update student set name=@name,city=@city where id=@id";
             
                cmd = new SqlCommand(qry, con);
          
                cmd.Parameters.AddWithValue("@name", txtsname.Text);
                cmd.Parameters.AddWithValue("@city", txtcity.Text);
                cmd.Parameters.AddWithValue("@id", txtsid.Text);
            
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

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {

             
                string qry = "delete from student where id=@id";
             
                cmd = new SqlCommand(qry, con);
            
                cmd.Parameters.AddWithValue("@id", txtsid.Text);
           
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
                string qry = "select *from student";
                cmd = new SqlCommand(qry, con);
                con.Open();
                dr = cmd.ExecuteReader();

                DataTable table = new DataTable();
                table.Load(dr);
                dataGridView2.DataSource = table;

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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    
    }
}
