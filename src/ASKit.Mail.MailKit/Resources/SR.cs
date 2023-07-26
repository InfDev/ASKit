namespace SKit.Mail.MailKit.Resources
{
    /// <summary>
    /// Localized string resources
    /// </summary>
    public class SR
    {
#pragma warning disable CS1591
        public const string FieldRequired = "The {0} field is required.";
        public const string SmtpServer = "Smtp Server";
        public const string PickupDirectoryLocation = "Pickup directory location";
        public const string DeliveryMethodNotSupported = "The '{0}' delivery method is not supported.";

        public const string SmtpSettingsMustBeConfigured = "SMTP settings must be configured before an email can be sent.";
        public const string ErrorOccurredWhileSendingEmail = "An error occurred while sending an email: '{0}'";
        public const string CredentialsNotDefined = "Credentials are not defined for SMTP (UserName and/or Password)";
#pragma warning restore CS1591
    }
}
