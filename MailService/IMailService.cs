namespace HKBlog.MailService
{
    public interface IMailService
    {
        Task<bool> Send(string from, string to, string subject, string body);
    }
}
