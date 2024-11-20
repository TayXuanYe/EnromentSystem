using System.Net;
using System.Net.Mail;

/// <summary>
/// This class is uset for manage email function
/// </summary>
public class EmailManager
{
    private static MailAddress sender = new MailAddress("email", "name");
    private MailAddress receiver = null;
    private static string senderPassword = "";
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
        receiver = new MailAddress("i23024312@student.newinti.edu.my", name);
        //receiver = new MailAddress("i22023829@student.newinti.edu.my", name);

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
            Body = body,
            IsBodyHtml = true
        })
        {
            smtp.Send(message);
        }
    }
}