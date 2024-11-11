using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection.Emit;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class StudentUpdateProfilePage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["sid"] == null)
            {
                Response.Redirect("StudentLoginPage.aspx");
                return;
            }

            LoadStudentData(Session["sid"].ToString());
        }
    }

    protected void LoadStudentData(string studentId)
    {
        using (SqlConnection conn = DatabaseManager.GetConnection())
        {
            try
            {
                conn.Open();
                string query = "SELECT permanent_address, permanent_postcode, permanent_city, permanent_state, permanent_country, " +
                               "current_address, current_postcode, current_city, current_state, current_country, primary_email, " +
                               "alternative_email, tel_no, hp_no, emergency_contact_relationship, emergency_contact_person, " +
                               "emergency_contact_number FROM student WHERE sid = @SID;";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    
                    cmd.Parameters.AddWithValue("@SID", Session["SID"].ToString());

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        txtPermanentAddress.Text = reader["permanent_address"] != DBNull.Value ? reader["permanent_address"].ToString() : string.Empty;
                        txtCurrentAddress.Text = reader["current_address"] != DBNull.Value ? reader["current_address"].ToString() : string.Empty;
                        txtPermanentPostcode.Text = reader["permanent_postcode"] != DBNull.Value ? reader["permanent_postcode"].ToString() : string.Empty;
                        txtCurrentPostcode.Text = reader["current_postcode"] != DBNull.Value ? reader["current_postcode"].ToString() : string.Empty;
                        txtPermanentCity.Text = reader["permanent_city"] != DBNull.Value ? reader["permanent_city"].ToString() : string.Empty;
                        txtCurrentCity.Text = reader["current_city"] != DBNull.Value ? reader["current_city"].ToString() : string.Empty;
                        txtPermanentState.Text = reader["permanent_state"] != DBNull.Value ? reader["permanent_state"].ToString() : string.Empty;
                        txtCurrentState.Text = reader["current_state"] != DBNull.Value ? reader["current_state"].ToString() : string.Empty;
                        txtPermanentCountry.Text = reader["permanent_country"] != DBNull.Value ? reader["permanent_country"].ToString() : string.Empty;
                        txtCurrentCountry.Text = reader["current_country"] != DBNull.Value ? reader["current_country"].ToString() : string.Empty;
                        ddlRelationship.SelectedValue = reader["emergency_contact_relationship"] != DBNull.Value ? reader["emergency_contact_relationship"].ToString() : string.Empty;
                        txtPrimaryEmail.Text = reader["primary_email"] != DBNull.Value ? reader["primary_email"].ToString() : string.Empty;
                        txtAlternativeEmail.Text = reader["alternative_email"] != DBNull.Value ? reader["alternative_email"].ToString() : string.Empty;
                        txtTelNo.Text = reader["tel_no"] != DBNull.Value ? reader["tel_no"].ToString() : string.Empty;
                        txtHpNo.Text = reader["hp_no"] != DBNull.Value ? reader["hp_no"].ToString() : string.Empty;
                        txtContactPerson.Text = reader["emergency_contact_person"] != DBNull.Value ? reader["emergency_contact_person"].ToString() : string.Empty;
                        txtContactPersonHpNo.Text = reader["emergency_contact_number"] != DBNull.Value ? reader["emergency_contact_number"].ToString() : string.Empty;

                    }
                    else
                    {
                        // No data found for the given SID
                       Label1.Text = "Student data not found.";
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
              
               Label1.Text = "Error loading data: " + ex.Message;
            }
        }


    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        string query = "UPDATE student SET permanent_address = @PermanentAddress, permanent_postcode = @PermanentPostcode," +
            " permanent_city = @PermanentCity, permanent_state = @PermanentState, permanent_country = @PermanentCountry," +
            " current_address = @CurrentAddress, current_postcode = @CurrentPostcode, current_city = @CurrentCity," +
            " current_state = @CurrentState, current_country = @CurrentCountry, primary_email = @PrimaryEmail, " +
            "alternative_email = @AlternativeEmail, tel_no = @TelNo, hp_no = @HpNo, " +
            "emergency_contact_relationship = @EmergencyRelationship, emergency_contact_person = @EmergencyContactPerson," +
            " emergency_contact_number = @EmergencyContactNumber WHERE sid = @SID;";

        using (SqlConnection con = DatabaseManager.GetConnection())
        {
            SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@PermanentAddress", txtPermanentAddress.Text);
            cmd.Parameters.AddWithValue("@PermanentPostcode", txtPermanentPostcode.Text);
            cmd.Parameters.AddWithValue("@PermanentCity", txtPermanentCity.Text);
            cmd.Parameters.AddWithValue("@PermanentState", txtPermanentState.Text);
            cmd.Parameters.AddWithValue("@PermanentCountry", txtPermanentCountry.Text);
            cmd.Parameters.AddWithValue("@CurrentAddress", txtCurrentAddress.Text);
            cmd.Parameters.AddWithValue("@CurrentPostcode", txtCurrentPostcode.Text);
            cmd.Parameters.AddWithValue("@CurrentCity", txtCurrentCity.Text);
            cmd.Parameters.AddWithValue("@CurrentState", txtCurrentState.Text);
            cmd.Parameters.AddWithValue("@CurrentCountry", txtCurrentCountry.Text);
            cmd.Parameters.AddWithValue("@PrimaryEmail", txtPrimaryEmail.Text);
            cmd.Parameters.AddWithValue("@AlternativeEmail", txtAlternativeEmail.Text);
            cmd.Parameters.AddWithValue("@TelNo", txtTelNo.Text);
            cmd.Parameters.AddWithValue("@HpNo", txtHpNo.Text);
            cmd.Parameters.AddWithValue("@EmergencyRelationship", ddlRelationship.SelectedValue);
            cmd.Parameters.AddWithValue("@EmergencyContactPerson", txtContactPerson.Text);
            cmd.Parameters.AddWithValue("@EmergencyContactNumber", txtContactPersonHpNo.Text);

            // Retrieve SID from session
            cmd.Parameters.AddWithValue("@SID", Session["SID"].ToString());

            con.Open();
            int result = cmd.ExecuteNonQuery();
            con.Close();

            if (result > 0)
            {
                string script = "alert('Profile updated successfully!');";
                ClientScript.RegisterStartupScript(this.GetType(), "SaveSuccess", script, true);
            }
            else
            {
                Label1.Text = "Error: Profile update failed. Please try again.";
            }
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("StudentHomePage.aspx");
    }
}