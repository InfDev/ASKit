using ASKit.Common.Mail;
using System.ComponentModel.DataAnnotations;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.Localization;
//using ASKit.Mail.MailKit.Resources;

namespace ASKit.Mail.MailKit
{
    /// <summary>
    /// Validators
    /// </summary>
    public static class Validators
    {
        /// <summary>
        /// Validate mail options
        /// </summary>
        /// <param name="mailOptions"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IEnumerable<ValidationResult> Validate(this SkMailOptions mailOptions)
        {
            if (mailOptions == null) throw new ArgumentNullException(nameof(mailOptions));

            if (string.IsNullOrWhiteSpace(mailOptions.From))
            {
                yield return new ValidationResult($"'From' field is required.");
            }
            if ((mailOptions.DeliveryMethod & (SkSmtpDeliveryMethod.Network | SkSmtpDeliveryMethod.SpecifiedPickupDirectory)) == 0)
            {
                yield return new ValidationResult($"The '{mailOptions.DeliveryMethod}' delivery method is not supported");
            }
            else
            {
                if ((mailOptions.DeliveryMethod & SkSmtpDeliveryMethod.Network) != 0)
                {
                    if (String.IsNullOrEmpty(mailOptions.SmtpServer))
                        yield return new ValidationResult($"'SmtpServer' field is required.");
                    if (mailOptions.RequireCredentials)
                    {
                        if (string.IsNullOrEmpty(mailOptions.UserName) || string.IsNullOrEmpty(mailOptions.Password))
                        {
                            yield return new ValidationResult($"'UserName' and/or 'Password' fields is required.");
                        }
                    }
                }
                if ((mailOptions.DeliveryMethod & SkSmtpDeliveryMethod.SpecifiedPickupDirectory) != 0)
                {
                    if (String.IsNullOrEmpty(mailOptions.PickupDirectoryLocation))
                    {
                        yield return new ValidationResult($"'PickupDirectoryLocation' field is required.");
                    }
                }
            }
        }


        ///// <summary>
        ///// Validate mail options
        ///// </summary>
        ///// <param name="mailOptions"></param>
        ///// <param name="serviceProvider"></param>
        ///// <returns></returns>
        ///// <exception cref="ArgumentNullException"></exception>
        //public static IEnumerable<ValidationResult> Validate(this SkMailOptions mailOptions, IServiceProvider serviceProvider)
        //{
        //    if (mailOptions == null) throw new ArgumentNullException(nameof(mailOptions));
        //    if (serviceProvider == null) throw new ArgumentNullException(nameof(serviceProvider));

        //    var S = serviceProvider.GetService<IStringLocalizer<SkMailOptions>>()!;

        //    if (string.IsNullOrWhiteSpace(mailOptions.From))
        //    {
        //        yield return new ValidationResult(S[SR.FieldRequired, "From"], new[] { nameof(mailOptions.From) });
        //    }

        //    if ((mailOptions.DeliveryMethod & (SkSmtpDeliveryMethod.Network | SkSmtpDeliveryMethod.SpecifiedPickupDirectory)) == 0)
        //    {
        //        yield return new ValidationResult(S[SR.DeliveryMethodNotSupported, mailOptions.DeliveryMethod]);
        //    } else
        //    {
        //        if ((mailOptions.DeliveryMethod & SkSmtpDeliveryMethod.Network) != 0)
        //        {
        //            if (String.IsNullOrEmpty(mailOptions.SmtpServer))
        //                yield return new ValidationResult(S[SR.FieldRequired, SR.SmtpServer], new[] { nameof(mailOptions.SmtpServer) });

        //            if (mailOptions.RequireCredentials)
        //            {
        //                if (string.IsNullOrEmpty(mailOptions.UserName) || string.IsNullOrEmpty(mailOptions.Password))
        //                {
        //                    yield return new ValidationResult(S[SR.CredentialsNotDefined]);
        //                }
        //            }
        //        }
        //        if ((mailOptions.DeliveryMethod & SkSmtpDeliveryMethod.SpecifiedPickupDirectory) != 0)
        //        {
        //            if (String.IsNullOrEmpty(mailOptions.PickupDirectoryLocation))
        //            {
        //                yield return new ValidationResult(S[SR.FieldRequired, SR.PickupDirectoryLocation], new[] { nameof(mailOptions.PickupDirectoryLocation) });
        //            }
        //        }
        //    }
        //}
    }
}
