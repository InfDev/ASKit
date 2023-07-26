
namespace ASKit.Common.Mail;

/// <summary>
/// Attachment to email message
/// </summary>
/// <param name="ContentId">File name or file path if Bytes is not specified</param>
/// <param name="ContentType">Mime type, if not specified then "application/octet-stream"</param>
/// <param name="Bytes">Content</param>
/// <remarks>
/// See <see href="https://developer.mozilla.org/en-US/docs/Web/HTTP/Basics_of_HTTP/MIME_types/Common_types">
/// a list of known mime-types</see> and their associated file extensions.<br />
/// You can use the System.Net.Mime namespace:
/// <code>
/// var contentType = (new ContentType { MediaType = MediaTypeNames.Application.Pdf }).ToString();
/// </code>
/// </remarks>
public record SkAttachment(string ContentId, string? ContentType = null, byte[]? Bytes = null);
