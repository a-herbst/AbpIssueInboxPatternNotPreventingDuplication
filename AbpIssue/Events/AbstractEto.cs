namespace AbpIssue.Events
{
    public abstract class AbstractEto
    {
        /// <summary>
        /// Sequentially increasing sequence number over all events
        /// </summary>
        public required ulong SequenceNumber { get; set; }
    }
}
