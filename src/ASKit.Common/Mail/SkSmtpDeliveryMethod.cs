namespace ASKit.Common.Mail;

/// <summary>
/// Mail Delivery Methods
/// </summary>
[Flags]
public enum SkSmtpDeliveryMethod
{
    /// <summary>
    /// By SMTP protocol
    /// </summary>
    Network = 1,
    /// <summary>
    /// Saving in a specified directory
    /// </summary>
    SpecifiedPickupDirectory = 2,
    /// <summary>
    /// By SMTP protocol, and then saving to the specified directory
    /// </summary>
    NetworkAndSpecifiedPickupDirectory = 3
}