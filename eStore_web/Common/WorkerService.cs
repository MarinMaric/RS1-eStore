using eStore_web.EF;
using eStore_web.EF_Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;

namespace eStore_web.Common
{
    public class Worker : BackgroundService
    {
        private HttpClient httpClient;
        string mailContent = "Hello, \n here is your weekly update on all the newest games we have in offer. Click the link to check them out: http://p1803.app.fit.ba/";
        public Worker(ILogger<Worker> logger)
        {
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            httpClient = new HttpClient();
            return base.StartAsync(cancellationToken);
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            httpClient.Dispose();
            return base.StopAsync(cancellationToken);
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    eContext db = new eContext();
                    List<Kupac> kupci = db.Kupac.Include(x=>x.AccountBase).ThenInclude(x=>x.EmailAddress).ToList();
                    foreach(Kupac k in kupci)
                    {
                        SendMail(k.AccountBase.EmailAddress.Email);
                    }
                }
                catch (Exception ex)
                {
                }
                finally
                {
                    await Task.Delay(TimeSpan.FromDays(7), stoppingToken);
                }
            }
        }
        void SendMail(string recepientMail)
        {
            var message = new MailMessage();
            message.From = new MailAddress("p1803.rs1seminarski@gmail.com", "RS1 Seminarski");
            message.To.Add(new MailAddress(recepientMail, "Recipient Name"));
            message.Subject = "SmtpClient Test";
            message.Body = mailContent;

            var client = new SmtpClient();
            client.Host = "smtp.gmail.com";
            client.Port = 587;
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential("p1803.rs1seminarski@gmail.com", "rs1seminarski");
            try
            {
                client.Send(message);
            }
            catch(Exception ex)
            {
                string poruka = ex.Message;
            }
        }
    }
}
