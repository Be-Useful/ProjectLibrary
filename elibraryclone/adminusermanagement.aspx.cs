using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace elibraryclone
{
    public partial class adminusermanagement : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            GridView1.DataBind();
        }

        // active button
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            updateMemberStatus("active");
        }

        // go button
        protected void LinkButton4_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    if (con.State == System.Data.ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    SqlCommand cmd = new SqlCommand("SELECT * FROM member_master_tbl WHERE member_id = @member_id", con);
                    cmd.Parameters.AddWithValue("@member_id", TextBox1.Text.Trim());

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                TextBox2.Text = dr.GetValue(0).ToString();
                                TextBox7.Text = dr.GetValue(10).ToString();
                                TextBox8.Text = dr.GetValue(1).ToString();
                                TextBox3.Text = dr.GetValue(2).ToString();
                                TextBox4.Text = dr.GetValue(3).ToString();
                                TextBox9.Text = dr.GetValue(4).ToString();
                                TextBox10.Text = dr.GetValue(5).ToString();
                                TextBox11.Text = dr.GetValue(6).ToString();
                                TextBox6.Text = dr.GetValue(7).ToString();
                            }
                        }
                        else
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Invalid credentials');", true);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle the exception (log it and show a user-friendly message)
                Response.Write("<script> alert('" + ex.Message + "');</script>");
            }
        }

        // pending button
        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            updateMemberStatus("pending");
        }

        // deactive button
        protected void LinkButton3_Click(object sender, EventArgs e)
        {
            updateMemberStatus("deactive");
        }

        // delete button
        protected void Button2_Click(object sender, EventArgs e)
        {
            // Implementation for delete button click
            DeleteMember();
        }

        // user defined function
        void updateMemberStatus(string status)
        {
            if (checkIfMemberExists())
            {
                try
                {
                    using (SqlConnection con = new SqlConnection(strcon))
                    {
                        if (con.State == System.Data.ConnectionState.Closed)
                        {
                            con.Open();
                        }

                        SqlCommand cmd = new SqlCommand("UPDATE member_master_tbl SET account_status = @status WHERE member_id = @member_id", con);
                        cmd.Parameters.AddWithValue("@status", status);
                        cmd.Parameters.AddWithValue("@member_id", TextBox1.Text.Trim());

                        cmd.ExecuteNonQuery();
                        GridView1.DataBind();
                        Response.Write("<script> alert('Member status updated');</script>");
                    }
                }
                catch (Exception ex)
                {
                    // Handle the exception (log it and show a user-friendly message)
                    Response.Write("<script> alert('" + ex.Message + "');</script>");
                }
            }
            else
            {
                Response.Write("<script> alert('member id doesn't exist.');</script>");
            }
            
        }

        void DeleteMember()
        {

            if (checkIfMemberExists())
            {
                try
                {
                    SqlConnection con = new SqlConnection(strcon);
                    if (con.State == System.Data.ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    SqlCommand cmd = new SqlCommand("DELETE from member_master_tbl  WHERE " +
                        "member_id = '" + TextBox1.Text.Trim() + "'", con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    Response.Write("<script> alert('member Deleted Successfully.');</script>");
                    clearform();
                    GridView1.DataBind();
                }
                catch (Exception ex)
                {
                    Response.Write("<script>alert('" + ex.Message + "');</script>");
                }
            }
            else
            {
                Response.Write("<script> alert('member id doesn't exist.');</script>");
            }
        }

        void clearform()
        {
            TextBox2.Text = "";
            TextBox7.Text = "";
            TextBox8.Text = "";
            TextBox3.Text = "";
            TextBox4.Text = "";
            TextBox9.Text = "";
            TextBox10.Text = "";
            TextBox11.Text = "";
            TextBox6.Text = "";
            TextBox1.Text = "";
        }

        bool checkIfMemberExists()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("SELECT * from member_master_tbl where member_id='" + TextBox1.Text.Trim() + "';", con);
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

    }
}
