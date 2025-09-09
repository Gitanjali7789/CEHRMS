using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web.UI;
using ZXing;
using ZXing.Common;
using ZXing.Presentation;
using ZXing.Rendering;
namespace CEHRMS
{
    public partial class PAT_HealthCardPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblHealthId.Text = "HealthId: " + Session["PatID"].ToString();
                lblname.Text = "Name: " + Session["UName"].ToString();
                lblDOB.Text = "DOB: " + Session["dob"].ToString();
                lblGen.Text = "Gender: " + Session["Gender"].ToString();
                lblMob.Text = "Phone: " + Session["Phone"].ToString();

                // Combine values for QR code
                string qrData = $"{lblHealthId.Text}\n{lblname.Text}\n{lblDOB.Text}\n{lblGen.Text}\n{lblMob.Text}";

                // Generate QR code
                GenerateQRCode(qrData);
            }
        }
        private void GenerateQRCode(string qrData)
        {
            var barcodeWriter = new ZXing.BarcodeWriter
            {
                Format = BarcodeFormat.QR_CODE,
                Options = new EncodingOptions
                {
                    Height = 150,
                    Width = 150,
                    Margin = 1
                },
                Renderer = new ZXing.Rendering.BitmapRenderer()
            };
            using (Bitmap bitmap = barcodeWriter.Write(qrData))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    byte[] byteImage = ms.ToArray();
                    string base64String = Convert.ToBase64String(byteImage);
                    Image1.ImageUrl = "data:image/png;base64," + base64String;
                }
            }
        }
        private void ReadQRCode()
        {
            lblHealthId.Text = "HealthId: " + Session["PatID"].ToString();
            lblname.Text = "Name: " + Session["UName"].ToString();
            lblDOB.Text = "DOB: " + Session["dob"].ToString();
            lblGen.Text = "Gender: " + Session["Gender"].ToString();
            lblMob.Text = "Phone: " + Session["Phone"].ToString();
        }
        protected void BtnLogin_Click(object sender, EventArgs e)
        {
            Response.Redirect("LoginPage.aspx", false);
        }
    }
}