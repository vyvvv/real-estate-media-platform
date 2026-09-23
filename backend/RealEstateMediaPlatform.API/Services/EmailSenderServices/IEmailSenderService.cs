namespace RealEstateMediaPlatform.API.Services.EmailSenderServices
{
    public interface IEmailSenderService
    {
        Task<bool> SendEmailAsync(string receiver,string title,string body);
      
    }
}
