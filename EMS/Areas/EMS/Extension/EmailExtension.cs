using EMS.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace EMS.Areas.EMS.Extension
{
    public class EmailExtension
    {
    
        private static readonly EMSEntities db = new EMSEntities();
        public static void SendNotificationEmail(Customer receiver, string prefix)
        {
            
            try
            {
                var module = db.Modules.First();
                StreamReader str = new StreamReader(System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/EmailTemplate/Event.html"));
                string MailText = str.ReadToEnd();
                str.Close();
                MailText = NotificationTemplate(MailText,module, receiver,prefix);
                if (!MailText.Equals(""))
                {
                   

                    var senderEmail = new MailAddress("system.notification@haphan.com", "HAPHAN");
                    if (!receiver.Equals(""))
                    {
                        var receiverEmail = new MailAddress(receiver.Email);
                        var password = "=o7lPXT#5y($";
                        string subject = "LỜI MỜI TRÂN TRỌNG NHẤT";


                        var smtp = new SmtpClient
                        {
                            Host = "mail.haphan.com",
                            Port = 25,
                            EnableSsl = true,
                            DeliveryMethod = SmtpDeliveryMethod.Network,
                            UseDefaultCredentials = false,
                            Credentials = new NetworkCredential(senderEmail.Address, password)
                        };
                        //var smtp = new SmtpClient
                        //{
                        //    Host = "smtp.gmail.com",
                        //    Port = 587,
                        //    EnableSsl = true,
                        //    DeliveryMethod = SmtpDeliveryMethod.Network,
                        //    UseDefaultCredentials = false,
                        //    Credentials = new NetworkCredential(senderEmail.Address, password)
                        //};


                        using (var mess = new MailMessage(senderEmail, receiverEmail)
                        {
                            IsBodyHtml = true,
                            Subject = subject,
                            Body = MailText
                        })
                        {
                            smtp.Send(mess);
                        }
                    }
                }



            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }
      
        public static void SendQREmail(Customer receiver, string src,string prefix)
        {
            
            try
            {
                var module = db.Modules.First();
                StreamReader str = new StreamReader(System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/EmailTemplate/QR.html"));
                string MailText = str.ReadToEnd();
                str.Close();
                string urlIMG = SaveImage(src, receiver.Id);
                urlIMG = prefix + urlIMG;
                MailText = QRTemplate(MailText, urlIMG);
                if (!MailText.Equals(""))
                {
                    var senderEmail = new MailAddress("system.notification@haphan.com", "HAPHAN");
                    if (!receiver.Equals(""))
                    {
                        //var receiverEmail = new MailAddress(receiver.Email);
                        var receiverEmail = new MailAddress(receiver.Email);
                        var password = "=o7lPXT#5y($";
                        string subject = "MÃ VÀO CỔNG";


                        var smtp = new SmtpClient
                        {
                            Host = "mail.haphan.com",
                            Port = 25,
                            EnableSsl = true,
                            DeliveryMethod = SmtpDeliveryMethod.Network,
                            UseDefaultCredentials = false,
                            Credentials = new NetworkCredential(senderEmail.Address, password)
                        };


                        using (var mess = new MailMessage(senderEmail, receiverEmail)
                        {
                            IsBodyHtml = true,
                            Subject = subject,
                            Body = MailText,


                        })
                        {
                            smtp.Send(mess);
                        }
                    }
                }



            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }

        public static string SaveImage(string ImgStr, string id)
        {
            ImgStr = ImgStr.Trim().Substring(22);

            //this is a simple white background image
            String path = HttpContext.Current.Server.MapPath("~/Images/"); //Path

            //Check if directory exist
            if (!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path); //Create directory if it doesn't exist
            }

            string imageName = id + ".jpg";

            //set the image path
            string imgPath = Path.Combine(path, imageName);

            byte[] imageBytes = Convert.FromBase64String(ImgStr);

            File.WriteAllBytes(imgPath, imageBytes);
            return "/Images/"+imageName;
        }
        private static string NotificationTemplate(string mailText,Module module, Customer customer,string prefix)
        {
            //https://localhost:44344/CustomerQR/Index?ID=5120235121557

            mailText = mailText.Replace("[EventName]", module.Des.Trim());
            mailText = mailText.Replace("[Invitation]", module.Invitation.Trim());
            mailText = mailText.Replace("[Content]", module.Content.Trim());
            mailText = mailText.Replace("[AccessLink]", prefix+ "/CustomerQR/Index?ID="+customer.Id);
            mailText = mailText.Replace("[Logo]", module.Logo.Trim());
            mailText = mailText.Replace("[PhotoLink]", module.Image.Trim());
            mailText = mailText.Replace("[CUSTOMERNAME]", customer.Name.Trim());
            return mailText;
        }
        private static string QRTemplate(string mailText, string src)
        {

            
            mailText = mailText.Replace("[qr]", src);
          
           
            return mailText;
        }

    }
}