# ASKit.Mail.MailKit

## About

Implementation of a service for sending emails using the MailKit library.
Definitions from the [ASKit.Common](./src/ASKit.Common/README.md) library are used.

```csharp
namespace ASKit.Common.Mail;

// Mail service interface
public interface ISkMailService
{
    // Sending a mail message
    Task SendEmailAsync(SkMailMessage mailMessage);
}
```

Defining messages limited to types:

```csharp
namespace ASKit.Common.Mail;

public record SkMailMessage(string To, string Subject, string Content,
                        IEnumerable<SkAttachment>? Attachments = null);
/// <summary>
/// Attachment to email message
/// </summary>
/// <param name="ContentId">File name or file path if Bytes is not specified</param>
/// <param name="ContentType">Mime type, if not specified then "application/octet-stream"</param>
/// <param name="Bytes">Content</param>
public record SkAttachment(string ContentId, string? ContentType = null, byte[]? Bytes = null);
```

Message includes multiple addresses in the destination fields (To, CC, BCC, ReplyTo) and also uses several
different forms of addresses ([RFC 5322, Appendix A.1.2, page 45](https://datatracker.ietf.org/doc/html/rfc5322#page-45)).

Mailbox examples:
- `admin@domen`
- `Admin <admin@domen>`
- `"Admin Alex" <admin@domen>`
- `"Admin Alex" <admin@domen>`, `supervisor@domen`

**Attention! Mailboxes are separated by `,` or `;`, so these characters should not be included in the Name part of the mailbox (in quotes).**

ContentType from [list of known mime-types](https://developer.mozilla.org/en-US/docs/Web/HTTP/Basics_of_HTTP/MIME_types/Common_types). 
You can use the **System.Net.Mime** namespace:

```csharp
var contentType = (new ContentType { MediaType = MediaTypeNames.Application.Pdf }).ToString();
```

## Configuration

*appsettings.json*
```json
{
  // ...
  "MailOptions": {
    "To": "", // if defined, it is added to the main To, while the main To can be empty
    "Cc": "",
    "Bcc": "",
    "ReplyTo": "",
    "From": "",
    "TextFormat": 1, // 0 - Plain, 1 - Html (default)
    "SmtpServer": "",
    "Port": 465,
    "UseSsl": false,
    "UserName": "",
    "Password": "",
    "RequireCredentials": true,
    "DeliveryMethod": 1, // 1 - Network (default),
                         // 2 - SpecifiedPickupDirectory,
                         // 3 - NetworkAndSpecifiedPickupDirectory 
    "PickupDirectoryLocation": ""
  }
  // ...
}
```

*program.cs*
```csharp
using ASKit.Mail.Extensions.DependencyInjection;
// ...
builder.Services.AddSkMailService(builder.Configuration);
// ...
var app = builder.Build();
// ...
// Testing mail service 
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var mailService = services.GetRequiredService<ISkMailService>();

    await mailService.SendEmailAsync(new SkMailMessage("Admin <unknown@gmail.com>, supervisor@domen", 
        "Test", "<h3>Body message</h3>"));

    byte[] myTextBytes = Encoding.ASCII.GetBytes("<h1>Hello, world</h1>");
    var attachments = new List<SkAttachment> { 
        new SkAttachment("appsettings.json"),
        new SkAttachment("MyFile.html", 
                        (new ContentType { MediaType = MediaTypeNames.Text.Html }).ToString(),
                        myTextBytes)
    };
    await mailService.SendEmailAsync(new SkMailMessage("Admin <unknown@gmail.com>",
        "Test with attachment", "<h3>Body message</h3>", attachments));
}
// ...
app.Run();
```

## Resources

- [MailKit | github.com](https://github.com/jstedfast/MailKit) - A cross-platform .NET library for IMAP, POP3, and SMTP.
- [How to Send an Email in ASP.NET Core | code-maze.com](https://code-maze.com/aspnetcore-send-email/)
- [smtp4dev | github.com](https://github.com/rnwood/smtp4dev) - the fake SMTP email server for development and testing.
