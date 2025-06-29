using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RestSharp;
using RestSharp.Authenticators;
//using System;
//using System.Threading.Tasks;

namespace Kino.Services.Mail
{
    public class MailService(IConfiguration configuration, ILogger<MailService> logger) : IMailService
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly ILogger<MailService> _logger = logger;

        public async Task<bool> SendEmailAsync(string email, string subject, string content)
        {
            try
            {
                var domain = _configuration["Mailgun:Domain"];
                var apiKey = _configuration["Mailgun:ApiKey"];
                var from = _configuration["Mailgun:From"];

                // Ensure apiKey is not null or empty to avoid null reference issues
                if (string.IsNullOrWhiteSpace(apiKey))
                {
                    _logger.LogError("Mailgun API key is not configured.");
                    return false;
                }

                var client = new RestClient(new RestClientOptions("https://api.mailgun.net/v3")
                {
                    Authenticator = new HttpBasicAuthenticator("api", password: apiKey)
                });

                var request = new RestRequest($"{domain}/messages", Method.Post);
                request.AddParameter("from", from);
                request.AddParameter("to", email);
                request.AddParameter("subject", subject);
                request.AddParameter("html", content);

                var response = await client.ExecuteAsync(request);

                if (response.IsSuccessful)
                {
                    _logger.LogInformation("Mail sent successfully to {Email}.", email);
                    return true;
                }
                else
                {
                    _logger.LogError("Mail sending failed with status code {StatusCode} and content {Content}.", response.StatusCode, response.Content);
                    return false;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An exception occurred while sending email.");
                return false;
            }
        }
    }
}