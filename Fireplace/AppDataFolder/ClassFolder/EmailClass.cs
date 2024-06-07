using System.Net;
using System.Net.Mail;

namespace Fireplace.AppDataFolder.ClassFolder
{
    public class EmailClass
    {
        public class EmailInfoClass_EC
        {
            // Информация об почте для работы sms
            public static readonly string loginMail_EIC = "orangeblood@rambler.ru";
            public static readonly string passwordMail_EIC = "OrangeBlood123";
            public static readonly string sMTP_EIC = "smtp.rambler.ru";
            public static readonly string fromWhom_EIC = "Автоматизированная информационная система Fireplace";
        }

        public class EnterEmail_EC // Код отправки сообщений
        {
            public string topicLetters_EE { get; set; }
            public string emailUser_EE { get; set; }
            public string toSend_EE { get; set; }

            public void SendEmail()
            {
                using (SmtpClient mySmtpClient = new SmtpClient(EmailInfoClass_EC.sMTP_EIC))
                {
                    mySmtpClient.UseDefaultCredentials = false;
                    NetworkCredential basicAuthenticationInfo = new NetworkCredential(EmailInfoClass_EC.loginMail_EIC, EmailInfoClass_EC.passwordMail_EIC);
                    mySmtpClient.Credentials = basicAuthenticationInfo;

                    MailAddress from = new MailAddress(EmailInfoClass_EC.loginMail_EIC, EmailInfoClass_EC.fromWhom_EIC);
                    MailAddress to = new MailAddress(emailUser_EE);
                    MailMessage myMail = new MailMessage(from, to)
                    {
                        ReplyToList = { new MailAddress(EmailInfoClass_EC.loginMail_EIC) },
                        IsBodyHtml = true,
                        Subject = topicLetters_EE,
                        Body = toSend_EE,
                        SubjectEncoding = System.Text.Encoding.UTF8,
                        BodyEncoding = System.Text.Encoding.UTF8
                    };

                    mySmtpClient.Send(myMail);
                }
            }
        }

        public class EnterRandomeCodeEmail_EC // Для отправки кода сброса пароля
        {
            public string ressetCode_ERCE;
            public string toSend_ERCE;

            public void SendPasswordResetEmail(string userEmail)
            {
                ressetCode_ERCE = RandomeCodeClass.GenerateRandomCode(10);
                toSend_ERCE =
                "<div style=\"position: relative; width:1120px; height: 630px; border:1px solid #fff; background-color: #fff; padding: 20px;\">\r\n" +
                    "<div style=\"border-radius: 10px; position: relative; width: 750px; left: 50%; right: 50%; top: 50%; bottom: 50%; height: 540px; transform: translate(-50%, -50%); background-color: #565470; padding: 20px; box-shadow: 0px 0px 10px 0px rgba(0,0,0,0.75);\">\r\n" +
                    "<p style=\"color: #b4c2ed; font-size: 40px; text-align: center; font-weight:bold; padding-top: 20px; text-shadow: 2px 2px 2px rgba(0, 0, 0, 0.5);\">СБРОС ПАРОЛЯ</p>\r\n            <p style=\"color: #e1e3e6; font-size: 20px; padding-top: 10px; padding-left: 10px; padding-right: 10px; text-shadow: 1px 1px 1px rgba(0, 0, 0, 0.5);\">Здравствуйте! Вы запросили сброс пароля для своей учётной записи в 'Fireplace'</p>\r\n" +
                    "<p style=\"color: #e04747; font-size: 20px; padding-top: 10px; padding-left: 10px; padding-right: 10px; text-shadow: 1px 1px 1px rgba(0, 0, 0, 0.5);\">ЕСЛИ ЭТО НЕ ВЫ, просто проигнорируйте это письмо и сообщите нашему администратору о случившемся</p>\r\n" +
                    "<div>\r\n" +
                        "<p style=\"color: #e1e3e6; font-size: 40px; padding-top: 15px; padding-left: 50px; font-weight:bold; text-shadow: 1px 1px 1px rgba(0, 0, 0, 0.5);\">Код для сброса пароля: <span style=\"color: #3158ce;\">" + ressetCode_ERCE + "</span></p>\r\n" +
                    "</div>\r\n" +
                    " <p style=\"color: #e1e3e6; font-size: 14px; padding-top: -5px; padding-left: 2px; padding-right: 10px; text-shadow: 1px 1px 1px rgba(0, 0, 0, 0.5);\">Наши горячие контакты:</p>\r\n" +
                    "<p style=\"color: #e1e3e6; font-size: 14px; padding-top: -5px; padding-left: 2px; padding-right: 10px; text-shadow: 1px 1px 1px rgba(0, 0, 0, 0.5);\">+7 968 627 33 95</p>\r\n" +
                    "<p style=\"color: #e1e3e6; font-size: 14px; padding-top: -5px; padding-left: 2px; padding-right: 10px; text-shadow: 1px 1px 1px rgba(0, 0, 0, 0.5);\">https://t.me/FireplaceProHelp</p>\r\n" +
                    "</div>\r\n" +
                "</div>";

                EnterEmail_EC emailSender = new EnterEmail_EC
                {
                    topicLetters_EE = "Сброс пароля",
                    emailUser_EE = userEmail,
                    toSend_EE = toSend_ERCE
                };

                emailSender.SendEmail();
            }
        }
    }
}
