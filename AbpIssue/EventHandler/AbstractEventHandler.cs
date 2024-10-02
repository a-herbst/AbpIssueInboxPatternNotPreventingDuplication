namespace AbpIssue.EventHandler
{
    public class AbstractEventHandler<TEventHandler>
        where TEventHandler : class
    {
        protected readonly IHostApplicationLifetime _hostApplicationLifetime;
        protected readonly ILogger<TEventHandler> _logger;

        ulong? _lastSequenceNumberSeen;

        public AbstractEventHandler(
            IHostApplicationLifetime hostApplicationLifetime,
            ILogger<TEventHandler> logger
        )
        {
            _hostApplicationLifetime = hostApplicationLifetime;
            _logger = logger;
        }

        protected void SequenceNumberDuplicationCheck(ulong sequenceNumber)
        {
            if (_lastSequenceNumberSeen.HasValue && sequenceNumber < _lastSequenceNumberSeen)
            {
                var msg =
                    $"SequenceNumber of Events went backwards from {_lastSequenceNumberSeen} to {sequenceNumber}!";
                _logger.LogError(msg);
                Task.Run(_hostApplicationLifetime.StopApplication);
                return;
            }

            if (_lastSequenceNumberSeen.HasValue && sequenceNumber == _lastSequenceNumberSeen.Value)
            {
                var msg = $"Duplicate SequenceNumber occured in event: {sequenceNumber}!";
                _logger.LogError(msg);
                Task.Run(_hostApplicationLifetime.StopApplication);
                return;
            }

            _lastSequenceNumberSeen = sequenceNumber;
        }
    }
}
