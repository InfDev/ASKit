namespace ASKit.Common;

/// <summary>
/// Result of Process
/// </summary>
/// <typeparam name="T"></typeparam>
public class ProcessResult<T>
{
    /// <summary>
    /// Result of T
    /// </summary>
    public T? Result { get; }

    /// <summary>
    /// Exit code
    /// </summary>
    public int ExitCode { get; }

    /// <summary>
    /// Standard Error Output 
    /// </summary>
    public string ErrorOutput { get; }

    /// <summary>
    /// Return code sign "Probably an error"
    /// </summary>
    public bool MayBeError => ExitCode != 0;

    /// <summary>
    /// Ctor
    /// </summary>
    /// <param name="result"></param>
    /// <param name="exitCode"></param>
    /// <param name="errorOutput"></param>
    public ProcessResult(T? result, int exitCode, string errorOutput = "")
    {
        Result = result;
        ExitCode = exitCode;
        ErrorOutput = errorOutput;
    }
}
