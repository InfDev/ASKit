using System.ComponentModel.DataAnnotations;

namespace ASKit.Common.Mail;

// https://www.elegantthemes.com/blog/business/email-etiquette-when-should-you-bcc-cc-or-reply-all
// https://blog.hubspot.com/sales/reply-reply-all-bcc-flowchart

/// <summary>
/// Mail configuration 
/// </summary>
public class SkMailOptions
{
#pragma warning disable CS1591

    public static string Section = "MailOptions";

    /// <summary>
    /// If defined, it is added to the main To, while the main To can be empty
    /// </summary>
    public string To { get; set; } = string.Empty;
    /// <summary>
    /// Carbon Copy
    /// </summary>
    public string Cc { get; set; } = string.Empty;
    /// <summary>
    /// Blind Carbon Copy of a message
    /// </summary>
    public string Bcc { get; set; } = string.Empty;
    /// <summary>
    /// If there is a need to receive a response to a different address than from
    /// </summary>
    public string ReplyTo { get; set; } = string.Empty;

    public string From { get; set; } = string.Empty;

    public SkMailTextFormat TextFormat { get; set; } = SkMailTextFormat.Html;

    public string SmtpServer { get; set; } = string.Empty;
    [Range(0, 65535)]
    public int Port { get; set; } = 25; // 465
    public bool UseSsl { get; set; } = false;
    public string UserName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public bool RequireCredentials { get; set; } = true;

    public SkSmtpDeliveryMethod DeliveryMethod { get; set; } = SkSmtpDeliveryMethod.Network;

    public string PickupDirectoryLocation { get; set; } = string.Empty;

#pragma warning restore CS1591
}
