namespace ASKit.Common.Mail;

/// <summary>
///  Simple Mail message
/// </summary>
/// <param name="To">Mailboxes</param>
/// <param name="Subject">Message subject</param>
/// <param name="Content">Message body</param>
/// <param name="Attachments">Attachment list</param>
/// <remarks>
/// Message includes multiple addresses in the destination fields and also uses several
/// different forms of addresses (<see href="https://datatracker.ietf.org/doc/html/rfc5322#page-45">RFC 5322, Appendix A.1.2, page 45</see>).
/// <br />Mailbox examples:
/// <list type="number">
/// <item>admin@domen</item>
/// <item>Admin &lt;admin@domen&gt;</item>
/// <item>"Admin Alex" &lt;admin@domen&gt;</item>
/// <item>"Admin Alex" &lt;admin@domen&gt;, supervisor@domen</item>
/// </list>
/// Mailboxes are separated by `,` or `;`, so these characters should not be included in the Name part of the mailbox (in quotes).
/// </remarks>
public record SkMailMessage(string To, string Subject, string Content, IEnumerable<SkAttachment>? Attachments = null);
