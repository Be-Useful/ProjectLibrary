using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace elibraryclone
{
    public partial class adminauthormanagement : System.Web.UI.Page
    {

        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            GridView1.DataBind();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            if (checkIfAuthorExists())
            {
                Response.Write("<script> alert('Author_ID already Exists');</script>");
            }
            else
            {
                AddNewAuthor();
            }
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            if (checkIfAuthorExists())
            {
                UpdateAuthor();
            }
            else
            {
                Response.Write("<script> alert('Author_ID doesn't Exists');</script>");
            }
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            if (checkIfAuthorExists())
            {
                DeleteAuthor();
            }
            else
            {
                Response.Write("<script> alert('Author_ID doesn't Exists');</script>");
            }
        }


        protected void Button1_Click(object sender, EventArgs e)
        {
            gofunction();
        }


        //user defined function 

        void gofunction()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("SELECT * from author_master_tbl where author_id='" + TextBox1.Text.Trim() + "';", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count >= 1)
                {
                    TextBox2.Text = dt.Rows[0][1].ToString();
                }
                else
                {
                    Response.Write("<script> alert('invalid author id');</script>");
                }
                con.Close();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
                
            }
        }


        void clearform()
        {
            TextBox1.Text = "";
            TextBox2.Text = "";
        }


        void DeleteAuthor()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == System.Data.ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("DELETE from author_master_tbl  WHERE " +
                    "author_id = '" + TextBox1.Text.Trim() + "'", con);
                cmd.ExecuteNonQuery();
                con.Close();
                Response.Write("<script> alert('Author Deleted Successfully.');</script>");
                clearform();
                GridView1.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }
        
        void UpdateAuthor()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == System.Data.ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("UPDATE author_master_tbl SET author_name = @author_name WHERE " +
                    "author_id = '"+TextBox1.Text.Trim()+"'" ,con);
                cmd.Parameters.AddWithValue("@author_name", TextBox2.Text.Trim());
                cmd.ExecuteNonQuery();
                con.Close();
                Response.Write("<script> alert('Author updated Successfully.');</script>");
                clearform();
                GridView1.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }
        
        bool checkIfAuthorExists()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("SELECT * from author_master_tbl where author_id='" + TextBox1.Text.Trim() + "';", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count >= 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
                con.Close();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
                return false;
            }
        }

        void AddNewAuthor()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == System.Data.ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("INSERT INTO author_master_tbl (author_id,author_name) " +
                    "values (@author_id,@author_name)", con);
                cmd.Parameters.AddWithValue("@author_id", TextBox1.Text.Trim());
                cmd.Parameters.AddWithValue("@author_name", TextBox2.Text.Trim());

                cmd.ExecuteNonQuery();
                con.Close();
                Response.Write("<script> alert('Author added Successfully.');</script>");
                clearform();
                GridView1.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }
    }
}