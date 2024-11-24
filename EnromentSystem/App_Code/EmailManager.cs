using System.Net;
using System.Net.Mail;

/// <summary>
/// This class is uset for manage email function
/// </summary>
public class EmailManager
{
    private static MailAddress sender = new MailAddress("i23024312@student.newinti.edu.my", "Tay Xuan Ye");
    private MailAddress receiver = null;
    private static string senderPassword = "iu040804130465";
    private string subject = null;
    private string body = null;

    public void SetEmailSubject(string subject)
    {
        this.subject = subject;
    }

    public void SetEmailBody(string body)
    {
        this.body = body;
    }

    public void SetEmailReceiver(MailAddress sender)
    {
        receiver = sender;
    }

    public void SendEmail()
    {
        SmtpClient smtp = new SmtpClient
        {
            Host = "smtp.gmail.com", // SMTP server address
            Port = 587,                // SMTP port
            EnableSsl = true,         
            DeliveryMethod = SmtpDeliveryMethod.Network, 
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential(sender.Address, senderPassword) 
        };

        using (MailMessage message = new MailMessage(sender, receiver)
        {
            Subject = subject,
            Body = body,
            IsBodyHtml = true
        })
        {
            smtp.Send(message);
        }
    }
}