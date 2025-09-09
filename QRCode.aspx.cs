using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZXing.Presentation;
using ZXing;
using ZXing.Common;
using ZXing.QrCode;
using ZXing.Rendering;


namespace CEHRMS
{
    public partial class QRCode : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           //if (!IsPostBack)
            //{
            //    // Example session data, replace with your actual session variables
            //    string healthId = Session["PatID"]?.ToString() ?? "H12345";
            //    string name = Session["UName"]?.ToString() ?? "John Doe";
            //    string dob = Session["dob"]?.ToString() ?? "01-01-2000";
            //    string gender = Session["Gender"]?.ToString() ?? "Male";
            //    string phone = Session["Phone"]?.ToString() ?? "9876543210";

            //    // Display info on page
            //    lblHealthId.Text = "Health ID: " + healthId;
            //    lblName.Text = "Name: " + name;
            //    lblDOB.Text = "DOB: " + dob;
            //    lblGender.Text = "Gender: " + gender;
            //    lblPhone.Text = "Phone: " + phone;

            //    // Generate QR code with combined info
            //    string qrText = $"Health ID: {healthId}\nName: {name}\nDOB: {dob}\nGender: {gender}\nPhone: {phone}";
            //    GenerateQRCodeImage(qrText);
            //}
        }
        protected void btnGenerate_Click(object sender, EventArgs e)
        {
            string qrText = txtInput.Text.Trim();

            if (!string.IsNullOrEmpty(qrText))
            {
                string path = Server.MapPath("~/Images/QRImage.jpg");

                var writer = new BarcodeWriter<Bitmap>
                {
                    Format = BarcodeFormat.QR_CODE,
                    Options = new QrCodeEncodingOptions
                    {
                        Height = 200,
                        Width = 200,
                        Margin = 1
                    },
                    Renderer = new BitmapRenderer()
                };

                using (Bitmap bitmap = writer.Write(qrText))
                {
                    bitmap.Save(path, ImageFormat.Jpeg);
                }

                imgQR.Visible = true;
                imgQR.ImageUrl = "~/Images/QRImage.jpg";
            }
        }
    }
}