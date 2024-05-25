using System;
using System.Net;
using System.Net.Mail;

namespace Fireplace.AppDataFolder.ClassFolder
{
    public class EmailClass
    {
        public class EmailInfoClass_EC
        {

            /// <summary>
            /// Информация об почте для работы sms
            /// </summary>
            public static readonly string loginMail_EIC = "orangeblood@rambler.ru";
            public static readonly string passwordMail_EIC = "OrangeBlood123";
            public static readonly string sMTP_EIC = "smtp.rambler.ru";
            public static readonly string fromWhom_EIC = "Автоматизированная информационная система Fireplace";

            public static readonly string toSend_EIC =
                "<div style=\"position: relative; width:1120px; height: 630px; border:1px solid #fff; background-color: #fff;\">\r\n" +
                "<div style=\"border-radius: 10px; position: relative; width: 780px; left: 50%; right: 50%; top: 50%; bottom: 50%; height: 340px; transform: translate(-50%, -50%); background-color: #232324;\">\r\n" +
                "<p style=\"color: #e1e3e6; font-size: 40px; text-align: center; font-weight:bold; padding-top: 20px;\">СБРОС ПАРОЛЯ</p>\r\n" +
                "<p style=\"color: #e1e3e6; font-size: 20px; padding-top: 10px; padding-left: 10px; padding-right: 10px;\">Здравствуйте " + "! Вы запросили сброс пароля для своей учётной записи в 'Cool Bible Library'</p>\r\n" +
                "<p style=\"color: #529ef4; font-size: 40px; padding-top: 15px; padding-left: 50px; font-weight:bold;\">Код для сброса пароля: " + "</p>\r\n" +
                "<p style=\"color: #e1e3e6; font-size: 15px; padding-top: 15px; padding-left: 10px; padding-right: 10px;\">Если вы этого не делали, обратитесь в службу поддержки 8 (916)-178-23-43. С уважением Cool Bible Library</p>\r\n" +
                "</div>\r\n" +
                "</div>";
        }

        public class EnterEmail_EC // Код отправки сообщений
        {
            public static readonly string topicLetters_EE = "Тестовое сообщение";

            public void SendEmail()
            {
                using (SmtpClient mySmtpClient = new SmtpClient(EmailInfoClass_EC.sMTP_EIC))
                {
                    mySmtpClient.UseDefaultCredentials = false;
                    NetworkCredential basicAuthenticationInfo = new NetworkCredential(EmailInfoClass_EC.loginMail_EIC, EmailInfoClass_EC.passwordMail_EIC);
                    mySmtpClient.Credentials = basicAuthenticationInfo;

                    MailAddress from = new MailAddress(EmailInfoClass_EC.loginMail_EIC, EmailInfoClass_EC.fromWhom_EIC);
                    MailAddress to = new MailAddress("vlad.banana.syca@yandex.ru");
                    MailMessage myMail = new MailMessage(from, to)
                    {
                        ReplyToList = { new MailAddress(EmailInfoClass_EC.loginMail_EIC) },
                        IsBodyHtml = true,
                        Subject = topicLetters_EE,
                        Body = EmailInfoClass_EC.toSend_EIC,
                        SubjectEncoding = System.Text.Encoding.UTF8,
                        BodyEncoding = System.Text.Encoding.UTF8
                    };

                    mySmtpClient.Send(myMail);
                }
            }
        }
    }
}
