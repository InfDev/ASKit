
namespace ASKit.Common.Data
{
    /// <summary>
    /// Parameters for audit with delete options
    /// </summary>
    public interface IAuditableWithDel : IAuditable, IDeletableTime
    {
        /// <summary>
        /// Who deleted
        /// </summary>
        public string DeletedBy { get; set; }
    }
}
