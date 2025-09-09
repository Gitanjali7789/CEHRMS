using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CEHRMS
{
    public partial class Mail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSendMail_Click(object sender, EventArgs e)
        {
            try
            {
                string smtpUserName = ConfigurationManager.AppSettings["smtpUserName"];
                string smtpPassword = ConfigurationManager.AppSettings["smtpPassword"];
                string smtpClientHost = ConfigurationManager.AppSettings["smtpClient"];
                int smtpPort = Convert.ToInt32(ConfigurationManager.AppSettings["smtpPort"]);
                bool enableSSL = Convert.ToBoolean(ConfigurationManager.AppSettings["enableSSL"]);
                string toEmail = "alishadix00@gmail.com";

                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(smtpUserName);
                mail.To.Add(toEmail);
                mail.Subject = "Test Email from CEHRMS";
                mail.Body = "This is a test email sent from ASP.NET using Gmail SMTP App Password.";

                using (SmtpClient smtp = new SmtpClient(smtpClientHost, smtpPort))
                {
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential(smtpUserName, smtpPassword);
                    smtp.EnableSsl = enableSSL;
                    smtp.Send(mail);
                }

                lblMessage.Text = "Email sent successfully!";
                lblMessage.ForeColor = System.Drawing.Color.Green;
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error sending email: " + ex.Message;
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }
    }
}