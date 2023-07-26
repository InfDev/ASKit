
namespace ASKit.Common
{
    /// <summary>
    /// Time parameters for audit
    /// </summary>
    public interface IAuditableTime
    {
        /// <summary>
        /// Creation date and time
        /// </summary>
        public DateTime CreateAt { get; set; }

        /// <summary>
        /// Date and time of last modification
        /// </summary>
        public DateTime ModifiedAt { get; set; }

    }
}
