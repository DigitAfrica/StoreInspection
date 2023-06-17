using System.Net;
using System.Net.Mail;

namespace Web.Data;

public interface IMail
{
    public void sendMail(MailObject mailObject);
}

public class Mail : IMail
{
    private readonly IConfiguration _configuration;

    public Mail(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    /// <summary>
    /// // send email using hangfire
    /// </summary>
    /// <param name="mailObject"></param>
    public async void sendMail(MailObject mailObject)
    {
        await sendMailAsync(mailObject);
    }

    /// <summary>
    /// Mail details to send
    /// </summary>
    /// <param name="mailObject"></param>
    /// <returns></returns>
    public async Task sendMailAsync(MailObject mailObject)
    {
        try
        {
            var from = _configuration["Portal:Mail:From"];
            var fromAlias = _configuration["Portal:Mail:FromAlias"];
            var to = string.IsNullOrEmpty(mailObject.To) ? _configuration["Portal:SupportEmail"] : mailObject.To;

            if (string.IsNullOrEmpty(from) || string.IsNullOrEmpty(fromAlias) || string.IsNullOrEmpty(to)
                || string.IsNullOrEmpty(mailObject.Subject) || string.IsNullOrEmpty(mailObject.Body))
            {
                return;
            }

            // core
            var mail = new MailMessage();
            mail.From = new MailAddress(from, fromAlias);
            mail.To.Add(new MailAddress(to));
            mail.Subject = mailObject.Subject;
            mail.Body = mailObject.Body;
            mail.IsBodyHtml = true;

            // attachment
            //System.Net.Mail.Attachment attachment;
            //attachment = new System.Net.Mail.Attachment("c:/textfile.txt");
            //mail.Attachments.Add(attachment);

            // security
            var host = "smtp.lpaudit.co.za";
            var port = 587; // 25
            var smtpClient = new SmtpClient(host, port);
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.Credentials = new System.Net.NetworkCredential("report@lpaudit.co.za", "c0NyouG@RGLo");
            //smtpClient.EnableSsl = true;

            await smtpClient.SendMailAsync(mail);
        }
        catch (Exception ex)
        {
        }
    }
}