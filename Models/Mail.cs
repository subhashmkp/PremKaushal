using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Configuration;
using Microsoft.Ajax.Utilities;

namespace PremKaushal.Models
{
    public static class Mail
    {
        public static void sendAlert(string subject, string body)
        {
            const string fromAddr = "info@premkaushal.com";
            const string toAddr = "alert@premkaushal.com";
            const string ccAddr = "subhash.gzb@gmail.com";
            const string ccAddr2 = "vikas.mkp@gmail.com";
            string fromPwd = WebConfigurationManager.AppSettings["InfoPwd"];
            try
            {
                var smtp = new System.Net.Mail.SmtpClient();
                {
                    smtp.Host = "smtpout.asia.secureserver.net";
                    smtp.Port = 25;
                    smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                    smtp.Credentials = new NetworkCredential(fromAddr, fromPwd);
                    smtp.Timeout = 2000;
                }
                var msg = new MailMessage(new MailAddress(fromAddr, "Prem Kaushal Info"), new MailAddress(toAddr));
                //msg.To.Add(new MailAddress(toAddr));
                msg.CC.Add(ccAddr);
                msg.CC.Add(ccAddr2);
                msg.Subject = subject;
                msg.Body = body;
                msg.IsBodyHtml = true;
                smtp.Send(msg);
            }
            catch (Exception ex)
            {
                using (PremKaushalEntities pEntities = new PremKaushalEntities())
                {
                    pEntities.sp_insertLog("Error",
                        "sendAlert: " + ex.Message + ", " + ex.InnerException + ", " + ex.StackTrace);
                }
            }
        }

        public static void sendInfo(string toAddr, string subject, string body)
        {
            const string fromAddr = "info@premkaushal.com";
            //const string toAddr = "alert@premkaushal.com";
            const string bccAddr = "subhash.gzb@gmail.com";
            const string bccAddr2 = "vikas.mkp@gmail.com";
            string fromPwd = WebConfigurationManager.AppSettings["InfoPwd"];
            var msg = new MailMessage(new MailAddress(fromAddr, "Prem Kaushal"), new MailAddress(toAddr));
            //msg.To.Add(new MailAddress(toAddr));
            msg.Bcc.Add(bccAddr);
            msg.Bcc.Add(bccAddr2);
            msg.Subject = subject;
            msg.Body = body;
            msg.IsBodyHtml = true;
            try
            {
                using (var smtp = new SmtpClient())
                {
                    smtp.Host = "smtpout.asia.secureserver.net";
                    smtp.Port = 25;
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.EnableSsl = false;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential(fromAddr, fromPwd);
                    smtp.Timeout = 2000;
                    smtp.Send(msg);
                }
                
            }
            catch (Exception ex)
            {
                using (PremKaushalEntities pEntities = new PremKaushalEntities())
                {
                    pEntities.sp_insertLog("Error",
                        "sendInfo: " + ex.Message + ", " + ex.InnerException + ", " + ex.StackTrace);
                }
            }
        }
    }
}