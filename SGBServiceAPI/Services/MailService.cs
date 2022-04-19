using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using WSIServiceAPI.Models;
using System.IO;
using System.Threading.Tasks;


namespace WSIServiceAPI.Services
{
    public class MailService : IMailService
    {
        private readonly MailSettings _mailSettings;
        private readonly IDapper _dapper;
        // public stringtoEmail
        public MailService(IOptions<MailSettings> mailSettings, IDapper dapper)
        {
            _mailSettings = mailSettings.Value;
            _dapper = dapper;
        }



        //public async Task<bool> SendEmailToMany(string Addresses, string Subject, string Body)
        //{
        //    using (System.Net.Mail.SmtpClient smtpClient = new System.Net.Mail.SmtpClient())
        //    {
        //        var basicCredential = new System.Net.NetworkCredential(_mailSettings.Mail, _mailSettings.Password);
        //        using (System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage())
        //        {
        //            System.Net.Mail.MailAddress fromAddress = new System.Net.Mail.MailAddress(_mailSettings.Mail, _mailSettings.DisplayName);

        //            smtpClient.Host = _mailSettings.Host;
        //            smtpClient.UseDefaultCredentials = false;
        //            smtpClient.Credentials = basicCredential;

        //            message.From = fromAddress;
        //            message.Subject = Subject;

        //            message.IsBodyHtml = true;
        //            message.Body = Body;
        //            message.To.Add(Addresses);

        //            try
        //            {
        //                smtpClient.Port = _mailSettings.Port;
        //                smtpClient.EnableSsl = true;
        //                smtpClient.Send(message);
        //            }
        //            catch (Exception ex)
        //            {
        //                //Error, could not send the message
        //                return await Task.FromResult(false);
        //            }
        //        }
        //    }

        //    return await Task.FromResult(true);

        //}


        public async Task SendOTPEmail(string UserName, string Pass)
        {
            string FilePath = Directory.GetCurrentDirectory() + "\\Templates\\PasswordresetTemplate.html";
            StreamReader str = new StreamReader(FilePath);
            string MailText = str.ReadToEnd();
            str.Close();



            var result = await Task.FromResult(_dapper.Get<UsersModel>($"SELECT * FROM [dbo].[tblUsers] WHERE [IDNumber]='{UserName}' OR [Persal]='{UserName}' OR [Passport]='{UserName}' ", null, commandType: System.Data.CommandType.Text));

            string toMail = result.Email;

            string firstname = result.FirstName;

            string lastname = result.Surname;

            string name = firstname + " " + lastname;
            MailText = MailText.Replace("[username]", name).Replace("[pass]", Pass);


            var email = new MimeMessage();

            email.Sender = MailboxAddress.Parse("aryanna.hermiston27@ethereal.email");
            email.To.Add(MailboxAddress.Parse(toMail));
            email.Subject = $"OTP Password Reset";

            var builder = new BodyBuilder();
            builder.HtmlBody = MailText;
            email.Body = builder.ToMessageBody();

            using var smtp = new SmtpClient();
            //smtp.Connect("smtp.ethereal.email", 587, SecureSocketOptions.StartTls);
            smtp.ServerCertificateValidationCallback = (s, c, h, e) => true;
            smtp.Connect("smtp.ethereal.email", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate("aryanna.hermiston27@ethereal.email", "5PsxnWgXEmV6FTQvJp");
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }


        public async Task SendPasswordComfirmationEmail(string UserName, string ToAddr, string Message)
        {
            string FilePath = Directory.GetCurrentDirectory() + "\\Templates\\ConfirmPasswordTemplate.html";
            StreamReader str = new StreamReader(FilePath);
            string MailText = str.ReadToEnd();
            str.Close();
            // MailText = MailText.Replace("[username]", UserName);

            var result = await Task.FromResult(_dapper.Get<UsersModel>($"SELECT * FROM [dbo].[tblUsers]  WHERE [IDNumber]='{UserName}' OR [Persal]='{UserName}' OR [Passport]='{UserName}'", null, commandType: System.Data.CommandType.Text));

            string toMail = result.Email;

            string firstname = result.FirstName;

            string lastname = result.Surname;

            string name = firstname + " " + lastname;
            MailText = MailText.Replace("[username]", name);

            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse("aryanna.hermiston27@ethereal.email");
            email.To.Add(MailboxAddress.Parse(toMail));
            email.Subject = $"Confirmation";

            var builder = new BodyBuilder();
            builder.HtmlBody = MailText;
            email.Body = builder.ToMessageBody();

            using var smtp = new SmtpClient();

            //smtp.Connect("smtp.ethereal.email", 587, SecureSocketOptions.StartTls);
            smtp.ServerCertificateValidationCallback = (s, c, h, e) => true;
            smtp.Connect("smtp.ethereal.email", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate("aryanna.hermiston27@ethereal.email", "5PsxnWgXEmV6FTQvJp");
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }

        //Mnqobi send email

        public async Task SendEmail(string hostname, int Id, string Password)
        {
            string FilePath = Directory.GetCurrentDirectory() + "\\Templates\\WelcomeTemplate.html";
            StreamReader str = new StreamReader(FilePath);
            string MailText = str.ReadToEnd();
            str.Close();
            // MailText = MailText.Replace("[username]", UserName);



            var result = await Task.FromResult(_dapper.Get<UsersModel>($"select * from [dbo].[tblUsers] where UserId = {Id}", null, commandType: System.Data.CommandType.Text));



            string toMail = result.Email;
            string firstname = result.FirstName;
            string lastname = result.Surname;
            string fullname= result.FirstName + " " + result.Surname;
            string Persal = result.Persal;
            string IDNumber = result.IDNumber;
            string Passport = result.Passport;
            string username = result.Username;
            string userid = Id.ToString();

            MailText = MailText.Replace("[fullname]", fullname).Replace("[username]", username).Replace("[email]", toMail).Replace("[persal]", Persal).Replace("[id]", userid).Replace("[passport]", Passport).Replace("[pass]", Password).Replace("[hostname]", hostname);



            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse("aryanna.hermiston27@ethereal.email");
            email.To.Add(MailboxAddress.Parse(toMail));
            email.Subject = $"Confirmation";



            var builder = new BodyBuilder();
            builder.HtmlBody = MailText;
            email.Body = builder.ToMessageBody();



            using var smtp = new SmtpClient();
            //smtp.Connect("smtp.ethereal.email", 587, SecureSocketOptions.StartTls);
            smtp.ServerCertificateValidationCallback = (s, c, h, e) => true;
            smtp.Connect("smtp.ethereal.email", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate("aryanna.hermiston27@ethereal.email", "5PsxnWgXEmV6FTQvJp");
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }
        //Mnqobi Documentemail

        public async Task SendDocumentUploadEmail(string UserName, string ToEmail)
        {
            string FilePath = Directory.GetCurrentDirectory() + "\\Templates\\DocumentTemplate.html";
            StreamReader str = new StreamReader(FilePath);
            string MailText = str.ReadToEnd();
            str.Close();
            MailText = MailText.Replace("[username]", UserName);
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
            email.To.Add(MailboxAddress.Parse(ToEmail));
            email.Subject = "Document uploaded successfully";
            var builder = new BodyBuilder();
            builder.HtmlBody = MailText;
            email.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();
            smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }

        public async Task SendGenericEmail(string ToAddress, string Body, string Subject)
        {
            string FilePath = Directory.GetCurrentDirectory() + "\\Templates\\GenericEmailTemplate.html";
            StreamReader str = new StreamReader(FilePath);
            string MailText = str.ReadToEnd();
            str.Close();
            MailText = MailText.Replace("[messagebody]", Body);
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
            email.To.Add(MailboxAddress.Parse(ToAddress));
            email.Subject = Subject;
            var builder = new BodyBuilder();
            builder.HtmlBody = MailText;
            email.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();
            smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }

        public async Task SendGenericEmailBatchAsync(MimeKit.InternetAddressList Addresses, string Subject, string Body)
        {
            string FilePath = Directory.GetCurrentDirectory() + "\\Templates\\GenericEmailTemplate.html";
            StreamReader str = new StreamReader(FilePath);
            string MailText = str.ReadToEnd();
            str.Close();

            MailText = MailText.Replace("[messagebody]", Body);
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
            email.To.AddRange(Addresses);
            email.Subject = Subject;
            var builder = new BodyBuilder();
            builder.HtmlBody = MailText;
            email.Body = builder.ToMessageBody();

            using var smtp = new SmtpClient();
            smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }
        //Mnqobi Documentemail
        public async Task DocumentEmail(string UserName)
        {
            string FilePath = Directory.GetCurrentDirectory() + "\\Templates\\DocumentTemplate.html";
            StreamReader str = new StreamReader(FilePath);
            string MailText = str.ReadToEnd();
            str.Close();
            // MailText = MailText.Replace("[username]", UserName);



            var result = await Task.FromResult(_dapper.Get<UsersModel>($"SELECT * FROM [dbo].[tblUsers] WHERE [UserId]='{UserName}'", null, commandType: System.Data.CommandType.Text));



            string toMail = result.Email;



            string firstname = result.FirstName;



            string lastname = result.Surname;



            string name = firstname + " " + lastname;
            MailText = MailText.Replace("[username]", name).Replace("[email]", toMail);



            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse("aryanna.hermiston27@ethereal.email");
            email.To.Add(MailboxAddress.Parse(toMail));
            email.Subject = $"Policy Document Uploaded";



            var builder = new BodyBuilder();
            builder.HtmlBody = MailText;
            email.Body = builder.ToMessageBody();



            using var smtp = new SmtpClient();
            //smtp.Connect("smtp.ethereal.email", 587, SecureSocketOptions.StartTls);
            smtp.ServerCertificateValidationCallback = (s, c, h, e) => true;
            smtp.Connect("smtp.ethereal.email", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate("aryanna.hermiston27@ethereal.email", "5PsxnWgXEmV6FTQvJp");
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }

        //Mnqobi reset email
        public async Task SendResetEmailAsync(string UserName, string ToEmail, string Pass, string Id)
        {
            string FilePath = Directory.GetCurrentDirectory() + "\\Templates\\PasswordResetTemp.html";
            StreamReader str = new StreamReader(FilePath);
            string MailText = str.ReadToEnd();
            str.Close();
            MailText = MailText.Replace("[username]", UserName).Replace("[email]", ToEmail).Replace("[pass]", Pass).Replace("[id]", Id);
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse("aryanna.hermiston27@ethereal.email");
            email.To.Add(MailboxAddress.Parse(ToEmail));
            email.Subject = "Password Reset";
            var builder = new BodyBuilder();
            builder.HtmlBody = MailText;
            email.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();
            smtp.ServerCertificateValidationCallback = (s, c, h, e) => true;
            smtp.Connect("smtp.ethereal.email", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate("aryanna.hermiston27@ethereal.email", "5PsxnWgXEmV6FTQvJp");
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
   
        }

        public Task SendPasswordComfirmationEmail(string UserName)
        {
            throw new System.NotImplementedException();
        }

        public async Task SendEmailBatchAsync(MimeKit.InternetAddressList Addresses, string Subject, string Body, string DocumentName)
        {
            string FilePath = Directory.GetCurrentDirectory() + "\\Templates\\ReceiversTemplate.html";
            StreamReader str = new StreamReader(FilePath);
            string MailText = str.ReadToEnd();
            str.Close();
            MailText = MailText.Replace("[DocumentName]", DocumentName);
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse("aryanna.hermiston27@ethereal.email");

            email.To.AddRange(Addresses);
            email.Subject = Subject;
            var builder = new BodyBuilder();
            builder.HtmlBody = MailText;
            email.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();
            smtp.Connect("smtp.ethereal.email", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate("aryanna.hermiston27@ethereal.email", "5PsxnWgXEmV6FTQvJp");
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }
 

        //public async Task<string> SendEmail(string toMail, string firstname, string persal)
        //{
        //    var Password = GetRandomPassword();
        //    string FilePath = Directory.GetCurrentDirectory() + "\\Templates\\WelcomeTemplate.html";
        //    StreamReader str = new StreamReader(FilePath);
        //    string MailText = str.ReadToEnd();
        //    str.Close();
        //    //MailText = MailText.Replace("[username]", UserName);



        //    // var result = await Task.FromResult(_dapper.Get<UsersModel>($"SELECT * FROM [dbo].[tblUsers] WHERE [IDNumber]='{UserName}' OR Persal='{UserName}' OR Passport='{UserName}'", null, commandType: System.Data.CommandType.Text));



        //    // string toMail = result.Email;



        //    // string firstname = result.FirstName;



        //    /*string lastname = result.Surname;
        //    string Password = result.Password;
        //    string Persal = result.Persal;
        //    string IDNumber = result.IDNumber;
        //    string Passport = result.Passport;



        //    string name = firstname + " " + lastname;*/

        //    MailText = MailText.Replace("[username]", firstname).Replace("[email]", toMail).Replace("[persal]", persal).Replace("[pass]", Password);



        //    var email = new MimeMessage();

        //    email.Sender = MailboxAddress.Parse("aryanna.hermiston27@ethereal.email");

        //    email.To.Add(MailboxAddress.Parse(toMail));
        //    email.Subject = $"Welcome Mail";



        //    var builder = new BodyBuilder();
        //    builder.HtmlBody = MailText;
        //    email.Body = builder.ToMessageBody();



        //    using var smtp = new SmtpClient();
        //    //smtp.Connect("smtp.ethereal.email", 587, SecureSocketOptions.StartTls);
        //    smtp.ServerCertificateValidationCallback = (s, c, h, e) => true;
        //    smtp.Connect("smtp.ethereal.email", 587, SecureSocketOptions.StartTls);
        //    smtp.Authenticate("aryanna.hermiston27@ethereal.email", "5PsxnWgXEmV6FTQvJp");
        //    await smtp.SendAsync(email);
        //    smtp.Disconnect(true);
        //    return Password;
        //}

        //public static string GetRandomPassword()
        //{

        //    var pwd = new Password(8).IncludeLowercase().IncludeUppercase().IncludeSpecial("[]{}^_=@#$%!*").IncludeNumeric();
        //    var randomPassword = pwd.Next();


        //    Console.WriteLine(randomPassword.ToString());
        //    return randomPassword.ToString();
        //}


    }
}
