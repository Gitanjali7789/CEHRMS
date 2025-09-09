using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Net.Mail;
using System.Security.Policy;
using System.Drawing;
using System.Drawing.Imaging;
using ZXing;
using Microsoft.Ajax.Utilities;
using System.Xml.Linq;
using System.Text.RegularExpressions;
using System.Configuration;
using System.Net;

namespace CEHRMS
{
    public partial class RegistrationPage : System.Web.UI.Page
    {
        //SqlConnection conn = new SqlConnection(
        //    @"Data Source=lol7789\SQLEXPRESS; Initial Catalog=CEHRMS; Integrated Security=True");
        SqlConnection conn = new SqlConnection(@"Server=localhost\SQLEXPRESS; Database=CEHRMS; Trusted_Connection=True;");

        DataTable dt;
        SqlDataAdapter sda;
        SqlCommand cmd;
        protected void Page_Load(object sender, EventArgs e)
        {

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
        protected void BtnPatientRegister_Click(object sender, EventArgs e)
        {
            try
            {
                bool f = ValidateName(txtFName.Text.ToString());
                bool l = ValidateName(txtLName.Text.ToString());
                bool adhaar = ValidateAdhaar(txtAdhaarno.Text.ToString());
                bool BoolEM = ValidateEmail(txtMail.Text.ToString());
                bool BoolNO = ValidateNo(txtMob.Text.ToString());
                if (!(f || l))
                {
                    lblMsg.Text = "Please Enter Valid Name";
                    lblMsg.ForeColor = Color.Red;
                    return;
                }
                if (!adhaar)
                {
                    lblMsg.Text = "Please Enter Valid Adhaar Number";
                    lblMsg.ForeColor = Color.Red;
                    return;
                }
                if (!BoolNO)
                {
                    lblMsg.Text = "PLease Enter Valid Phone number";
                    lblMsg.ForeColor = Color.Red;
                    return;
                }
                if (!(ValidateEmail(txtMail.Text.ToString())))
                {
                    lblMsg.Text = "Please enter valid Email";
                    lblMsg.ForeColor = Color.Red;
                    return;
                }
                if ((txtAdd.Text.ToString()).IsNullOrWhiteSpace())
                {
                    lblMsg.Text = "Please Enter valid Address";
                    lblMsg.ForeColor = Color.Red;
                    return;
                }
                if (!(f && l && adhaar && BoolEM && BoolNO))
                {
                    lblMsg.Text = " Please Enter correct credentials";
                    lblMsg.ForeColor = Color.Red;
                    return;
                }
                bool DVC = DuplicateValueCheck();
                if (!DVC)
                {
                    lblMsg.Text = "User already existing ";
                    lblMsg.ForeColor = Color.Red;
                    return;
                }
                String PatientName = txtFName.Text.ToString();
                String Adhaarno = txtAdhaarno.Text.ToString();
                String PatientId = PatientName.Substring(0, 2) + Adhaarno.Substring(Adhaarno.Length - 4);
                Session["PatID"] = PatientId;
                String UserName = txtFName.Text.ToString() + " " + txtLName.Text.ToString();
                Session["UName"] = UserName;
                String photo = FileUpload1.FileName;
                FileUpload1.PostedFile.SaveAs(Server.MapPath("~/UserPhoto1/" + photo));
                String image = "../UserPhoto/" + photo;
                SqlCommand cmd = new SqlCommand("StrPrcdREGISTRATION", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Photo", image);
                cmd.Parameters.AddWithValue("@PATID", PatientId);
                cmd.Parameters.AddWithValue("@USERN", UserName);
                cmd.Parameters.AddWithValue("@FNAME", txtFName.Text.ToString());
                cmd.Parameters.AddWithValue("@LNAME", txtLName.Text.ToString());
                cmd.Parameters.AddWithValue("@DOB", txtDob.Text.ToString());
                cmd.Parameters.AddWithValue("@TXTGEN", ddlGen.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@TXTBL", ddlBl.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@TXTADHAARNO", txtAdhaarno.Text.ToString());
                cmd.Parameters.AddWithValue("@TXTMAIL", txtMail.Text.ToString());
                cmd.Parameters.AddWithValue("@TXTMOB", txtMob.Text.ToString());
                cmd.Parameters.AddWithValue("@TXTADD", txtAdd.Text.ToString());
                cmd.Parameters.AddWithValue("@TXTPASS", Encrypt(txtPass.Text.ToString()));
                cmd.Parameters.AddWithValue("@Stat", "1");
                conn.Open();
                int res = cmd.ExecuteNonQuery();
                if (res == 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "alert", "alert('Your Data is not Saved!!!');", true);
                    lblMsg.Text = "Your Data is not Saved!!!";
                    lblMsg.ForeColor = Color.Red;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "alert", "alert('Registration Successful!!!');", true);
                    lblMsg.Text = "Registered Successfull";
                    lblMsg.ForeColor = Color.Green;
                }
                conn.Close();
                String msgBody = "Dear " + txtFName.Text.ToString() + " Your HealthID is: " + PatientId + " User Name is: " + UserName + " & Password is: " + txtPass.Text.ToString();
                SendSuccess(msgBody);
                GenerateHealthCard();
                lblMsg.Text = "Registration done! <a href='LoginPage.aspx' style='color:blue; text-decoration:underline;'>Login?</a>";
                //do not allow same user(adhaar no or email or mobile)
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
                lblMsg.Text = ex.Message;
            }
        }
        private void SendSuccess(string msgBody)
        {
            string smtpUserName = ConfigurationManager.AppSettings["smtpUserName"];
            string smtpPassword = ConfigurationManager.AppSettings["smtpPassword"];
            string smtpClientHost = ConfigurationManager.AppSettings["smtpClient"];
            int smtpPort = Convert.ToInt32(ConfigurationManager.AppSettings["smtpPort"]);
            bool enableSSL = Convert.ToBoolean(ConfigurationManager.AppSettings["enableSSL"]);
            string toEmail = txtMail.Text.ToString();

            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(smtpUserName);
            mail.To.Add(toEmail);
            mail.Subject = "CEHRMS:Registration successfull";
            mail.Body = msgBody;

            using (SmtpClient smtp = new SmtpClient(smtpClientHost, smtpPort))
            {
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(smtpUserName, smtpPassword);
                smtp.EnableSsl = enableSSL;
                smtp.Send(mail);
            }
        }

        public void GenerateHealthCard()
        {
            Session["User"] = $"Name:{Session["UName"].ToString()}, HealthId:{Session["PatID"].ToString()}, Date Of Birth:{txtDob.Text.ToString()}, Gender:{ddlGen.SelectedValue.ToString()}, Phone:{txtMob.Text.ToString()}";
            Session["dob"] = txtDob.Text.ToString();
            Session["Gender"] = ddlGen.SelectedValue.ToString();
            Session["Phone"] = txtMob.Text.ToString();
            Response.Redirect("PAT_HealthCardPage.aspx", false);

        }
        public bool ValidateName(String name)
        {
            if (name.Any(char.IsDigit) || name.IsNullOrWhiteSpace())
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public bool ValidateEmail(String em)
        {
            Regex rex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match mtc = rex.Match(em);
            bool check = em.IsNullOrWhiteSpace();
            bool check2 = mtc.Success;
            if (mtc.Success)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool ValidateNo(String num)
        {
            if (num.Any(char.IsLetter) || num.Length != 10 || num.IsNullOrWhiteSpace())
            {
                return false;
            }
            else
            {
                return true;
            }

        }
        public bool ValidateAdhaar(String ad)
        {
            return ad.Length == 12 && !ad.Any(char.IsLetter) && !string.IsNullOrWhiteSpace(ad);
        }
        public bool DuplicateValueCheck()
        {
            conn.Open();
            sda = new SqlDataAdapter();
            dt = new DataTable();
            string query = "select * from Patients where Adharnumber='" + txtAdhaarno.Text.ToString() + "' or email='" + txtMail.Text.ToString() + "' or mobile='" + txtMob.Text.ToString() + "'";
            cmd = new SqlCommand(query, conn);
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;
            sda.SelectCommand = cmd;
            sda.Fill(dt);
            cmd.ExecuteNonQuery();
            conn.Close();
            if (dt.Rows.Count > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}