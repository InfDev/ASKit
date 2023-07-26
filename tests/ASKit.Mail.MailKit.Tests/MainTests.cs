using Microsoft.Extensions.Options;
using ASKit.Common.Mail;
using ASKit.Mail.MailKit;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ASKit.Mail.MailKit.Tests;

public class MainTests
{
    [Fact]
    public void MailOptionsTest()
    {
        var list = new List<SkMailOptions>
        {
            new SkMailOptions {},
            new SkMailOptions { UserName = "login", Password = "***" }
        };


        IEnumerable<ValidationResult> errors = null!;

        errors = (new SkMailOptions { }).Validate();
        Assert.Equal(3, errors.Count());

        errors = (new SkMailOptions { 
            From = "xxx@xxx.xx", SmtpServer = "smtp.xxx.xx", UserName = "aaa", Password = "***"}).Validate();
        Assert.Empty(errors);

        errors = (new SkMailOptions { 
            From = "xxx@xxx.xx",
            DeliveryMethod = SkSmtpDeliveryMethod.SpecifiedPickupDirectory }).Validate();
        Assert.Single(errors);

        errors = (new SkMailOptions
        {
            From = "xxx@xxx.xx",
            DeliveryMethod = SkSmtpDeliveryMethod.SpecifiedPickupDirectory,
            PickupDirectoryLocation = "C:\temp"
        }).Validate();
        Assert.Empty(errors);
    }
}