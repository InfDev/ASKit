using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.ObjectiveC;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ASKit.Common.Tests;

public class CliServiceTests
{
    class User
    {
        public string Username { get; set; } = default!;
        public string Email { get; set; } = default!;
        public DateTime Birthday { get; set; }

        public Double Weight { get; set; }
    }

    private User _user = new User
    {
        Username = "Alexander",
        Email = "alex@domen",
        Birthday = new DateTime(2000, 8, 13),
        Weight = 80.4
    };

    async Task<ProcessResult<string?>> EchoExecutor(string content)
    {
        (var shell, var shellKey) = OsHelper.DefaultShell();
        var cli = new CliBaseService(shell);
        return await cli.ExecuteCommand<string>(new KvArg[] { new KvArg(shellKey, $"echo {content}") });
    }

    async Task<ProcessResult<DateTime?>> EchoExecutor(DateTime content)
    {
        (var shell, var shellKey) = OsHelper.DefaultShell();
        var cli = new CliBaseService(shell);
        var dtStr = content.ToString("s");
        return await cli.ExecuteCommand<DateTime?>(new KvArg[] { new KvArg(shellKey, $"echo {dtStr}") });
    }

    [Fact]
    public async Task RetStringTest()
    {
        var expected = "Hello world";
        var output = await EchoExecutor(expected);
        Assert.StartsWith(expected, output.Result);
    }

    [Fact]
    public async Task  RetDateTimeTest()
    {
        var expected = DateTime.Now.Date;
        var output = await EchoExecutor(expected);
        Assert.Equal(expected, output.Result);
    }

    [Fact]
    public async Task RetUserTest()
    {
        (var shell, var shellKey) = OsHelper.DefaultShell();
        var cli = new CliBaseService(shell);
        var userStr = JsonSerializer.Serialize<User>(_user);
        var output =  await cli.ExecuteCommand<User?>(new KvArg[] { new KvArg(shellKey, $"echo {userStr}") });
        Assert.Equal(_user.Birthday, output.Result?.Birthday);
        Assert.Equal(_user.Username, output.Result?.Username);
        Assert.Equal(_user.Weight, output.Result?.Weight);
    }

    [Fact]
    public async Task RetDemo()
    {
        var content = "Hello world";
        (var shell, var shellKey) = OsHelper.DefaultShell();
        var cli = new CliBaseService(shell);
        var processResult = await cli.ExecuteCommand($"{shellKey} echo {content}");
        // or (another type can be used if the CLI tool returns JSON data)
        var processResult2 = await cli.ExecuteCommand<string>(new KvArg[] { new KvArg(shellKey, $"echo {content}") });
        Assert.Equal(content, processResult.Result);
        Assert.Equal(content, processResult2.Result);

        var expected = DateTime.Now;
        content = JsonSerializer.Serialize<DateTime>(expected);
        var typedProcessResult = await cli.ExecuteCommand<DateTime>(new KvArg[] { new KvArg(shellKey, $"echo {content}") });
        var retDateTime = typedProcessResult.Result;
        Assert.Equal(expected, retDateTime);
    }
}

