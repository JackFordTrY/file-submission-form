using System;
using System.Data;
using System.IO;
using MailKit.Net.Smtp;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Logging;
using MimeKit;

namespace BlobUploadFunction
{
    [StorageAccount("BlobConnectionString")]
    public class BlobUploadFunction
    {
        [FunctionName("BlobUploadFunction")]
        public void Run([BlobTrigger("taskstorage/{name}")]Stream myBlob, string name, ILogger log)
        {
            MimeMessage message = new MimeMessage();

            string[] submittedData = name.Split('|');

            message.From.Add(new MailboxAddress("Sender", "testtaskemail000@gmail.com"));

            message.To.Add(MailboxAddress.Parse(submittedData[0]));

            message.Subject = "File submission notification.";

            message.Body = new TextPart("plain")
            {
                Text = $"You have sumbmitted file: {submittedData[1]}"
            };

            SmtpClient client = new SmtpClient();

            try
            {
                client.Connect("smtp.gmail.com", 465, true);
                client.Authenticate("testtaskemail000@gmail.com", Environment.GetEnvironmentVariable("Password"));
                client.Send(message);
            }
            catch(Exception ex)
            {
                log.LogError(ex.Message);
            }
            finally
            {
                log.LogInformation($"Mail was successfuly sent to {submittedData[0]}");
                client.Disconnect(true);
                client.Dispose();
            }
        }
    }
}
