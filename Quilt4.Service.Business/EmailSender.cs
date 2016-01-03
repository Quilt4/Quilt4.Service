using System.Net;
using System.Net.Mail;
using Quilt4.Service.Interface.Business;
using Quilt4.Service.Interface.Repository;

namespace Quilt4.Service.Business
{
    public class EmailSender : IEmailSender
    {
        private readonly ISettingBusiness _settingBusiness;
        private readonly IRepository _repository;

        public EmailSender(ISettingBusiness settingBusiness, IRepository repository)
        {
            _settingBusiness = settingBusiness;
            _repository = repository;
        }

        public void Send(string to, string subject, string body)
        {
            //TODO: Send email and write to email history log in the database

            var hostAndPort = _settingBusiness.GetSetting("SmtpHost", "smtp.domain.com:25");
            //TODO: Separate the host and port
            var host = "smtp.domain.com";
            var port = 25;

            var from = _settingBusiness.GetSetting("SmtpFromEMail", "from@domain");

            using (var smtp = new SmtpClient(host, port))
            {
                //TODO: Get smtp credentials from settings and assign here
                //smtp.Credentials.GetCredential()
                
                smtp.Send(new MailMessage(from, to, subject, body));
            }
        }
    }
}