namespace ASKit.Common;

/// <summary>
/// Argument of CLI or program
/// </summary>
/// <param name="Key"></param>
/// <param name="Value"></param>
/// <remark>Key and Value may be empty string</remark>
public record KvArg(string Key, string Value);
