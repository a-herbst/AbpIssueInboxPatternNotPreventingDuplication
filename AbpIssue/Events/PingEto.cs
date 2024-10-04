namespace AbpIssue.Events
{
    public class PingEto : AbstractEto
    {
        /// <summary>
        /// Identifies the ping event within each package.
        /// </summary>
        public required uint PackageIndex { get; set; }

        /// <summary>
        /// Size of the package events.
        /// </summary>
        public required uint PackageSize { get; set; }

        public override string? ToString()
        {
            return $"PingEto {{ SequenceNumber: {SequenceNumber}, PackageIndex: {PackageIndex}, PackageSize: {PackageSize} }}";
        }
    }
}
