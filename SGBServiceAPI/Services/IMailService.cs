using System.Threading.Tasks;

namespace WSIServiceAPI.Services
{
    public interface IMailService
    {
        Task SendDocumentUploadEmail(string UserName, string ToEmail);
        Task SendOTPEmail(string UserName, string Pass);
        Task SendGenericEmail(string ToAddress, string Body, string Subject);
        Task SendPasswordComfirmationEmail(string UserName, string ToAddr, string Message);
        Task SendEmail(string hostname, int Id, string Password);
        Task DocumentEmail(string UserName);
        //Task<string> SendEmail(string toMail, string firstname, string persal);
        Task SendResetEmailAsync(string UserName,  string ToEmail, string Pass, string Id);
        Task SendEmailBatchAsync(MimeKit.InternetAddressList Addresses, string Subject, string Body, string DocumentName);
        Task SendGenericEmailBatchAsync(MimeKit.InternetAddressList Addresses, string Subject, string Body);

    }

}