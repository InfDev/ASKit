# ASKit.Common

Common definitions, services, extensions and helpers.

## ASKit.Common namespace

- *Helpers*: OsHelper
- *Services*: CliBaseService
- *record*: KvArg

An example of using CliBaseService and OsHelper:

```csharp
var content = "Hello world";
(var shell, var shellKey) = OsHelper.DefaultShell();
var cli = new CliBaseService(shell);
var processResult = await cli.ExecuteCommand($"{shellKey} echo {content}");
// or (type other than **string** can be used if the CLI tool returns JSON data)
var processResult2 = await cli.ExecuteCommand<string>(
					new KvArg[] { new KvArg(shellKey, $"echo {content}") });
Assert.Equal(content, processResult.Result);
Assert.Equal(content, processResult2.Result);

// Acceptable option
var expected = DateTime.Now;
content = JsonSerializer.Serialize<DateTime>(expected);
var typedProcessResult = await cli.ExecuteCommand<DateTime>
					(new KvArg[] { new KvArg(shellKey, $"echo {content}") });
var retDateTime = typedProcessResult.Result;
Assert.Equal(expected, retDateTime);
```

## ASKit.Common.Extensions namespace

- *enum*: TimePeriod
- *record*: DateTimeRange, DateTimeOffsetRange
- *extensions for*: DateTime and DateTimeOffset 

## ASKit.Common.Data namespace

- IAuditableWithDelTime, IAuditableTime, IDeletableTime
- IAuditableWithDel, IAuditable

## ASKit.Common.Mail namespace

Mail service definitions:
- ISkMailService
- SkMailMessage, SkAttachment
- SkMailOptions, SkMailTextFormat, SkSmtpDeliveryMethod
 
Used in the ASKit.Mail.MailKit library.
