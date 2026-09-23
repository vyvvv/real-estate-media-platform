using System.Net;
using System.Net.Mail;

namespace RealEstateMediaPlatform.API.Services.EmailSenderServices
{
    public class EmailSenderService: IEmailSenderService
    {
        
        private readonly IConfiguration _configuration;
        public EmailSenderService(IConfiguration configuration) { 
        
            _configuration = configuration;
        
        }


        public async Task<bool> SendEmailAsync(string receiver, string title, string body)
        {

            try
            {
                //Create email message
                var mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(_configuration["EmailSettings:SenderEmail"], _configuration["EmailSettings:SenderName"]);
                mailMessage.To.Add(receiver);
                mailMessage.Subject = title;
                mailMessage.Body = body;
                mailMessage.IsBodyHtml = false;

                //Configure SMTP client

                var smtpClient = new SmtpClient("smtp.gmail.com",587);
                smtpClient.Credentials = new NetworkCredential(_configuration["EmailSettings:SenderEmail"],
                    _configuration["EmailSettings:SenderPassword"]);
                smtpClient.EnableSsl = true;
                await smtpClient.SendMailAsync(mailMessage);
                return true;

            }
            catch(Exception ex) 
            {
                Console.WriteLine($"Email send failed. Error: {ex.Message}");
                return false;


            }
            
        }



    }
}
