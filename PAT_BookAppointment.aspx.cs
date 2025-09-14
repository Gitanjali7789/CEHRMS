using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CEHRMS
{
    public partial class PAT_BookAppointment : System.Web.UI.Page
    {

        SqlConnection conn = new SqlConnection(@"Server=localhost\SQLEXPRESS; Database=CEHRMS; Trusted_Connection=True;");
        DataTable dt;
        SqlDataAdapter sda;
        SqlCommand cmd;
        static String email;
        static string Hospital;
        static string HospitalName;
        string constr = ConfigurationManager.ConnectionStrings["dbConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
                getHospitalList();
                ddlHospital.Items.Insert(0, "Hospitals");
                //ddlDepartment.Items.Insert(0, "Departments");
                //getDepartmentList();
                ddlDepartment.Items.Insert(0, "Departments");
                //getDoctorList();
                ddlDoctor.Items.Insert(0, "Doctors");
            }
            lblPID.Text = Session["PatId"].ToString();
            lblName.Text = Session["PatFName"].ToString();
            email = Session["Email"].ToString();
            if (!String.IsNullOrEmpty(Session["Photo"].ToString()))
            {
                UserPhoto.ImageUrl = Session["Photo"].ToString();
            }
            else
            {
                UserPhoto.ImageUrl = @"..\\assets2\\img\\user.png";
            }
        }
        public void getHospitalList()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(@"Server=localhost\SQLEXPRESS; Database=CEHRMS; Trusted_Connection=True;"))
                {
                    string query = "SELECT Hospital_ID, HospitalName FROM Hospital";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.CommandType = CommandType.Text;

                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            sda.Fill(dt);

                            if (dt.Rows.Count > 0)
                            {
                                ddlHospital.DataSource = dt;
                                ddlHospital.DataTextField = "HospitalName";
                                ddlHospital.DataValueField = "Hospital_ID";
                                ddlHospital.DataBind();
                            }
                            else
                            {
                                ddlHospital.Items.Clear();
                                ddlHospital.Items.Add(new ListItem("No Hospitals Found", "0"));
                            }
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                // Log SQL errors
                System.Diagnostics.Debug.WriteLine("SQL Error: " + sqlEx.Message);
            }
            catch (Exception ex)
            {
                // Log general errors
                System.Diagnostics.Debug.WriteLine("Error: " + ex.Message);
            }
        }

        protected void ddlDoctor_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void ddlHospital_SelectedIndexChanged(object sender, EventArgs e)
        {
            Hospital = ddlHospital.SelectedValue;
            HospitalName = ddlHospital.SelectedIndex.ToString();
            getDepartmentList(Hospital);
        }
        public void getDepartmentList(string hospitalId)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(@"Server=localhost\SQLEXPRESS; Database=CEHRMS; Trusted_Connection=True;"))
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT DISTINCT DOcDepartmentID,DepartmentName FROM DOCDEPHOSP WHERE DocHospitalID='" + hospitalId + "'"))
                    {
                        using (SqlDataAdapter sda = new SqlDataAdapter())
                        {
                            DataTable dt = new DataTable();
                            cmd.CommandType = CommandType.Text;
                            cmd.Connection = con;
                            sda.SelectCommand = cmd;
                            sda.Fill(dt);
                            ddlDepartment.DataSource = dt;
                            ddlDepartment.Items.Insert(0, "Departments");
                            ddlDepartment.DataBind();
                            ddlDepartment.DataTextField = "DepartmentName";
                            ddlDepartment.DataValueField = "DOcDepartmentID";
                            ddlDepartment.DataBind();
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                System.Diagnostics.Debug.WriteLine("SQL Error: " + sqlEx.Message);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error: " + ex.Message);
            }
        }

        protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            String dept = ddlDepartment.SelectedValue;
            getDoctorList(dept, Hospital);
        }
        public void getDoctorList(string deptId, string hospId)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(@"Server=localhost\SQLEXPRESS; Database=CEHRMS; Trusted_Connection=True;"))
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT DoctorID, DocFullName FROM DocDepHosp WHERE DocDepartmentID = '"+deptId+ "' AND DocHospitalID = '" + hospId+"'"))

                    {
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            cmd.CommandType = CommandType.Text;
                            cmd.Connection = con;
                            sda.SelectCommand = cmd;
                            sda.Fill(dt);
                            ddlDoctor.DataSource = dt;
                            ddlDoctor.DataBind();
                            ddlDoctor.DataTextField = "DocFullName";
                            ddlDoctor.DataValueField = "DoctorID";
                            ddlDoctor.DataBind();
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                System.Diagnostics.Debug.WriteLine("SQL Error: " + sqlEx.Message);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error: " + ex.Message);
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            //SqlConnection conn = new SqlConnection(constr);
            Random appid = new Random();
            int app_id = appid.Next(0, 200);
            string aid = Convert.ToString(appid.Next());
            int status = 1;
            string pid = Session["PatId"].ToString();
            string UserName = Session["PatFname"].ToString();
            try
            {
                if (!Validate())
                {
                    lblMsg.Text = "Please enter a valid data";
                }
                else
                {
                    conn.Open();// To open DB connection
                    SqlCommand cmd = new SqlCommand("StrPrcdBookAppointment", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@APPID", aid);
                    cmd.Parameters.AddWithValue("@STATUS", status);
                    cmd.Parameters.AddWithValue("@PATID", pid);
                    cmd.Parameters.AddWithValue("@HOSID", ddlHospital.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@DEPTID", ddlDepartment.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@DOCID", ddlDoctor.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@APPDATE", txtDate.Text.ToString());
                    cmd.Parameters.AddWithValue("@APPTIME", txtTime.Text.ToString());
                    cmd.ExecuteNonQuery(); // To Run the QSL query
                    String msgbody = "Dear " + UserName + " your Appointment have been booked successfully at " + ddlHospital.SelectedItem.Text.ToString() + " Hospital and your appointment date is : " + txtDate.Text.ToString() + " at time : " + txtTime.Text.ToString();
                    SendSuccess(msgbody);
                    btnCancel.Visible = false;
                    btnSubmit.Visible = false;
                    lblMsg.Text = "Appointment has been listed.Waiting for approval!!!. Check your Email";
                    conn.Close();
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "alert", "alert('Submited successfully');", true);
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
                System.Console.WriteLine(ex.Message);

            }
        }
        private void SendSuccess(string msgBody)
        {
            string smtpUserName = ConfigurationManager.AppSettings["smtpUserName"];
            string smtpPassword = ConfigurationManager.AppSettings["smtpPassword"];
            string smtpClientHost = ConfigurationManager.AppSettings["smtpClient"];
            int smtpPort = Convert.ToInt32(ConfigurationManager.AppSettings["smtpPort"]);
            bool enableSSL = Convert.ToBoolean(ConfigurationManager.AppSettings["enableSSL"]);

            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(smtpUserName);
            mail.To.Add(email);
            mail.Subject = "Appointment Booking In CEHRMS";
            mail.Body = msgBody;

            using (SmtpClient smtp = new SmtpClient(smtpClientHost, smtpPort))
            {
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(smtpUserName, smtpPassword);
                smtp.EnableSsl = enableSSL;
                smtp.Send(mail);
            }
        }
        public bool Validate()
        {
            if (ddlDepartment.Items.Count == 0 || ddlDoctor.Items.Count == 0 || ddlHospital.Items.Count == 0 || string.IsNullOrEmpty(txtDate.Text) || string.IsNullOrEmpty(txtTime.Text))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("PatientDashboard.aspx", false);
        }
    }
}