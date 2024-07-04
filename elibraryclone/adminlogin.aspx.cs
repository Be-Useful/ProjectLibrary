using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace elibraryclone
{
    public partial class userlogin : System.Web.UI.Page
    {

        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    if (con.State == System.Data.ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    SqlCommand cmd = new SqlCommand("SELECT * FROM admin_login_tbl WHERE username = @member_id AND password = @password", con);
                    cmd.Parameters.AddWithValue("@member_id", TextBox1.Text.Trim());
                    cmd.Parameters.AddWithValue("@password", TextBox2.Text.Trim());

                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            string alertMessage = dr.GetValue(0).ToString();
                            ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('{alertMessage}');", true);
                            Session["username"] = dr.GetValue(0).ToString();
                            Session["fullname"] = dr.GetValue(2).ToString();
                            Session["role"] = "admin";
                        }
                        Response.Redirect("homepage.aspx");
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Invalid credentials');", true);
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle the exception (log it and show a user-friendly message)
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('An error occurred. Please try again later.');", true);
            }
        }
    }
}