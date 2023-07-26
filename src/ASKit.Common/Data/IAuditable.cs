
namespace ASKit.Common.Data
{
    /// <summary>
    /// Parameters for audit
    /// </summary>
    public interface IAuditable : IAuditableTime
    {
        /// <summary>
        /// Who created
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// Who modified
        /// </summary>
        public string ModifiedBy { get; set; }
    }
}
