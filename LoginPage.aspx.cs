using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CEHRMS
{
    public partial class LoginPage : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(
            @"Data Source=lol7789\SQLEXPRESS; Initial Catalog=CEHRMS; Integrated Security=True");
        DataTable dt;
        SqlDataAdapter sda;
        SqlCommand cmd;
        static int count = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["count1"] != null)
            {
                string a = Session["count1"].ToString();
                count = int.Parse(a);
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime mydt = DateTime.Now;
                string dat = mydt.ToString("yyyy-MM-dd HH:mm:ss.fff");
                string zero = "0";
                if (ddlType.SelectedValue == "1")//UserType is Patient
                {
                    if (!(CheckStatus()))
                    {
                        return;
                    }
                    conn.Open();
                    sda = new SqlDataAdapter();
                    dt = new DataTable();
                    string query = "select * from Patients where PatientId='" + txtUser.Text.ToString() + "' and Password='" + Encrypt(TxtPass.Text.Trim()) + "'";
                    cmd = new SqlCommand(query, conn);
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;
                    sda.SelectCommand = cmd;
                    sda.Fill(dt);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    //string dat = DateTime.Now.ToString();
                    if (dt.Rows.Count > 0)
                    {
                        if ((dt.Rows[0]["Status"].ToString()) == zero)
                        {
                            lblMsg.Text = "Account Inactive!!! Contact Admin for activation process and further detail";
                            lblMsg.ForeColor = Color.Red;
                            return;
                        }
                        conn.Open();
                        string query1 = "update Patients set Status='1',LastLogin='" + dat + "' where patientId = '" + dt.Rows[0]["PatientId"].ToString() + "'";
                        cmd = new SqlCommand(query1, conn);
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = conn;
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        Session["PatId"] = dt.Rows[0]["PatientId"].ToString();
                        String ab = dt.Rows[0]["PatientId"].ToString();
                        Session["PatFname"] = dt.Rows[0]["FirstName"].ToString();
                        Session["photo"] = dt.Rows[0]["Photo"].ToString();
                        Session["Email"] = dt.Rows[0]["email"].ToString();
                        Response.Redirect("PAT_Dashboard.aspx", false);
                    }
                    else
                    {
                        InvalidLogin();
                    }
                }
                if (ddlType.SelectedValue == "2")
                {
                    conn.Open();
                    sda = new SqlDataAdapter();
                    dt = new DataTable();
                    string query = "select * from EMP_DETAILS where EmployeeId='" + txtUser.Text.ToString() + "' and Password='" + TxtPass.Text.ToString() + "'";
                    cmd = new SqlCommand(query, conn);
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;
                    sda.SelectCommand = cmd;
                    sda.Fill(dt);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    if (dt.Rows.Count > 0)
                    {
                        if ((dt.Rows[0]["ActiveStatus"].ToString()) == zero)
                        {
                            lblMsg.Text = "Account Inactive!!! Contact Admin for activation process and further detail";
                            lblMsg.ForeColor = Color.Red;
                            return;
                        }
                        Session["DocId"] = dt.Rows[0]["EmployeeId"].ToString();
                        Session["DocName"] = dt.Rows[0]["EMPLOYEE_NAME"].ToString();
                        Session["HosID"] = dt.Rows[0]["Hospital_Id"].ToString();
                        Response.Redirect("DOC_Dashboard.aspx", false);
                    }
                }
                if (ddlType.SelectedValue == "3")
                {
                    conn.Open();
                    sda = new SqlDataAdapter();
                    dt = new DataTable();
                    string query = "select * from EMP_DETAILS where EmployeeId='" + txtUser.Text.ToString() + "' and Password='" + TxtPass.Text.ToString() + "'";
                    cmd = new SqlCommand(query, conn);
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;
                    sda.SelectCommand = cmd;
                    sda.Fill(dt);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    if (dt.Rows.Count > 0)
                    {
                        if ((dt.Rows[0]["ActiveStatus"].ToString()) == zero)
                        {
                            lblMsg.Text = "Account Inactive!!! Contact Admin for activation process and further detail";
                            lblMsg.ForeColor = Color.Red;
                            return;
                        }
                        Session["EmpId"] = dt.Rows[0]["EmployeeId"].ToString();
                        Session["EmpName"] = dt.Rows[0]["EMPLOYEE_NAME"].ToString();
                        Session["HospID"] = dt.Rows[0]["Hospital_Id"].ToString();
                        Response.Redirect("REC_Dashboard.aspx", false);
                    }
                }
                if (ddlType.SelectedValue == "4")
                {
                    Response.Redirect("ADM_Dashboard.aspx");
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }
        public void InvalidLogin()
        {
            try
            {
                int attempt;
                attempt = 2 - count;
                lblMsg.Text = "Invalid Credentials! " + attempt.ToString() + " chances Remaining";
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('invalid');", true);
                count = count + 1;
                Session["count1"] = count.ToString();
                //btnLogin_Click(sender, new EventArgs());
                if (count == 3)
                {
                    conn.Open();
                    string query1 = "update Patients set Status='0' where patientId = '" + txtUser.Text.ToString() + "'";
                    cmd = new SqlCommand(query1, conn);
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    TxtPass.Visible = false;
                    btnLogin.Visible = false;
                    lblMsg.Text = "Your account has been blocked! contact the admin for further details";
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }
        public bool CheckStatus()
        {
            conn.Open();
            sda = new SqlDataAdapter();
            dt = new DataTable();
            string query = "select * from Patients where PatientId='" + txtUser.Text.ToString() + "'";
            cmd = new SqlCommand(query, conn);
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;
            sda.SelectCommand = cmd;
            sda.Fill(dt);
            cmd.ExecuteNonQuery();
            conn.Close();
            string ab = dt.Rows[0]["Status"].ToString();
            string cd = "0";
            if (ab == cd)
            {
                txtUser.Enabled = false;
                TxtPass.Enabled = false;
                btnLogin.Enabled = false;
                lblMsg.Text = "Your Account has been blocked! Contact Admin ";
                return false;
            }
            else
            {
                return true;

            }
        }
        private string Encrypt(string clearText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }
    }
}