namespace Quilt4.Service.Interface.Business
{
    public interface IEmailSender
    {
        void Send(string to, string subject, string body);
    }
}