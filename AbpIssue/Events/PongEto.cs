namespace AbpIssue.Events
{
    public class PongEto : AbstractEto
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
            return $"PongEto {{ SequenceNumber: {SequenceNumber}, PackageIndex: {PackageIndex}, PackageSize: {PackageSize} }}";
        }
    }
}
