namespace BuildingBlock.Application.Email;

public class EmailMessage
{
    public EmailMessage(List<string> emails, string subject, string content)
    {
        ToEmails = emails;
        Subject = subject;
        Content = content;
    }

    public EmailMessage()
    {
    }

    public List<string> ToEmails { get; set; }
    public string Subject { get; set; }
    public string Content { get; set; }
}