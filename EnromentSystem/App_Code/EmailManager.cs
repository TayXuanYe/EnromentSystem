using System.Net;
using System.Net.Mail;

/// <summary>
/// This class is uset for manage email function
/// </summary>
public class EmailManager
{
    private static MailAddress sender = new MailAddress("your_email@example.com", "Your Name");
    private MailAddress receiver = null;
    private static string senderPassword = "your_password";
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

    public void SetEmailReceiver(string emailAddress, string name)
    {
        receiver = new MailAddress(emailAddress, name);
    }

    public void SendEmail()
    {
        var smtp = new SmtpClient
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
            Body = body
        })
        {
            smtp.Send(message);
        }
    }
}