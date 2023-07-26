using System;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using System.Threading;

namespace ASKit.Common
{
    /// <summary>
    /// CLI (Command Line Interface) common service 
    /// </summary>
    public class CliBaseService
    {
        private string _keyPrefix = string.Empty;
        private readonly List<KvArg>? _args;
        private readonly string _cliPath;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="cliPath">Name or path to shell or cli program</param>
        /// <param name="args">Arguments</param>
        /// <param name="keyPrefix">Prefix before key</param>
        /// <remarks>May be suitable as a cliPath: OsHelper.DefaultShell()</remarks>
        public CliBaseService(string cliPath, List<KvArg>? args = null, string keyPrefix = "")
        {
            if (string.IsNullOrWhiteSpace(cliPath)) throw new ArgumentException(nameof(cliPath));

            _cliPath = cliPath;
            _args = args;
            _keyPrefix = keyPrefix;
        }

        /// <summary>
        /// Executing a command
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public async Task<ProcessResult<string?>> ExecuteCommand(string args)
            => await ExecuteCommand<string>(new KvArg[] { new KvArg("", args) });

        /// <summary>
        /// Executing a command
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public async Task<ProcessResult<string?>> ExecuteCommand(IEnumerable<KvArg>? args)
            => await ExecuteCommand<string>(args);

        /// <summary>
        /// Executing a command
        /// </summary>
        /// <param name="args"></param>
        /// <remarks>Type (except string) can be used when CLI tool returns data in JSON format</remarks>
        /// <returns></returns>
        public async Task<ProcessResult<T?>> ExecuteCommand<T>(IEnumerable<KvArg>? args)
        {
            var command = String.Empty;
            if (args != null && args.Any())
                command = string.Join(" ", args.Select(x => $"{_keyPrefix}{x.Key} {x.Value}"));

            var processStartInfo = new ProcessStartInfo
            {
                FileName = _cliPath,
                Arguments = command,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                RedirectStandardInput = false,
                CreateNoWindow = true,
            };
            var process = Process.Start(processStartInfo)!;
            ArgumentNullException.ThrowIfNull(process);
            await process.WaitForExitAsync();

            var output = process.StandardOutput.ReadToEnd();
            var errorOutput = process.StandardError.ReadToEnd();

            output = output.TrimEnd('\r', '\n');
            var exitCode = process.ExitCode;

            T? result = default;
            if (exitCode == 0)
            {
                if (typeof(T) != typeof(string) && !typeof(T).IsPrimitive)
                {
                    if (typeof(T) == typeof(DateTime) || typeof(T) == typeof(DateTimeOffset) ||
                        typeof(T) == typeof(DateTime?) || typeof(T) == typeof(DateTimeOffset?))
                    {
                        if (output.Length >= 10 && output[0] != '"')
                            output = "\"" + output + "\"";
                    }
                    result = JsonSerializer.Deserialize<T>(output);
                }
                else 
                    result = (T)(object)output;
            }
            return new ProcessResult<T?>(result, exitCode, errorOutput);
        }
    }
}
