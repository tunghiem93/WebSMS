using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CMS_Shared.Utilities
{
    public class CommonHelper
    {
        static string key { get; set; } = "A!9HHhi%XjjYY4YP2@Nob00900X";
        public static string Encrypt(string text)
        {
            using (var md5 = new MD5CryptoServiceProvider())
            {
                using (var tdes = new TripleDESCryptoServiceProvider())
                {
                    tdes.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                    tdes.Mode = CipherMode.ECB;
                    tdes.Padding = PaddingMode.PKCS7;

                    using (var transform = tdes.CreateEncryptor())
                    {
                        byte[] textBytes = UTF8Encoding.UTF8.GetBytes(text);
                        byte[] bytes = transform.TransformFinalBlock(textBytes, 0, textBytes.Length);
                        return Convert.ToBase64String(bytes, 0, bytes.Length);
                    }
                }
            }
        }

        public static string Decrypt(string cipher)
        {
            using (var md5 = new MD5CryptoServiceProvider())
            {
                using (var tdes = new TripleDESCryptoServiceProvider())
                {
                    tdes.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                    tdes.Mode = CipherMode.ECB;
                    tdes.Padding = PaddingMode.PKCS7;

                    using (var transform = tdes.CreateDecryptor())
                    {
                        byte[] cipherBytes = Convert.FromBase64String(cipher);
                        byte[] bytes = transform.TransformFinalBlock(cipherBytes, 0, cipherBytes.Length);
                        return UTF8Encoding.UTF8.GetString(bytes);
                    }
                }
            }
        }

        public static string RemoveUnicode(string text)
        {
            string[] arr1 = new string[] { "á", "à", "ả", "ã", "ạ", "â", "ấ", "ầ", "ẩ", "ẫ", "ậ", "ă", "ắ", "ằ", "ẳ", "ẵ", "ặ",
    "đ",
    "é","è","ẻ","ẽ","ẹ","ê","ế","ề","ể","ễ","ệ",
    "í","ì","ỉ","ĩ","ị",
    "ó","ò","ỏ","õ","ọ","ô","ố","ồ","ổ","ỗ","ộ","ơ","ớ","ờ","ở","ỡ","ợ",
    "ú","ù","ủ","ũ","ụ","ư","ứ","ừ","ử","ữ","ự",
    "ý","ỳ","ỷ","ỹ","ỵ",};
            string[] arr2 = new string[] { "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a",
    "d",
    "e","e","e","e","e","e","e","e","e","e","e",
    "i","i","i","i","i",
    "o","o","o","o","o","o","o","o","o","o","o","o","o","o","o","o","o",
    "u","u","u","u","u","u","u","u","u","u","u",
    "y","y","y","y","y",};
            for (int i = 0; i < arr1.Length; i++)
            {
                text = text.Replace(arr1[i], arr2[i]);
                text = text.Replace(arr1[i].ToUpper(), arr2[i].ToUpper());
            }
            return text;
        }

        public static bool SendContentMail(string EmailTo, string Content, string Name, string Subject, string imgUrl = "", string attachment = "")
        {
            bool isOk = false;
            try
            {

                string email = ConfigurationManager.AppSettings["Email"];
                string passWord = ConfigurationManager.AppSettings["Password"];
                string smtpServer = "smtp.gmail.com";
                if (email != "" && passWord != "")
                {
                    MailMessage mail = new MailMessage(email, EmailTo);
                    mail.Subject = Subject;
                    mail.Body = Content;
                    mail.IsBodyHtml = true;
                    if (!string.IsNullOrEmpty(imgUrl))
                        mail.Body = string.Format("<div><img src='{0}'/><div>", imgUrl);

                    SmtpClient client = new SmtpClient();
                    client.Port = 587;
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential(email, passWord);
                    client.Host = smtpServer;
                    client.Timeout = 10000;
                    client.EnableSsl = true;
                    if (!string.IsNullOrEmpty(attachment))
                        mail.Attachments.Add(new System.Net.Mail.Attachment(attachment));
                    client.Send(mail);
                    isOk = true;
                }
            }
            catch (Exception ex)
            {
                NSLog.Logger.Error("Send Mail Error", ex);
            }
            return isOk;
        }
        private static readonly Random GetRandom = new Random();
        public static string RandomVerifiCode()
        {
            try
            {
                lock(GetRandom)
                {
                    return GetRandom.Next(0, 9999).ToString("D4");
                }
            } catch(Exception ex)
            {
                NSLog.Logger.Error("Random Verifi Code", ex);
            }
            return "0000";
        }
    }
}
