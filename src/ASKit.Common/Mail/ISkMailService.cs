namespace ASKit.Common.Mail;

/// <summary>
/// Mail service interface
/// </summary>
public interface ISkMailService
{
    /// <summary>
    /// Sending a mail message
    /// </summary>
    /// <param name="mailMessage"></param>
    /// <returns></returns>
    Task SendEmailAsync(SkMailMessage mailMessage);
}
