using ASKit.Common.Mail;
using MimeKit;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ASKit.Mail.MailKit;

/// <summary>
/// Mail sending service
/// </summary>
public class SkMailService : ISkMailService
{
    private readonly SkMailOptions _mailOptions;
    private readonly ILogger<SkMailService> _logger;

    /// <summary>
    /// Ctor
    /// </summary>
    /// <param name="mailOptions"></param>
    /// <param name="logger"></param>
    public SkMailService(IOptions<SkMailOptions> mailOptions, ILogger<SkMailService> logger)
    {
        _mailOptions = mailOptions.Value;
        _logger = logger;
    }
    
    /// <summary>
    /// Sending a mail message
    /// </summary>
    /// <param name="mailMessage">Message of types: MailMessage, MailMessageWithFiles, MailMessageWithFormFiles</param>
    /// <returns></returns>
    public async Task SendEmailAsync(SkMailMessage mailMessage)
    {
        var emailMessage = CreateEmailMessage(mailMessage);
        await SendAsync(emailMessage);
    }

    private MimeMessage CreateEmailMessage(SkMailMessage mailMessage)
    {
        List<string> AddressParse(string? address)
        {
            var list = new List<string>();
            if (!string.IsNullOrWhiteSpace(address))
            {
                list = address.Split(new char[] { ',', ';' },
                            StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).ToList();
            }
            return list;
        }

        var mimeMessage = new MimeMessage();
        mimeMessage.From.Add(MailboxAddress.Parse(_mailOptions.From));
        
        var to = AddressParse(mailMessage.To);
        var optionsTo = AddressParse(_mailOptions.To);
        to.AddRange(optionsTo);
        mimeMessage.To.AddRange(to.Select(x => MailboxAddress.Parse(x)).ToList());

        var cc = AddressParse(_mailOptions.Cc);
        mimeMessage.Cc.AddRange(cc.Select(x => MailboxAddress.Parse(x)).ToList());
        var bcc = AddressParse(_mailOptions.Bcc);
        mimeMessage.Bcc.AddRange(bcc.Select(x => MailboxAddress.Parse(x)).ToList());
        var replyTo = AddressParse(_mailOptions.ReplyTo);
        mimeMessage.ReplyTo.AddRange(replyTo.Select(x => MailboxAddress.Parse(x)).ToList());

        mimeMessage.Subject = mailMessage.Subject;

        BodyBuilder bodyBuilder = _mailOptions.TextFormat == SkMailTextFormat.Html
            ? new BodyBuilder { HtmlBody = mailMessage.Content }
            : new BodyBuilder { TextBody = mailMessage.Content };

        if (mailMessage.Attachments != null)
        {
            foreach (var attachment in mailMessage.Attachments)
            {
                ContentType? ct = null;
                if (attachment.ContentType != null)
                    ContentType.TryParse(attachment.ContentType, out ct);

                if (attachment.Bytes == null)
                {
                    if (ct != null)
                        bodyBuilder.Attachments.AddAsync(attachment.ContentId, ct);
                    else
                        bodyBuilder.Attachments.AddAsync(attachment.ContentId);
                }
                else
                {
                    if (!attachment.Bytes.Any())
                        continue;
                    using (var ms = new MemoryStream(attachment.Bytes))
                    {
                        if (ct != null)
                            bodyBuilder.Attachments.AddAsync(attachment.ContentId, ms, ct);
                        else
                            bodyBuilder.Attachments.AddAsync(attachment.ContentId, ms);
                    }
                }
            }
        }
        mimeMessage.Body = bodyBuilder.ToMessageBody();

        return mimeMessage;
    }

    private async Task SendAsync(MimeMessage message)
    {
        bool notSend = false;
        if (_mailOptions.DeliveryMethod.HasFlag(SkSmtpDeliveryMethod.Network))
        {
            using (var client = new SmtpClient())
            {
                try
                {
                    await client.ConnectAsync(_mailOptions.SmtpServer, _mailOptions.Port, _mailOptions.UseSsl);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    if (_mailOptions.RequireCredentials)
                    {
                        await client.AuthenticateAsync(_mailOptions.UserName, _mailOptions.Password);
                    }
                    await client.SendAsync(message);
                }
                catch (Exception ex)
                {
                    notSend = true;
                    //log an error message or throw an exception, or both.
                    _logger.LogError(ex.Message);
                    //throw;
                }
                finally
                {
                    await client.DisconnectAsync(true);
                    client.Dispose();
                }
            }
        }
        if (_mailOptions.DeliveryMethod.HasFlag(SkSmtpDeliveryMethod.SpecifiedPickupDirectory))
        {
            var notSendSign = notSend ? "~" : "";
            var fileName = $"{Guid.NewGuid().ToString()}{notSendSign}.eml";
            // var fileName = $"{DateTime.Now:yyyy-MM-ddTHHmmss.fff}{notSendSign}.eml";
            var filePath = Path.Combine(_mailOptions.PickupDirectoryLocation, fileName);
            await message.WriteToAsync(filePath, CancellationToken.None);
        }
    }
}
