namespace ServicesLayer.Features.Accounts.Requests
{

    public class EmailModelRequest
    {
        public string To { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public EmailModelRequest(string to, string subject, string content)
        {
            To = to;
            Subject = subject;
            Content = content;
        }
    }
}
