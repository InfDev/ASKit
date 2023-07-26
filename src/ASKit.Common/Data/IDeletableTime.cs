
namespace ASKit.Common.Data
{
    /// <summary>
    /// Time parameters of the entity to be deleted
    /// </summary>
    public interface IDeletableTime
    {
        /// <summary>
        /// Deletion date and time
        /// </summary>
        public DateTime DeletedAt { get; set; }

        /// <summary>
        /// Sign of completed deletion
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}
