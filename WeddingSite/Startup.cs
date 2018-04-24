using Microsoft.Owin;
using Microsoft.WindowsAzure.Storage;
using Owin;
using Serilog;
using Serilog.Sinks.Email;
using System;

[assembly: OwinStartupAttribute(typeof(WeddingSite.Startup))]
namespace WeddingSite
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var cloudStorage = CloudStorageAccount.Parse("");

            var log = new LoggerConfiguration()
                .WriteTo.AzureTableStorage(cloudStorage, Serilog.Events.LogEventLevel.Information)
                .WriteTo.Email(new EmailConnectionInfo()
                {
                    EmailSubject = "WeddingSiteLog",
                    EnableSsl = true,
                    FromEmail = "contact@anthonyaliciawedding.com",
                    ToEmail = "contact@anthonyaliciawedding.com",
                    MailServer = "mail.privateemail.com",
                    NetworkCredentials = new System.Net.NetworkCredential("", ""),
                    Port = 587
                }, restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Error)
                .CreateLogger();

            Log.Logger = log;

            ConfigureAuth(app);
        }
    }
}
